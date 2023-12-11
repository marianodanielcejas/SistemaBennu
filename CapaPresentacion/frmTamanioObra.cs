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
using static ClosedXML.Excel.XLPredefinedFormat;

namespace CapaPresentacion
{
    public partial class frmTamanioObra : Form
    {
        public frmTamanioObra()
        {
            InitializeComponent();
        }

        private void frmTamanioObra_Load(object sender, EventArgs e)
        {
            //cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            //cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            //cboestado.DisplayMember = "Texto";
            //cboestado.ValueMember = "Valor";
            //cboestado.SelectedIndex = 0;


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



            
            List<Tamanio> lista = new CN_TamanioObra().Listar();

            foreach (Tamanio item in lista)
            {

                dgvdata.Rows.Add(new object[] {"",item.IdTamanio,
                    item.CantBocas,
                    item.PersonalTrabajo
                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            Tamanio obj = null;
            try
            {
                obj = new Tamanio()
                {
                    IdTamanio = Convert.ToInt32(txtid.Text),
                    CantBocas = Convert.ToInt32(txtBocas.Text),
                    PersonalTrabajo = Convert.ToInt32(txtPersonalTrabajo.Text),
                };

                if (obj.IdTamanio == 0)
                {
                    int idgenerado = new CN_TamanioObra().Registrar(obj, out mensaje);

                    if (idgenerado != 0)
                    {

                        dgvdata.Rows.Add(new object[] {"",idgenerado,txtBocas.Text,txtPersonalTrabajo.Text
                    });

                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }


                }
                else
                {
                    bool resultado = new CN_TamanioObra().Editar(obj, out mensaje);

                    if (resultado)
                    {
                        DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                        row.Cells["Id"].Value = txtid.Text;
                        row.Cells["CantBocas"].Value = txtBocas.Text;
                        row.Cells["PersonalTrabajo"].Value = txtPersonalTrabajo.Text;
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBocas.Select();
            }
            //Tamanio obj = new Tamanio()
            //{
            //    IdTamanio = Convert.ToInt32(txtid.Text),
            //    CantBocas = Convert.ToInt32(txtBocas.Text),
            //    PersonalTrabajo = Convert.ToInt32(txtPersonalTrabajo.Text),
            //};

            //if (obj.IdTamanio == 0)
            //{
            //    int idgenerado = new CN_TamanioObra().Registrar(obj, out mensaje);

            //    if (idgenerado != 0)
            //    {

            //        dgvdata.Rows.Add(new object[] {"",idgenerado,txtBocas.Text,txtPersonalTrabajo.Text
            //        });

            //        Limpiar();
            //    }
            //    else
            //    {
            //        MessageBox.Show(mensaje);
            //    }


            //}
            //else
            //{
            //    bool resultado = new CN_TamanioObra().Editar(obj, out mensaje);

            //    if (resultado)
            //    {
            //        DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
            //        row.Cells["Id"].Value = txtid.Text;
            //        row.Cells["CantBocas"].Value = txtBocas.Text;
            //        row.Cells["PersonalTrabajo"].Value = txtPersonalTrabajo.Text;
            //        Limpiar();
            //    }
            //    else
            //    {
            //        MessageBox.Show(mensaje);
            //    }
            //}
        }
        private void Limpiar()
        {

            txtindice.Text = "-1";
            txtid.Text = "0";
            txtBocas.Text = "";
            txtPersonalTrabajo.Text = "";
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
                    txtBocas.Text = dgvdata.Rows[indice].Cells["CantBocas"].Value.ToString();
                    txtPersonalTrabajo.Text = dgvdata.Rows[indice].Cells["PersonalTrabajo"].Value.ToString();
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar la Magnitud de la Obra", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    string mensaje = string.Empty;
                    Tamanio obj = new Tamanio()
                    {
                        IdTamanio = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_TamanioObra().Eliminar(obj, out mensaje);

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

        private void txtBocas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtBocas.Text.Length == 2 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPersonalTrabajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            if (txtPersonalTrabajo.Text.Length == 1 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
