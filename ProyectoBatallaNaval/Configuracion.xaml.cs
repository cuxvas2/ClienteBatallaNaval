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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Page
    {
        public Configuracion()
        {
            InitializeComponent();
        }



 
        private void cambiarIdioma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cambiarIdioma.SelectedIndex == 0)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");

            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            }
        }

        private void aplicarCambios_Click(object sender, RoutedEventArgs e)
        {
            //Debe mandar a lobby pero requiere un parametro
            //NavigationService.Navigate(new Lobby());
        }
    }
}
