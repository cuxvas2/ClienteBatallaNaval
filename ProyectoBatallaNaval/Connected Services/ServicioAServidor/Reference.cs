﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoBatallaNaval.ServicioAServidor {
    using System.Runtime.Serialization;
    using System;
    using System.ServiceModel;

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Jugador", Namespace="http://schemas.datacontract.org/2004/07/Entidades")]
    [System.SerializableAttribute()]
    public partial class Jugador : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ApodoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContraseñaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CorreoElectronicoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdJuegoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdJugadorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Apodo {
            get {
                return this.ApodoField;
            }
            set {
                if ((object.ReferenceEquals(this.ApodoField, value) != true)) {
                    this.ApodoField = value;
                    this.RaisePropertyChanged("Apodo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Contraseña {
            get {
                return this.ContraseñaField;
            }
            set {
                if ((object.ReferenceEquals(this.ContraseñaField, value) != true)) {
                    this.ContraseñaField = value;
                    this.RaisePropertyChanged("Contraseña");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CorreoElectronico {
            get {
                return this.CorreoElectronicoField;
            }
            set {
                if ((object.ReferenceEquals(this.CorreoElectronicoField, value) != true)) {
                    this.CorreoElectronicoField = value;
                    this.RaisePropertyChanged("CorreoElectronico");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdJuego {
            get {
                return this.IdJuegoField;
            }
            set {
                if ((this.IdJuegoField.Equals(value) != true)) {
                    this.IdJuegoField = value;
                    this.RaisePropertyChanged("IdJuego");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdJugador {
            get {
                return this.IdJugadorField;
            }
            set {
                if ((this.IdJugadorField.Equals(value) != true)) {
                    this.IdJugadorField = value;
                    this.RaisePropertyChanged("IdJugador");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Chat", Namespace="http://schemas.datacontract.org/2004/07/Entidades")]
    [System.SerializableAttribute()]
    public partial class Chat : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MensajeEnviadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemitenteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SalaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MensajeEnviado {
            get {
                return this.MensajeEnviadoField;
            }
            set {
                if ((object.ReferenceEquals(this.MensajeEnviadoField, value) != true)) {
                    this.MensajeEnviadoField = value;
                    this.RaisePropertyChanged("MensajeEnviado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Remitente {
            get {
                return this.RemitenteField;
            }
            set {
                if ((object.ReferenceEquals(this.RemitenteField, value) != true)) {
                    this.RemitenteField = value;
                    this.RaisePropertyChanged("Remitente");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sala {
            get {
                return this.SalaField;
            }
            set {
                if ((object.ReferenceEquals(this.SalaField, value) != true)) {
                    this.SalaField = value;
                    this.RaisePropertyChanged("Sala");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioAServidor.IAdminiUsuarios")]
    public interface IAdminiUsuarios {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/iniciarSesion", ReplyAction="http://tempuri.org/IAdminiUsuarios/iniciarSesionResponse")]
        bool iniciarSesion(string usuario, string contraseña);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/iniciarSesion", ReplyAction="http://tempuri.org/IAdminiUsuarios/iniciarSesionResponse")]
        System.Threading.Tasks.Task<bool> iniciarSesionAsync(string usuario, string contraseña);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/registarUsuario", ReplyAction="http://tempuri.org/IAdminiUsuarios/registarUsuarioResponse")]
        bool registarUsuario(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/registarUsuario", ReplyAction="http://tempuri.org/IAdminiUsuarios/registarUsuarioResponse")]
        System.Threading.Tasks.Task<bool> registarUsuarioAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/cambiarContraseña", ReplyAction="http://tempuri.org/IAdminiUsuarios/cambiarContraseñaResponse")]
        bool cambiarContraseña(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/cambiarContraseña", ReplyAction="http://tempuri.org/IAdminiUsuarios/cambiarContraseñaResponse")]
        System.Threading.Tasks.Task<bool> cambiarContraseñaAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/recuperarJugadorPorCorreo", ReplyAction="http://tempuri.org/IAdminiUsuarios/recuperarJugadorPorCorreoResponse")]
        ProyectoBatallaNaval.ServicioAServidor.Jugador recuperarJugadorPorCorreo(string correoElectronico);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiUsuarios/recuperarJugadorPorCorreo", ReplyAction="http://tempuri.org/IAdminiUsuarios/recuperarJugadorPorCorreoResponse")]
        System.Threading.Tasks.Task<ProyectoBatallaNaval.ServicioAServidor.Jugador> recuperarJugadorPorCorreoAsync(string correoElectronico);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminiUsuariosChannel : ProyectoBatallaNaval.ServicioAServidor.IAdminiUsuarios, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdminiUsuariosClient : System.ServiceModel.ClientBase<ProyectoBatallaNaval.ServicioAServidor.IAdminiUsuarios>, ProyectoBatallaNaval.ServicioAServidor.IAdminiUsuarios {
        
        public AdminiUsuariosClient() {
        }
        
        public AdminiUsuariosClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AdminiUsuariosClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminiUsuariosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AdminiUsuariosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool iniciarSesion(string usuario, string contraseña) {
            return base.Channel.iniciarSesion(usuario, contraseña);
        }
        
        public System.Threading.Tasks.Task<bool> iniciarSesionAsync(string usuario, string contraseña) {
            return base.Channel.iniciarSesionAsync(usuario, contraseña);
        }
        
        public bool registarUsuario(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.registarUsuario(jugador);
        }
        
        public System.Threading.Tasks.Task<bool> registarUsuarioAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.registarUsuarioAsync(jugador);
        }
        
        public bool cambiarContraseña(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.cambiarContraseña(jugador);
        }
        
        public System.Threading.Tasks.Task<bool> cambiarContraseñaAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.cambiarContraseñaAsync(jugador);
        }
        
        public ProyectoBatallaNaval.ServicioAServidor.Jugador recuperarJugadorPorCorreo(string correoElectronico) {
            return base.Channel.recuperarJugadorPorCorreo(correoElectronico);
        }
        
        public System.Threading.Tasks.Task<ProyectoBatallaNaval.ServicioAServidor.Jugador> recuperarJugadorPorCorreoAsync(string correoElectronico) {
            return base.Channel.recuperarJugadorPorCorreoAsync(correoElectronico);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioAServidor.IAdminiSocial", CallbackContract=typeof(ProyectoBatallaNaval.ServicioAServidor.IAdminiSocialCallback))]
    public interface IAdminiSocial {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/Conectado")]
        void Conectado(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/Conectado")]
        System.Threading.Tasks.Task ConectadoAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/desconectado")]
        void desconectado(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/desconectado")]
        System.Threading.Tasks.Task desconectadoAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/enviarMensaje")]
        void enviarMensaje(ProyectoBatallaNaval.ServicioAServidor.Chat mensaje);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/enviarMensaje")]
        System.Threading.Tasks.Task enviarMensajeAsync(ProyectoBatallaNaval.ServicioAServidor.Chat mensaje);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/crearSala")]
        void crearSala(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/crearSala")]
        System.Threading.Tasks.Task crearSalaAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/unirseASala")]
        void unirseASala(string sala, ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/unirseASala")]
        System.Threading.Tasks.Task unirseASalaAsync(string sala, ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/todoListo")]
        void todoListo(string sala, string jugador, int numeroDeListos);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/todoListo")]
        System.Threading.Tasks.Task todoListoAsync(string sala, string jugador, int numeroDeListos);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/cancelarTodoListo")]
        void cancelarTodoListo(string sala, string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/cancelarTodoListo")]
        System.Threading.Tasks.Task cancelarTodoListoAsync(string sala, string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/EliminarSala")]
        void EliminarSala(string codigoSala);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/EliminarSala")]
        System.Threading.Tasks.Task EliminarSalaAsync(string codigoSala);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/Tiro")]
        void Tiro(string coordenadas, string contricante, string sala, string NombreJugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/Tiro")]
        System.Threading.Tasks.Task TiroAsync(string coordenadas, string contricante, string sala, string NombreJugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/IniciarPartida")]
        void IniciarPartida(string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/IniciarPartida")]
        System.Threading.Tasks.Task IniciarPartidaAsync(string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/TerminarPartida")]
        void TerminarPartida(string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/TerminarPartida")]
        System.Threading.Tasks.Task TerminarPartidaAsync(string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/PrimerTiro")]
        void PrimerTiro(string jugador1, string jugador2);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/PrimerTiro")]
        System.Threading.Tasks.Task PrimerTiroAsync(string jugador1, string jugador2);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/ActualizarCallbackEnPartida")]
        void ActualizarCallbackEnPartida(string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/ActualizarCallbackEnPartida")]
        System.Threading.Tasks.Task ActualizarCallbackEnPartidaAsync(string jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/PartidaGanada")]
        void PartidaGanada(string janador, string jugadorParaNotificar);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/PartidaGanada")]
        System.Threading.Tasks.Task PartidaGanadaAsync(string janador, string jugadorParaNotificar);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/TiroCertero")]
        void TiroCertero(string coordenadas, string contricante);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/TiroCertero")]
        System.Threading.Tasks.Task TiroCerteroAsync(string coordenadas, string contricante);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/CerrarJuego")]
        void CerrarJuego(string nombreJugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/CerrarJuego")]
        System.Threading.Tasks.Task CerrarJuegoAsync(string nombreJugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/ExpulsarDeSala")]
        void ExpulsarDeSala(string sala);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/ExpulsarDeSala")]
        System.Threading.Tasks.Task ExpulsarDeSalaAsync(string sala);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminiSocialCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/actualizarJugadores")]
        void actualizarJugadores(ProyectoBatallaNaval.ServicioAServidor.Jugador[] jugadores);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/unionDeJugador")]
        void unionDeJugador(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/jugadorSeFue")]
        void jugadorSeFue(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAdminiSocial/escribiendoEnCallback")]
        void escribiendoEnCallback(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/recibirMensaje", ReplyAction="http://tempuri.org/IAdminiSocial/recibirMensajeResponse")]
        void recibirMensaje(ProyectoBatallaNaval.ServicioAServidor.Chat respuesta);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/recibirCodigoSala", ReplyAction="http://tempuri.org/IAdminiSocial/recibirCodigoSalaResponse")]
        void recibirCodigoSala(string codigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/jugadorSeUnio", ReplyAction="http://tempuri.org/IAdminiSocial/jugadorSeUnioResponse")]
        void jugadorSeUnio(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador, string sala, bool seUnio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/recibirTodoListo", ReplyAction="http://tempuri.org/IAdminiSocial/recibirTodoListoResponse")]
        void recibirTodoListo(string contricante);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/recibirTodoListoParaIniciar", ReplyAction="http://tempuri.org/IAdminiSocial/recibirTodoListoParaIniciarResponse")]
        void recibirTodoListoParaIniciar(string contricante);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/recibirCancelarListo", ReplyAction="http://tempuri.org/IAdminiSocial/recibirCancelarListoResponse")]
        void recibirCancelarListo(string contricante);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/insertarDisparo", ReplyAction="http://tempuri.org/IAdminiSocial/insertarDisparoResponse")]
        void insertarDisparo(string coordenadas);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/IniciarPartidaCallback", ReplyAction="http://tempuri.org/IAdminiSocial/IniciarPartidaCallbackResponse")]
        void IniciarPartidaCallback(bool inicar);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/primerTiroCallback", ReplyAction="http://tempuri.org/IAdminiSocial/primerTiroCallbackResponse")]
        void primerTiroCallback(bool iniciar);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/PartidaGanadaCallback", ReplyAction="http://tempuri.org/IAdminiSocial/PartidaGanadaCallbackResponse")]
        void PartidaGanadaCallback(string jugadorGanado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/ActualizarCallbackEnPartidaCallback", ReplyAction="http://tempuri.org/IAdminiSocial/ActualizarCallbackEnPartidaCallbackResponse")]
        void ActualizarCallbackEnPartidaCallback(bool actualizado);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/TiroCerteroCallback", ReplyAction="http://tempuri.org/IAdminiSocial/TiroCerteroCallbackResponse")]
        void TiroCerteroCallback(string coordenadas);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAdminiSocial/RecibirExpulsacion", ReplyAction="http://tempuri.org/IAdminiSocial/RecibirExpulsacionResponse")]
        void RecibirExpulsacion();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAdminiSocialChannel : ProyectoBatallaNaval.ServicioAServidor.IAdminiSocial, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AdminiSocialClient : System.ServiceModel.DuplexClientBase<ProyectoBatallaNaval.ServicioAServidor.IAdminiSocial>, ProyectoBatallaNaval.ServicioAServidor.IAdminiSocial {
        
        public AdminiSocialClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public AdminiSocialClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public AdminiSocialClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AdminiSocialClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AdminiSocialClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Conectado(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            base.Channel.Conectado(jugador);
        }
        
        public System.Threading.Tasks.Task ConectadoAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.ConectadoAsync(jugador);
        }
        
        public void desconectado(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            base.Channel.desconectado(jugador);
        }
        
        public System.Threading.Tasks.Task desconectadoAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.desconectadoAsync(jugador);
        }
        
        public void enviarMensaje(ProyectoBatallaNaval.ServicioAServidor.Chat mensaje) {
            base.Channel.enviarMensaje(mensaje);
        }
        
        public System.Threading.Tasks.Task enviarMensajeAsync(ProyectoBatallaNaval.ServicioAServidor.Chat mensaje) {
            return base.Channel.enviarMensajeAsync(mensaje);
        }
        
        public void crearSala(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            base.Channel.crearSala(jugador);
        }
        
        public System.Threading.Tasks.Task crearSalaAsync(ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.crearSalaAsync(jugador);
        }
        
        public void unirseASala(string sala, ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            base.Channel.unirseASala(sala, jugador);
        }
        
        public System.Threading.Tasks.Task unirseASalaAsync(string sala, ProyectoBatallaNaval.ServicioAServidor.Jugador jugador) {
            return base.Channel.unirseASalaAsync(sala, jugador);
        }
        
        public void todoListo(string sala, string jugador, int numeroDeListos) {
            base.Channel.todoListo(sala, jugador, numeroDeListos);
        }
        
        public System.Threading.Tasks.Task todoListoAsync(string sala, string jugador, int numeroDeListos) {
            return base.Channel.todoListoAsync(sala, jugador, numeroDeListos);
        }
        
        public void cancelarTodoListo(string sala, string jugador) {
            base.Channel.cancelarTodoListo(sala, jugador);
        }
        
        public System.Threading.Tasks.Task cancelarTodoListoAsync(string sala, string jugador) {
            return base.Channel.cancelarTodoListoAsync(sala, jugador);
        }
        
        public void EliminarSala(string codigoSala) {
            base.Channel.EliminarSala(codigoSala);
        }
        
        public System.Threading.Tasks.Task EliminarSalaAsync(string codigoSala) {
            return base.Channel.EliminarSalaAsync(codigoSala);
        }
        
        public void Tiro(string coordenadas, string contricante, string sala, string NombreJugador) {
            base.Channel.Tiro(coordenadas, contricante, sala, NombreJugador);
        }
        
        public System.Threading.Tasks.Task TiroAsync(string coordenadas, string contricante, string sala, string NombreJugador) {
            return base.Channel.TiroAsync(coordenadas, contricante, sala, NombreJugador);
        }
        
        public void IniciarPartida(string jugador) {
            base.Channel.IniciarPartida(jugador);
        }
        
        public System.Threading.Tasks.Task IniciarPartidaAsync(string jugador) {
            return base.Channel.IniciarPartidaAsync(jugador);
        }
        
        public void TerminarPartida(string jugador) {
            base.Channel.TerminarPartida(jugador);
        }
        
        public System.Threading.Tasks.Task TerminarPartidaAsync(string jugador) {
            return base.Channel.TerminarPartidaAsync(jugador);
        }
        
        public void PrimerTiro(string jugador1, string jugador2) {
            base.Channel.PrimerTiro(jugador1, jugador2);
        }
        
        public System.Threading.Tasks.Task PrimerTiroAsync(string jugador1, string jugador2) {
            return base.Channel.PrimerTiroAsync(jugador1, jugador2);
        }
        
        public void ActualizarCallbackEnPartida(string jugador) {
            base.Channel.ActualizarCallbackEnPartida(jugador);
        }
        
        public System.Threading.Tasks.Task ActualizarCallbackEnPartidaAsync(string jugador) {
            return base.Channel.ActualizarCallbackEnPartidaAsync(jugador);
        }
        
        public void PartidaGanada(string janador, string jugadorParaNotificar) {
            base.Channel.PartidaGanada(janador, jugadorParaNotificar);
        }
        
        public System.Threading.Tasks.Task PartidaGanadaAsync(string janador, string jugadorParaNotificar) {
            return base.Channel.PartidaGanadaAsync(janador, jugadorParaNotificar);
        }
        
        public void TiroCertero(string coordenadas, string contricante) {
            base.Channel.TiroCertero(coordenadas, contricante);
        }
        
        public System.Threading.Tasks.Task TiroCerteroAsync(string coordenadas, string contricante) {
            return base.Channel.TiroCerteroAsync(coordenadas, contricante);
        }
        
        public void CerrarJuego(string nombreJugador) {
            base.Channel.CerrarJuego(nombreJugador);
        }
        
        public System.Threading.Tasks.Task CerrarJuegoAsync(string nombreJugador) {
            return base.Channel.CerrarJuegoAsync(nombreJugador);
        }
        
        public void ExpulsarDeSala(string sala) {
            base.Channel.ExpulsarDeSala(sala);
        }
        
        public System.Threading.Tasks.Task ExpulsarDeSalaAsync(string sala) {
            return base.Channel.ExpulsarDeSalaAsync(sala);
        }
    }
}
