using CapaEntidad;
using CapaNegocio;
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

namespace CapaPresentacion.Modales
{
    public partial class mdProducto : Form
    {
        public Producto _Producto { get; set; }
        public mdProducto()
        {
            InitializeComponent();
        }

        private void mdProducto_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {

                if (columna.Visible == true)
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
                if (item.TipoProducto == "O")
                {
                    continue;
                }

                dgvdata.Rows.Add(new object[] {
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.oCategoria.Descripcion,
                    item.TipoProducto,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta
                }) ;
            }
        }

        private void dgvdata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;
            if (iRow >= 0 && iColum > 0)
            {
                _Producto = new Producto()
                {
                    IdProducto = Convert.ToInt32(dgvdata.Rows[iRow].Cells["Id"].Value.ToString()),
                    Codigo = dgvdata.Rows[iRow].Cells["Codigo"].Value.ToString(),
                    Nombre = dgvdata.Rows[iRow].Cells["Nombre"].Value.ToString(),
                    //ojo con tipo producto
                    TipoProducto = dgvdata.Rows[iRow].Cells["TipoProducto"].Value.ToString(),
                    Stock = Convert.ToInt32(dgvdata.Rows[iRow].Cells["Stock"].Value.ToString()),
                    PrecioCompra = Convert.ToDecimal(dgvdata.Rows[iRow].Cells["PrecioCompra"].Value.ToString()),
                    PrecioVenta = Convert.ToDecimal(dgvdata.Rows[iRow].Cells["PrecioVenta"].Value.ToString()),
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
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

        
        //private Usuario _Usuario;
        //public frmCompras(Usuario oUsuario = null)
        //{
        //    _Usuario = oUsuario;
        //    InitializeComponent();
        //}

        //private void frmCompras_Load(object sender, EventArgs e)
        //{
        //    cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
        //    cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
        //    cbotipodocumento.DisplayMember = "Texto";
        //    cbotipodocumento.ValueMember = "Valor";
        //    cbotipodocumento.SelectedIndex = 0;

        //    txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

        //    txtidproveedor.Text = "0";
        //    txtidproducto.Text = "0";
        //}

        //private void btnbuscarproveedor_Click(object sender, EventArgs e)
        //{
        //    using (var modal = new mdProveedor())
        //    {
        //        var result = modal.ShowDialog();

        //        if (result == DialogResult.OK)
        //        {
        //            //txtdocproveedor.Enabled = false;
        //            //txttipoproveedor.Enabled = false;
        //            txtidproveedor.Text = modal._Proveedor.IdProveedor.ToString();
        //            txtdocproveedor.Text = modal._Proveedor.Documento;
        //            txttipoproveedor.Text = modal._Proveedor.Tipo_Proveedor;
        //            txtdocproveedor.Enabled = true;
        //            txttipoproveedor.Enabled = true;
        //            if (txttipoproveedor.Text != "C")
        //            {
        //                //txtdocproveedor.Text = "";
        //                //txttipoproveedor.Text = "";
        //                txtdocproveedor.Enabled = false;
        //                txttipoproveedor.Enabled = false;
        //                MessageBox.Show("Solo se puede seleccionar proveedor de compra", "Opción incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //            //una intervencion para filtrar puede ser aca
        //        }
        //        else
        //        {
        //            txtdocproveedor.Select();
        //        }

        //    }
        //}

        //private void btnbuscarproducto_Click(object sender, EventArgs e)
        //{
        //    using (var modal = new mdProducto())
        //    {
        //        var result = modal.ShowDialog();

        //        if (result == DialogResult.OK)
        //        {
        //            txtidproducto.Text = modal._Producto.IdProducto.ToString();
        //            txtcodproducto.Text = modal._Producto.Codigo;
        //            txtproducto.Text = modal._Producto.Nombre;
        //            txttipoproducto.Text = modal._Producto.TipoProducto;
        //            txtcodproducto.Enabled = true;
        //            txtproducto.Enabled = true;
        //            txttipoproducto.Enabled = true;
        //            txtpreciocompra.Select();
        //            if (txttipoproducto.Text != "C")
        //            {
        //                txtcodproducto.Enabled = false;
        //                txtproducto.Enabled = false;
        //                txttipoproducto.Enabled = false;
        //                MessageBox.Show("Solo se puede seleccionar producto de compra", "Opción incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //        else
        //        {
        //            txtcodproducto.Select();
        //        }

        //    }
        //}

        //private void btnagregarproducto_Click(object sender, EventArgs e)
        //{
        //    decimal preciocompra = 0;
        //    decimal precioventa = 0;
        //    bool producto_existe = false;

        //    if (int.Parse(txtidproducto.Text) == 0)
        //    {
        //        MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }

        //    if (!decimal.TryParse(txtpreciocompra.Text, out preciocompra))
        //    {
        //        MessageBox.Show("Precio Compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtpreciocompra.Select();
        //        return;
        //    }

        //    if (!decimal.TryParse(txtprecioventa.Text, out precioventa))
        //    {
        //        MessageBox.Show("Precio Venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtprecioventa.Select();
        //        return;
        //    }

        //    foreach (DataGridViewRow fila in dgvdata.Rows)
        //    {
        //        if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
        //        {
        //            producto_existe = true;
        //            break;
        //        }
        //    }

        //    if (!producto_existe)
        //    {

        //        dgvdata.Rows.Add(new object[] {
        //            txtidproducto.Text,
        //            txtproducto.Text,
        //            preciocompra.ToString("0.00"),
        //            precioventa.ToString("0.00"),
        //            txtcantidad.Value.ToString(),
        //            (txtcantidad.Value * preciocompra).ToString("0.00")

        //        });

        //        calcularTotal();
        //        limpiarProducto();
        //        txtcodproducto.Select();

        //    }
        //}
        //private void limpiarProducto()
        //{
        //    txtidproducto.Text = "0";
        //    txtcodproducto.Text = "";
        //    txtcodproducto.BackColor = Color.White;
        //    txtproducto.Text = "";
        //    txtpreciocompra.Text = "";
        //    txtprecioventa.Text = "";
        //    txtcantidad.Value = 1;
        //}
        //private void calcularTotal()
        //{
        //    decimal total = 0;
        //    if (dgvdata.Rows.Count > 0)
        //    {
        //        foreach (DataGridViewRow row in dgvdata.Rows)
        //            total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
        //    }
        //    txttotalpagar.Text = total.ToString("0.00");
        //}

        //private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.RowIndex < 0)
        //        return;

        //    if (e.ColumnIndex == 6)
        //    {

        //        e.Paint(e.CellBounds, DataGridViewPaintParts.All);

        //        var w = Properties.Resources.delete25.Width;
        //        var h = Properties.Resources.delete25.Height;
        //        var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
        //        var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

        //        e.Graphics.DrawImage(Properties.Resources.delete25, new Rectangle(x, y, w, h));
        //        e.Handled = true;
        //    }
        //}

        //private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
        //    {
        //        int indice = e.RowIndex;

        //        if (indice >= 0)
        //        {
        //            dgvdata.Rows.RemoveAt(indice);
        //            calcularTotal();
        //        }
        //    }
        //}
    }
}
