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
    public class CN_VentaInsumos
    {
        private CD_VentaInsumos objcd_venta_Insumos = new CD_VentaInsumos();

        public bool RestarStock(int idproducto, int cantidad)
        {
            return objcd_venta_Insumos.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            return objcd_venta_Insumos.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_venta_Insumos.ObtenerCorrelativo();
        }
        public bool Registrar(Venta_Insumos obj, DataTable DetalleVentaInsumos, out string Mensaje)
        {
            return objcd_venta_Insumos.Registrar(obj, DetalleVentaInsumos, out Mensaje);
        }
        public Venta_Insumos ObtenerVenta(string numero)
        {
            Venta_Insumos oVentaInsumos = objcd_venta_Insumos.ObtenerVenta(numero);

            if (oVentaInsumos.IdVentaIC != 0)
            {
                List<Detalle_Venta_Insumos> oDetalleVentaInsumos = objcd_venta_Insumos.ObtenerDetalleVenta(oVentaInsumos.IdVentaIC);
                oVentaInsumos.oDetalle_Venta_Materiales = oDetalleVentaInsumos;
            }

            return oVentaInsumos;
        }

    }
}
