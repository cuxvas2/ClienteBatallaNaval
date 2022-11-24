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
        private Jugador jugadorPartida;
        private string sala;
        private int jugadoresEnSala = 0;
        private int jugadoresListos = 0;
        private Jugador jugadorContricante;
        private bool jugadorLider = false;
        private bool todoListo = false;
        public Lobby(Jugador jugador)
        {
            InitializeComponent();
            jugadorPartida = jugador;
        }

        public void actualizarJugadores(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void escribiendoEnCallback(Jugador jugador)
        {
            throw new NotImplementedException();
        }


        public void jugadorSeFue(Jugador jugador)
        {
            throw new NotImplementedException();
        }


        public void unionDeJugador(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        private void buttonEnviar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = txtMensaje.Text;
            InstanceContext context = new InstanceContext(this);
            ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
            Chat chat = new Chat();
            chat.Remitente = jugadorPartida.Apodo;
            chat.Sala = this.sala;
            chat.MensajeEnviado = mensaje;
            cliente.enviarMensaje(chat);
            txtMensaje.Text = "";

            /*int[] coordenadas = { 3, 5 };
            ServicioAServidor.AdminiPartidaClient clientePartida = new ServicioAServidor.AdminiPartidaClient(context);
            clientePartida.tiro(coordenadas, "Invitado");*/


        }

        private void buttonCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext context = new InstanceContext(this);
            ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
            cliente.crearSala(this.jugadorPartida);

        }

        public void IniciarPartidaCallback(bool inicar)
        {
            if (inicar)
            {
                Partida partida = new Partida(jugadorContricante, jugadorPartida, jugadorLider, this);
                this.Hide();
                partida.Show();
            }
        }

        public void PrimerTiroCallback(bool jugadorInicio)
        {
            throw new NotImplementedException();
        }

        public void recibirMensaje(Chat respuesta)
        {
            ListBoxItem listBoxItemMensaje = new ListBoxItem();
            //Aquí pueden inyectar codigo??
            listBoxItemMensaje.Content = respuesta.Remitente + ": " + respuesta.MensajeEnviado;
            listBoxMensajes.Items.Add(listBoxItemMensaje);
        }

        public void recibirCodigoSala(string codigo)
        {
            labelCodigoPartida.Content = codigo;
            this.sala = codigo;
            this.jugadoresEnSala += 1;
            jugadorLider = true;
            imagenBarcoHost.Visibility = Visibility.Visible;
            buttonAbandonarSala.Visibility = Visibility.Visible;
        }

        public void jugadorSeUnio(Jugador jugador, string sala, bool seUnio)
        {
            if (seUnio)
            {
                if(jugador.Apodo != this.jugadorPartida.Apodo)
                {
                    this.sala = sala;
                    jugadorContricante = jugador;
                    this.jugadoresEnSala += 1;
                    buttonAbandonarSala.Visibility = Visibility.Visible;
                    labelBarcoContricante.Content = jugador.Apodo;
                    labelBarcoContricante.Visibility = Visibility.Visible;
                    imagenBarcoContricante.Visibility = Visibility.Visible;
                    labelBarcoHost.Content = this.jugadorPartida.Apodo;
                    labelBarcoHost.Visibility = Visibility.Visible;
                    imagenBarcoHost.Visibility = Visibility.Visible;
                }else if (jugador.Apodo == this.jugadorPartida.Apodo)
                {
                    this.jugadoresEnSala += 1;
                }
            }
            else
            {
                MessageBox.Show("La sala a la que intenta unirse se encuentra llena", "Sala llena", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void buttonUnirseSala_Click(object sender, RoutedEventArgs e)
        {
            String codigo = textBoxCodigoSala.Text;
            if (codigo.Length == 5)
            {
                this.sala = codigo;
                InstanceContext context = new InstanceContext(this);
                ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
                cliente.unirseASala(codigo, jugadorPartida);
                textBoxCodigoSala.Text = "";
                labelCodigoPartida.Content = codigo;
            }
            else
            {
                MessageBox.Show("El código ingresado es invalido, deben ser de 5 caracteres", "Codigo inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void buttonAbandonarSala_Click(object sender, RoutedEventArgs e)
        {
            //Poner el método para eliminar el callback del diccionario del servidor
            //y si solo queda una persona en la sala eliminar la sala por completo
            labelCodigoPartida.Content = "";
            buttonAbandonarSala.Visibility = Visibility.Hidden;
            jugadorContricante = null;
            jugadoresListos = 0;
            jugadoresEnSala = 0;
            imagenBarcoContricante.Visibility = Visibility.Hidden;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        private void buttonIniciarPartida_Click(object sender, RoutedEventArgs e)
        {
            if (this.jugadorContricante.Apodo != null)
            {
                InstanceContext context = new InstanceContext(this);
                ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
                if (!todoListo)
                {
                    imagenTodoListoHost.Visibility = Visibility.Visible;
                    buttonIniciarPartida.Content = "Cancelar";
                    jugadoresListos += 1;
                    todoListo = true;
                    cliente.todoListo(this.sala, this.jugadorPartida.Apodo, jugadoresListos);
                }
                else
                {
                    imagenTodoListoHost.Visibility = Visibility.Hidden;
                    buttonIniciarPartida.Content = "Todo listo";
                    jugadoresListos -= 1;
                    todoListo = false;
                    cliente.cancelarTodoListo(this.sala, this.jugadorContricante.Apodo);
                }
            }
            else
            {
                MessageBox.Show("Debe haber dos personas en la sala para poder jugar", "Jugadores en sala", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void recibirTodoListo(string contricante)
        {
            imagenTodoListoContricante.Visibility = Visibility.Visible;
            jugadoresListos += 1;
        }

        public void recibirTodoListoParaIniciar(string contricante)
        {
            InstanceContext context = new InstanceContext(this);
            ServicioAServidor.AdminiPartidaClient cliente = new ServicioAServidor.AdminiPartidaClient(context);
            cliente.IniciarPartida(this.jugadorPartida.Apodo);
            
        }

        public void recibirCancelarListo(string contricante)
        {
            this.jugadoresListos -= 1;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        public void insertarDisparo(string coordenadas)
        {
            throw new NotImplementedException();
        }
    }
}
