using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Post : Publicacion, IComparable<Post>, IValidable
    {
        private string _imagen;
        private TipoPublicacion _tipoPost;
        private List<Comentario> _comentarios;
        private bool _estaCensurado;
       


        public Post(string texto, Miembro autor, string imagen, string titulo, TipoPublicacion tipoPost) : base(titulo, texto, autor)
        {
            _imagen = imagen;
            _tipoPost = tipoPost;
            _comentarios = new List<Comentario>();
            _estaCensurado = false;
        }

        public Post(string texto, Miembro autor, string imagen, string titulo, DateTime fechaPublicacion, TipoPublicacion tipoPost) : base( titulo, texto, autor, fechaPublicacion)
        {
            _imagen = imagen;
            _tipoPost = tipoPost;
            _comentarios = new List<Comentario>();
            _estaCensurado = false;
        }

        public Post() : base()
        {
            _comentarios = new List<Comentario>();
        }

       

        public List<Comentario> Comentarios
        {
            get { return _comentarios; } 
            set { _comentarios = value; } 
        }

        public bool EstaCensurado
        {
            get { return _estaCensurado; }
            set { _estaCensurado = value; }
        }

        public string Imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }

        public TipoPublicacion TipoDePost
        {
            get { return _tipoPost; }
            set { _tipoPost = value; }
        }

        public List<Comentario> ObtenerComentarioPorMail(string mail)
        {
            List<Comentario> lc = new List<Comentario>();
            
            foreach(Comentario c in _comentarios)
            {
                if (c.Autor.Email == mail)
                {
                    lc.Add(c);
                } 
            }
            return lc;
        }

        public bool UsuarioComento(string mail) //Devuelve true o false dependiendo de si un usuario hizo o no un comentario
        {
            bool comento = false;
            foreach(Comentario c in _comentarios)
            {
                if (c.Autor.Email == mail)
                {
                    comento = true;
                }
            }
            return comento;
        }

        public Comentario ObtenerComentarioPorId(int id)
        {
            Comentario c = null;
            int i = 0;
            while (c == null && i < _comentarios.Count)
            {
                if (_comentarios[i].Id == id)
                {
                    c = _comentarios[i];
                }
                i++;
            }

            if (c == null) throw new Exception("No existe ningun post con ese id");

            return c;

        }

        private void ValidarNombreImagen()
        {
            if (string.IsNullOrEmpty(_imagen)) throw new Exception("El nombre de la imagen no puede ser vacio");
            if (_imagen.Length <= 4 || (_imagen.Substring(_imagen.Length - 4) != ".jpg" && _imagen.Substring(_imagen.Length - 4) != ".png")) throw new Exception("El nombre de la imagen debe terminar en .jpg");
        }

        private void ValidarComentarios()
        {
            if (_comentarios == null) throw new Exception("Los comentarios no pueden ser nulos");
        }

        private void ValidarReacciones()
        {
            if (_reacciones == null) throw new Exception("Las reacciones no pueden ser nulas");
        }
       
        public override void Validar()
        {
            base.Validar();
            ValidarNombreImagen();
            ValidarComentarios();
            ValidarReacciones();
        }

        public override void AgregarReaccion(Reaccion r)
        {
            base.AgregarReaccion(r);
        }

        public void AgregarComentarioAPost(Comentario c)
        {
            if (c == null) throw new Exception("El comentario no puede ser vacio");
            c.Validar();
            _comentarios.Add(c); 
        }
        

        public override string ToString()
        {
            return $"Post- Fecha de Publicacion: {_fechaPublicacion.ToShortDateString()} Id: {_id}, Titulo:{_titulo}, Contenido: {_texto.Substring(0,Math.Min(50, _texto.Length))}"; //El substring va hasta 50 o el largo del texto que se le pasa cualquiera de los valores que sea el menor
        }

        public int CompareTo(Post? other)
        {
            return _titulo.CompareTo(other._titulo) * -1;
        }

        public override int CalcularValorDeAceptacion()
        {
            int valorAceptacion = base.CalcularValorDeAceptacion();
            if(_tipoPost == TipoPublicacion.Publico)
            {
                valorAceptacion += 10;
            }
            return valorAceptacion;
        }

        public List<Comentario> ObtenerComentariosQueCumplenConAceptacionYPalabra(string palabra, int aceptacion)
        {
            List<Comentario> comentarios = new List<Comentario>();
            foreach(Comentario c in _comentarios)
            {
                if(c.Texto.Contains(palabra) && c.CalcularValorDeAceptacion() > aceptacion) //Aca tambien hay que cambiarlo es que contenga la palabra no que sea igual
                {
                    comentarios.Add(c);
                }
            }
            return comentarios;
        }

        public override string TipoPublicaicon()
        {
            return "Post";
        }
    }
}
