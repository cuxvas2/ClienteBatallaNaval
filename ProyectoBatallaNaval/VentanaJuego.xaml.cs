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

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para VentanaJuego.xaml
    /// </summary>
    public partial class VentanaJuego : Window
    {
        public Jugador jugadorEnVentanaPrincipal;
        public VentanaJuego()
        {
            InitializeComponent();
        }

        private void CerrandoVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string mensaje = "Desea abandonar la partida y salir del juego?";
            MessageBoxResult result = MessageBox.Show(mensaje, "Salir", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                InstanceContext context = new InstanceContext(this);
                ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
                Object objeto = frameParaPaginas.TryFindResource("InicioSesion.xaml");
                if(objeto is Page)
                {
                    Page paginaInicioSesion = objeto as Page;
                    
                }
                if(jugadorEnVentanaPrincipal != null)
                {
                    cliente.CerrarJuego(jugadorEnVentanaPrincipal.Apodo);
                }
                
            }
        }
    }
}
