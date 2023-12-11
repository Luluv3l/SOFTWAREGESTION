using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class Productos
    {
   
        public int value { get; set; }
        public string label { get; set; }
        public int id_producto { get; set; }
        public int IdCliente { get; set; }
        public int cantidad { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int id_categorias { get; set; }
        public int id_tallas { get; set; }
        public decimal precio_venta { get; set; }
    }
}