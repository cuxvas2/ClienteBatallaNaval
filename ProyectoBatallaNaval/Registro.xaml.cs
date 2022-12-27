using System;
using System.Collections.Generic;
using System.Linq;
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
                mensajeError.Text = "Escribe un correo electronico";
                textBoxCorreoElectronico.Focus();
            }
            else if (!Regex.IsMatch(textBoxCorreoElectronico.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                mensajeError.Text = "Ingresa un Correo electronico valido.";
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
                    mensajeError.Text = "Escribe una contraseña";
                    passwordBoxContraseña.Focus();
                }
                else if(passwordBoxConfirmarContraseña.Password.Length == 0)
                {
                    mensajeError.Text = "Confirma la contraseña. ";
                    passwordBoxConfirmarContraseña.Focus();
                }
                else if(passwordBoxContraseña.Password != passwordBoxContraseña.Password)
                {
                    mensajeError.Text = "La contraseñas deben ser las mismas.";
                    passwordBoxConfirmarContraseña.Focus();
                }
                else
                {
                    //DEbemos cambiar para que las contraseñas se cifren antes de guardar en la base de datos
                    ServicioAServidor.AdminiUsuariosClient servidor = new ServicioAServidor.AdminiUsuariosClient();

                    ServicioAServidor.Jugador jugador = new ServicioAServidor.Jugador();
                    jugador.Contraseña = contraseña;
                    jugador.CorreoElectronico = correoElectronico;
                    jugador.Apodo = nombreUsuario;

                    servidor.registarUsuario(jugador);
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
            //Close();
        }

        private void InicioSesion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InicioSesion());
        }
    }
}
