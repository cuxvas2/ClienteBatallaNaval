using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ProyectoBatallaNaval
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var codigoDeLenguaje = ProyectoBatallaNaval.Properties.Settings.Default.codigoLenguaje;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(codigoDeLenguaje);
            base.OnStartup(e);

        }

        App()
        {

        }
    }
}
