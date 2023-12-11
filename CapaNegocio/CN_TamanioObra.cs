using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_TamanioObra
    {
        private CD_TamanioObra objCD_TamanioObra = new CD_TamanioObra();

        public List<Tamanio> Listar()
        {
            return objCD_TamanioObra.Listar();
        }

        public int Registrar(Tamanio obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.CantBocas.ToString() == "")
            {
                Mensaje += "Es necesario la Cantidad de bocas\n";
            }

            if(obj.PersonalTrabajo.ToString() == null)
            {
                Mensaje += "Es necesario el personal de trabajo\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCD_TamanioObra.Registrar(obj, out Mensaje);
            }


        }


        public bool Editar(Tamanio obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (obj.CantBocas.ToString() == "")
            {
                Mensaje += "Es necesario la cantidad de bocas\n";
            }

            if (obj.PersonalTrabajo.ToString() == null)
            {
                Mensaje += "Es necesario el personal de trabajo\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCD_TamanioObra.Editar(obj, out Mensaje);
            }


        }


        public bool Eliminar(Tamanio obj, out string Mensaje)
        {
            return objCD_TamanioObra.Eliminar(obj, out Mensaje);
        }
    }
}
