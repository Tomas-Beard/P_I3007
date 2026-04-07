using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Collections
{
    /// <summary>
    /// Colección compuesta que contiene una Pila y una Cola.
    /// Delega las operaciones a ambas sub-colecciones de forma transparente.
    /// Agregar() no hace nada (patrón Null Object para esta operación).
    /// </summary>
    public class ColeccionMultiple : Coleccionable
    {
        // ─── Sub-colecciones ────────────────────────────────────────────────
        private readonly Pila _pila;
        private readonly Cola _cola;

        // ─── Constructor ────────────────────────────────────────────────────
        public ColeccionMultiple(Pila pila, Cola cola)
        {
            _pila = pila ?? throw new ArgumentNullException(nameof(pila));
            _cola = cola ?? throw new ArgumentNullException(nameof(cola));
        }

        // ─── Accesores (para carga directa desde el servicio) ───────────────
        public Pila GetPila() => _pila;
        public Cola GetCola() => _cola;

        // ─── IColeccionable ─────────────────────────────────────────────────

        /// <summary>Suma de los elementos de la pila y la cola.</summary>
        public int Cuantos() => _pila.Cuantos() + _cola.Cuantos();

        /// <summary>
        /// No agrega elementos directamente; la carga se hace sobre
        /// la Pila y la Cola a través de GetPila()/GetCola().
        /// (Null Object para esta colección compuesta)
        /// </summary>
        public void Agregar(IComp obj)
        {
            // Intencionalmente vacío
        }

        /// <summary>Retorna el mínimo entre ambas colecciones.</summary>
        public IComp Minimo()
        {
            bool pilaVacia = _pila.Cuantos() == 0;
            bool colaVacia = _cola.Cuantos() == 0;

            if (pilaVacia && colaVacia)
                throw new InvalidOperationException("Ambas colecciones están vacías.");
            if (pilaVacia) return _cola.Minimo();
            if (colaVacia) return _pila.Minimo();

            IComp minPila = _pila.Minimo();
            IComp minCola = _cola.Minimo();
            return minPila.SosMenor(minCola) ? minPila : minCola;
        }

        /// <summary>Retorna el máximo entre ambas colecciones.</summary>
        public IComp Maximo()
        {
            bool pilaVacia = _pila.Cuantos() == 0;
            bool colaVacia = _cola.Cuantos() == 0;

            if (pilaVacia && colaVacia)
                throw new InvalidOperationException("Ambas colecciones están vacías.");
            if (pilaVacia) return _cola.Maximo();
            if (colaVacia) return _pila.Maximo();

            IComp maxPila = _pila.Maximo();
            IComp maxCola = _cola.Maximo();
            return maxPila.SosMayor(maxCola) ? maxPila : maxCola;
        }

        /// <summary>Retorna true si el elemento está en la pila O en la cola.</summary>
        public bool Contiene(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            return _pila.Contiene(obj) || _cola.Contiene(obj);
        }

        public override string ToString() =>
            $"ColeccionMultiple [{Cuantos()} elemento(s) | {_pila} + {_cola}]";
    }
}
