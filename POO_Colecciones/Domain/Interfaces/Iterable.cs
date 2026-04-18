namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz Iterable (modelo de cátedra).
    /// Una colección que implementa Iterable puede proveer un Iterator propio.
    /// </summary>
    public interface Iterable
    {
        /// <summary>
        /// Crea y retorna un iterador listo para recorrer la colección.
        /// El iterador queda posicionado antes del primer elemento;
        /// llame a Primero() para iniciar el recorrido.
        /// </summary>
        Iterator CrearIterador();
    }
}
