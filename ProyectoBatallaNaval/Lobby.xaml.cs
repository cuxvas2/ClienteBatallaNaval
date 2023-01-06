using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para Lobby.xaml
    /// </summary>
    public partial class Lobby : Page, IAdminiSocialCallback
    {
        private readonly Jugador jugadorPartida;
        private string sala;
        private int jugadoresEnSala = 0;
        private int jugadoresListos = 0;
        private Jugador jugadorContricante;
        private bool jugadorLider = false;
        private bool todoListo = false;
        private readonly InstanceContext context;
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

        private void ButtonEnviar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = txtMensaje.Text;
            Chat chat = new Chat();
            chat.Remitente = jugadorPartida.Apodo;
            chat.Sala = this.sala;
            chat.MensajeEnviado = mensaje;
            try
            {
                cliente.enviarMensaje(chat);
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
            }
            txtMensaje.Text = "";

        }

        private void ButtonCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.crearSala(this.jugadorPartida);
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
            }

        }

        public void IniciarPartidaCallback(bool inicar)
        {
            if (inicar)
            {
                imagenTodoListoHost.Visibility = Visibility.Hidden;
                imagenTodoListoContricante.Visibility = Visibility.Hidden;
                buttonIniciarPartida.Content = Properties.Idiomas.Resources.todoListo;
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
                if (jugador.Apodo != this.jugadorPartida.Apodo)
                {
                    if (jugadorLider)
                    {
                        buttonExpulsar.Visibility = Visibility.Visible;
                    }
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
                }
                else if (jugador.Apodo == this.jugadorPartida.Apodo)
                {
                    this.jugadoresEnSala += 1;
                }
            }
            else
            {
                MessageBox.Show(Properties.Idiomas.Resources.salaLlena);
            }
        }

        private void ButtonUnirseSala_Click(object sender, RoutedEventArgs e)
        {
            String codigo = textBoxCodigoSala.Text;
            if (codigo.Length == 5)
            {
                this.sala = codigo;
                try
                {
                    cliente.unirseASala(codigo, jugadorPartida);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
                textBoxCodigoSala.Text = "";
                labelCodigoPartida.Content = codigo;
                jugadorLider = false;
            }
            else
            {
                MessageBox.Show(Properties.Idiomas.Resources.codigoInvalido);
            }
        }

        private void ButtonAbandonarSala_Click(object sender, RoutedEventArgs e)
        {
            if (jugadorContricante == null)
            {
                try
                {
                    cliente.EliminarSala(labelCodigoPartida.Content.ToString());
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
            }
            ReiniciarElementosPorDeefecto();
        }

        private void ReiniciarElementosPorDeefecto()
        {
            labelCodigoPartida.Content = "";
            buttonAbandonarSala.Visibility = Visibility.Hidden;
            buttonExpulsar.Visibility = Visibility.Hidden;
            jugadorContricante = null;
            jugadoresListos = 0;
            jugadoresEnSala = 0;
            jugadorLider = false;
            imagenBarcoContricante.Visibility = Visibility.Hidden;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        private void ButtonIniciarPartida_Click(object sender, RoutedEventArgs e)
        {
            if (this.jugadorContricante != null)
            {
                if (!todoListo)
                {
                    imagenTodoListoHost.Visibility = Visibility.Visible;
                    buttonIniciarPartida.Content = Properties.Idiomas.Resources.cancelar;
                    jugadoresListos += 1;
                    todoListo = true;
                    try
                    {
                        cliente.todoListo(this.sala, this.jugadorPartida.Apodo, jugadoresListos);
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                    }
                }
                else
                {
                    imagenTodoListoHost.Visibility = Visibility.Hidden;
                    buttonIniciarPartida.Content = Properties.Idiomas.Resources.todoListo;
                    jugadoresListos -= 1;
                    todoListo = false;
                    try
                    {
                        cliente.cancelarTodoListo(this.sala, this.jugadorContricante.Apodo);
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                    }
                    catch (CommunicationException)
                    {
                        MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                    }
                }
            }
            else
            {
                MessageBox.Show(Properties.Idiomas.Resources.faltanJugadores);
            }
        }

        public void recibirTodoListo(string contricante)
        {
            imagenTodoListoContricante.Visibility = Visibility.Visible;
            jugadoresListos += 1;
        }

        public void recibirTodoListoParaIniciar(string contricante)
        {
            try
            {
                cliente.IniciarPartida(this.jugadorPartida.Apodo);
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
            }

        }

        public void recibirCancelarListo(string contricante)
        {
            this.jugadoresListos -= 1;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        public void insertarDisparo(string coordenadas)
        {
            return;
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
            return;
        }

        public void TiroCerteroCallback(string coordenadas)
        {
            throw new NotImplementedException();
        }

        private void ButtonConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Configuracion());
        }

        public void RecibirExpulsacion()
        {
            ReiniciarElementosPorDeefecto();
        }

        private void ButtonExpulsar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente.ExpulsarDeSala(labelCodigoPartida.Content.ToString());
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
            }
            catch (CommunicationException)
            {
                MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
            }
        }
    }
}
