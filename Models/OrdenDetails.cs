using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class OrdenDetails
    {
        public int Orden { get; set; }
        public string Cliente { get; set; }
        public int DetalleOrden { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoOrden { get; set; }
    }
}