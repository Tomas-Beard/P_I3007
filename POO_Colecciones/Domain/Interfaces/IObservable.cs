namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del patrón Observer — rol "Observado" (publicador/sujeto).
    /// Define el protocolo para registrar, quitar y notificar observadores.
    ///
    /// Modelo de cátedra:
    ///   • Notificar() no recibe parámetros.
    ///   • El Observado expone su estado mediante getters (ej: GetEstado()).
    ///   • El Observador consulta ese estado en su propio Actualizar().
    /// </summary>
    public interface IObservado
    {
        /// <summary>Registra un observador en la lista de suscriptores.</summary>
        void AgregarObservador(IObservador o);

        /// <summary>Elimina un observador de la lista de suscriptores.</summary>
        void QuitarObservador(IObservador o);

        /// <summary>
        /// Notifica a todos los observadores registrados pasándose a sí mismo.
        /// No lleva datos extra: el observador lee el estado con getters.
        /// </summary>
        void Notificar();
    }
}
