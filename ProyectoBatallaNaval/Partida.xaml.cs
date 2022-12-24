using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para Partida.xaml
    /// </summary>
    public partial class Partida : Page, IAdminiSocialCallback
    {
        private Jugador jugadorContricante;
        private Jugador jugadorPartida;
        private Lobby lobby;
        private List<string> listaDePosicionesPulsadas;
        private List<string> listaDePosicionesDeBarcos;
        private string botonPresionadoCordenadas;
        private DispatcherTimer temporizador;
        private Button ultimoBotonSeleccionado;
        private bool jugadorLider;
        private string sala;

        public Partida()
        {
            InitializeComponent();
        }

        public Partida(Jugador jugadorContricante, Jugador jugadorPartida, bool jugadorLider, Lobby lobby, string sala)
        {
            InitializeComponent();
            this.jugadorContricante = jugadorContricante;
            this.jugadorPartida = jugadorPartida;
            this.jugadorLider = jugadorLider;
            this.lobby = lobby;
            this.sala = sala;
            labelHost.Content = jugadorPartida.Apodo;
            labelContricante.Content = jugadorContricante.Apodo;
            actualizarCallbackEnServidor();
            Thread.Sleep(500);
            iniciarPartida();
        }

        private void actualizarCallbackEnServidor()
        {
            InstanceContext contexto = new InstanceContext(this);
            ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(contexto);
            cliente.ActualizarCallbackEnPartida(jugadorPartida.Apodo);
        }

        private void iniciarPartida()
        {
            if (jugadorLider)
            {
                InstanceContext contexto = new InstanceContext(this);
                ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(contexto);
                cliente.PrimerTiro(jugadorPartida.Apodo, jugadorContricante.Apodo);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null)//O que el boton sea diiferente al seleccionado
            {
                if(ultimoBotonSeleccionado != null)
                {
                    volverTransparenteElBoton();
                    
                }
                ultimoBotonSeleccionado = boton;
                cambiarColorVerdeABoton();
                string nombreBoton = boton.Name;
                string posiciones = obtenerPosicionString(nombreBoton);
                botonPresionadoCordenadas = posiciones;
            }
                
        }

        private void volverTransparenteElBoton()
        {
            ultimoBotonSeleccionado.Background = Brushes.Transparent;
        }

        private int[] obtenerPosicion(string nombreBoton)
        {
            string[] nombreBotonSeparado = nombreBoton.Split('_');
            string posicionString = nombreBotonSeparado[1];
            char filaChar = posicionString[0];
            char columnaChar = posicionString[1];
            int[] posiciones = { (int)Char.GetNumericValue(filaChar), (int)Char.GetNumericValue(columnaChar) };
            return posiciones;
        }

        private string obtenerPosicionString(string nombreBoton)
        {
            string[] nombreBotonSeparado = nombreBoton.Split('_');
            string posicionString = nombreBotonSeparado[1];
            return posicionString;
        }

        private void buttonTirar_Click(object sender, RoutedEventArgs e)
        {
            if(botonPresionadoCordenadas == null)
            {
                labelSeleccionarPosicion.Visibility = Visibility.Visible;
                temporizador = new DispatcherTimer();
                temporizador.Interval = TimeSpan.FromSeconds(5);
                temporizador.Tick += TimerTick;
                temporizador.Start();
            }
            else
            {
                InstanceContext contexto = new InstanceContext(this);
                ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(contexto);
                cliente.Tiro(botonPresionadoCordenadas, jugadorContricante.Apodo, sala, jugadorPartida.Apodo);
                buttonTirar.IsEnabled = false;
                botonPresionadoCordenadas = null;
            }
        }

        private void cambiarColorVerdeABoton()
        {
            ultimoBotonSeleccionado.Background = Brushes.Green;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            temporizador.Stop();
            labelSeleccionarPosicion.Visibility = Visibility.Hidden;
        }

        

        public void IniciarPartidaCallback(bool inicar)
        {
            throw new NotImplementedException();
        }

        public void primerTiroCallback(bool iniciar)
        {
            if (true)
            {
                buttonTirar.IsEnabled = true;
            }
        }

        private void CerrandoVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string mensaje = "Desea abandonar la partida y salir del juego?";
            MessageBoxResult result =
                MessageBox.Show(mensaje,"Salir",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                //this.lobby.Close();
                this.lobby = null;
                //this.Close();
            }              
        }

        public void PartidaGanadaCallback(string jugadorGanado)
        {
            buttonTirar.Visibility = Visibility.Hidden;
            btnAtras.Visibility = Visibility.Visible;
            textGanador.Text += jugadorGanado;
            textGanador.Visibility = Visibility.Visible;
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            //this.lobby.Show();
            //this.Close();
        }

        public void actualizarJugadores(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void unionDeJugador(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void jugadorSeFue(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void escribiendoEnCallback(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void recibirMensaje(Chat respuesta)
        {
            throw new NotImplementedException();
        }

        public void recibirCodigoSala(string codigo)
        {
            throw new NotImplementedException();
        }

        public void jugadorSeUnio(Jugador jugador, string sala, bool seUnio)
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

        public void recibirCancelarListo(string contricante)
        {
            throw new NotImplementedException();
        }

        public void insertarDisparo(String coordenadas)
        {
            /*if (listaDePosicionesDeBarcos.Contains(coordenadas)){
                //Mandar a llamar un método que diga que golpeó a un barco
            }
            else
            {
                //Que hacer si no le da a ningun barco
            }*/


            buttonTirar.IsEnabled = true;
            MessageBox.Show("boton habilitado en teoria"); 

            String nombreBoton = "casilla_" + coordenadas + "_Propia";
            Button botonPresionado = (Button)GridPartida.FindName(nombreBoton);
            Button botonPresionado1 = (Button)GridPartida.TryFindResource(nombreBoton);
            MessageBox.Show("atacó en la posicion " + coordenadas + " B: " + botonPresionado.Name + " - " + botonPresionado1.Name);
            botonPresionado.Background = Brushes.Black;
        }
    }
}
