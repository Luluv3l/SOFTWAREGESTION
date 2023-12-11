using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class CostosProduccion
    {
        public int numero_orden { get; set; }
        public decimal costo_materia_prima { get; set; }
        public decimal costo_total_general { get; set; }
        public decimal costo_total_pedidos { get; set; }
    }
}