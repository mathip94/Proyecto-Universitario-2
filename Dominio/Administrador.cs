using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador : Usuario
    {
        public Administrador(string email, string contraseña) : base (email,contraseña)
        {
            
        }

        public override string Tipo()
        {
            return "Administrador";
        }
    }
}
