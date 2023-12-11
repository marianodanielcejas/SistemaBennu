using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
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
    public partial class frmCompraMateriales : Form
    {
        private Usuario _Usuario;
        public frmCompraMateriales(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmCompraMateriales_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "REMITO|FACTURA", Texto = "REMITO|FACTURA" });
            //cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Nota de pedido", Texto = "Nota de pedido" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtidproveedor.Text = "0";
            txtidproducto.Text = "0";
        }

        private void btnbuscarproveedormateriales_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedorMateriales())
            {

                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //txtdocproveedor.Enabled = false;
                    //txttipoproveedor.Enabled = false;
                    txtidproveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtcuitproveedor.Text = modal._Proveedor.Documento;
                    txttipoproveedor.Text = modal._Proveedor.Tipo_Proveedor;
                    txtcuitproveedor.Enabled = true;
                    txttipoproveedor.Enabled = true;

                    //if (txttipoproveedor.Text != "C")
                    //{
                    //    //Prueba de cambios aqui
                    //    txtdocproveedor.Enabled = false;
                    //    txttipoproveedor.Enabled = false;

                    //    MessageBox.Show("Solo se puede seleccionar proveedor de compra", "Opción incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtdocproveedor.Text = "";
                    //    txttipoproveedor.Text = "";
                    //}
                    //una intervencion para filtrar puede ser aca
                }
                else
                {
                    txtcuitproveedor.Select();
                }

            }
        }

        private void btnbuscarmateriales_Click(object sender, EventArgs e)
        {
            using (var modal = new mdMateriales())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text = modal._Producto.IdProducto.ToString();
                    txtcodproducto.Text = modal._Producto.Codigo;
                    txtproducto.Text = modal._Producto.Nombre;
                    txttipoproducto.Text = modal._Producto.TipoProducto;
                    txtcodproducto.Enabled = true;
                    txtproducto.Enabled = true;
                    txttipoproducto.Enabled = true;
                    txtpreciocompra.Text = modal._Producto.PrecioCompra.ToString("0.00");
                    txtprecioventa.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    //txtpreciocompra.Select();
                    //    if (txttipoproducto.Text != "C")
                    //    {
                    //        txtcodproducto.Enabled = false;
                    //        txtproducto.Enabled = false;
                    //        txttipoproducto.Enabled = false;
                    //        MessageBox.Show("Solo se puede seleccionar producto de compra", "Opción incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        txtcodproducto.Text = "";
                    //        txtproducto.Text = "";
                    //        txttipoproducto.Text = "";
                    //    }
                }
                else
                {
                    txtcodproducto.Select();
                }
                //limpiarProveedor();
            }
        }

        private void btnagregarmaterial_Click(object sender, EventArgs e)
        {
            decimal preciocompra = 0;
            decimal precioventa = 0;
            bool producto_existe = false;

            if (int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (int.Parse(txtidproveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtpreciocompra.Text, out preciocompra))
            {
                MessageBox.Show("Precio Compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpreciocompra.Select();
                return;
            }

            if (!decimal.TryParse(txtprecioventa.Text, out precioventa))
            {
                MessageBox.Show("Precio Venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioventa.Select();
                return;
            }

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }

            //if (!producto_existe)
            //{

            //    dgvdata.Rows.Add(new object[] {
            //            txtidproducto.Text,
            //            txtproducto.Text,
            //            preciocompra.ToString("0.00"),
            //            precioventa.ToString("0.00"),
            //            txtcantidad.Value.ToString(),
            //            (txtcantidad.Value * preciocompra).ToString("0.00")

            //    });

            //    calcularTotal();
            //    limpiarProducto();
            //    txtcodproducto.Select();

            //}
            if (!producto_existe && txtpreciocompra.Text == "0,00" || txtpreciocompra.Text == "00,00" || txtpreciocompra.Text == "0" || txtpreciocompra.Text == "00")
            {
                MessageBox.Show("El precio de compra no puede contener '0'", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpreciocompra.Select();
            }
            else if (!producto_existe && txtprecioventa.Text == "0,00" || txtprecioventa.Text == "00,00" || txtprecioventa.Text == "0" || txtprecioventa.Text == "00")
            {
                MessageBox.Show("El precio de venta no puede contener '0'", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecioventa.Select();
            }
            else
            {
                dgvdata.Rows.Add(new object[] {
                        txtidproducto.Text,
                        txtproducto.Text,
                        preciocompra.ToString("0.00"),
                        precioventa.ToString("0.00"),
                        txtcantidad.Value.ToString(),
                        (txtcantidad.Value * preciocompra).ToString("0.00")

                });

                calcularTotal();
                limpiarProducto();
                txtcodproducto.Select();
            }
        }

        private void limpiarsoloproveedor()
        {
            txttipoproveedor.Text = "";
            txtcuitproveedor.Text = "";
        }
        private void limpiarProducto()
        {
            txtidproducto.Text = "0";
            txtcodproducto.Text = "";
            txtcodproducto.BackColor = Color.White;
            txttipoproducto.Text = "";
            //txttipoproveedor.Text = "";
            //txtcuitproveedor.Text = "";
            txtproducto.Text = "";
            txtpreciocompra.Text = "";
            txtprecioventa.Text = "";
            txtcantidad.Value = 1;
        }
        private void limpiarProveedor()
        {
            txttipoproveedor.Text = "";
            txtcuitproveedor.Text = "";
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
       

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidproveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor de materiales", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable detalle_compra = new DataTable();

            detalle_compra.Columns.Add("IdProducto", typeof(int));
            detalle_compra.Columns.Add("PrecioCompra", typeof(decimal));
            detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_compra.Columns.Add("Cantidad", typeof(int));
            detalle_compra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalle_compra.Rows.Add(
                    new object[] {
                       Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                       row.Cells["PrecioCompra"].Value.ToString(),
                       row.Cells["PrecioVenta"].Value.ToString(),
                       row.Cells["Cantidad"].Value.ToString(),
                       row.Cells["SubTotal"].Value.ToString()
                    });

            }

            int idcorrelativo = new CN_CompraMateriales().ObtenerCorrelativo();
            string numerodocumento = string.Format("{0:00000}", idcorrelativo);

            Compra oCompra = new Compra()
            {
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtidproveedor.Text) },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numerodocumento,
                MontoTotal = Convert.ToDecimal(txttotalpagar.Text)
            };

            //string mensaje = string.Empty;
            //bool respuesta = new CN_CompraMateriales().Registrar(oCompra, detalle_compra, out mensaje);

            //if (respuesta)
            //{
            //    var result = MessageBox.Show("Numero de compra generada:\n" + numerodocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            //    if (result == DialogResult.Yes)
            //        Clipboard.SetText(numerodocumento);

            //    txtidproveedor.Text = "0";
            //    txtcuitproveedor.Text = "";
            //    txttipoproveedor.Text = "";
            //    dgvdata.Rows.Clear();
            //    calcularTotal();

            //}
            //else
            //{
            //    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            string mensaje = string.Empty;

            //if (respuesta)
            //{
            var result = MessageBox.Show("Numero de compra generada:\n" + numerodocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Clipboard.SetText(numerodocumento);
                bool respuesta = new CN_Compra().Registrar(oCompra, detalle_compra, out mensaje);
                txtidproveedor.Text = "0";
                txtcuitproveedor.Text = "";
                txttipoproveedor.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
            }
            else
            {
                txtidproveedor.Text = "0";
                txtcuitproveedor.Text = "";
                txttipoproveedor.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
            }
        }

        private void txtpreciocompra_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtpreciocompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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

        private void txtprecioventa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecioventa.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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

        private void dgvdata_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 6)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete25.Width;
                var h = Properties.Resources.delete25.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete25, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    dgvdata.Rows.RemoveAt(indice);
                    calcularTotal();
                }
            }
        }

      
    }
}
