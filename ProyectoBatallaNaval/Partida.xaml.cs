using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para Partida.xaml
    /// </summary>
    public partial class Partida : Window
    {
        private Jugador jugadorContricante;
        private Jugador jugadorPartida;
        private Lobby lobby;

        public Partida()
        {
            InitializeComponent();
        }

        public Partida(Jugador jugadorContricante, Jugador jugadorPartida, Lobby lobby)
        {
            this.jugadorContricante = jugadorContricante;
            this.jugadorPartida = jugadorPartida;
            this.lobby = lobby;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            if (boton != null)
            {
                string nombreBoton = boton.Name;
                int[] posiciones = obtenerPosicion(nombreBoton);
                
            }
                
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
    }
}
