using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Iterators
{
    /// <summary>
    /// Iterador concreto para Cola (FIFO).
    /// Trabaja sobre una instantánea tomada al momento de CrearIterador().
    /// Orden de recorrido: frente → fondo.
    /// </summary>
    public class ColaIterator : Iterator
    {
        private readonly IComp[] _snapshot;
        private int _cursor;

        public ColaIterator(IEnumerable<IComp> elementos)
        {
            _snapshot = elementos.ToArray();
            _cursor   = 0;
        }

        /// <summary>Posiciona el cursor en el frente de la cola.</summary>
        public void Primero()   => _cursor = 0;

        /// <summary>Avanza el cursor al siguiente elemento.</summary>
        public void Siguiente() => _cursor++;

        /// <summary>Retorna true cuando el cursor superó el último elemento.</summary>
        public bool Fin()       => _cursor >= _snapshot.Length;

        /// <summary>Retorna el elemento en la posición actual del cursor.</summary>
        public IComp Actual()
        {
            if (Fin())
                throw new InvalidOperationException("El iterador de Cola ha llegado al final.");
            return _snapshot[_cursor];
        }
    }
}
