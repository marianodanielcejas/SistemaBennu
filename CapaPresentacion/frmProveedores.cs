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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "ACTIVO" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "NO ACTIVO" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;


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

            List<Proveedor> lista = new CN_Proveedor().Listar();

            foreach (Proveedor item in lista)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdProveedor,item.Documento,item.Tipo_Proveedor,item.Correo,item.Telefono,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "ACTIVO" : "NO ACTIVO"
                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if(txtdocumento.ForeColor == Color.Red)
            {
                LPCP();
            }
            if (txtcorreo.ForeColor == Color.Red)
            {
                LPCE();
            }
            string mensaje = string.Empty;

            Proveedor obj = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                Tipo_Proveedor = txttipoproveedor.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };
            //if(txttipoproveedor.Text != "O" && txttipoproveedor.Text != "C")
            //{
            //    MessageBox.Show("Letra inválida debe seleccionar 'O' o 'C' ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txttipoproveedor.Text = "";
            //}
            //if (txtdocumento.ForeColor == Color.Red)
            //{
            //    MessageBox.Show("email inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtdocumento.Text = "";
            //}

            if (obj.IdProveedor == 0)
            {
                int idgenerado = new CN_Proveedor().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {
                    //if (txtdocumento.ForeColor == Color.Red)
                    //{
                    //    MessageBox.Show("Cuil inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtdocumento.Text = "";
                    //}
                    //else
                    //{
                        dgvdata.Rows.Add(new object[] {"",idgenerado,txtdocumento.Text,txttipoproveedor.Text,txtcorreo.Text,txttelefono.Text,
                    ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                    ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()
                    });
                    //Limpiar();
                    //}
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                    //txtdocumento.Text = "";
                }


            }
            else
            {
                bool resultado = new CN_Proveedor().Editar(obj, out mensaje);

                if (resultado)
                {
                   
                      DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                      row.Cells["Id"].Value = txtid.Text;
                      row.Cells["Documento"].Value = txtdocumento.Text;
                      row.Cells["TipoProveedor"].Value = txttipoproveedor.Text;
                      row.Cells["Correo"].Value = txtcorreo.Text;
                      row.Cells["Telefono"].Value = txttelefono.Text;
                      row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                      row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                      Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                    //MessageBox.Show(mensaje, "Cuil repetido");


                }
            }
        }
        private void LPCP()
        {
            txtdocumento.Text = "";
        }
        private void LPCE()
        {
            txtcorreo.Text = "";
        }
        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Text = "";
            txttipoproveedor.Text = "";
            txtcorreo.Text = "";
            txttelefono.Text = "";
            cboestado.SelectedIndex = 0;
            txtdocumento.Select();
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
                    txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    txttipoproveedor.Text = dgvdata.Rows[indice].Cells["TipoProveedor"].Value.ToString();
                    txtcorreo.Text = dgvdata.Rows[indice].Cells["Correo"].Value.ToString();
                    txttelefono.Text = dgvdata.Rows[indice].Cells["Telefono"].Value.ToString();

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
                if (MessageBox.Show("¿Desea eliminar el proveedor", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Proveedor obj = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Proveedor().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        Limpiar();
                    }
                    else
                    {
                        //MessageBox.Show(mensaje, "No es posible eliminar ya que esta asociado a una Compra");
                        MessageBox.Show(mensaje, "No es posible eliminar el proveedor por que esta asociado a una Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtdocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtdocumento.Text.Length == 13 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            string texto = txtdocumento.Text;

            // Agrega el carácter que se acaba de escribir
            if (e.KeyChar != (char)Keys.Back)
            {
                texto += e.KeyChar;
            }

            // Utiliza una expresión regular para validar el correo electrónico
            string patronCuil = @"^\d{2}-\d{8}-\d{1}$";
            bool esValido = Regex.IsMatch(texto, patronCuil);

            if (esValido)
            {
                // El correo electrónico es válido
                txtdocumento.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                // El correo electrónico no es válido
                txtdocumento.ForeColor = System.Drawing.Color.Red;
                //txtdocumento.Text = "";
                //txtdocumento.Select();
            }
        }

        private void txttipoproveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txttipoproveedor.Text.Length > 0 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtcorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtén el texto actual del TextBox
            string texto = txtcorreo.Text;

            // Agrega el carácter que se acaba de escribir
            if (e.KeyChar != (char)Keys.Back)
            {
                texto += e.KeyChar;
            }

            // Utiliza una expresión regular para validar el correo electrónico
            string patronEmail = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            bool esValido = Regex.IsMatch(texto, patronEmail);

            if (esValido)
            {
                // El correo electrónico es válido
                txtcorreo.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                // El correo electrónico no es válido
                txtcorreo.ForeColor = System.Drawing.Color.Red;
                //txtcorreo.Text = "";
                //txtcorreo.Select();
            }
        }

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txttelefono.Text.Length == 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txttipoproveedor_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txttipoproveedor.Text) && txttipoproveedor.Text.All(char.IsUpper)) ;
            {
                txttipoproveedor.Text = txttipoproveedor.Text.ToUpper();
                txttipoproveedor.SelectionStart = txttipoproveedor.Text.Length;
            }
            //if (txtTipoProducto.Text == "O" && txtTipoProducto.Text != "C" || txtTipoProducto.Text == "C" && txtTipoProducto.Text != "O")
            //{
            //    MessageBox.Show("Las letras 'O' o 'C' son las unicas admitidas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtTipoProducto.Select();
            //    txtTipoProducto.Text = "";
            //    //txtTipoProducto.Text = "";
            //    //txtTipoProducto.Select();
            //}
            if (txttipoproveedor.Text == "O" || txttipoproveedor.Text == "o")
            {
                txttipoproveedor.Text = txttipoproveedor.Text.ToUpper();
                txttipoproveedor.SelectionStart = txttipoproveedor.Text.Length;
            }
            else if (txttipoproveedor.Text == "C" || txttipoproveedor.Text == "c")
            {
                txttipoproveedor.Text = txttipoproveedor.Text.ToUpper();
                txttipoproveedor.SelectionStart = txttipoproveedor.Text.Length;
            }
            else
            {
                txttipoproveedor.Text = "";
                txttipoproveedor.Select();
            }
        }

        private void txtdocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtdocumento.Text.StartsWith("0") || txtdocumento.Text.StartsWith("1"))
            {
                MessageBox.Show("El Cuil no puede iniciar ni con '0' ni con '1'", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtdocumento.Text = "";
            }
        }

        //private void txtdocumento_TextChanged(object sender, EventArgs e)
        //{
        //    if(txtdocumento.ForeColor == Color.Red)
        //    {
        //        txtdocumento.Text = "";
        //        txtdocumento.Select();
        //    }
        //}
    }
}
