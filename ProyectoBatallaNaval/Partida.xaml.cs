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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para Partida.xaml
    /// </summary>
    public partial class Partida : Window, IAdminiPartidaCallback
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

        public Partida()
        {
            InitializeComponent();
        }

        public Partida(Jugador jugadorContricante, Jugador jugadorPartida, bool jugadorLider, Lobby lobby)
        {
            InitializeComponent();
            this.jugadorContricante = jugadorContricante;
            this.jugadorPartida = jugadorPartida;
            this.jugadorLider = jugadorLider;
            this.lobby = lobby;
            labelHost.Content = jugadorPartida.Apodo;
            labelContricante.Content = jugadorContricante.Apodo;
        }

        private void iniciarPartida()
        {
            if (jugadorLider)
            {
                InstanceContext contexto = new InstanceContext(this);
                ServicioAServidor.AdminiPartidaClient cliente = new ServicioAServidor.AdminiPartidaClient(contexto);
                cliente.PrimerTiro(jugadorPartida.Apodo, jugadorContricante.Apodo);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null)
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
                ServicioAServidor.AdminiPartidaClient cliente = new ServicioAServidor.AdminiPartidaClient(contexto);
                cliente.tiro(botonPresionadoCordenadas, jugadorContricante.Apodo);
                cambiarColorVerdeABoton();
                buttonTirar.IsEnabled = false;
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

        public void insertarDisparo(string coordenadas)
        {
            if (listaDePosicionesDeBarcos.Contains(coordenadas)){
                //Mandar a llamar un método que diga que golpeó a un barco
            }
            else
            {
                //Que hacer si no le da a ningun barco
            }
            buttonTirar.IsEnabled = true;
            string nombreBoton = "casilla_" + coordenadas + "_Propia";
            Button button = new Button();
            button.Name = coordenadas;
            button.Background = Brushes.Green;
        }

        public void IniciarPartidaCallback(bool inicar)
        {
            if (true)
            {
                MessageBox.Show("El lider es " + jugadorPartida.Apodo, "Lider", MessageBoxButton.OK, MessageBoxImage.Warning);
                buttonTirar.IsEnabled = true;
            }
        }

    }
}
