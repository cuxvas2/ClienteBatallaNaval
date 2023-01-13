using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Text.RegularExpressions;

namespace ProyectoBatallaNaval
{

    public partial class Lobby : Page, IAdminiSocialCallback
    {
        private readonly Jugador jugadorPartida;
        private string sala;
        private int jugadoresListos = 0;
        private Jugador jugadorContricante;
        private bool jugadorLider = false;
        private bool todoListo = false;
        private readonly bool esInvitado;
        private readonly InstanceContext context;
        private readonly ServicioAServidor.AdminiSocialClient cliente;
        private string amigoParaEliminar;
        
        public Lobby(Jugador jugador, bool esInvitado)
        {
            InitializeComponent();
            jugadorPartida = jugador;
            this.esInvitado = esInvitado;
            context = new InstanceContext(this);
            cliente = new ServicioAServidor.AdminiSocialClient(context);
            if (esInvitado)
            {
                listViewAMigos.Visibility = Visibility.Hidden;
                buttonAñadirAmigo.IsEnabled = false;
            }
            else
            {
                LlnarListaDeAmigos();
            }
        }

        private void LlnarListaDeAmigos()
        {
            ServicioAServidor.AdminiUsuariosClient clienteUsuario = new ServicioAServidor.AdminiUsuariosClient();
            string[] amigos = clienteUsuario.RecuperarListaDeAmigos(jugadorPartida.Apodo);
            List<Amigo> listaDeAmigos = new List<Amigo>();
            Amigo amigo = new Amigo();
            foreach(string apodo in amigos)
            {
                amigo.Apodo = apodo;
                listaDeAmigos.Add(amigo);
            }
            listViewAMigos.ItemsSource = listaDeAmigos;
        }

