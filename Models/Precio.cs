using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class Precio
    {
        public decimal PrecioVenta { get; set; }
        
        public decimal SumaMateria { get; set; }

        public int Alerta { get; set; }

    }
}