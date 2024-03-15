using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        protected string _email;
        private string _contraseña;

        public Usuario(string email, string contraseña)
        {
            _email = email;
            _contraseña = contraseña;
        }

        public Usuario() { }

        public string Email
        {
            get { return _email; }
        }

        public string Pass
        {
            get { return _contraseña; }
        }

        public void ValidarMail()
        {
            if (string.IsNullOrEmpty(_email)) throw new Exception("El mail no puede ser vacio");
        }
        
        public void ValidarPass()
        {
            if (string.IsNullOrEmpty(_contraseña)) throw new Exception("La contraseña no puede ser vacia");
        }

        public void Validar()
        {
            ValidarMail();
            ValidarPass();
        }

        public abstract string Tipo();
    }
}
