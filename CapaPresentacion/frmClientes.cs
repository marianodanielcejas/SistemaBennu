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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
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



            //MOSTRAR TODOS LOS USUARIOS
            List<Cliente> lista = new CN_Cliente().Listar();

            foreach (Cliente item in lista)
            {
                dgvdata.Rows.Add(new object[] {"",item.IdCliente,item.Documento,item.NombreCompleto,item.Correo,item.Telefono,
                    item.Estado == true ? 1 : 0 ,
                    item.Estado == true ? "ACTIVO" : "NO ACTIVO"
                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (txtcorreo.ForeColor == Color.Red)
            {
                LPCC();
            }
            if (txtdocumento.ForeColor == Color.Red)
            {
                LPDOC();
            }
            string mensaje = string.Empty;

            Cliente obj = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1 ? true : false
            };

            //if(txtcorreo.ForeColor == Color.Red)
            //{
            //    MessageBox.Show("email inválido", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtcorreo.Text = "";
            //}

            if (obj.IdCliente == 0)
            {
                int idgenerado = new CN_Cliente().Registrar(obj, out mensaje);

                if (idgenerado != 0)
                {

                    dgvdata.Rows.Add(new object[] {"",idgenerado,txtdocumento.Text,txtnombrecompleto.Text,txtcorreo.Text,txttelefono.Text,
                        ((OpcionCombo)cboestado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboestado.SelectedItem).Texto.ToString()
                    });

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
                bool resultado = new CN_Cliente().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtnombrecompleto.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Telefono"].Value = txttelefono.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboestado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboestado.SelectedItem).Texto.ToString();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                    //txtdocumento.Text = "";
                }
            }
        }
        private void LPCC()
        {
            txtcorreo.Text = "";
        }
        private void LPDOC()
        {
            txtdocumento.Text = "";
        }
        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtdocumento.Text = "";
            txtnombrecompleto.Text = "";
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
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {

                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    txtdocumento.Text = dgvdata.Rows[indice].Cells["Documento"].Value.ToString();
                    txtnombrecompleto.Text = dgvdata.Rows[indice].Cells["NombreCompleto"].Value.ToString();
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
                if (MessageBox.Show("¿Desea eliminar el cliente", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Cliente obj = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Cliente().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtdocumento.Text.Length == 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            string texto = txtdocumento.Text;

            // Agrega el carácter que se acaba de escribir
            if (e.KeyChar != (char)Keys.Back)
            {
                texto += e.KeyChar;
            }

            // Utiliza una expresión regular para validar el Dec
            string patronDoc = @"^[1-9]\d{6,7}$";
            bool esValido = Regex.IsMatch(texto, patronDoc);

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

        private void txtnombrecompleto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtnombrecompleto.Text.Length > 20 && !char.IsControl(e.KeyChar))
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

        private void txtnombrecompleto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombrecompleto.Text) && txtnombrecompleto.Text.All(char.IsUpper)) ;
            {
                txtnombrecompleto.Text = txtnombrecompleto.Text.ToUpper();
                txtnombrecompleto.SelectionStart = txtnombrecompleto.Text.Length;
            }
        }

        private void txtdocumento_TextChanged(object sender, EventArgs e)
        {
            if (txtdocumento.Text.StartsWith("0"))
            {
                MessageBox.Show("El documento no puede iniciar con 0", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtdocumento.Text = "";
            }
        }
    }
}
