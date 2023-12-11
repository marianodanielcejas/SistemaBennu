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
    public class CN_CompraMateriales
    {
        private CD_CompraMateriales objcd_compramateriales = new CD_CompraMateriales();


        public int ObtenerCorrelativo()
        {
            return objcd_compramateriales.ObtenerCorrelativo();
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            return objcd_compramateriales.Registrar(obj, DetalleCompra, out Mensaje);
        }

        public Compra ObtenerCompra(string numero)
        {

            Compra oCompra = objcd_compramateriales.ObtenerCompra(numero);

            if (oCompra.IdCompra != 0)
            {
                List<Detalle_Compra> oDetalleCompra = objcd_compramateriales.ObtenerDetalleCompra(oCompra.IdCompra);

                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }
    }
}