        public void ActualizarJugadores(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void EscribiendoEnCallback(Jugador jugador)
        {
            throw new NotImplementedException();
        }


        public void JugadorSeFue(Jugador jugador)
        {
            throw new NotImplementedException();
        }


        public void UnionDeJugador(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        private void ButtonEnviar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = txtMensaje.Text;
            if (!String.IsNullOrWhiteSpace(mensaje))
            {
                Chat chat = new Chat();
                chat.Remitente = jugadorPartida.Apodo;
                chat.Sala = this.sala;
                chat.MensajeEnviado = mensaje;
                try
                {
                    cliente.EnviarMensaje(chat);
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

        }

        private void ButtonCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            if (this.jugadorPartida != null)
            {
                try
                {
                    cliente.CrearSala(this.jugadorPartida);
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

        public void IniciarPartidaCallback(bool inicar)
        {
            if (inicar)
            {
                imagenTodoListoHost.Visibility = Visibility.Hidden;
                imagenTodoListoContricante.Visibility = Visibility.Hidden;
                buttonIniciarPartida.Content = Properties.Idiomas.Resources.todoListo;
                jugadoresListos = 0;
                todoListo = false;
                NavigationService.Navigate(new Partida(jugadorContricante, jugadorPartida, jugadorLider, this, sala, esInvitado));
            }
        }

        public void PrimerTiroCallback(bool iniciar)
        {
            throw new NotImplementedException();
        }

        public void RecibirMensaje(Chat respuesta)
        {
            ListBoxItem listBoxItemMensaje = new ListBoxItem();
            listBoxItemMensaje.Content = respuesta.Remitente + ": " + respuesta.MensajeEnviado;
            listBoxMensajes.Items.Add(listBoxItemMensaje);
        }

        public void RecibirCodigoSala(string codigo)
        {
            labelCodigoPartida.Content = codigo;
            this.sala = codigo;
            jugadorLider = true;
            imagenBarcoHost.Visibility = Visibility.Visible;
            buttonAbandonarSala.Visibility = Visibility.Visible;
            labelBarcoHost.Visibility = Visibility.Visible;
        }

        public void JugadorSeUnio(Jugador jugador, string sala, bool seUnio)
        {
            if (seUnio && jugador != null)
            {
                if (jugador.Apodo != this.jugadorPartida.Apodo)
                {
                    if (jugadorLider)
                    {
                        buttonExpulsar.Visibility = Visibility.Visible;
                    }
                    this.sala = sala;
                    jugadorContricante = jugador;
                    if (esInvitado)
                    {
                        buttonAñadirAmigo.Visibility = Visibility.Hidden;
                        listViewAMigos.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        buttonAñadirAmigo.Visibility = Visibility.Visible;
                    }
                    buttonAbandonarSala.Visibility = Visibility.Visible;
                    labelBarcoContricante.Content = jugador.Apodo;
                    labelBarcoContricante.Visibility = Visibility.Visible;
                    imagenBarcoContricante.Visibility = Visibility.Visible;
                    labelBarcoHost.Content = this.jugadorPartida.Apodo;
                    labelBarcoHost.Visibility = Visibility.Visible;
                    imagenBarcoHost.Visibility = Visibility.Visible;
                    labelCodigoPartida.Content = sala;
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
            if (codigo.Length == 5 && jugadorPartida != null)
            {
                this.sala = codigo;
                try
                {
                    cliente.UnirseASala(codigo, jugadorPartida);
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
            buttonAñadirAmigo.Visibility = Visibility.Hidden;
            jugadorContricante = null;
            jugadoresListos = 0;
            jugadorLider = false;
            imagenBarcoContricante.Visibility = Visibility.Hidden;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
            labelBarcoContricante.Content = "";
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
                        cliente.TodoListo(this.sala, this.jugadorPartida.Apodo, jugadoresListos);
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
                        cliente.CancelarTodoListo(this.sala, this.jugadorContricante.Apodo);
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

        public void RecibirTodoListo(string contricante)
        {
            imagenTodoListoContricante.Visibility = Visibility.Visible;
            jugadoresListos += 1;
        }

        public void RecibirTodoListoParaIniciar(string contricante)
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

        public void RecibirCancelarListo(string contricante)
        {
            this.jugadoresListos -= 1;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
        }

        public void InsertarDisparo(string coordenadas)
        {
            throw new NotImplementedException();
        }

        public void PartidaGanadaCallback(string jugadorGanado)
        {
            throw new NotImplementedException();
        }

        public void ActualizarCallbackEnPartidaCallback(bool actualizado)
        {
            throw new NotImplementedException();
        }

        public void TiroCerteroCallback(string coordenadas)
        {
            throw new NotImplementedException();
        }

        private void ButtonConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Configuracion(jugadorPartida, esInvitado));
        }

        public void RecibirExpulsacion()
        {
            ReiniciarElementosPorDeefecto();
        }

        private void ButtonExpulsar_Click(object sender, RoutedEventArgs e)
        {
            bool jugadorExpulsado = false;
            string codigo = labelCodigoPartida.Content.ToString();
            if (String.IsNullOrWhiteSpace(codigo))
            {
                try
                {
                    cliente.ExpulsarDeSala(codigo);
                    jugadorExpulsado = true;
                }
                catch (TimeoutException)
                {
                    jugadorExpulsado = false;
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                }
                catch (CommunicationException)
                {
                    jugadorExpulsado = false;
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
            }
            if (jugadorExpulsado)
            {
                jugadorContricante = null;
                if (imagenTodoListoContricante.IsVisible)
                {
                    jugadoresListos -= 1;
                    imagenTodoListoContricante.Visibility = Visibility.Hidden;
                }
                labelBarcoContricante.Content = "";
                imagenBarcoContricante.Visibility = Visibility.Hidden;
                buttonAñadirAmigo.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonEliminarAmigo_Click(object sender, RoutedEventArgs e)
        {
            ServicioAServidor.AdminiUsuariosClient clienteUsuario = new ServicioAServidor.AdminiUsuariosClient();
            string jugador = jugadorPartida.Apodo;
            if (!String.IsNullOrWhiteSpace(jugador) && !String.IsNullOrWhiteSpace(amigoParaEliminar)) { 
                try
                {
                    bool eliminado = clienteUsuario.EliminarAmigo(jugador, amigoParaEliminar);
                    if (eliminado)
                    {
                        LlnarListaDeAmigos();
                    }
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
                catch (EntityException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
            }
        }


        public void NotificarAbandorarSala()
        {
            jugadorContricante = null;
            buttonExpulsar.Visibility = Visibility.Hidden;
            jugadoresListos = 0;
            jugadorLider = false;
            imagenBarcoContricante.Visibility = Visibility.Hidden;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
            labelBarcoHost.Content = "";
            todoListo = false;
            imagenTodoListoContricante.Visibility = Visibility.Hidden;
            imagenTodoListoHost.Visibility = Visibility.Hidden;
            buttonIniciarPartida.Content = Properties.Idiomas.Resources.todoListo;
            buttonAñadirAmigo.Visibility = Visibility.Hidden;
        }

        private void ButtonAñadirAmigo_Click(object sender, RoutedEventArgs e)
        {
            string jugador = jugadorPartida.Apodo;
            string contricante = jugadorContricante.Apodo;
            if (!String.IsNullOrEmpty(jugador) && !String.IsNullOrEmpty(contricante))
            {
                ServicioAServidor.AdminiUsuariosClient clienteUsuario = new ServicioAServidor.AdminiUsuariosClient();
                bool añadido = false;
                try
                {
                    añadido = clienteUsuario.AñadirAmigo(jugador, contricante);
                    if (añadido)
                    {
                        buttonAñadirAmigo.Visibility = Visibility.Hidden;
                    }
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorTiempoAgotado);
                }
                catch (CommunicationException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
                catch (EntityException)
                {
                    MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
                }
            }
        }

        public static bool EnviarCorreo(string to, string emailSubject, string message)
        {
            bool status = false;
            string from = "batallanaval.fei@hotmail.com";
            string displayName = "Batalla Naval Juego";

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from, displayName);
                mailMessage.To.Add(to);

                mailMessage.Subject = emailSubject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.Credentials = new NetworkCredential(from, "gabriel2002");
                client.EnableSsl = true;

                client.Send(mailMessage);
                status = true;
            }
            catch (SmtpException ex)
            {
                throw new SmtpException(ex.Message);
            }
            return status;
        }

        private void ButtonEnviarCodigoCorreo_Click(object sender, RoutedEventArgs e)
        {
            string correoEmail = textBoxCorreoElectronico.Text;
            if (string.IsNullOrWhiteSpace(correoEmail))
            {
                MessageBox.Show("Correo vacio");
            }
            else if (Regex.IsMatch(correoEmail, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                if(!String.IsNullOrWhiteSpace(sala))
                {
                    try 
                    { 
                        EnviarCorreo(correoEmail, "CorreoBatallaNaval", sala);
                        MessageBox.Show("CorreoEnviado");

                    }
                    catch(SmtpException)
                    {
                        MessageBox.Show("No se pudo establecer conexion al servicio de correo electronico");
                    }
                }
                else
                {
                    MessageBox.Show("CorreoNoEnviado");
                }
            }
            else
            {
                MessageBox.Show("Correo electronico no valido");
            }
        }

        private void ExpanderOpciones_Expanded(object sender, RoutedEventArgs e)
        {
            if(sender is Expander)
            {
                Expander expanderExpandido = sender as Expander;
                amigoParaEliminar = expanderExpandido.Header.ToString();
            }
        }
    }

    public class Amigo
    {
        public string Apodo { get; set; }
        public string CodigoSalaEnviado { get; set; }
        public Amigo()
        {
            Apodo = "";
            CodigoSalaEnviado = "";
        }
        public Amigo(string apodo, string codigoSalaEnviado)
        {
            Apodo = apodo;
            CodigoSalaEnviado = codigoSalaEnviado;
        }

        public override string ToString()
        {
            return this.Apodo;
        }
    }
}
