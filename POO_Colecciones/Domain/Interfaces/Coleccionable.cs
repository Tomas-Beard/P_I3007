namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz que define el comportamiento de una colección genérica de objetos Comparable.
    /// Permite agregar, consultar, buscar mínimos y máximos de forma polimórfica.
    /// </summary>
    public interface Coleccionable
    {
        /// <summary>Retorna la cantidad de elementos en la colección.</summary>
        int Cuantos();

        /// <summary>Retorna el elemento mínimo de la colección.</summary>
        IComp Minimo();

        /// <summary>Retorna el elemento máximo de la colección.</summary>
        IComp Maximo();

        /// <summary>Agrega un elemento a la colección.</summary>
        void Agregar(IComp obj);

        /// <summary>Retorna true si la colección contiene el elemento indicado.</summary>
        bool Contiene(IComp obj);
    }
}
