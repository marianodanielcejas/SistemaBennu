using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "NO ACTIVO" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;


            List<Categoria> listacategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listacategoria)
            {
                cbocategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
            }
            cbocategoria.DisplayMember = "Texto";
            cbocategoria.ValueMember = "Valor";
            cbocategoria.SelectedIndex = 0;

            List<TipoObra> listaTipoObra = new CN_TipoObra().Listar();

            foreach (TipoObra item in listaTipoObra)
            {
                cbotipoobra.Items.Add(new OpcionCombo() { Valor = item.Id_Tipo_Obra, Texto = item.DescripcionTipo });
            }
            cbotipoobra.DisplayMember = "Texto";
            cbotipoobra.ValueMember = "Valor";
            cbotipoobra.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {

                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;


            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {

                dgvdata.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.oTipo_Obra.Id_Tipo_Obra,
                    item.oTipo_Obra.DescripcionTipo,
                    item.TipoProducto,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "ACTIVO" : "NO ACTIVO"
                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Producto obj = new Producto()
            {
                IdProducto = Convert.ToInt32(txtid.Text),
                Codigo = txtcodigo.Text,
                Nombre = txtnombre.Text,
                Descripcion = txtdescripcion.Text,
                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionCombo)cbocategoria.SelectedItem).Valor) },
                oTipo_Obra = new TipoObra() { Id_Tipo_Obra = Convert.ToInt32(((OpcionCombo)cbotipoobra.SelectedItem).Valor) },
                TipoProducto = txtTipoProducto.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };
            //if (txtTipoProducto.Text == "O" || txtTipoProducto.Text == "o")
            //{
            //    txtTipoProducto.Text = txtTipoProducto.Text.ToUpper();
            //    txtTipoProducto.SelectionStart = txtTipoProducto.Text.Length;
            //}
            //else if (txtTipoProducto.Text == "C" || txtTipoProducto.Text == "c")
            //{
            //    txtTipoProducto.Text = txtTipoProducto.Text.ToUpper();
            //    txtTipoProducto.SelectionStart = txtTipoProducto.Text.Length;
            //}
            //else
            //{
            //    txtTipoProducto.Text = "";
            //    txtTipoProducto.Select();
            //}
            if (obj.IdProducto == 0)
            {
                int idgenerado = new CN_Producto().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {
                        "",
                       idgenerado,
                       txtcodigo.Text,
                       txtnombre.Text,
                       txtdescripcion.Text,
                       ((OpcionCombo)cbocategoria.SelectedItem).Valor.ToString(),
                       ((OpcionCombo)cbocategoria.SelectedItem).Texto.ToString(),
                       ((OpcionCombo)cbotipoobra.SelectedItem).Valor.ToString(),
                       ((OpcionCombo)cbotipoobra.SelectedItem).Texto.ToString(),
                       txtTipoProducto.Text,
                       "0",
                       "0.00",
                       "0.00",
                       ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                       ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()
                    }) ;

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }


            }
            else
            {
                bool resultado = new CN_Producto().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Codigo"].Value = txtcodigo.Text;
                    row.Cells["Nombre"].Value = txtnombre.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)cbocategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cbocategoria.SelectedItem).Texto.ToString();
                    row.Cells["Id_Tipo_Obra"].Value = ((OpcionCombo)cbotipoobra.SelectedItem).Valor.ToString();
                    row.Cells["Tipo_Obra"].Value = ((OpcionCombo)cbotipoobra.SelectedItem).Texto.ToString();
                    row.Cells["Tipo_Producto"].Value = txtTipoProducto.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            
        }
        private void Limpiar()
        {

            txtindice.Text = "-1";
            txtid.Text = "0";
            txtcodigo.Text = "";
            txtnombre.Text = "";
            txtdescripcion.Text = "";
            txtTipoProducto.Text = "";
            cbocategoria.SelectedIndex = 0;
            cbotipoobra.SelectedIndex = 0;
            cboestado.SelectedIndex = 0;

            txtcodigo.Select();

        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();

                    txtcodigo.Text = dgvdata.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtnombre.Text = dgvdata.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtdescripcion.Text = dgvdata.Rows[indice].Cells["Descripcion"].Value.ToString();
                    txtTipoProducto.Text = dgvdata.Rows[indice].Cells["Tipo_Producto"].Value.ToString();


                    foreach (OpcionCombo oc in cbocategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indice_combo = cbocategoria.Items.IndexOf(oc);
                            cbocategoria.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cbotipoobra.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["Id_Tipo_Obra"].Value))
                        {
                            int indice_combo = cbotipoobra.Items.IndexOf(oc);
                            cbotipoobra.SelectedIndex = indice_combo;
                            break;
                        }
                    }


                    foreach (OpcionCombo oc in cboestado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvdata.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboestado.Items.IndexOf(oc);
                            cboestado.SelectedIndex = indice_combo;
                            break;
                        }
                    }


                }


            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el producto", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Producto obj = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Producto().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "No es posible eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvdata.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[] {
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[11].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                            row.Cells[14].Value.ToString(),
                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {

                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
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

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtcodigo.Text.Length == 4 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtnombre.Text.Length > 20 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtdescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtdescripcion.Text.Length > 20 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTipoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtTipoProducto.Text.Length > 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void txtnombre_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text) && txtnombre.Text.All(char.IsUpper)) ;
            {
                txtnombre.Text = txtnombre.Text.ToUpper();
                txtnombre.SelectionStart = txtnombre.Text.Length;
            }
        }

        private void txtdescripcion_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdescripcion.Text) && txtdescripcion.Text.All(char.IsUpper)) ;
            {
                txtdescripcion.Text = txtdescripcion.Text.ToUpper();
                txtdescripcion.SelectionStart = txtdescripcion.Text.Length;
            }
        }

        private void txtTipoProducto_TextChanged_1(object sender, EventArgs e)
        {
            //if (txtTipoProducto.Text == "O" && txtTipoProducto.Text != "C" || txtTipoProducto.Text == "C" && txtTipoProducto.Text != "O")
            //{
            //    MessageBox.Show("Las letras 'O' o 'C' son las unicas admitidas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtTipoProducto.Select();
            //    txtTipoProducto.Text = "";
            //    //txtTipoProducto.Text = "";
            //    //txtTipoProducto.Select();
            //}
            if (!string.IsNullOrEmpty(txtTipoProducto.Text) && txtTipoProducto.Text.All(char.IsUpper)) ;
            {

                if (txtTipoProducto.Text == "O" || txtTipoProducto.Text == "o")
                {
                    txtTipoProducto.Text = txtTipoProducto.Text.ToUpper();
                    txtTipoProducto.SelectionStart = txtTipoProducto.Text.Length;
                }
                else if (txtTipoProducto.Text == "C" || txtTipoProducto.Text == "c")
                {
                    txtTipoProducto.Text = txtTipoProducto.Text.ToUpper();
                    txtTipoProducto.SelectionStart = txtTipoProducto.Text.Length;
                }
                else
                {
                    txtTipoProducto.Text = "";
                    txtTipoProducto.Select();
                }

            }
        }
    }
}
