using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario : Publicacion, IValidable
    {
        private TipoPublicacion _tipoComentario;
        

        public Comentario(string titulo, string texto, Miembro autor, TipoPublicacion tipoComentario) : base(titulo,texto,autor)
        {
            _tipoComentario = tipoComentario;
            

        }

        public Comentario(string titulo, string texto, Miembro autor) : base(titulo, texto, autor)
        {
            _tipoComentario = TipoPublicacion.Publico;
            
        }

       

        private void ValidarReacciones()
        {
            if (_reacciones == null) throw new Exception("No puede haber reacciones nulas");
        }

        public override void Validar()
        {
            base.Validar();
            ValidarReacciones();
        }

        public override void AgregarReaccion(Reaccion r) 
        {
            base.AgregarReaccion(r);
        }

        public override string ToString()
        {
            return $"Comentario- Id: {_id}, Titulo:{_titulo}, Cotenido: {_texto}";
        }

        public override int CalcularValorDeAceptacion()
        {
            return base.CalcularValorDeAceptacion();
        }

        public override string TipoPublicaicon()
        {
            return "Comentario";
        }
    }
}
