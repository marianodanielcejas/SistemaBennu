using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class frmVentaInsumos : Form
    {
        private Usuario _Usuario;
        public frmVentaInsumos(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentaInsumos_Load(object sender, EventArgs e)
        {
            //cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "FACTURA", Texto = "FACTURA" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txttipofactura.Text = "A";
            txtnrosucursal.Text = "0001";

            //txtidproveedor.Text = "0";
            txtidproducto.Text = "0";
        }

        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtdocumentocliente.Text = modal._Cliente.Documento;
                    txtnombrecliente.Text = modal._Cliente.NombreCompleto;
                    //txtcodproducto.Select();
                }
                else
                {
                    //txtdocumentocliente.Select();
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text = modal._Producto.IdProducto.ToString();
                    txtcodproducto.Text = modal._Producto.Codigo;
                    txtproducto.Text = modal._Producto.Nombre;
                    txttipoproducto.Text = modal._Producto.TipoProducto;
                    txtprecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtstock.Text = modal._Producto.Stock.ToString();
                    txtcodproducto.Enabled = true;
                    txtproducto.Enabled = true;
                    txttipoproducto.Enabled = true;
                    //txtcantidad.Select();
                    //if (txttipoproducto.Text != "O")
                    //{
                    //    txtcodproducto.Enabled = false;
                    //    txtproducto.Enabled = false;
                    //    txttipoproducto.Enabled = false;
                    //    MessageBox.Show("Solo se puede seleccionar producto de Obra", "Opción incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtcodproducto.Text = "";
                    //    txtproducto.Text = "";
                    //    txttipoproducto.Text = "";
                    //    txtprecio.Text = "0.00";
                    //}
                }
                else
                {
                    //txtcodproducto.Select();
                }
                //limpiarProducto();
            }
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            decimal precio = 0;

            bool producto_existe = false;

            if (int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un Insumo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtdocumentocliente.Text == "")
            {
                MessageBox.Show("Debe seleccionar el documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtnombrecliente.Text == "")
            {
                MessageBox.Show("Debe seleccionar el nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtidtipoobra.Text == "")
            {
                MessageBox.Show("Debe seleccionar el Tipo de obra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(txtprecio.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecio.Select();
                return;
            }
            if (Convert.ToInt32(txtstock.Text) < Convert.ToInt32(txtcantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock, si desea una mayor cantidad dirigase al menú compras", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //frmCompraMateriales nuevoFormulario = new frmCompraMateriales();

                //// Muestra el nuevo formulario
                //nuevoFormulario.ShowDialog();
                //txtcantidad.Value = 1;
            }

            //---------------------------------------------------------
            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }
            if (!producto_existe && Convert.ToInt32(txtstock.Text) >= Convert.ToInt32(txtcantidad.Value.ToString()))
            {
                bool respuesta = new CN_VentaInsumos().RestarStock(
                    Convert.ToInt32(txtidproducto.Text),
                    Convert.ToInt32(txtcantidad.Value.ToString())
                    );
                //-------------------------------------------------
                if (respuesta)
                {
                    dgvdata.Rows.Add(new object[] {
                        txtidproducto.Text,
                        txtproducto.Text,
                        precio.ToString("0.00"),
                        txtcantidad.Value.ToString(),
                        (txtcantidad.Value * precio).ToString("0.00")

                });

                    calcularTotal();
                    //limpiarProducto();
                    //txtcodproducto.Select();
                }
            }
        }
        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            txttotalpagar.Text = total.ToString("0.00");
        }
        private void limpiarProducto()
        {
            //txtdocumentocliente.Text = "";
            //txtnombrecliente.Text = "";
            txtdescripciontipo.Text = "";
            //txtcantbocas.Text = "";
            //txtpersonaltrabajo.Text = "";
            txtidproducto.Text = "0";
            txtcodproducto.Text = "";
            txtproducto.Text = "";
            txttipoproducto.Text = "";
            txtprecio.Text = "";
            txtstock.Text = "";
            txtcantidad.Value = 1;
        }

        private void limpiarCliente()
        {
            txtdocumentocliente.Text = "";
            txtnombrecliente.Text = "";
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 5)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete25.Width;
                var h = Properties.Resources.delete25.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete25, new System.Drawing.Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int index = e.RowIndex;

                if (index >= 0)
                {
                    bool respuesta = new CN_VentaInsumos().SumarStock(
                        Convert.ToInt32(dgvdata.Rows[index].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvdata.Rows[index].Cells["Cantidad"].Value.ToString()));
                    ////----------------------------------------------------------------------------
                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(index);
                        calcularTotal();
                        txtpagocon.Text = "0";
                        txtcambio.Text = "0";
                    }
                }
            }
            limpiarProducto();
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtpagocon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecio.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void calcularcambio()
        {

            if (txttotalpagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen Productos en la venta de Insumos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            decimal pagacon;
            decimal total = Convert.ToDecimal(txttotalpagar.Text);

            if (txtpagocon.Text.Trim() == "")
            {
                //txtpagocon.Text = "0";
                txtcambio.Text = "0";
                txtpagocon.Text = "0";
            }

            if (decimal.TryParse(txtpagocon.Text.Trim(), out pagacon))
            {

                if (pagacon < total)
                {
                    txtcambio.Text = "0";
                    txtpagocon.Text = "0";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtcambio.Text = cambio.ToString("0.00");

                }
            }
        }

        private void txtpagocon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularcambio();
            }
        }
        //Es insumos
        private void btncrearventaobra_Click(object sender, EventArgs e)
        {
            if (txtdocumentocliente.Text == "")
            {
                MessageBox.Show("Debe ingresar documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtnombrecliente.Text == "")
            {
                MessageBox.Show("Debe ingresar nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //if (txttipoproveedor.Text == "")
            //{
            //    MessageBox.Show("Debe ingresar Tipo y nombre del proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            //if (txtcantbocas.Text == "")
            //{
            //    MessageBox.Show("Debe ingresar la cantidad de bocas y el personal de trabajo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            if (txtdescripciontipo.Text == "")
            {
                MessageBox.Show("Debe ingresar el tipo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtpagocon.Text == "" || txtpagocon.Text == "0" || txtpagocon.Text == "0.00")
            {
                MessageBox.Show("Pago vacio o incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtcambio.Text == "" || txtcambio.Text == "0" || txtcambio.Text == "0.00")
            {
                MessageBox.Show("Cambio vacio o incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta de insumos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DataTable DetalleVentaInsumos = new DataTable();

            DetalleVentaInsumos.Columns.Add("IdProducto", typeof(int));
            DetalleVentaInsumos.Columns.Add("PrecioVenta", typeof(decimal));
            DetalleVentaInsumos.Columns.Add("Cantidad", typeof(int));
            DetalleVentaInsumos.Columns.Add("SubTotal", typeof(decimal));


            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                DetalleVentaInsumos.Rows.Add(new object[] {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()
                });
            }

            int idcorrelativo = new CN_VentaInsumos().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idcorrelativo);
            calcularcambio();

            Venta_Insumos oVentaInsumos = new Venta_Insumos()
            {
                //revisar esta parte
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                //oCliente = new Cliente() {IdCliente = Convert.ToInt32(txtidcliente.Text),NombreCompleto = txtnombrecliente.Text,Documento = txtdocumentocliente.Text},
                //oTamanio = new Tamanio() {IdTamanio = Convert.ToInt32(txtidtamanioobra.Text), CantBocas = Convert.ToInt32(txtcantbocas.Text), PersonalTrabajo = Convert.ToInt32(txtpersonaltrabajo.Text)},
                //oTipoObra = new TipoObra() {Id_Tipo_Obra = Convert.ToInt32(txtidtipoobra.Text), DescripcionTipo = txtdescripciontipo.Text},
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                DocumentoCliente = txtdocumentocliente.Text,
                NombreCliente = txtnombrecliente.Text,
                DescripcionTipo = txtdescripciontipo.Text,
                //CantBocas = Convert.ToInt32(txtcantbocas.Text),
                //PersonalTrabajo = Convert.ToInt32(txtpersonaltrabajo.Text),
                //DocumentoProveedor = txtdocproveedor.Text,
                //Tipo_Proveedor = txttipoproveedor.Text,
                MontoPago = Convert.ToDecimal(txtpagocon.Text),
                MontoCambio = Convert.ToDecimal(txtcambio.Text),
                MontoTotal = Convert.ToDecimal(txttotalpagar.Text),
                //-------------------------------------------------------
                //oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                //TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                //NumeroDocumento = numeroDocumento,
                //MontoPago = Convert.ToDecimal(txtpagocon.Text),
                //MontoCambio = Convert.ToDecimal(txtcambio.Text),
                //MontoTotal = Convert.ToDecimal(txttotalpagar.Text)
            };
            //string mensaje = string.Empty;
            //bool respuesta = new CN_VentaInsumos().Registrar(oVentaInsumos, DetalleVentaInsumos, out mensaje);

            //if (respuesta)
            //{
            //    var result = MessageBox.Show("Numero de venta insumo generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //    if (result == DialogResult.Yes)
            //        Clipboard.SetText(numeroDocumento);

            //    txtdocumentocliente.Text = "";
            //    txtnombrecliente.Text = "";
            //    //txtdocproveedor.Text = "";
            //    //txttipoproveedor.Text = "";
            //    txtproducto.Text = "";
            //    txtcodproducto.Text = "";
            //    txttipoproducto.Text = "";
            //    dgvdata.Rows.Clear();
            //    calcularTotal();
            //    txtpagocon.Text = "";
            //    txtcambio.Text = "";
            //}
            //else
            //    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            string mensaje = string.Empty;

            //if (respuesta)
            //{
            var result = MessageBox.Show("Numero de venta generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                Clipboard.SetText(numeroDocumento);
                bool respuesta = new CN_VentaInsumos().Registrar(oVentaInsumos, DetalleVentaInsumos, out mensaje);
                txtdocumentocliente.Text = "";
                txtnombrecliente.Text = "";
                txtprecio.Text = "";
                txtstock.Text = "";
                //txtdocproveedor.Text = "";
                //txttipoproveedor.Text = "";
                txtproducto.Text = "";
                txtcodproducto.Text = "";
                txttipoproducto.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
                txtpagocon.Text = "";
                txtcambio.Text = "";
            }
            //if (result == DialogResult.No)
            //{
            //    //for(int i = dgvdata.Rows.Count - 1; i <= dgvdata.Rows.Count ; i--)
            //    //foreach (DataGridViewRow row in dgvdata.Rows)
            //    ////for (int i = dgvdata.Rows.Count - 1; i >= 0; i++)
            //    ////for (int i = dgvdata.Rows.Count - 1; i >= 0; i--)
            //    //{
            //    //    bool respuesta = new CN_VentaInsumos().SumarStock(
            //    //    Convert.ToInt32(txtidproducto.Text),
            //    //    Convert.ToInt32(txtcantidad.Value.ToString())
            //    //    );

            //    //    dgvdata.Rows.Remove(row);

            //    //    //dgvdata.ClearSelection();
            //    //    //txtcantidad.Value = 0;
            //    //}
            //    //foreach (DataGridViewRow row in dgvdata.Rows)
            //    //{
            //    //   dgvdata.Rows.Remove(row);
            //    //   //calcularTotal(); 
            //    //   bool respuesta = new CN_VentaInsumos().SumarStock(
            //    //   Convert.ToInt32(txtidproducto.Text),
            //    //   Convert.ToInt32(txtcantidad.Value.ToString())
            //    //   );
            //    //}
            //    txtdocumentocliente.Text = "";
            //    txtnombrecliente.Text = "";
            //    txtprecio.Text = "";
            //    txtstock.Text = "";
            //    //txtdocproveedor.Text = "";
            //    //txttipoproveedor.Text = "";
            //    txtproducto.Text = "";
            //    txtcodproducto.Text = "";
            //    txttipoproducto.Text = "";
            //    dgvdata.Rows.Clear();
            //    calcularTotal();
            //    txtpagocon.Text = "";
            //    txtcambio.Text = "";
            //}
        }
        private void btnbuscartipoobra_Click_1(object sender, EventArgs e)
        {
            using (var modal = new mdTipoProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtdescripciontipo.Enabled = false;
                    //txttipoproveedor.Enabled = false;
                    //txtidproveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtidtipoobra.Text = modal._TipoObra.Id_Tipo_Obra.ToString();
                    txtdescripciontipo.Text = modal._TipoObra.DescripcionTipo.ToString();
                    //if (txtdescripciontipo.Text == "No Contempla")
                    //{
                    //    //txtdocproveedor.Text = "";
                    //    //txttipoproveedor.Text = "";
                    //    txtdescripciontipo.Enabled = false;
                    //    MessageBox.Show("Tipo de obra no valida", "Opción incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    //una intervencion para filtrar puede ser aca
                }
                else
                {
                    //txtdescripciontipo.Select();
                }
                //limpiarTipo();
            }
        }
    }
}
