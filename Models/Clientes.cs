using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class Clientes
    {
        public int value { get; set; }
        public string label { get; set; }
        public int id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public DateTime fecha_registro { get; set; }
        public char estado { get; set; }

    }
}