using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmReporteCompras : Form
    {
        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            List<Proveedor> lista = new CN_Proveedor().Listar();

            cboproveedor.Items.Add(new OpcionCombo() { Valor = 0, Texto = "TODOS" });
            foreach (Proveedor item in lista)
            {
                if(item.Tipo_Proveedor == "O")
                {
                    continue;
                }
                //else
                //{
                //    cboproveedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedor, Texto = item.Tipo_Proveedor });
                //}
            }
            cboproveedor.DisplayMember = "Texto";
            cboproveedor.ValueMember = "Valor";
            cboproveedor.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;
        }

        private void btnbuscarresultado_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((OpcionCombo)cboproveedor.SelectedItem).Valor.ToString());



            List<ReporteCompra> lista = new List<ReporteCompra>();

            lista = new CN_Reporte().Compra(
                txtfechainicio.Value.ToString(),
                txtfechafin.Value.ToString(),
                idproveedor
                );


            dgvdata.Rows.Clear();
            //Aca va la condicion para que no deje entrar los materiales
           

            foreach (ReporteCompra rc in lista)
            {
                if(rc.TipoProveedor == "O")
                {
                    continue;
                }
                else
                {
                    dgvdata.Rows.Add(new object[] {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.TipoProveedor,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
                }
            }
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {

                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvdata.Columns)
                {
                    dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[] {
                            row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[13].Value.ToString()
                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteComprasInsumos_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Reporte Compra Insumos");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch
                    {
                        MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void btndescargar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count > 1)
            {
                string Texto_Html = Properties.Resources.ReporteCompraInsumosMariano.ToString();
                Negocio odatos = new CN_Negocio().ObtenerDatos();

                Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
                Texto_Html = Texto_Html.Replace("@docnegocio", odatos.Cuil);
                Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

                //Texto_Html = Texto_Html.Replace("@tipodocumento", txttipodocumento.Text.ToUpper());
                //Texto_Html = Texto_Html.Replace("@numerodocumento", txtnumerodocumento.Text);


                //Texto_Html = Texto_Html.Replace("cuitproveedor", txtcuitproveedor.Text);
                //Texto_Html = Texto_Html.Replace("@tipoproveedor", txttipoproveedor.Text);
                //Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecha.Text);
                //Texto_Html = Texto_Html.Replace("@usuarioregistro", txtusuario.Text);

                string filas = string.Empty;
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    filas += "<tr>";
                    filas += "<td>" + row.Cells["FechaRegistro"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["TipoDocumento"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["NumeroDocumento"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["MontoTotal"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["UsuarioRegistro"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["CuilProveedor"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["TipoProveedor"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["CodigoProducto"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["NombreProducto"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Categoria"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["PrecioVenta"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                    filas += "<td>" + row.Cells["Subtotal"].Value.ToString() + "</td>";
                    filas += "</tr>";
                }
                Texto_Html = Texto_Html.Replace("@filas", filas);
                //Texto_Html = Texto_Html.Replace("@montototal", txtmontototal.Text);

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteCompraInsumos.pdf");
                savefile.Filter = "Pdf Files|*.pdf";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                    {

                        Document pdfDoc = new Document(PageSize.B0, 20, 20, 20, 20);

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        bool obtenido = true;
                        byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                        if (obtenido)
                        {
                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                            img.ScaleToFit(60, 60);
                            img.Alignment = iTextSharp.text.Image.UNDERLYING;
                            img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(25));
                            pdfDoc.Add(img);
                        }

                        using (StringReader sr = new StringReader(Texto_Html))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        }

                        pdfDoc.Close();
                        stream.Close();
                        MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //if (txttipodocumento.Text == "")
            //{
            //    MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
        }
    }
}
