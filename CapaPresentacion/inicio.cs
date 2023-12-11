using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using FontAwesome.Sharp;
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
    public partial class inicio : Form
    {
        private static Usuario usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;
        public inicio(Usuario objusuario = null)
        {
            if (objusuario == null)
                usuarioActual = new Usuario() { NombreCompleto = "ADMIN PREDEFINIDO", IdUsuario = 1 };
            else
                usuarioActual = objusuario;
            InitializeComponent();
        }

        private void inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> ListaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);

            foreach (IconMenuItem iconmenu in menu.Items)
            {

                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }

            }

            lblusuario.Text = usuarioActual.NombreCompleto;
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {

            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            contenedor.Controls.Add(formulario);
            formulario.Show();


        }

        private void menuusuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void submenucategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmCategoria());
        }

        private void submenuproducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmProducto());
        }

        private void submenuregistrarventaobra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentaObra(usuarioActual));
        }

        private void submenuverdetalleventaobra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmDetalleVentaObra());
        }

        private void submenuregistrarcompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompra(usuarioActual));
        }

        private void submenutverdetallecompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmDetalleCompra());
        }

        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void menuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        //private void menureportes_Click(object sender, EventArgs e)
        //{
        //    AbrirFormulario((IconMenuItem)sender, new frmReportes());
        //}

        private void submenutipoobra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmTipoObra());
        }

        private void submenutamanioobra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmTamanioObra());
        }

        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmNegocio());
        }
        //Aqui esta el submenu de reporte de compras de insumos.
        private void submenureportecompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteCompras());
        }
        //Aqui esta el submenu de reporte de venta de obra/materiales.
        private void submenureporteventasobra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteVentasObra());
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void menuacercade_Click(object sender, EventArgs e)
        {
            mdAcercaDe md = new mdAcercaDe();
            md.ShowDialog();
        }
        //Aqui esta el submenu de reporte de venta de Insumos.
        private void submenureporteventasIC_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteVentasIC());
        }


        private void submenuregistrarventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentaInsumos(usuarioActual));
        }

        private void submenudetalleinsumos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmDetalleVentaInsumos());
        }

        private void submenuregistrarcompramateriales_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompraMateriales(usuarioActual));
        }

        private void submenuverdetallesmatereales_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmDetalleCompraMateriales());
        }

        private void submenureportecompramateriales_Click(object sender, EventArgs e)
        {
            //Revisar formulario aqui
            AbrirFormulario(menureportes, new frmReporteCompraMateriales());
        }
    }
    
}
