using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class DetalleOrden
    {
        public int id_orden_pedido { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha { get; set; }
        public int estado { get; set; }

        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public int id_detalle_orden_pedido { get; set; }

        public int cantidad { get; set; }
        public int id_producto { get; set; }


    }
}