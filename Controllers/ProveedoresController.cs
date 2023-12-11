using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;

namespace SoftwareGestion.Controllers;

public class ProveedoresController : Controller
{

    string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Create(Proveedores proveedores)
    {
        try
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               // string query = ("INSERT INTO provedores (nombre_proveedor, direccion, correo, telefono,estado) VALUES @nombre_proveedor, @direccion, @correo, @telefono, @estado)
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_PROVEEDOR", connection))
                {   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_proveedor", proveedores.nombre_proveedor);
                    cmd.Parameters.AddWithValue("@direccion", proveedores.direccion);
                    cmd.Parameters.AddWithValue("@correo", proveedores.correo);
                    cmd.Parameters.AddWithValue("@telefono", proveedores.telefono);
                    cmd.Parameters.AddWithValue("@estado", 1);


                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    TempData["SuccedMensaje"] = "SE HA CREADO EL REGISTRO CORRECTAMENTE...";
                }

            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMensaje1"] = "ERROR!! AL CREAR EL NUEVO REGISTRO";
            TempData["ErrorMensaje"] = ex.Message;
        }
        return View();
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