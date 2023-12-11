using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_TipoObra
    {
        private CD_TipoObra objcd_TipoObra = new CD_TipoObra();

        public List<TipoObra> Listar()
        {
            return objcd_TipoObra.Listar();
        }

        public int Registrar(TipoObra obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.DescripcionTipo == "")
            {
                Mensaje += "Es necesario la descripcion del Tipo de Obra\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_TipoObra.Registrar(obj, out Mensaje);
            }


        }


        public bool Editar(TipoObra obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.DescripcionTipo == "")
            {
                Mensaje += "Es necesario la descripcion del Tipo de Obra\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_TipoObra.Editar(obj, out Mensaje);
            }


        }


        public bool Eliminar(TipoObra obj, out string Mensaje)
        {
            return objcd_TipoObra.Eliminar(obj, out Mensaje);
        }
    }
}
