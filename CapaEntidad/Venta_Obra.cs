using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta_Obra
    {
        public int IdVentaObra { get; set; }
        public Usuario oUsuario { get; set; }
        //public Tamanio oTamanio { get; set; }
        //public Cliente oCliente { get; set; }
        //public TipoObra oTipoObra { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string DescripcionTipo { get; set; }
        public int CantBocas { get; set; }
        public int PersonalTrabajo { get; set; }
        public string DocumentoProveedor { get; set; }
        public string Tipo_Proveedor { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }
        public List<Detalle_Venta_Obra> oDetalle_Venta { get; set; }
        public string FechaRegistro { get; set; }

    }
}
