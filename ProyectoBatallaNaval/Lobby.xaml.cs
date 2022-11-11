using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby : Window, IAdminiSocialCallback, IAdminiPartidaCallback
    {
        public Lobby()
        {
            InitializeComponent();
        }

        public void actualizarJugadores(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void escribiendoEnCallback(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public bool insertarDisparo(int[] coordenadas)
        {
            Disparo.Content = coordenadas.ToString();
            //REgresar true si le da a un barco y false si no
            return true;
        }

        public void jugadorSeFue(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void recibirMensaje(string response)
        {
            ApodoMensaje.Content = "Invitado";
            MensajeChat.Content = response;

        }

        public void unionDeJugador(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        private void buttonIniciar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = txtMensaje.Text;
            InstanceContext context = new InstanceContext(this);
            ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
            cliente.enviarMensaje(mensaje);

            /*int[] coordenadas = { 3, 5 };
            ServicioAServidor.AdminiPartidaClient clientePartida = new ServicioAServidor.AdminiPartidaClient(context);
            clientePartida.tiro(coordenadas, "Invitado");*/


        }

        
    }
}
