using Dominio;

namespace Consola
{
    internal class Program
    {
        private static Sistema sistema = Sistema.Instancia;

        static void Main(string[] args)
        {
            int opcion = int.MinValue;
            do
            {
                MostrarTitulo("Menu de opciones");
                MostrarMenu();
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        AltaMiembro();
                        break;
                    case 2:
                        ListarPublicacionesDeUsuario();
                        break;
                    case 3:
                        ListarPostEnLosQueComentoUsuario();
                        break;
                    case 4:
                        ListarPostEntreFechas();
                        break;
                    case 5:
                        MostrarMiembrosConMayorCantidadPublicaciones();
                        break;
                    case 0:
                        MostrarMensaje("Saliendo...");
                        break;
                    default:
                        MostrarError("Debe seleccionar una opcion valida");
                        break;
                }

            } while (opcion != 0);
        }

        static void MostrarMenu()
        {
            MostrarMensaje("Ingrese una opcion");
            MostrarMensaje("1 - Dar miembro de alta");
            MostrarMensaje("2 - Mostrar todas las publicaciones de usuario");
            MostrarMensaje("3 - Listar todos los post en los que un usuario haya hecho un comentario");
            MostrarMensaje("4 - Listar todos los post realizados entre fechas");
            MostrarMensaje("5 - Obtener el/los miembros con mayor cantidad de publicaciones");
        }

        static void MostrarTitulo(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine($"**** {titulo}  ****");
            Console.WriteLine("--------------------");
            Console.WriteLine();
        }

        static void MostrarSeparador()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------");
            Console.WriteLine();
        }

        static void MostrarError(string error)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"**** {error}  ****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static void MostrarExito(string exito)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"**** {exito}  ****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        static void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        static string PedirPalabras(string mensaje)
        {
            MostrarMensaje(mensaje);
            return Console.ReadLine();
        }

        static int PedirNumeros(string mensaje)
        {
            int numero;
            bool exito = false;
            do
            {
                MostrarMensaje(mensaje);
                exito = int.TryParse(Console.ReadLine(), out numero);
                if (!exito)
                {
                    MostrarError("Debe ingresar solo numeros");
                }
            } while (!exito);

            return numero;
        }

        static void AltaMiembro()
        {
            Console.Clear();
            Console.WriteLine("Alta miembro");
            Console.WriteLine();

            string mail = PedirPalabras("Ingrese un mail");
            string contraseña = PedirPalabras("Ingrese una contraseña");
            string nombre = PedirPalabras("Ingrese su nombre");
            string apellido = PedirPalabras("Ingrese su apellido");
            string fechaNacimiento = PedirPalabras("Ingrese su fecha de nacimiento en formato x/x/xxxx");
            bool siFechaNacimientoConvertida = DateTime.TryParse(fechaNacimiento, out DateTime fechaNacimientoConvertida);
            
            try
            {
                Miembro m = new Miembro(mail, contraseña, nombre, apellido, fechaNacimientoConvertida);
                sistema.AltaMiembros(m);
                MostrarExito("Miembro dado de alta");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void ListarPublicacionesDeUsuario()
        {
            Console.Clear();
            Console.WriteLine("Listar Publicaciones");
            Console.WriteLine();
            string mail = PedirPalabras("Ingrese el mail del usuario que busca");
            Console.WriteLine();
            try
            {
                List<Publicacion> listaPublicaciones = sistema.ObtenerPublicacionesPorAutor(mail);
                if (listaPublicaciones.Count == 0) throw new Exception("No existen publicaciones de ese miembro");
                foreach (Publicacion p in listaPublicaciones)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
                
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

        }

        static void ListarPostEnLosQueComentoUsuario()
        {
            Console.Clear();
            Console.WriteLine("Listar Post en los que comento un usuario");
            Console.WriteLine();

            string mail = PedirPalabras("Ingrese el mail del usuario que busca");
            Console.WriteLine();
            try
            {
                List<Post> listaPosts = sistema.ObtenerPostEnLosQueComentaUsuario(mail);
                if (listaPosts.Count == 0) throw new Exception("No existen post en los que haya comentado ese miembro");
                foreach (Post p in listaPosts)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void ListarPostEntreFechas()
        {
            Console.Clear();
            Console.WriteLine("Listar posts realizados entre 2 fechas");
            Console.WriteLine();

            string fecha1 = PedirPalabras("Ingrese la primera fecha en formato x/x/xxxx");
            string fecha2 = PedirPalabras("Ingrese la ultima fecha en formato x/x/xxxx");
            bool siFechaNacimientoConvertida1 = DateTime.TryParse(fecha1, out DateTime fecha1Convertida);
            bool siFechaNacimientoConvertida2 = DateTime.TryParse(fecha2, out DateTime fecha2Convertida);
            try
            {
                List<Post> listaPosts = sistema.ObtenerPostEntreFechasOrdenDescendente(fecha1Convertida, fecha2Convertida);
                if (listaPosts.Count == 0) throw new Exception("No existen post realizados entre esas fechas");
                foreach(Post p in listaPosts)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        static void MostrarMiembrosConMayorCantidadPublicaciones()
        {
            Console.Clear();
            Console.WriteLine("Ver miembros con mas publicaciones");
            Console.WriteLine();

            try
            {
                List<Miembro> listaMiembros = sistema.ObtenerListaMiembrosConMasPublicaciones();
                if (listaMiembros.Count == 0) throw new Exception("No hay publicaciones");
                if (listaMiembros.Count == 1)
                {
                    Console.WriteLine("El miembro con mas publicaciones es:");
                }
                else
                {
                    Console.WriteLine("Los miembros con mas publicaciones son:");
                }
                foreach (Miembro m in listaMiembros)
                {
                    Console.WriteLine(m);
                }
            }
            catch(Exception ex)
            {
                MostrarError(ex.Message);
            }
        }
    }
}
