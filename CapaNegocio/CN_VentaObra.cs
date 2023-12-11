using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_VentaObra
    {
        private CD_VentaObra objcd_venta_obra = new CD_VentaObra();

        public bool RestarStock(int idproducto, int cantidad)
        {
            return objcd_venta_obra.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            return objcd_venta_obra.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_venta_obra.ObtenerCorrelativo();
        }

        public bool Registrar(Venta_Obra obj, DataTable DetalleVentaObra, out string Mensaje)
        {
            return objcd_venta_obra.Registrar(obj, DetalleVentaObra, out Mensaje);
        }
        public Venta_Obra ObtenerVenta(string numero)
        {
            Venta_Obra oVenta = objcd_venta_obra.ObtenerVenta(numero);

            if (oVenta.IdVentaObra != 0)
            {
                List<Detalle_Venta_Obra> oDetalleVenta = objcd_venta_obra.ObtenerDetalleVenta(oVenta.IdVentaObra);
                oVenta.oDetalle_Venta = oDetalleVenta;
            }

            return oVenta;
        }
    }
}
