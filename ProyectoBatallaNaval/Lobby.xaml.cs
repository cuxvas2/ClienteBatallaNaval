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
    public partial class Lobby : Page, IAdminiSocialCallback
    {
        private Jugador jugadorPartida;
        private string sala;
        private int jugadoresEnSala = 0;
        private int jugadoresListos = 0;
        private Jugador jugadorContricante;
        private bool jugadorLider = false;
        private bool todoListo = false;
        private InstanceContext context;
        private readonly ServicioAServidor.AdminiSocialClient cliente;
        public Lobby(Jugador jugador)
        {
            InitializeComponent();
            jugadorPartida = jugador;
            context = new InstanceContext(this);
            cliente = new ServicioAServidor.AdminiSocialClient(context);
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
            Chat chat = new Chat();
            chat.Remitente = jugadorPartida.Apodo;
            chat.Sala = this.sala;
            chat.MensajeEnviado = mensaje;
            cliente.enviarMensaje(chat);
            txtMensaje.Text = "";

        }

        private void buttonCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            cliente.crearSala(this.jugadorPartida);

        }

        public void IniciarPartidaCallback(bool inicar)
        {
            if (inicar)
            {
                imagenTodoListoHost.Visibility = Visibility.Hidden;
                imagenTodoListoContricante.Visibility = Visibility.Hidden;
                buttonIniciarPartida.Content = "Todo listo";
                jugadoresListos = 0;
                todoListo = false;
                NavigationService.Navigate(new Partida(jugadorContricante, jugadorPartida, jugadorLider, this, sala));
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
                cliente.unirseASala(codigo, jugadorPartida);
                textBoxCodigoSala.Text = "";
                labelCodigoPartida.Content = codigo;
                jugadorLider = false;
            }
            else
            {
                MessageBox.Show("El código ingresado es invalido, deben ser de 5 caracteres", "Codigo inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void buttonAbandonarSala_Click(object sender, RoutedEventArgs e)
        {
            if(jugadorContricante == null)
            {
                cliente.EliminarSala(labelCodigoPartida.Content.ToString());
            }
            labelCodigoPartida.Content = "";
            buttonAbandonarSala.Visibility = Visibility.Hidden;
            jugadorContricante = null;
            jugadoresListos = 0;
            jugadoresEnSala = 0;
            jugadorLider = false;
            imagenBarcoContricante.Visibility = Visibility.Hidden;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        private void buttonIniciarPartida_Click(object sender, RoutedEventArgs e)
        {
            if (this.jugadorContricante != null)
            {
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
            cliente.IniciarPartida(this.jugadorPartida.Apodo);
            
        }

        public void recibirCancelarListo(string contricante)
        {
            this.jugadoresListos -= 1;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        public void insertarDisparo(string coordenadas)
        {
        }

        public void primerTiroCallback(bool iniciar)
        {
            throw new NotImplementedException();
        }

        public void PartidaGanadaCallback(string jugadorGanado)
        {
            throw new NotImplementedException();
        }

        public void ActualizarCallbackEnPartidaCallback(bool actualizado)
        {
            
        }

        public void TiroCerteroCallback(string coordenadas)
        {
            throw new NotImplementedException();
        }
    }
}
