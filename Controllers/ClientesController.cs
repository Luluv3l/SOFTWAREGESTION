using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using SoftwareGestion.Models;
using System.Diagnostics;

namespace SoftwareGestion.Controllers
{
    public class ClientesController : Controller
    {
        string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Clientes clientes)
        {
            // Configurar la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Configurar el comando y los parámetros
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_CLIENTE", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Configurar los parámetros del procedimiento almacenado
                    //cmd.Parameters.AddWithValue("@id_cliente", 1);
                    cmd.Parameters.AddWithValue("@nombre_cliente",clientes.nombre_cliente);
                    cmd.Parameters.AddWithValue("@direccion", clientes.direccion);
                    cmd.Parameters.AddWithValue("@correo", clientes.correo);
                    cmd.Parameters.AddWithValue("@telefono",clientes.telefono);
                    cmd.Parameters.AddWithValue("@fecha_registro", DateTime.Now);
                    cmd.Parameters.AddWithValue("@estado", 1);

                    try
                    {
                        // Ejecutar el procedimiento almacenado
                        cmd.ExecuteNonQuery();
                       TempData["SuccedMensaje"] = "SE HA CREADO EL REGISTRO CORRECTAMENTE...";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMensaje1"] = "ERROR!! AL CREAR EL NUEVO REGISTRO";
                        TempData["ErrorMensaje"] = ex.Message;
                }
            }

            return View();
        }

        }
        public IActionResult Details()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
