using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Invitacion : IValidable
    {
        private int _id;
        private static int s_ultNumero = 1000;
        private Miembro _miembroSolicitante; //Quien envia la solicitud
        private Miembro _miembroReceptor; //Quien recibe la solicitud
        private DateTime _fechaSolicitud;
        private EstadoSolicitud _estadoSolicitud;

        public Invitacion(Miembro solicitante, Miembro receptor)
        {
            _id = s_ultNumero;
            _miembroSolicitante = solicitante;
            _miembroReceptor = receptor;
            _fechaSolicitud = DateTime.Now;
            _estadoSolicitud = EstadoSolicitud.PENDIENTE_APROVACION;
            s_ultNumero++;
        }

        public EstadoSolicitud EstadoDeSolicitud
        {
            get { return _estadoSolicitud;}
        }

        public DateTime FechaSolicitud
        {
            get { return _fechaSolicitud;}
        }

        public int Id
        {
            get { return _id; }
        }

        public Miembro MSolicitante
        {
            get { return _miembroSolicitante; }
        }

        public Miembro MReceptor
        {
            get { return _miembroReceptor; }
        }

        private void ValidarMSolicitante()
        {
            if (_miembroSolicitante == null) throw new Exception("El miembro solicitante no puede ser nulo");
        }

        private void ValidarMReceptor()
        {
            if (_miembroReceptor == null) throw new Exception("El miembro que recibe la invitacion no puede ser nulo");
        }

        public void Validar()
        {
            ValidarMReceptor();
            ValidarMSolicitante();
        }

        public void AceptarSolicitud(Miembro mReceptor)
        {
            if (mReceptor != _miembroReceptor) throw new Exception("El miembro solicitado no es el mismo");
            if (mReceptor.EstaBanneado) throw new Exception("Usted esta banneado no puede aceptar solicitudes");
            if (mReceptor.SonAmigos(_miembroSolicitante)) throw new Exception("Ustedes ya son amigos");
            _estadoSolicitud = EstadoSolicitud.APROVADA;
            _miembroSolicitante.AgregarAmigo(mReceptor);
            mReceptor.AgregarAmigo(_miembroSolicitante);
        }

        public void RechazarSolicitud(Miembro mReceptor) 
        {
            if (mReceptor != _miembroReceptor) throw new Exception("El miembro solicitado no es el mismo");
            if (mReceptor.EstaBanneado) throw new Exception("Usted esta banneado no puede rechazar solicitudes");
            _estadoSolicitud = EstadoSolicitud.RECHAZADA;
        }
    }
}
