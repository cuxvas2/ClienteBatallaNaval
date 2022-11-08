using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoBatallaNaval
{
    public partial class InicioSesion : Window
    {

        public InicioSesion()
        {
            InitializeComponent();
        }
        Registro registro = new Registro();

        private void buttonIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCorreo.Text.Length == 0)
            {
                mensajeError.Text = "Escribe un Email.";
                textBoxCorreo.Focus();
            }
            else if(!Regex.IsMatch(textBoxCorreo.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                mensajeError.Text = "Ingresa un Correo electronico valido.";
                textBoxCorreo.Select(0, textBoxCorreo.Text.Length);
                textBoxCorreo.Focus();
            }
            else
            {
                string correoElectronico = textBoxCorreo.Text;
                string password = passwordBoxContraseña.Password;
                ServicioAServidor.AdminiUsuariosClient cliente = new ServicioAServidor.AdminiUsuariosClient();

                Boolean regisrado = cliente.iniciarSesion(correoElectronico, password);
                if (regisrado)
                {
                    InstanceContext context = new InstanceContext(this);
                    ServicioAServidor.AdminiSocialClient clienteJoin = new ServicioAServidor.AdminiSocialClient(context);
                    //Agregar su contecto desde aqui?
                    Lobby lobby = new Lobby();
                    lobby.Show();
                    Close();
                }
                else
                {
                    mensajeError.Text = "El usuario no esta registrado";
                }
            }
        }

        private void buttonRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            registro.Show();
            Close();
        }
    }
}
