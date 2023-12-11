using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SoftwareGestion.Models;


namespace SoftwareGestion.Controllers;

public class HomeController : Controller
{
    string connectionString = "Data Source=localhost;Initial Catalog=BD_SOFTWAREG;User ID=SA;Password=didpah19";
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Usuarios usuarios)
    {
        usuarios.contrasena = ConvertirSha256(usuarios.contrasena);
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_VALIDAR_USUARIO", conn);
            cmd.Parameters.AddWithValue("@correo", usuarios.correo);
            cmd.Parameters.AddWithValue("@contrasena", usuarios.contrasena);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            usuarios.id_usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        }

        Usuarios usuarios1 = new Usuarios();

        if (usuarios.id_usuario != 0)
        {
            usuarios1.id_usuario = usuarios.id_usuario;
            return View("Index", usuarios1);
        }
        else
        {
            ViewData["Mensaje"] = "Usuario no encontrado";
            ViewBag.UsuarioNoEncontrado = true; // Agregar una variable indicando que el usuario no fue encontrado
            return View();
        }


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


    public IActionResult Privacy()
    {
        return View();
    }

    public ActionResult cerrarsesion()
    {

        HttpContext.Session.Remove("usuario");

        return RedirectToAction("Index", "sistema");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
