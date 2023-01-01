using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        private List<string> listaDePosicionesPulsadas = new List<string>() { };
        private List<string> listaDePosicionesDeBarcos = new List<string>() { };
        private string botonPresionadoCordenadas;
        private DispatcherTimer temporizador;
        private Button ultimoBotonSeleccionado;
        private Button ultimoBotonDisparado;
        private bool jugadorLider;
        private string sala;
        private ImageBrush brushBarcoDestruido = new ImageBrush();
        private ImageBrush brushBarco = new ImageBrush();
        private readonly InstanceContext contexto;
        private readonly ServicioAServidor.AdminiSocialClient cliente;

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
            labelHost.Content = jugadorPartida.Apodo + Properties.Idiomas.Resources.tu;
            labelContricante.Content = jugadorContricante.Apodo;
            brushBarcoDestruido.ImageSource = imagenBarcoDestruido.Source;
            brushBarco.ImageSource = imagenBarco.Source;
            contexto = new InstanceContext(this);
            cliente = new ServicioAServidor.AdminiSocialClient(contexto);
            actualizarCallbackEnServidor();
            columnDefinitionContricante.Width = new GridLength(0);
            columnDefinitionContricanteNumerosPosiciones. Width = new GridLength(0);
            rowDefinitionUltima.Height = new GridLength(50);
            buttonTirar.Visibility = Visibility.Hidden;
            posicionarBarcos();

        }

        private void posicionarBarcos()
        {
            
        }

        private void actualizarCallbackEnServidor()
        {
            cliente.ActualizarCallbackEnPartida(jugadorPartida.Apodo);
        }

        private void iniciarPartida()
        {
            buttonTirar.Visibility = Visibility.Visible;
            if (jugadorLider)
            {
                cliente.PrimerTiro(jugadorPartida.Apodo, jugadorContricante.Apodo);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null || boton != ultimoBotonSeleccionado || !listaDePosicionesPulsadas.Contains(botonPresionadoCordenadas))
            {
                string nombreBoton = boton.Name;
                string posiciones = obtenerPosicionString(nombreBoton);
                if(ultimoBotonSeleccionado != null && !listaDePosicionesPulsadas.Contains(posiciones))
                {
                    volverTransparenteElBoton();
                    
                }
                ultimoBotonSeleccionado = boton;
                cambiarColorVerdeABoton();
                botonPresionadoCordenadas = posiciones;
            }
                
        }

        private void volverTransparenteElBoton()
        {
            ultimoBotonSeleccionado.Background = Brushes.Transparent;
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
                cliente.Tiro(botonPresionadoCordenadas, jugadorContricante.Apodo, sala, jugadorPartida.Apodo);
                buttonTirar.IsEnabled = false;
                listaDePosicionesPulsadas.Add(botonPresionadoCordenadas);
                botonPresionadoCordenadas = null;
                ultimoBotonDisparado = ultimoBotonSeleccionado;
                ultimoBotonSeleccionado = null;
                ultimoBotonDisparado.Background = Brushes.Black;
                ultimoBotonDisparado.Click -= Button_Click;


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
            string mensaje = Properties.Idiomas.Resources.salirDelJuego;
            MessageBoxResult result = MessageBox.Show(mensaje,Properties.Idiomas.Resources.salir,MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.lobby = null;
            }              
        }

        public void PartidaGanadaCallback(string jugadorGanado)
        {
            textGanador.Text = Properties.Idiomas.Resources.hasGanando+jugadorGanado;
            ActivarBotonesEnPartidaTerminada();
        }

        private void ActivarBotonesEnPartidaTerminada()
        {
            buttonTirar.Visibility = Visibility.Hidden;
            btnAtras.Visibility = Visibility.Visible;
            textGanador.Visibility = Visibility.Visible;
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(lobby);
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
            buttonTirar.IsEnabled = true;

            String nombreBoton = Properties.Idiomas.Resources.casilla + coordenadas + Properties.Idiomas.Resources.propia;
            Object botonSolicitado = GridPartida.FindName(nombreBoton);
            if(botonSolicitado is Button)
            {
                Button botonPresionado = botonSolicitado as Button;
                if (listaDePosicionesDeBarcos.Contains(coordenadas)){
                    botonPresionado.Background = brushBarcoDestruido;
                    cliente.TiroCertero(coordenadas, jugadorContricante.Apodo);
                    if (listaDePosicionesDeBarcos.Contains(coordenadas))
                    {
                        listaDePosicionesDeBarcos.Remove(coordenadas);
                        if(listaDePosicionesDeBarcos.Count == 0)
                        {
                            cliente.PartidaGanada(jugadorContricante.Apodo, jugadorContricante.Apodo);
                            textGanador.Text = Properties.Idiomas.Resources.hasPerdido;
                            ActivarBotonesEnPartidaTerminada();
                        }
                    }
                    
                }
                else
                {
                    botonPresionado.Background = Brushes.Black;
                } 
            }
            
        }

        public void ActualizarCallbackEnPartidaCallback(bool actualizado)
        {
            return;
        }

        private void buttonPosicionListas_Click(object sender, RoutedEventArgs e)
        {
            columnDefinitionContricante.Width = new GridLength(1, GridUnitType.Star);
            columnDefinitionContricanteNumerosPosiciones.Width = new GridLength(30);
            rowDefinitionUltima.Height = new GridLength(100);
            buttonPosicionListas.IsEnabled = false;
            buttonPosicionListas.Visibility = Visibility.Hidden;
            //Hacer un método para saber cuando los dos tengan las posiciones listas
            //O si uno esta comodadndo las piezas y el otro tira que se espere hasta que termine de poner las posiciones
            //y despes de poner las posiciones que se imprima el disparo
            iniciarPartida();
        }

        public void TiroCerteroCallback(string coordenadas)
        {

            ultimoBotonDisparado.Background = brushBarcoDestruido;
            ultimoBotonSeleccionado = null;
        }

        private void ButtonPropio_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button)
            {
                Button buttonSeleccionado = sender as Button;
                string posicionBarco = obtenerPosicionString(buttonSeleccionado.Name);
                int numeroDeBarcos = 0;
                if(listaDePosicionesDeBarcos != null)
                {
                    numeroDeBarcos = listaDePosicionesDeBarcos.Count;
                }
                if (buttonSeleccionado.Background == Brushes.Transparent && numeroDeBarcos < 5)
                {
                    buttonSeleccionado.Background = brushBarco;
                    
                    listaDePosicionesDeBarcos.Add(posicionBarco);
                }
                else
                {
                    buttonSeleccionado.Background = Brushes.Transparent;
                    if (listaDePosicionesDeBarcos.Contains(posicionBarco))
                    {
                        listaDePosicionesDeBarcos.Remove(posicionBarco);
                    }
                }
                habilitarBotonTodoListo();
            }
        }

        private void habilitarBotonTodoListo()
        {
            int numeroDeBarcos = 0;
            if (listaDePosicionesDeBarcos != null)
            {
                numeroDeBarcos = listaDePosicionesDeBarcos.Count;
            }
            if (numeroDeBarcos == 4 && buttonPosicionListas.IsEnabled == false)
            {
                buttonPosicionListas.IsEnabled = true;
                buttonPosicionListas.Visibility = Visibility.Visible;
            }
            else
            {
                buttonPosicionListas.IsEnabled = false;
                buttonPosicionListas.Visibility = Visibility.Hidden;
            }
        }
    }
}
