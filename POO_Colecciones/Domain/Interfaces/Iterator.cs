namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del patrón Iterator (modelo de cátedra).
    /// Define el protocolo de recorrido sin exponer la estructura interna.
    /// </summary>
    public interface Iterator
    {
        /// <summary>Posiciona el cursor en el primer elemento.</summary>
        void Primero();

        /// <summary>Avanza el cursor al siguiente elemento.</summary>
        void Siguiente();

        /// <summary>Retorna true si el cursor superó el último elemento (recorrido finalizado).</summary>
        bool Fin();

        /// <summary>Retorna el elemento en la posición actual del cursor.</summary>
        IComp Actual();
    }
}
