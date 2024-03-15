using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        #region Singleton
        private static Sistema _instancia;

        private Sistema()
        {
            //Precargas
            PrecargarMiembros();
            PrecargaAdministradores();
            PrecargaPosts();
            PrecargaComentarios();
            PrecargarInvitacionesAMiembros();
            PrecargaReacciones();
            PrecargaCambioEstadoInvitacion();

        }

        public static Sistema Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new Sistema();
                return _instancia;
            }
        }
        #endregion

        private List<Post> _posts = new List<Post>();
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Invitacion> _invitaciones = new List<Invitacion>();


        public List<Post> Posts
        {
            get { return _posts; }
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
        }

        public void AltaMiembros(Miembro m)
        {
            if (m == null) throw new Exception("El objeto miembro no puede ser nulo");
            foreach (Usuario u in _usuarios)
            {
                if (m.Equals(u))
                {
                    throw new Exception("Ya existe un usuario con ese mail");
                }
            }
            m.Validar();
            _usuarios.Add(m);
        }

        private void PrecargarMiembros()
        {
            AltaMiembros(new Miembro("abc@gmail.com", "1234", "Daniel", "Diaz", new DateTime(1999, 10, 30)));
            AltaMiembros(new Miembro("cbd@gmail.com", "4321", "Carlos", "Dominguez", new DateTime(1978, 5, 24)));
            AltaMiembros(new Miembro("iuqp@gmail.com", "7121", "Esteban", "Gomez", new DateTime(2000, 7, 12)));
            AltaMiembros(new Miembro("poqi@gmail.com", "qo145", "Sara", "Martinez", new DateTime(1999, 2, 1)));
            AltaMiembros(new Miembro("loiz@gmail.com", "po190", "Luisa", "Lane", new DateTime(1950, 3, 15)));
            AltaMiembros(new Miembro("qizxo@gmail.com", "9oq9p", "Gabriel", "Nuñez", new DateTime(2002, 7, 18)));
            AltaMiembros(new Miembro("qezz@gmail.com", "9991q", "Marcos", "Damiani", new DateTime(1981, 12, 9)));
            AltaMiembros(new Miembro("vopqpp@gmail.com", "lll123", "Damian", "Suarez", new DateTime(1967, 4, 4)));
            AltaMiembros(new Miembro("roqiz@gmail.com", "jo2iqruw", "Iñaki", "Godoy", new DateTime(1982, 8, 22)));
            AltaMiembros(new Miembro("poquxm@gmail.com", "19zfnmkoj", "Marcia", "Cuestas", new DateTime(1996, 9, 25)));
        }

        private void AltaAdministradores(Administrador a)
        {
            if (a == null) throw new Exception("El objeto administrador no puede ser nulo");
            a.Validar();
            _usuarios.Add(a);
        }

        private void PrecargaAdministradores()
        {
            AltaAdministradores(new Administrador("mat12@gmail.com", "4567"));
        }

        public void AgregarInvitacion(Invitacion i)
        {
            if (i == null) throw new Exception("La invitacion no puede ser nula");
            if (i.MSolicitante.EstaBanneado) throw new Exception("No puede enviar invitaciones, estas bannead");
            i.Validar();
            _invitaciones.Add(i);
        }

        private void PrecargarInvitacionesAMiembros() //Agregar mas invitaciones pendientes
        {
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("cbd@gmail.com"))); //abc tiene mas amigos que roberto carlos probar con otro el buscador de amigos
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("iuqp@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("poqi@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("loiz@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("qizxo@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("qezz@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("vopqpp@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("roqiz@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("abc@gmail.com"), ObtenerMiembroPorEmail("poquxm@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("poquxm@gmail.com"))); //cbd idem abc
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("roqiz@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("vopqpp@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("qezz@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("qizxo@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("loiz@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("poqi@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("cbd@gmail.com"), ObtenerMiembroPorEmail("iuqp@gmail.com")));
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("qezz@gmail.com"), ObtenerMiembroPorEmail("iuqp@gmail.com"))); //Invitacion rechazada
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("iuqp@gmail.com"), ObtenerMiembroPorEmail("loiz@gmail.com"))); //Invitacion pendiente
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("poqi@gmail.com"), ObtenerMiembroPorEmail("iuqp@gmail.com"))); //Invitacion pendiente
            AgregarInvitacion(new Invitacion(ObtenerMiembroPorEmail("vopqpp@gmail.com"), ObtenerMiembroPorEmail("iuqp@gmail.com"))); //Invitacion pendiente, iuqp@gmail.com tiene 2 pendientes para probar aceptar y rechazar
        }

        private void PrecargaCambioEstadoInvitacion() //Dejarle un comentario a santi en cuales estan pendientes
        {
            AceptarInvitacion(1000, ObtenerMiembroPorEmail("cbd@gmail.com"));
            AceptarInvitacion(1001, ObtenerMiembroPorEmail("iuqp@gmail.com"));
            AceptarInvitacion(1002, ObtenerMiembroPorEmail("poqi@gmail.com"));
            AceptarInvitacion(1003, ObtenerMiembroPorEmail("loiz@gmail.com"));
            AceptarInvitacion(1004, ObtenerMiembroPorEmail("qizxo@gmail.com"));
            AceptarInvitacion(1005, ObtenerMiembroPorEmail("qezz@gmail.com"));
            AceptarInvitacion(1006, ObtenerMiembroPorEmail("vopqpp@gmail.com"));
            AceptarInvitacion(1007, ObtenerMiembroPorEmail("roqiz@gmail.com"));
            AceptarInvitacion(1008, ObtenerMiembroPorEmail("poquxm@gmail.com"));
            AceptarInvitacion(1009, ObtenerMiembroPorEmail("poquxm@gmail.com"));
            AceptarInvitacion(1010, ObtenerMiembroPorEmail("roqiz@gmail.com"));
            AceptarInvitacion(1011, ObtenerMiembroPorEmail("vopqpp@gmail.com"));
            AceptarInvitacion(1012, ObtenerMiembroPorEmail("qezz@gmail.com"));
            AceptarInvitacion(1013, ObtenerMiembroPorEmail("qizxo@gmail.com"));
            AceptarInvitacion(1014, ObtenerMiembroPorEmail("loiz@gmail.com"));
            AceptarInvitacion(1015, ObtenerMiembroPorEmail("poqi@gmail.com"));
            AceptarInvitacion(1016, ObtenerMiembroPorEmail("iuqp@gmail.com"));
            RechazarInvitacion(1017, ObtenerMiembroPorEmail("iuqp@gmail.com")); //Invitacion Rechazada
        }

        public void CrearPost(Post p)
        {
            if (p == null) throw new Exception("El objeto post no puede ser nulo");
            p.Validar();
            if (p.Autor.EstaBanneado) throw new Exception("No puedes crear posts, estas banneado.");
            _posts.Add(p);
        }

        private void PrecargaPosts()
        {
            CrearPost(new Post("Este es mi primer post en esta red!", ObtenerMiembroPorEmail("loiz@gmail.com"), "10.jpg", "Mi primer post", new DateTime(2016, 12, 2), TipoPublicacion.Publico));
            CrearPost(new Post("Hace poco tiempo descubri esta red y sinceramente pienso que podria ser el proximo facebook!", ObtenerMiembroPorEmail("abc@gmail.com"), "11.jpg", "Mi opinion sobre esta red", new DateTime(2018, 7, 20), TipoPublicacion.Publico));
            CrearPost(new Post("Quien diria que facebook tendria competencia!", ObtenerMiembroPorEmail("poqi@gmail.com"), "12.jpg", "La competencia de Facebook", new DateTime(2020, 10, 30), TipoPublicacion.Publico));
            CrearPost(new Post("Este fin de semana toca ir a la playa", ObtenerMiembroPorEmail("qizxo@gmail.com"), "13.jpg", "Finde de playa", new DateTime(2022, 4, 15), TipoPublicacion.Publico));
            CrearPost(new Post("Estoy muy anciosa por mi viaje a España el mes que viene", ObtenerMiembroPorEmail("poquxm@gmail.com"), "14.jpg", "Viaje a España", new DateTime(2023, 8, 19), TipoPublicacion.Publico));
        }

        private void PrecargaComentarios()
        {
            AgregarComentarioAPost(1, new Comentario(ObtenerTituloPost(1), "Que casualidad este tambien es mi primer comentario en esta red", ObtenerMiembroPorEmail("poquxm@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(1, new Comentario(ObtenerTituloPost(1), "Yo llegue hace unos meses y me parece fantastica!", ObtenerMiembroPorEmail("qizxo@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(1, new Comentario(ObtenerTituloPost(1), "Veremos si llego a durar un mes yo", ObtenerMiembroPorEmail("poquxm@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(2, new Comentario(ObtenerTituloPost(2), "Asi que tambien se pueden comentar los posts", ObtenerMiembroPorEmail("abc@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(2, new Comentario(ObtenerTituloPost(2), "No", ObtenerMiembroPorEmail("poqi@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(2, new Comentario(ObtenerTituloPost(2), "xD", ObtenerMiembroPorEmail("abc@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(3, new Comentario(ObtenerTituloPost(3), "Probando 1 2 3", ObtenerMiembroPorEmail("qizxo@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(3, new Comentario(ObtenerTituloPost(3), "Esto no es un microfono(?", ObtenerMiembroPorEmail("poqi@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(3, new Comentario(ObtenerTituloPost(3), "Hasta donde sabemos...", ObtenerMiembroPorEmail("cbd@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(4, new Comentario(ObtenerTituloPost(4), "Espero que pases muy bien en la playa", ObtenerMiembroPorEmail("cbd@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(4, new Comentario(ObtenerTituloPost(4), "Sube las fotos luego!", ObtenerMiembroPorEmail("iuqp@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(4, new Comentario(ObtenerTituloPost(4), "Y no invitan :'(", ObtenerMiembroPorEmail("vopqpp@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(5, new Comentario(ObtenerTituloPost(5), "Suerte en tu viaje!", ObtenerMiembroPorEmail("vopqpp@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(5, new Comentario(ObtenerTituloPost(5), "Saca muchas fotos!", ObtenerMiembroPorEmail("roqiz@gmail.com"), TipoPublicacion.Publico));
            AgregarComentarioAPost(5, new Comentario(ObtenerTituloPost(5), "Probando 1 2 3", ObtenerMiembroPorEmail("qizxo@gmail.com"), TipoPublicacion.Publico));
        }

        public void AgregarComentarioAPost(int id, Comentario c)
        {
            try
            {
                if (c == null) throw new Exception("El comentario no puede ser nulo");
                if (c.Autor.EstaBanneado) throw new Exception("No puedes realizar comentarios, estas banneado");
                ObtenerPostPorId(id).AgregarComentarioAPost(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ObtenerTituloPost(int id)
        {
            return ObtenerPostPorId(id).Titulo;
        }

        private void PrecargaReacciones()
        {
            AgregarReaccionAPost(1, new Reaccion(ObtenerMiembroPorEmail("abc@gmail.com"), TipoReaccion.Like));
            AgregarReaccionAPost(1, new Reaccion(ObtenerMiembroPorEmail("qizxo@gmail.com"), TipoReaccion.Dislike));
            AgregarReaccionAPost(3, new Reaccion(ObtenerMiembroPorEmail("poqi@gmail.com"), TipoReaccion.Like));
            AgregarReaccionAPost(3, new Reaccion(ObtenerMiembroPorEmail("roqiz@gmail.com"), TipoReaccion.Like));
            AgregarReaccionAComentario(2, 11, new Reaccion(ObtenerMiembroPorEmail("poqi@gmail.com"), TipoReaccion.Like));
            AgregarReaccionAComentario(4, 17, new Reaccion(ObtenerMiembroPorEmail("abc@gmail.com"), TipoReaccion.Dislike));

        }

        public void AgregarReaccionAPost(int id, Reaccion r) 
        {
            try
            {
                if (r == null) throw new Exception("La reaccion no puede ser nula");
                
                Post p = ObtenerPostPorId(id);
                bool existeReaccionDelMismoAutor = false;

                foreach (Reaccion re in p.ListaReacciones)
                {
                    if (re.AutorReaccion == r.AutorReaccion)
                    {
                        existeReaccionDelMismoAutor = true;
                        break;
                    }
                }
                if (!existeReaccionDelMismoAutor)
                {
                    p.AgregarReaccion(r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AgregarReaccionAComentario(int idpost, int idcomentario, Reaccion r) 
        {
            try
            {
                if (r == null) throw new Exception("La reaccion no puede ser nula");
                
                Comentario c = ObtenerPostPorId(idpost).ObtenerComentarioPorId(idcomentario);
                bool existeReaccionDelMismoAutor = false;

                foreach(Reaccion re in c.ListaReacciones)
                {
                    if (re.AutorReaccion == r.AutorReaccion)
                    {
                        existeReaccionDelMismoAutor |= true;
                        break;
                    }
                }
                if (!existeReaccionDelMismoAutor)
                {
                    c.AgregarReaccion(r);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Miembro ObtenerMiembroPorEmail(string mail)
        {
            Miembro m = null;
            int i = 0;
            List<Miembro> listaMiembros = ObtenerListaMiembros();
            while (m == null && i < listaMiembros.Count)
            {
                if (listaMiembros[i].Email == mail)
                {
                    m = listaMiembros[i];
                }
                i++;
            }

            if (m == null) throw new Exception("No existe ningun miembro con ese mail");

            return m;

        }

        public List<Miembro> ObtenerListaMiembros() //Lista completa de miembros
        {
            List<Miembro> lm = new List<Miembro>();
            List<Usuario> lu = _usuarios;


            foreach (Usuario u in lu)
            {
                if (u is Miembro)
                {
                    lm.Add(u as Miembro);
                }
            }
            lm.Sort();
            return lm;
        }

        private bool MandoSolicitud(Miembro MSolicitante, Miembro MReceptor) //Metodo auxiliar para  ObtenerListaMiembrosNoAmigosDe()
        {
            bool mandoSolicitud = false;
            
            foreach (Invitacion i  in _invitaciones)
            {
                if (i.MSolicitante.Equals(MSolicitante) && i.MReceptor.Equals(MReceptor))
                {
                    mandoSolicitud = true;
                }
                if(i.MSolicitante.Equals(MReceptor) && i.MReceptor.Equals(MSolicitante))
                {
                    mandoSolicitud = true;
                }
            }
            return mandoSolicitud;
        }

        public List<Miembro> ObtenerListaMiembrosNoAmigosDe(string email)
        {
            List<Miembro> lm = new List<Miembro>();
            List<Usuario> lu = _usuarios;
            Miembro mBuscandoAmigos = ObtenerMiembroPorEmail(email);

            foreach (Usuario u in lu)
            {
                if (u is Miembro)
                {
                    Miembro m = u as Miembro;
                    
                    if (!m.SonAmigos(mBuscandoAmigos) && !m.Equals(mBuscandoAmigos) && !MandoSolicitud(m,mBuscandoAmigos)) //Chequea que no tengan una solicitud entre ellos asi cuando se envian solicitudes desaparecen de la lista de posibles amigos
                    {
                        lm.Add(m);
                    }
                }
            }
            return lm;
        }

        public Post ObtenerPostPorId(int id)
        {
            Post p = null;
            int i = 0;
            while (p == null && i < _posts.Count)
            {
                if (_posts[i].Id == id)
                {
                    p = _posts[i];
                }
                i++;
            }

            if (p == null) throw new Exception("No existe ningun post con ese id");

            return p;
        }

        public List<Publicacion> ObtenerPublicacionesPorAutor(string mail)
        {
            List<Publicacion> publicaciones = new List<Publicacion>();
            foreach (Post p in _posts)
            {
                if (p.Autor.Email == mail)
                {
                    publicaciones.Add(p);
                }

                publicaciones.AddRange(p.ObtenerComentarioPorMail(mail));
            }

            return publicaciones;
        }

        public List<Post> ObtenerPostEnLosQueComentaUsuario(string mail)
        {
            List<Post> posts = new List<Post>();
            foreach (Post p in _posts)
            {
                if (p.UsuarioComento(mail))
                {
                    posts.Add(p);
                }
            }
            return posts;
        }

        public List<Post> ObtenerPostEntreFechasOrdenDescendente(DateTime fecha1, DateTime fecha2) //Fecha 1 es la fecha inicial de la busqueda y Fecha 2 la fecha final
        {
            List<Post> posts = new List<Post>();
            if (fecha1 > fecha2) throw new Exception("La primera fecha debe ser la fecha de inicio de la busqueda");
            if (fecha2 > DateTime.Now) throw new Exception("Solo se pueden buscar los posts realizados hasta hoy");
            foreach (Post p in _posts)
            {
                if (p.FechaPublicacion.Date >= fecha1.Date && p.FechaPublicacion.Date <= fecha2.Date)
                {
                    posts.Add(p);
                }
            }
            posts.Sort();
            return posts;
        }

        private List<Publicacion> ObtenerListaDePublicaciones() //Todas las publicaciones posts y comentarios
        {
            List<Publicacion> publicaciones = new List<Publicacion>();
            foreach (Post p in _posts)
            {
                publicaciones.Add(p);
                publicaciones.AddRange(p.Comentarios);
            }
            return publicaciones;
        }

        public List<Post> ObtenerListaPosts()
        {
            return _posts;
        }

        private int ContarPublicacionesDeAutor(List<Publicacion> publicaciones, Miembro autor)
        {
            int contador = 0;
            foreach (Publicacion p in publicaciones)
            {
                if (p.Autor == autor)
                {
                    contador++;
                }
            }
            return contador;
        }

        public List<Miembro> ObtenerListaMiembrosConMasPublicaciones()
        {
            List<Miembro> miembrosConMasPublicaciones = new List<Miembro>();
            List<Publicacion> listaPublicaciones = ObtenerListaDePublicaciones();
            int cantidadMaximaPublicaciones = 0;

            foreach (Publicacion p in listaPublicaciones)
            {
                Miembro autor = p.Autor;
                int publicacionesDeAutor = ContarPublicacionesDeAutor(listaPublicaciones, autor);

                    if (publicacionesDeAutor > cantidadMaximaPublicaciones)
                    {
                        cantidadMaximaPublicaciones = publicacionesDeAutor;
                        miembrosConMasPublicaciones.Clear();
                        miembrosConMasPublicaciones.Add(autor);
                    }
                    else if (publicacionesDeAutor == cantidadMaximaPublicaciones && !miembrosConMasPublicaciones.Contains(autor))
                    {
                        miembrosConMasPublicaciones.Add(autor);
                    }
            }

            return miembrosConMasPublicaciones;
        }

        public void AceptarInvitacion(int id, Miembro mReceptor)
        {
            Invitacion inv = null;
            int i = 0;
            while (inv == null && i < _invitaciones.Count)
            {
                if (_invitaciones[i].Id == id)
                {
                    inv = _invitaciones[i];
                }
                i++;
            }
            if (inv == null) throw new Exception("No se encontro una invitacion con ese id");
            inv.AceptarSolicitud(mReceptor);
        }

        public void RechazarInvitacion(int id, Miembro mReceptor)
        {
            Invitacion inv = null;
            int i = 0;
            while (inv == null && i < _invitaciones.Count)
            {
                if (_invitaciones[i].Id == id)
                {
                    inv = _invitaciones[i];
                }
                i++;
            }
            if (inv == null) throw new Exception("No se encontro una invitacion con ese id");
            inv.RechazarSolicitud(mReceptor);
        }

        public Usuario Login(string email, string pass)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass)) throw new Exception("Datos vacios");
            Usuario u = null;
            int i = 0;
            while (u == null && i < _usuarios.Count)
            {
                if (_usuarios[i].Email == email && _usuarios[i].Pass == pass) u = _usuarios[i];
                i++;
            }
            return u;
        }

        public void BannearMiembro(Miembro m)
        {
            m.EstaBanneado = true;
        }

        public void DesbannearMiembro(Miembro m)
        {
            m.EstaBanneado = false;
        }

        public void BannearPost(Post p)
        {
            p.EstaCensurado = true;
        }

        public void DesbannearPost(Post p)
        {
            p.EstaCensurado = false;
        }

        public List<Invitacion> ObtenerListaInvitacionesPendientesPorUsuario(Miembro m)
        {
            List<Invitacion> invitaciones = new List<Invitacion>();
            foreach (Invitacion i in _invitaciones)
            {
                if(i.EstadoDeSolicitud == EstadoSolicitud.PENDIENTE_APROVACION && i.MReceptor == m)
                {
                    invitaciones.Add(i);
                }  
            }
            return invitaciones;
        }

        public List<Post> ObtenerListaDePostHabilitadosPara(Miembro m)
        {
            List<Post> postsHabilitados = new List<Post>();
            foreach (Post p in _posts)
            {
                if(p.EstaCensurado == false)
                {
                    if (p.TipoDePost == TipoPublicacion.Publico || p.Autor.Email == m.Email || p.Autor.SonAmigos(m)) 
                    {
                        postsHabilitados.Add(p);
                    }
                }
            }
            return postsHabilitados;
        }

        public List<Publicacion> ObtenerPublicacionesQueCumplenConAceptacionYPalabra(string palabra, int aceptacion)
        {
            List<Publicacion> publicaciones = new List<Publicacion>();
            foreach (Post p in _posts)
            {
                if(p.Texto.Contains(palabra) && p.CalcularValorDeAceptacion() > aceptacion)
                {
                    publicaciones.Add(p);
                }
                publicaciones.AddRange(p.ObtenerComentariosQueCumplenConAceptacionYPalabra(palabra, aceptacion));
            }

            return publicaciones;
        }

    }
}

