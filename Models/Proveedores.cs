using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class Proveedores
    {
        public int id_proveedor { get; set; }
        public string nombre_proveedor { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public DateTime fecha_registro { get; set; }
        public char estado { get; set; }
    }
}