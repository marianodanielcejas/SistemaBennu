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
    public partial class mdTamanioObra : Form
    {
        public Tamanio _Tamanio { get; set; }
        public mdTamanioObra()
        {
            InitializeComponent();
        }

        private void mdTamanioObra_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            List<Tamanio> lista = new CN_TamanioObra().Listar();

            foreach (Tamanio item in lista)
            {
                //if (item.Estado)
                    dgvdata.Rows.Add(new object[] {item.IdTamanio, item.CantBocas, item.PersonalTrabajo });
            }
        }

        private void dgvdata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;
            if (iRow >= 0 && iColum >= 0)
            {
                _Tamanio = new Tamanio()
                {
                    IdTamanio = Convert.ToInt32(dgvdata.Rows[iRow].Cells["Id"].Value.ToString()),
                    CantBocas =  Convert.ToInt32(dgvdata.Rows[iRow].Cells["Bocas"].Value.ToString()),
                    PersonalTrabajo = Convert.ToInt32(dgvdata.Rows[iRow].Cells["Personal"].Value.ToString())
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
    }
}
