namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define la capacidad de comparación entre objetos.
    /// Permite implementar igualdad, menor y mayor de forma polimórfica.
    /// </summary>
    public interface Comparable
    {
        /// <summary>Retorna true si este objeto es igual al recibido.</summary>
        bool SosIgual(Comparable obj);

        /// <summary>Retorna true si este objeto es menor al recibido.</summary>
        bool SosMenor(Comparable obj);

        /// <summary>Retorna true si este objeto es mayor al recibido.</summary>
        bool SosMayor(Comparable obj);
    }
}
