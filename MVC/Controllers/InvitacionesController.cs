using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class InvitacionesController : Controller
    {

        Sistema sistema = Sistema.Instancia;


        public IActionResult ListarSolicitudesAmistad()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            string email = HttpContext.Session.GetString("email");
            Miembro m = sistema.ObtenerMiembroPorEmail(email);
            ViewBag.Solicitudes = sistema.ObtenerListaInvitacionesPendientesPorUsuario(m);
            return View();
        }

        public IActionResult AceptarSolicitud(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            try
            {
                string email = HttpContext.Session.GetString("email");
                Miembro m = sistema.ObtenerMiembroPorEmail(email);
                sistema.AceptarInvitacion(id, m);
                TempData["Exito"] = "Solicitud aceptada";
                return RedirectToAction("ListarSolicitudesAmistad");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListarSolicitudesAmistad");
            }
        }

        public IActionResult RechazarSolicitud(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            try
            {
                string email = HttpContext.Session.GetString("email");
                Miembro m = sistema.ObtenerMiembroPorEmail(email);
                sistema.RechazarInvitacion(id, m);
                TempData["Exito"] = "Solicitud rechazada";
                return RedirectToAction("ListarSolicitudesAmistad");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListarSolicitudesAmistad");
            }
        }

    }
}
