using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoBatallaNaval
{

    public partial class Registro : Page
    {
        public Registro()
        {
            InitializeComponent();
        }
        private void Registrarse_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCorreoElectronico.Text.Length == 0)
            {
                mensajeError.Text = Properties.Idiomas.Resources.escribeCorreoElectronico;
                textBoxCorreoElectronico.Focus();
            }
            else if (!Regex.IsMatch(textBoxCorreoElectronico.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                mensajeError.Text = Properties.Idiomas.Resources.correoInvalido;
                textBoxCorreoElectronico.Select(0, textBoxCorreoElectronico.Text.Length);
                textBoxCorreoElectronico.Focus();
            }
            else
            {
                string nombreUsuario = textBoxNombreUsuario.Text;
                string correoElectronico = textBoxCorreoElectronico.Text;
                string contraseña = passwordBoxContraseña.Password;
                if (passwordBoxContraseña.Password.Length == 0)
                {
                    mensajeError.Text = Properties.Idiomas.Resources.escribaContraseña;
                    passwordBoxContraseña.Focus();
                }
                else if(passwordBoxConfirmarContraseña.Password.Length == 0)
                {
                    mensajeError.Text = Properties.Idiomas.Resources.confirmaContraseña;
                    passwordBoxConfirmarContraseña.Focus();
                }
                else if(passwordBoxContraseña.Password != passwordBoxConfirmarContraseña.Password)
                {
                    mensajeError.Text = Properties.Idiomas.Resources.contraseñasDiferentes;
                    passwordBoxConfirmarContraseña.Focus();
                }
                else
                {
                    //DEbemos cambiar para que las contraseñas se cifren antes de guardar en la base de datos
                    ServicioAServidor.AdminiUsuariosClient servidor = new ServicioAServidor.AdminiUsuariosClient();

                    ServicioAServidor.Jugador jugador = new ServicioAServidor.Jugador();
                    jugador.Contraseña = HashearContraseña(contraseña);
                    jugador.CorreoElectronico = correoElectronico;
                    jugador.Apodo = nombreUsuario;

                    bool jugadorRegistrado = false;
                    try
                    {
                        jugadorRegistrado = servidor.registarUsuario(jugador);
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                    }
                    catch (DuplicateNameException)
                    {
                        mensajeError.Text = Properties.Idiomas.Resources.ErrorDatosDuplicados;
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                    }
                    catch (EntityException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionBaseDeDatos);
                    }
                    if (jugadorRegistrado)
                    {
                        mensajeError.Text = "Jugador registrado";
                        Reiniciar();
                    }
                    else
                    {
                        mensajeError.Text = Properties.Idiomas.Resources.ErrorDatosDuplicados;
                    }
                }
            }
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            Reiniciar();
        }
        public void Reiniciar()
        {
            textBoxCorreoElectronico.Text = "";
            textBoxNombreUsuario.Text = "";
            passwordBoxContraseña.Password = "";
            passwordBoxConfirmarContraseña.Password = "";
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InicioSesion());
        }

        private void InicioSesion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InicioSesion());
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
