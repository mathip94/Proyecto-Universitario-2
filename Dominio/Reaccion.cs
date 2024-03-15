using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Reaccion : IValidable
    {
        private Miembro _miembro;
        private TipoReaccion _reaccion;

        public Reaccion(Miembro miembro, TipoReaccion reaccion)
        {
            _miembro = miembro;
            _reaccion = reaccion;
        }

        public TipoReaccion TipoReaccion
        {
            get { return _reaccion; }
        }

        public Miembro AutorReaccion
        {
            get { return _miembro;}
        }

        private void ValidarMiembro()
        {
            if (_miembro == null) throw new Exception("El miembro no puede ser nulo");
        }

        public void Validar()
        {
            ValidarMiembro();
        }
    }
}
