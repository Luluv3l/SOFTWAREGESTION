using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class MateriaPrima
    {
        public int codigo { get; set; }
        public decimal costo_unitario { get; set; }
        public string materia_prima { get; set; }
        public string descripcion { get; set; }
        public decimal costo_total { get; set; }
    }
}