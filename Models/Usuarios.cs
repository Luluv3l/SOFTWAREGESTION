using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareGestion.Models
{
    public class Usuarios
    {
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellido_1 { get; set; }
        public string apellido_2 { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        [Range(0,8)]
        public DateTime fecha_registro { get; set; }
        public int id_rol_usuarios { get; set; }
    }
}