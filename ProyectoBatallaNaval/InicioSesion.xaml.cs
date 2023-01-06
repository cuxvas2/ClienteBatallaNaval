using Microsoft.Win32;
using ProyectoBatallaNaval.ServicioAServidor;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
using ProyectoBatallaNaval.Properties;
using System.Data;
using System.Security.Cryptography;

namespace ProyectoBatallaNaval
{
    public partial class InicioSesion : Page, IAdminiSocialCallback
    {
        private Jugador jugadorPartida;

        public InicioSesion()
        {
            InitializeComponent();
        }

        public void ActualizarCallbackEnPartidaCallback(bool actualizado)
        {
            return;
        }

        public void ActualizarJugadores(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void EscribiendoEnCallback(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void IniciarPartidaCallback(bool inicar)
        {
            throw new NotImplementedException();
        }

        public void InsertarDisparo(string coordenadas)
        {
            throw new NotImplementedException();
        }

        public void JugadorSeFue(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void JugadorSeUnio(Jugador jugador, string sala, bool seUnio)
        {
            throw new NotImplementedException();
        }

        public void PartidaGanadaCallback(string jugadorGanado)
        {
            throw new NotImplementedException();
        }

        public void PrimerTiroCallback(bool iniciar)
        {
            throw new NotImplementedException();
        }

        public void RecibirCancelarListo(string contricante)
        {
            throw new NotImplementedException();
        }

        public void RecibirCodigoSala(string codigo)
        {
            throw new NotImplementedException();
        }

        public void RecibirExpulsacion()
        {
            throw new NotImplementedException();
        }

        public void RecibirMensaje(Chat respuesta)
        {
            throw new NotImplementedException();
        }

        public void RecibirTodoListo(string contricante)
        {
            throw new NotImplementedException();
        }

        public void RecibirTodoListoParaIniciar(string contricante)
        {
            throw new NotImplementedException();
        }

        public void TiroCerteroCallback(string coordenadas)
        {
            throw new NotImplementedException();
        }

        public void UnionDeJugador(Jugador jugador)
        {
            return;
        }

        private void ButtonIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCorreo.Text.Length == 0 || passwordBoxContraseña.Password.Length == 0)
            {
                mensajeError.Text = Properties.Idiomas.Resources.noCamposVacios;
                textBoxCorreo.Focus();
            }
            else if(!Regex.IsMatch(textBoxCorreo.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                mensajeError.Text = Properties.Idiomas.Resources.correoInvalido;
                textBoxCorreo.Select(0, textBoxCorreo.Text.Length);
                textBoxCorreo.Focus();
            }
            else
            {
                string correoElectronico = textBoxCorreo.Text;
                string password = HashearContraseña(passwordBoxContraseña.Password);
                ServicioAServidor.AdminiUsuariosClient cliente = new ServicioAServidor.AdminiUsuariosClient();
                Boolean regisrado = false;
                try
                {
                    regisrado = cliente.IniciarSesion(correoElectronico, password);
                }
                catch (TimeoutException)
                {
                    AvisoErrorTiempoAgotado();
                }
                catch (CommunicationException ex)
                {
                    AvisoDeErrorConElServidor();
                }
                catch (EntityException)
                {
                    AvisoErrorConBaseDeDatos();
                }
                if (regisrado)
                {
                    Jugador jugador = new Jugador();
                    try
                    {
                        jugador = cliente.RecuperarJugadorPorCorreo(correoElectronico);
                    }
                    catch (TimeoutException)
                    {
                        AvisoErrorTiempoAgotado();
                    }
                    catch (ArgumentNullException)
                    {
                        mensajeError.Text = Properties.Idiomas.Resources.ErrorDatosDuplicados;
                    }
                    catch (EntityException)
                    {
                        AvisoErrorConBaseDeDatos();
                    }
                    catch (CommunicationException ex)
                    {
                        AvisoDeErrorConElServidor();
                    }
                    bool iniciar = false;
                    if (jugador != null)
                    {
                        if(jugador.Apodo != "" || jugador.Apodo != null)
                        {
                            iniciar = true;
                            jugadorPartida = jugador;
                            Application.Current.MainWindow.Closing += CerrandoVentana;
                            InstanceContext context = new InstanceContext(this);
                            ServicioAServidor.AdminiSocialClient clienteJoin = new ServicioAServidor.AdminiSocialClient(context);
                            try
                            {
                                clienteJoin.Conectado(jugador);
                            }
                            catch (TimeoutException)
                            {
                                AvisoErrorTiempoAgotado();
                                NavigationService.Refresh();
                            }
                            catch (CommunicationException)
                            {
                                AvisoDeErrorConElServidor();
                                NavigationService.Refresh();
                            }

                        }

                    }
                    if (iniciar) {
                        NavigationService.Navigate(new Lobby(jugador));
                    }
                     
                }
                else
                {
                    MessageBox.Show(Properties.Idiomas.Resources.correoInvalido);
                }
            }
        }

        private void ButtonRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registro());
        }



        private void CerrandoVentana(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string mensaje = Properties.Idiomas.Resources.salirDelJuego;
            MessageBoxResult result = MessageBox.Show(mensaje, Properties.Idiomas.Resources.salir, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if(jugadorPartida != null)
                {
                    try
                    {
                        InstanceContext context = new InstanceContext(this);
                        ServicioAServidor.AdminiSocialClient cliente = new ServicioAServidor.AdminiSocialClient(context);
                        cliente.CerrarJuego(jugadorPartida.Apodo);
                    }
                    catch (EndpointNotFoundException)
                    {
                        
                    }
                }
                
            }
        }

        private void AvisoDeErrorConElServidor()
        {
            MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
        }
        private void AvisoErrorTiempoAgotado()
        {
            MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
        }
        private void AvisoErrorConBaseDeDatos()
        {
            MessageBox.Show(Properties.Idiomas.Resources.ErrorConexionServidor);
        }

        private string HashearContraseña(string contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder contraseñaHasheada = new StringBuilder();
                for (int i = 0; i < (bytes.Length); i++)
                {
                    contraseñaHasheada.Append(bytes[i].ToString("x2"));
                }
                return contraseñaHasheada.ToString();
            }
        }

        private void ButtonIniviarComoInvitado_Click(object sender, RoutedEventArgs e)
        {
            Jugador jugador = new Jugador();
            jugador.Apodo = GenerarApodo();
            jugador.Contraseña = "";
            jugador.CorreoElectronico = jugador.Apodo + "@invitado.com";
            jugador.IdJuego = -1;

            jugadorPartida = jugador;
            Application.Current.MainWindow.Closing += CerrandoVentana;
            InstanceContext context = new InstanceContext(this);
            ServicioAServidor.AdminiSocialClient clienteJoin = new ServicioAServidor.AdminiSocialClient(context);
            bool iniciar = false;
            try
            {
                iniciar = true;
                clienteJoin.Conectado(jugador);
            }
            catch (TimeoutException)
            {
                iniciar = false;
                AvisoErrorTiempoAgotado();
                NavigationService.Refresh();
            }
            catch (CommunicationException)
            {
                iniciar = false;
                AvisoDeErrorConElServidor();
                NavigationService.Refresh();
            }
            if (iniciar)
            {
                NavigationService.Navigate(new Lobby(jugador));
            }
        }

        private string GenerarApodo()
        {
            Random r = new Random();
            String codigo = "";
            for (int i = 0; i < 5; i++)
            {
                int numero = r.Next(0, 10);
                string numeroEnString = numero.ToString();
                codigo += numeroEnString;
            }
            return "Invitado" + codigo;
        }
    }
}
