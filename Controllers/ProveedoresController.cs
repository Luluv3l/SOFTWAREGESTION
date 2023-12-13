using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;

namespace SoftwareGestion.Controllers;

public class ProveedoresController : Controller
{

    // string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";
    string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;Integrated Security=True;User ID=.";


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
                //string query = "INSERT INTO provedores (nombre_proveedor, direccion, correo, telefono,estado)  VALUES (@nombre, @direccion, @correo, @telefono, @estado)";

                
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_PROVEEDOR", connection))
<<<<<<< Updated upstream
                {   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_proveedor", proveedores.nombre_proveedor);
=======
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@nombre", proveedores.nombre_proveedor);
>>>>>>> Stashed changes
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
        try
        {
            List<Proveedores> proveedores = ObtenerInformacionOrden(); // Asegúrate de que este método devuelve datos válidos

            if (proveedores == null || !proveedores.Any())
            {
                // Manejar el caso en el que no se obtuvieron datos, puede ser un mensaje de error, redirección, etc.
                return RedirectToAction("Error");
            }

            return View(proveedores);
        }
        catch (Exception ex)
        {
            // Loguear el error o manejarlo de alguna manera
            Console.WriteLine($"Error en la acción Details: {ex.Message}");
            return RedirectToAction("Error");
        }
    }
       
    private List<Proveedores> ObtenerInformacionOrden()
    {
        try
        {
            List<Proveedores> proveedores = new List<Proveedores>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * from Provedores where estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Proveedores proveedor = new Proveedores
                            {
                                id_proveedor = Convert.ToInt32(reader["id_proveedor"]),
                                nombre_proveedor = reader["nombre_proveedor"].ToString(),
                                direccion = reader["direccion"].ToString(),
                                correo = reader["correo"].ToString(),
                                telefono = Convert.ToInt32(reader["telefono"]),
                                estado = Convert.ToChar(reader["estado"])
                            };

                            proveedores.Add(proveedor);
                        }
                    }
                }
            }

           return proveedores;
        }

        catch (Exception ex)
        {
            // Loguear el error o manejarlo de alguna manera
            Console.WriteLine($"Error al obtener datos: {ex.Message}");
            return new List<Proveedores>(); // o devuelve null si prefieres
        }
    }

    [HttpPost]
    public ActionResult Editar(int id_proveedor, Proveedores proveedores){
        try{
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //string query = "INSERT INTO provedores (nombre_proveedor, direccion, correo, telefono,estado)  VALUES (@nombre, @direccion, @correo, @telefono, @estado)";

                
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_PROVEEDOR", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_proveedor", id_proveedor);



                    cmd.Parameters.AddWithValue("@nombre", proveedores.nombre_proveedor);
                    cmd.Parameters.AddWithValue("@direccion", proveedores.direccion);
                    cmd.Parameters.AddWithValue("@correo", proveedores.correo);
                    cmd.Parameters.AddWithValue("@telefono", proveedores.telefono);
                    cmd.Parameters.AddWithValue("@estado", proveedores.estado);


                    // Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();

                    TempData["SuccedMensaje"] = "SE HA MODIFICADO EL REGISTRO CORRECTAMENTE...";
                    return RedirectToAction("Details");
                }

            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMensaje1"] = "ERROR!! AL EDITAR EL REGISTRO";
            TempData["ErrorMensaje"] = ex.Message;
        }
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}