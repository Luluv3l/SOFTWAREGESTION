using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;

namespace SoftwareGestion.Controllers;

public class UsuariosController : Controller
{
    string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult GuardarValor()
    {
        string opcionSeleccionada = Request.Form["opcionSeleccionadarol"];


        //Guardar el valor seleccionado en la base de datos
        return RedirectToAction("Create");
    }


    // POST: usuarios/Create
    [HttpPost]
    public ActionResult Create(Usuarios ousuarios)
    {
        try
        {
            string opcionSeleccionada = Request.Form["opcionSeleccionadarol"];
       
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                ousuarios.contrasena = ConvertirSha256(ousuarios.contrasena);
           
                //string insertQuery = "INSERT INTO usuarios (nombre,apellido_1,apellido_2,correo,contrasena,fecha_registro,id_rol_usuarios) VALUES (@nombre, @apellido_1, @apellido_2, @correo, @contrasena, @fecha_registro, @id_rol_usuarios)";
                using (SqlCommand command = new SqlCommand("SP_REGISTRAR_USUARIO", connection))
                {
                    command.Parameters.AddWithValue("@nombre", ousuarios.nombre);
                    command.Parameters.AddWithValue("@apellido_1", ousuarios.apellido_1);
                    command.Parameters.AddWithValue("@apellido_2", ousuarios.apellido_2);
                    command.Parameters.AddWithValue("@correo", ousuarios.correo);
                    command.Parameters.AddWithValue("@contrasena", ousuarios.contrasena);
                    command.Parameters.AddWithValue("@fecha_registro", ousuarios.fecha_registro);
                    command.Parameters.AddWithValue("@id_rol_usuarios", opcionSeleccionada);
             
                    command.CommandType = CommandType.StoredProcedure;

                    command.ExecuteNonQuery();

                

                    TempData["SuccedMensaje"] = "SE HA CREADO EL REGISTRO CORRECTAMENTE...";
                }


            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMensaje2"] = "USUARIO YA EXISTE";
            TempData["ErrorMensaje1"] = "ERROR!! AL CREAR EL NUEVO REGISTRO";
            TempData["ErrorMensaje"] = ex.Message;
        }
        return View();
    }


    public IActionResult Details()
    {
        return View();
    }


    public static string ConvertirSha256(string texto)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 sha256 = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = sha256.ComputeHash(enc.GetBytes(texto));

            foreach (byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
