using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class OrdenPedido
    {
        public int id_orden_pedido { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha { get; set; }
        public int estado { get; set; }

 
        public List<DetalleOrden> DetallesOrden { get; set; }

    }
}