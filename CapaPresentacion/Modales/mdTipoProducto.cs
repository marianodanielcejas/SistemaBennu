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
    public partial class mdTipoProducto : Form
    {
        public TipoObra _TipoObra { get; set; }
        public mdTipoProducto()
        {
            InitializeComponent();
        }

        private void mdTipoProducto_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
            }

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            List<TipoObra> lista = new CN_TipoObra().Listar();

            foreach (TipoObra item in lista)
            {
                if (item.DescripcionTipo == "TELECOMUNICACIONES" || item.DescripcionTipo == "ENERGIA" || item.DescripcionTipo == "SEGURIDAD" || item.DescripcionTipo == "REDES INFORMATICAS")
                {
                    continue;
                }
                dgvdata.Rows.Add(new object[] { item.Id_Tipo_Obra, item.DescripcionTipo });
            }
        }

        private void dgvdata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;
            if (iRow >= 0 && iColum >= 0)
            {
                _TipoObra = new TipoObra()
                {
                    Id_Tipo_Obra = Convert.ToInt32(dgvdata.Rows[iRow].Cells["Id"].Value.ToString()),
                    DescripcionTipo = dgvdata.Rows[iRow].Cells["Descripcion"].Value.ToString()
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
