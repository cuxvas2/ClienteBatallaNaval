using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Page
    {
        public Jugador JugadorPartida { get; }

        public Configuracion()
        {
            InitializeComponent();
        }

        public Configuracion(Jugador jugadorPartida)
        {
            InitializeComponent();
            JugadorPartida = jugadorPartida;
        }

        private void CambiarIdioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cambiarIdioma.SelectedIndex == 0)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");

            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            }
        }

        private void AplicarCambios_Click(object sender, RoutedEventArgs e)
        {
            string contraseñaNueva = passwordBoxNuevaContraseña.Password;

            if(contraseñaNueva.Count() > 3)
            {
                string contraseñaHasheada = HashearContraseña(contraseñaNueva);
                if(contraseñaNueva.Contains(" "))
                {
                    //No se puede tener una contraseña con espacios
                }
                else if(JugadorPartida.Contraseña != contraseñaHasheada)
                {
                    bool contraseñaCambiada = false;
                    ServicioAServidor.AdminiUsuariosClient cliente = new ServicioAServidor.AdminiUsuariosClient();
                    try
                    {
                        contraseñaCambiada = cliente.CambiarContraseña(JugadorPartida.Apodo, contraseñaHasheada);
                    }
                    catch (TimeoutException)
                    {
                        AvisoErrorTiempoAgotado();
                    }
                    catch (CommunicationException)
                    {
                        AvisoDeErrorConElServidor();
                    }
                    catch (EntityException)
                    {
                        AvisoErrorConBaseDeDatos();
                    }
                    if (contraseñaCambiada)
                    {
                        MessageBox.Show("Contraseña cambiada correctamente");
                    }
                }
            
            }
            NavigationService.Refresh();
            NavigationService.GoBack();

        }

        private void AvisoDeErrorConElServidor()
        {
            MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
        }
        private void AvisoErrorTiempoAgotado()
        {
            MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
        }
        private void AvisoErrorConBaseDeDatos()
        {
            MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
        }

        private string HashearContraseña(string contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder contraseñaHasheada = new StringBuilder();
                for (int i = 0; i < (bytes.Length); i++)
                {
                    contraseñaHasheada.Append(bytes[i].ToString("x2"));
                }
                return contraseñaHasheada.ToString();
            }
        }
    }
}
