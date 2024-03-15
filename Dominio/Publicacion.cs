using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Publicacion : IValidable
    {
        protected int _id; 
        private static int s_ultNumero = 1;
        protected string _texto;
        protected string _titulo;
        protected DateTime _fechaPublicacion;
        protected Miembro _autor;
        protected List<Reaccion> _reacciones;

        public Publicacion(string titulo,string texto, Miembro autor) 
        {
            _titulo = titulo;
            _texto = texto;
            _fechaPublicacion = DateTime.Now;
            _autor = autor;
            _id = s_ultNumero;
            s_ultNumero++;
            _reacciones = new List<Reaccion>(); 
        }

        public Publicacion(string titulo,string texto, Miembro autor, DateTime fechaPublicacion)
        {
            _titulo = titulo;
            _texto = texto;
            _fechaPublicacion = fechaPublicacion;
            _autor = autor;
            _id = s_ultNumero;
            s_ultNumero++;
            _reacciones = new List<Reaccion>();
        }

        public Publicacion()
        {
            _reacciones = new List<Reaccion>();
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo= value; }
        }

        public Miembro Autor
        {
            get { return _autor; }
            set { _autor = value; }
        }

        public DateTime FechaPublicacion
        {
            get { return _fechaPublicacion;}
            set { _fechaPublicacion = value;}
        }

        public List<Reaccion> ListaReacciones
        {
            get { return _reacciones; }
            set { _reacciones= value; } 
        }

        public virtual void AgregarReaccion(Reaccion r)
        {
            if (r == null) throw new Exception("La reaccion no puede ser vacia");
            r.Validar();
            _reacciones.Add(r);
        }

        private void ValidarTexto()
        {
            if (string.IsNullOrEmpty(_texto)) throw new Exception("El texto no puede ser vacio");
        }

        private void ValidarTitulo()
        {
            if (_titulo.Length < 3 || string.IsNullOrEmpty(_titulo)) throw new Exception("El titulo debe tener al menos 3 caracteres");
        }

        private void ValidarAutor()
        {
            if (_autor == null) throw new Exception("El autor no puede ser nulo");
        }

        public virtual void Validar()
        {
            ValidarTexto();
            ValidarTitulo();    
            ValidarAutor();
        }

        public override string ToString()
        {
            return $"Id: {_id}, Titulo:{_titulo}, Cotenido: {_texto}";
        }

        public int ObtenerCantidadDeLikes()
        {
            int likes = 0;
            foreach (Reaccion r in _reacciones)
            {
                if (r.TipoReaccion == TipoReaccion.Like)
                {
                    likes++;
                }
            }
            return likes;
        }

        public int ObtenerCantidadDeDislikes()
        {
            int dislikes = 0;
            foreach (Reaccion r in _reacciones)
            {
                if (r.TipoReaccion == TipoReaccion.Dislike)
                {
                    dislikes++;
                }
            }
            return dislikes;
        }

        public virtual int CalcularValorDeAceptacion()
        {
            int likes = ObtenerCantidadDeLikes();
            int dislikes = ObtenerCantidadDeDislikes();
            int valorAceptacion = (likes * 5) + (dislikes * -2);
            
            return valorAceptacion;
        }

        public abstract string TipoPublicaicon();
    }
}

