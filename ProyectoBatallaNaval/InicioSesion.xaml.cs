using Microsoft.Win32;
using ProyectoBatallaNaval.ServicioAServidor;
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
using ProyectoBatallaNaval.Properties;

namespace ProyectoBatallaNaval
{
    public partial class InicioSesion : Page, IAdminiSocialCallback
    {
        private Jugador jugadorPartida;

        public InicioSesion()
        {
            InitializeComponent();
        }

        public void ActualizarCallbackEnPartidaCallback(bool actualizado)
        {
            return;
        }

        public void actualizarJugadores(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void escribiendoEnCallback(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void IniciarPartidaCallback(bool inicar)
        {
            throw new NotImplementedException();
        }

        public void insertarDisparo(string coordenadas)
        {
            throw new NotImplementedException();
        }

        public void jugadorSeFue(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void jugadorSeUnio(Jugador jugador, string sala, bool seUnio)
        {
            throw new NotImplementedException();
        }

        public void PartidaGanadaCallback(string jugadorGanado)
        {
            throw new NotImplementedException();
        }

        public void primerTiroCallback(bool iniciar)
        {
            throw new NotImplementedException();
        }

        public void recibirCancelarListo(string contricante)
        {
            throw new NotImplementedException();
        }

        public void recibirCodigoSala(string codigo)
        {
            throw new NotImplementedException();
        }

        public void RecibirExpulsacion()
        {
            throw new NotImplementedException();
        }

        public void recibirMensaje(Chat respuesta)
        {
            throw new NotImplementedException();
        }

        public void recibirTodoListo(string contricante)
        {
            throw new NotImplementedException();
        }

        public void recibirTodoListoParaIniciar(string contricante)
        {
            throw new NotImplementedException();
        }

        public void TiroCerteroCallback(string coordenadas)
        {
            throw new NotImplementedException();
        }

        public void unionDeJugador(Jugador jugador)
        {
            return;
        }

        private void ButtonIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCorreo.Text.Length == 0 || passwordBoxContraseña.Password.Length == 0)
            {
                mensajeError.Text = Properties.Idiomas.Resources.noCamposVacios;
                textBoxCorreo.Focus();
            }
            else if(!Regex.IsMatch(textBoxCorreo.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                mensajeError.Text = Properties.Idiomas.Resources.correoInvalido;
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
                    Jugador jugador;
                    jugador = cliente.recuperarJugadorPorCorreo(correoElectronico);
                    if(jugador != null)
                    {
                        jugadorPartida = jugador;
                        Application.Current.MainWindow.Closing += CerrandoVentana;
                        InstanceContext context = new InstanceContext(this);
                        ServicioAServidor.AdminiSocialClient clienteJoin = new ServicioAServidor.AdminiSocialClient(context);
                        //Agregar su contecto desde aqui?
                        clienteJoin.Conectado(jugador);

                        NavigationService.Navigate(new Lobby(jugador));
                    }
                     
                }
                else
                {
                    MessageBox.Show(Properties.Idiomas.Resources.correoInvalido);
                }
            }
        }

        private void ButtonRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registro());
        }



        private void CerrandoVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string mensaje = Properties.Idiomas.Resources.salirDelJuego;
            MessageBoxResult result = MessageBox.Show(mensaje, Properties.Idiomas.Resources.salir, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if(jugadorPartida != null)
                {
                    InstanceContext context = new InstanceContext(this);
                    ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
                    cliente.CerrarJuego(jugadorPartida.Apodo);
                }
                
            }
        }

    }
}
