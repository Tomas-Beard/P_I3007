namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del patrón Observer — rol "Observador" (suscriptor).
    /// El observador recibe una referencia al IObservado y consulta
    /// su estado por sí mismo: NO se pasan datos como parámetro.
    /// </summary>
    public interface IObservador
    {
        /// <summary>
        /// Invocado por el IObservado cuando cambia su estado.
        /// El observador debe hacer cast al tipo concreto para leer el estado.
        /// </summary>
        /// <param name="observado">El objeto que disparó la notificación.</param>
        void Actualizar(IObservado observado);
    }
}
