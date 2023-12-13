using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class Productos
    {
<<<<<<< Updated upstream
   
        public int value { get; set; }
        public string label { get; set; }
=======
         public int value { get; set; }
         public string label { get; set; }
>>>>>>> Stashed changes
        public int id_producto { get; set; }
        public int IdCliente { get; set; }
        public int cantidad { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string id_categorias { get; set; }
        public string id_tallas { get; set; }
        public decimal precio_venta { get; set; }

        public string estado {get; set;}
    }
}