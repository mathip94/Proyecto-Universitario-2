using Dominio;
using Microsoft.AspNetCore.Mvc;


namespace MVC.Controllers
{
    public class PublicacionesController : Controller
    {
        Sistema sistema = Sistema.Instancia;

        public IActionResult ListaPosts()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("NoAutorizado");
            }
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];

            ViewBag.Posts = sistema.ObtenerListaPosts();
            return View();
        }

        public IActionResult BannearPost(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("NoAutorizado");
            }
            try
            {
                Post p = sistema.ObtenerPostPorId(id);
                sistema.BannearPost(p);
                TempData["Exito"] = "Post banneado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListaPosts");
        }

        public IActionResult DesbannearPost(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Administrador")
            {
                return View("NoAutorizado");
            }
            try
            {
                Post p = sistema.ObtenerPostPorId(id);
                sistema.DesbannearPost(p);
                TempData["Exito"] = "Post desbanneado exitosamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListaPosts");
        }

        public IActionResult ListarPostHabilitados()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"];
            if (TempData["Exito"] != null) ViewBag.Exito = TempData["Exito"];
            string email = HttpContext.Session.GetString("email");
            Miembro m = sistema.ObtenerMiembroPorEmail(email);
            ViewBag.Posts = sistema.ObtenerListaDePostHabilitadosPara(m);
            return View();
        }

        public IActionResult DarLikePost(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            string email = HttpContext.Session.GetString("email");
            Miembro m = sistema.ObtenerMiembroPorEmail(email);
            Reaccion r = new Reaccion(m, TipoReaccion.Like);
            sistema.AgregarReaccionAPost(id, r);
            return RedirectToAction("ListarPostHabilitados");
        }

        public IActionResult DarDislikePost(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            string email = HttpContext.Session.GetString("email");
            Miembro m = sistema.ObtenerMiembroPorEmail(email);
            Reaccion r = new Reaccion(m, TipoReaccion.Dislike);
            sistema.AgregarReaccionAPost(id, r);
            return RedirectToAction("ListarPostHabilitados");
        }

        public IActionResult VerComentarios(int id)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            try
            {
                if (id <= -1) throw new Exception("El id no puede ser negativo");
                Post p = sistema.ObtenerPostPorId(id);
                if (p == null) throw new Exception("Post no encontrado");
                ViewBag.Post = p;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListarPostHabilitados");
            }
            return View();
        }

        public IActionResult DarLikeComentario(int idc, int idp)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
                {
                    return View("NoAutorizado");
                }
            string email = HttpContext.Session.GetString("email");
            Miembro m = sistema.ObtenerMiembroPorEmail(email);
            Reaccion r = new Reaccion(m, TipoReaccion.Like);
            sistema.AgregarReaccionAComentario(idp, idc, r);
            return RedirectToAction("VerComentarios", new { id = idp} );
        }

        public IActionResult DarDislikeComentario(int idc, int idp)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
                {
                    return View("NoAutorizado");
                }
            string email = HttpContext.Session.GetString("email");
            Miembro m = sistema.ObtenerMiembroPorEmail(email);
            Reaccion r = new Reaccion(m, TipoReaccion.Dislike);
            sistema.AgregarReaccionAComentario(idp, idc, r);
            return RedirectToAction("VerComentarios", new { id = idp } );
        }

        [HttpGet]
        public IActionResult CrearPost()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            if (HttpContext.Session.GetString("bloqueado") == "si")
            {
                return View("Banneado");
            }
            return View(new Post());
        }

        [HttpPost]
        public IActionResult ProcesarPost(Post p, IFormFile archivo)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            if (HttpContext.Session.GetString("bloqueado") == "si")
            {
                return View("Banneado");
            }
            try
            {
                string email = HttpContext.Session.GetString("email");
                Miembro m = sistema.ObtenerMiembroPorEmail(email);
                if (m == null) throw new Exception("No se encontró miembro");
                p.Autor = m;

                //-- > LOGICA Para agregar la imagen
                if (archivo == null || archivo.Length == 0) throw new Exception("No se seleccionó archivo");
                string ruta = "wwwroot/images/";

                //Con esto pueden ver el tipo de file
                string tipo = archivo.ContentType;

                string[] arraySplit = archivo.FileName.Split('.');
                string extension = arraySplit[arraySplit.Length - 1];

                string nuevoNombre = $"{p.Id}.{extension}";

                //EN ESTE PUNTO TODAVIA NO SUBO EL ARCHIVO, PREPARO LA RUTA Y AGREGO EL NOMBRE DE LA IMAGEN A MI POST PARA QUE LO VALIDE
                ruta += nuevoNombre;
                p.Imagen = nuevoNombre;

                sistema.CrearPost(p);

                //DOY DE ALTA EL DISCO Y RECIEN DESPUES QUE SALE TODO BIEN, COPIO LA IMAGEN A LA CARPETA IMAGES ASI NO SOBRECARGO ESA CARPETA EN CASO DE QUE ALGUNA VALIDACION FALLE

                // Creamos el stream que nos permite escribir archivos a la ruta dada (incluido el nombre del archivo)
                using var stream = System.IO.File.Create(ruta);
                // Subimos el archivo a la carpeta
                archivo.CopyTo(stream);

                ViewBag.Exito = "Post publicado";
                return View("CrearPost", new Post());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("CrearPost", p);
            }
        }

        public IActionResult Comentar(int id, string titulo, string comentario)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            if (HttpContext.Session.GetString("bloqueado") == "si")
            {
                return View("Banneado");
            }
            try
            {
                string email = HttpContext.Session.GetString("email");
                Miembro m = sistema.ObtenerMiembroPorEmail(email);
                Comentario c = new Comentario(titulo, comentario, m);
                sistema.AgregarComentarioAPost(id, c);
                TempData["Exito"] = "Comentario publicado";
                return RedirectToAction("ListarPostHabilitados");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListarPostHabilitados");
            }
        }

        public IActionResult PublicacionesFiltradas()
        {
            return View();
        }

        public IActionResult FiltrarPublicaciones(string palabra, int aceptacion)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "Miembro")
            {
                return View("NoAutorizado");
            }
            try
            {
                if (string.IsNullOrEmpty(palabra)) throw new Exception("La palabra no puede ser vacio");
                ViewBag.Publicaciones = sistema.ObtenerPublicacionesQueCumplenConAceptacionYPalabra(palabra, aceptacion);
                return View("PublicacionesFiltradas");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("ListarPostHabilitados");
            }

        }


    }


}
