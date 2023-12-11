using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;
using System.Diagnostics;


namespace SoftwareGestion.Controllers
{
    public class GestionOrdenesController : Controller
    {
        string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";


        public IActionResult MateriaPrima()
        {
            try
            {
                List<MateriaPrima> materias = ObtieneInfoMateriaPrima(); // Asegúrate de que este método devuelve datos válidos

                if (materias == null || !materias.Any())
                {
                    // Manejar el caso en el que no se obtuvieron datos, puede ser un mensaje de error, redirección, etc.
                    return RedirectToAction("Error");
                }

                return View(materias);
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error en la acción Details: {ex.Message}");
                return RedirectToAction("Error");
            }
        }


        public ActionResult Details()
        {
            try
            {
                List<OrdenDetails> ordenes = ObtenerInformacionOrden(); // Asegúrate de que este método devuelve datos válidos

                if (ordenes == null || !ordenes.Any())
                {
                    // Manejar el caso en el que no se obtuvieron datos, puede ser un mensaje de error, redirección, etc.
                    return RedirectToAction("Error");
                }

                return View(ordenes);
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error en la acción Details: {ex.Message}");
                return RedirectToAction("Error");
            }
        }

        private List<OrdenDetails> ObtenerInformacionOrden()
        {
            try
            {
                List<OrdenDetails> ordenes = new List<OrdenDetails>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    

                    using (SqlCommand cmd = new SqlCommand("ObtenerInformacionOrden", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrdenDetails orden = new OrdenDetails
                                {
                                    Orden = Convert.ToInt32(reader["Orden"]),
                                    Cliente = reader["Cliente"].ToString(),
                                    NombreProducto = reader["NombreProducto"].ToString(),
                                    DescripcionProducto = reader["DescripcionProducto"].ToString(),
                                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                                    EstadoOrden = reader["EstadoOrden"].ToString(),
                                };

                                ordenes.Add(orden);
                            }
                        }
                    }
                }

                return ordenes;
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
                return new List<OrdenDetails>(); // o devuelve null si prefieres
            }
        }


        private List<MateriaPrima> ObtieneInfoMateriaPrima()
        {
            try
            {
                List<MateriaPrima> materiaPrimas = new List<MateriaPrima>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT id_materia_prima As Codigo ,nombre AS MateriaPrima,descripcion,costo_unitario AS CostoUnitario ,costo_unitario * disponible AS costo_total FROM materia_prima", connection))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MateriaPrima materia = new MateriaPrima
                                {
                                    codigo = Convert.ToInt32(reader["Codigo"]),
                                    materia_prima = reader["MateriaPrima"].ToString(),
                                    descripcion = reader["descripcion"].ToString(),
                                    costo_unitario = Convert.ToDecimal(reader["CostoUnitario"]),
                                    costo_total = Convert.ToDecimal(reader["costo_total"]),

                                };

                                materiaPrimas.Add(materia);
                            }
                        }
                    }
                }

                return materiaPrimas;
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
                return new List<MateriaPrima>(); // o devuelve null si prefieres
            }
        }



        public IActionResult CostoTotalProducto()
        {
            try
            {
                List<CostosProduccion> costos = ObtieneCostoProduccion(); // Asegúrate de que este método devuelve datos válidos

                if (costos == null || !costos.Any())
                {
                    // Manejar el caso en el que no se obtuvieron datos, puede ser un mensaje de error, redirección, etc.
                    return RedirectToAction("Error");
                }

                return View(costos);
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error en la acción Details: {ex.Message}");
                return RedirectToAction("Error");
            }
        }


        private List<CostosProduccion> ObtieneCostoProduccion()
        {
            try
            {
                List<CostosProduccion> costos = new List<CostosProduccion>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerInformacionPedidos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CostosProduccion costosp = new CostosProduccion
                                {
                                    //codigo = Convert.ToInt32(reader["Codigo"]),
                                    numero_orden = Convert.ToInt32(reader["NumeroOrden"]),
                                    costo_materia_prima = Convert.ToDecimal(reader["CostosMaterialesPorPedido"]),
                                    costo_total_general = Convert.ToDecimal(reader["CostosAsociadosGenerales"]),
                                    costo_total_pedidos = Convert.ToDecimal(reader["CostoTotalPedido"]),

                                };

                                costos.Add(costosp);
                            }
                        }
                    }
                }

                return costos;
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error al obtener datos: {ex.Message}");
                return new List<CostosProduccion>(); // o devuelve null si prefieres
            }
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpGet]
        public JsonResult BusquedaCliente(string busqueda)
        {
            try
            {
                List<Clientes> lista = new List<Clientes>();
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SP_BUSQUEDA_CLIENTE", conn);
                    cmd.Parameters.AddWithValue("@busqueda", busqueda);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Clientes()
                                {
                                    value = Convert.ToInt32(dr["id_cliente"]),
                                    label = dr["Texto"].ToString(),
                                    nombre_cliente = dr["nombre_cliente"].ToString(),
                                }
                            );
                        }
                    }
                }
                return Json(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BusquedaCliente: {ex.Message}");
                return Json(new { error = "Error en la búsqueda de clientes." });
            }
        }


        public JsonResult CalcularPrecioVenta(int id_producto, int Cantidad)
        {
            try
            {
                // Crear una lista para almacenar el resultado
                var result = new Precio();

                // Establecer la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Crear un comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("CalcularPrecioVentaYActualizarDisponibilidad", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros al comando
                        command.Parameters.AddWithValue("@CodigoProducto", id_producto); // Reemplaza con el valor correcto
                        command.Parameters.AddWithValue("@Cantidad", Cantidad); // Reemplaza con el valor correcto

                        // Abrir la conexión
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Asignar el resultado a la clase de modelo
                                result.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]);
                                result.SumaMateria = Convert.ToDecimal(reader["CostoMateriaPrimaporPedido"]);
                                // Nueva propiedad para almacenar mensajes de alerta
                                result.Alerta = Convert.ToInt32(reader["Alerta"]);
                            }
                        }
        

                     
                        return Json(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Loguear el error o manejarlo de alguna manera
                Console.WriteLine($"Error al calcular el precio de venta: {ex.Message}");
                return Json(new { error = "Error al calcular el precio de venta", message = ex.Message });
            }
        }



        [HttpGet]
        public JsonResult BusquedaProducto(string busqueda)
        {
            try
            {
                List<Productos> lista = new List<Productos>();
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SP_BUSQUEDA_PRODUCTO", conn);
                    cmd.Parameters.AddWithValue("@busqueda", busqueda);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                                new Productos()
                                {
                                    value = Convert.ToInt32(dr["id_producto"]),
                                    label = dr["Texto"].ToString(),
                                    nombre = dr["nombre"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                    precio_venta = Convert.ToDecimal(dr["precio_venta"]),
                                }
                            );
                        }
                    }
                }
                return Json(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in BusquedaProducto: {ex.Message}");
                return Json(new { error = "Error en la búsqueda de productos." });
            }
        }
        [HttpPost]
        public ActionResult GuardarOrdenPedido(int idCliente, int codigo,int alerta, int cantidad,decimal sumaMateria,decimal precioVenta)
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Llamada al stored procedure
                    using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_ORDEN_PEDIDO", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Estado", alerta);
                        cmd.Parameters.AddWithValue("@IdProducto", codigo);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@SumaMateria", sumaMateria);
                        cmd.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                        // Ejecutar el stored procedure
                        cmd.ExecuteNonQuery();
                    }
                }

                return Json(new { message = "Orden de pedido guardada con éxito" });
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error al guardar la orden de pedido.", message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}