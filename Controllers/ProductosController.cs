using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;

namespace SoftwareGestion.Controllers;

public class ProductosController : Controller
{

    string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";


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