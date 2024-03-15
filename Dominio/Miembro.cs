using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Miembro : Usuario, IValidable, IComparable<Miembro>
    {
        private string _nombre;
        private string _apellido;
        private DateTime _fechaNacimiento;
        private List<Miembro> _amigos;
        private bool _bloqueado;

        public Miembro(string email, string contraseña, string nombre, string apellido, DateTime fechaNacimiento) : base(email, contraseña)
        {
            _nombre = nombre;
            _apellido = apellido;
            _fechaNacimiento = fechaNacimiento;
            _amigos = new List<Miembro>();
            _bloqueado = false;
        }

        public Miembro() : base()
        {

        }

        public  DateTime FechaNacimiento
        {
            get {  return _fechaNacimiento; }
        }

        public List<Miembro> Amigos
        {
            get { return _amigos; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public string Apellido
        {
            get { return _apellido; }
        }

        public bool EstaBanneado
        {
            get { return _bloqueado; }
            set { _bloqueado = value; }
        }

        public string MetodoEstaBanneado()
        {
            string estado = "no";
            if (_bloqueado)
            {
                estado = "si";
            }
            return estado;
        }

        public bool SonAmigos(Miembro m)
        {
            
            return _amigos.Contains(m);
        }

        public void AgregarAmigo(Miembro m)
        {
            _amigos.Add(m);
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        private void ValidarApellido()
        {
            if (string.IsNullOrEmpty(_apellido)) throw new Exception("El apellido no puede ser vacio");
        }

        private void ValidarFechaNacimiento()
        {
            if (_fechaNacimiento > DateTime.Now || _fechaNacimiento.Year < 1900) throw new Exception("Debe ingresarse una fecha de nacimiento valida");
        }


        public void Validar()
        {
            ValidarNombre();
            ValidarApellido();
            ValidarMail();
            ValidarPass();
            ValidarFechaNacimiento();
        }


        public override string ToString()
        {
            return $"Nombre: {_nombre} {_apellido} - Fecha de Nacimiento: {_fechaNacimiento.ToShortDateString()} - Mail: {_email}"; 
        }

        public override bool Equals(object? obj)
        {
            Miembro m = obj as Miembro;
            return m != null && this.Email.Equals(m.Email);
        }

        public override string Tipo()
        {
            return "Miembro";
        }

        public int CompareTo(Miembro? other)
        {
            int comparacion = this.Apellido.CompareTo(other.Apellido);

            if (comparacion == 0)
            {
                comparacion = this.Nombre.CompareTo(other.Nombre);
            }

            return comparacion;
        }

    }
}
