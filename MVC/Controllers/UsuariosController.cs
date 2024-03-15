using Microsoft.AspNetCore.Mvc;
using Dominio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Controllers
{
    public class UsuariosController : Controller
    {

        Sistema sistema = Sistema.Instancia;

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProcesarLogin(string email, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
                {
                    throw new Exception("Datos vacios");
                }

                Usuario u = sistema.Login(email, pass);
                if (u == null) throw new Exception("Email o contraseña incorrectos");

                HttpContext.Session.SetString("email", u.Email);
                HttpContext.Session.SetString("rol", u.Tipo());
                if (HttpContext.Session.GetString("rol") == "Miembro") //chequearlo tambien en sistema
                {
                    Miembro m = u as Miembro;
                    HttpContext.Session.SetString("bloqueado", m.MetodoEstaBanneado());
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message; 
                return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("rol") == null)
            {
                return View("NoAutorizado");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult RegistroUsuario()
        {
            return View();
        }

        public IActionResult ProcesarRegistroUsuario(string? email, string? pass, string nombre, string apellido, DateTime fechaNacimiento)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
                {
                    throw new Exception("Valores nulos, revise la info");
                }

                Miembro m = new Miembro(email, pass, nombre, apellido, fechaNacimiento);
                sistema.AltaMiembros(m);
                ViewBag.Exito = "Miembro dado de alta correctamente";
                return View("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Nombre = nombre;
                ViewBag.Apellido = apellido;
                ViewBag.Email = email;
                ViewBag.Pass = pass;
                return View("RegistroUsuario");
            }

            
        }

        public IActionResult ListaMiembros()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("NoAutorizado");
            }
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];

            ViewBag.Miembros = sistema.ObtenerListaMiembros();
            return View();
        }

        public IActionResult BannearMiembro(string id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("NoAutorizado");
            }
            try
            {
                Miembro miembro = sistema.ObtenerMiembroPorEmail(id);
                sistema.BannearMiembro(miembro);
                TempData["Exito"] = "Miembro banneado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListaMiembros");
        }

        public IActionResult DesbannearMiembro(string id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("NoAutorizado");
            }
            try
            {
                Miembro miembro = sistema.ObtenerMiembroPorEmail(id);
                sistema.DesbannearMiembro(miembro);
                TempData["Exito"] = "Miembro desbanneado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListaMiembros");
        }
        

        public IActionResult ListaMiembrosNoAmigos()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            string email = HttpContext.Session.GetString("email");
            ViewBag.Miembros = sistema.ObtenerListaMiembrosNoAmigosDe(email);
            return View();
        }

        public IActionResult EnviarSolicitudAmistad(string emailReceptor) //Consultar si deberia estar aca o no 
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            try
            {
                string emailSolicitante = HttpContext.Session.GetString("email");
                Invitacion i = new Invitacion(sistema.ObtenerMiembroPorEmail(emailSolicitante),sistema.ObtenerMiembroPorEmail(emailReceptor));
                sistema.AgregarInvitacion(i);
                TempData["Exito"] = "Invitacion enviada exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListaMiembrosNoAmigos");
        }

        
    }
}
