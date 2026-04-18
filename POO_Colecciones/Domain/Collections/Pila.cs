using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Iterators;

namespace POO_Colecciones.Domain.Collections
{
    /// <summary>
    /// Colección LIFO (Last In, First Out) que implementa IColeccionable.
    /// Internamente usa Stack&lt;IComp&gt; de .NET.
    /// </summary>
    public class Pila : Coleccionable, Iterable
    {
        // ─── Estructura interna ─────────────────────────────────────────────
        private readonly Stack<IComp> _pila = new();

        // ─── IColeccionable ─────────────────────────────────────────────────

        public int Cuantos() => _pila.Count;

        public void Agregar(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            _pila.Push(obj);
        }

        /// <summary>Retorna el mínimo recorriendo todos los elementos.</summary>
        public IComp Minimo()
        {
            if (_pila.Count == 0)
                throw new InvalidOperationException("La pila está vacía.");

            IComp min = _pila.Peek();
            foreach (IComp elemento in _pila)
                if (elemento.SosMenor(min))
                    min = elemento;
            return min;
        }

        /// <summary>Retorna el máximo recorriendo todos los elementos.</summary>
        public IComp Maximo()
        {
            if (_pila.Count == 0)
                throw new InvalidOperationException("La pila está vacía.");

            IComp max = _pila.Peek();
            foreach (IComp elemento in _pila)
                if (elemento.SosMayor(max))
                    max = elemento;
            return max;
        }

        public bool Contiene(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            foreach (IComp elemento in _pila)
                if (elemento.SosIgual(obj))
                    return true;
            return false;
        }

        /// <summary>Busca un Alumno por legajo. Retorna null si no lo encuentra.</summary>
        public Alumno? BuscarAlumno(int legajo)
        {
            foreach (IComp elemento in _pila)
                if (elemento is Alumno a && a.GetLegajo() == legajo)
                    return a;
            return null;
        }

        public override string ToString() => $"Pila [{Cuantos()} elemento(s)]";

        // ─── IIterable ──────────────────────────────────────────────────────

        /// <summary>
        /// Retorna un iterador que recorre la pila de cima a base (orden LIFO).
        /// La iteración opera sobre una instantánea, no sobre la estructura viva.
        /// </summary>
        public Iterator CrearIterador() => new PilaIterator(_pila);
    }
}
