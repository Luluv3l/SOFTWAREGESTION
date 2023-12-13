using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;

namespace SoftwareGestion.Controllers;

public class ProductosController : Controller
{

    //string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";

    string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;Integrated Security=True;User ID=.";


    [HttpPost]
    public ActionResult GuardarValor()
    {
        string categoria = Request.Form["categoria"];
        string talla = Request.Form["talla"];


        //Guardar el valor seleccionado en la base de datos
        return RedirectToAction("Create");
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Productos productos)
    {
        try
        {
            string categoria = Request.Form["categoria"];
            string talla = Request.Form["talla"];
            // Configurar la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Configurar el comando y los parámetros
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_PRODUCTO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", productos.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", productos.descripcion);
                    cmd.Parameters.AddWithValue("@id_categoria", categoria);
                    cmd.Parameters.AddWithValue("@id_tallas", talla);
                    cmd.Parameters.AddWithValue("@precio_venta", productos.precio_venta);
                    cmd.Parameters.AddWithValue("@estado", 1);

                    try
                    {
                        // Ejecutar el procedimiento almacenado
                        cmd.ExecuteNonQuery();
                        TempData["SuccedMensaje"] = "SE HA CREADO EL REGISTRO CORRECTAMENTE...";
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMensaje1"] = "ERROR!! AL CREAR EL REGISTRO";
                        TempData["ErrorMensaje"] = ex.Message;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMensaje1"] = "ERROR!! AL CONECTAR CON LA BASE DE DATOS";
            TempData["ErrorMensaje"] = ex.Message;
        }

        return View();
    }



     [HttpPost]
    public ActionResult Editar(int id_producto, Productos productos){
        try{

            string categoria = Request.Form["categoria"];
            string talla = Request.Form["talla"];

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //string query = "INSERT INTO provedores (nombre_proveedor, direccion, correo, telefono,estado)  VALUES (@nombre, @direccion, @correo, @telefono, @estado)";

                
                using (SqlCommand cmd = new SqlCommand("SP_UPDATE_PRODUCTO", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                

                    cmd.Parameters.AddWithValue("@nombre", productos.nombre);
                    cmd.Parameters.AddWithValue("@descripcion", productos.descripcion);
                    cmd.Parameters.AddWithValue("@id_categorias", categoria);
                    cmd.Parameters.AddWithValue("@id_tallas", talla);
                    //cmd.Parameters.AddWithValue("@precio_venta", productos.precio_venta);
                    cmd.Parameters.AddWithValue("@estado", productos.estado);


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



 public IActionResult Details()
    {
        try
        {
            List<Productos> productos = ObtenerInformacionProducto(); // Asegúrate de que este método devuelve datos válidos

            if (productos == null || !productos.Any())
            {
                // Manejar el caso en el que no se obtuvieron datos, puede ser un mensaje de error, redirección, etc.
                return RedirectToAction("Error");
            }

            return View(productos);
        }
        catch (Exception ex)
        {
            // Loguear el error o manejarlo de alguna manera
            Console.WriteLine($"Error en la acción Details: {ex.Message}");
            return RedirectToAction("Error");
        }
    }


private List<Productos> ObtenerInformacionProducto()
    {
        try
        {
            List<Productos> proveedores = new List<Productos>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select PR.id_producto, PR.nombre, PR.descripcion ,  CA.descripcion AS categoria, TA.descripcion AS talla, (CASE  when PR.estado = 1 then 'Activo' else 'Inactivo' END) AS estado from productos PR inner join categorias CA on CA.id_categoria = PR.id_categoria inner join tallas TA on TA.id_tallas = PR.id_tallas";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Productos producto = new Productos
                            {
                                id_producto = Convert.ToInt32(reader["id_producto"]),
                                nombre = reader["nombre"].ToString(),
                                descripcion = reader["descripcion"].ToString(),
                                id_categorias = reader["categoria"].ToString(),
                                id_tallas = reader["talla"].ToString(),
                                //precio_venta = Convert.ToDecimal(reader["precio_venta"]),
                                estado = reader["estado"].ToString(),
                            };

                            proveedores.Add(producto);
                        }
                    }
                }
            }

           return proveedores;
        }

        catch (Exception ex)
        {

            TempData["ErrorMensaje1"] = "ERROR!! AL CONECTAR CON LA BASE DE DATOS";
            TempData["SuccedMensaje"] = ex.Message;
            // Loguear el error o manejarlo de alguna manera
            Console.WriteLine($"Error al obtener datos: {ex.Message}");
            return new List<Productos>(); // o devuelve null si prefieres
            
        }
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}