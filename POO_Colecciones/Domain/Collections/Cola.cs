using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Iterators;

namespace POO_Colecciones.Domain.Collections
{
    /// <summary>
    /// Colección FIFO (First In, First Out) que implementa IColeccionable.
    /// Internamente usa Queue&lt;IComp&gt; de .NET.
    /// </summary>
    public class Cola : Coleccionable, Iterable
    {
        // ─── Estructura interna ─────────────────────────────────────────────
        private readonly Queue<IComp> _cola = new();

        // ─── IColeccionable ─────────────────────────────────────────────────

        public int Cuantos() => _cola.Count;

        public void Agregar(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            _cola.Enqueue(obj);
        }

        /// <summary>Retorna el mínimo recorriendo todos los elementos.</summary>
        public IComp Minimo()
        {
            if (_cola.Count == 0)
                throw new InvalidOperationException("La cola está vacía.");

            IComp min = _cola.Peek();
            foreach (IComp elemento in _cola)
                if (elemento.SosMenor(min))
                    min = elemento;
            return min;
        }

        /// <summary>Retorna el máximo recorriendo todos los elementos.</summary>
        public IComp Maximo()
        {
            if (_cola.Count == 0)
                throw new InvalidOperationException("La cola está vacía.");

            IComp max = _cola.Peek();
            foreach (IComp elemento in _cola)
                if (elemento.SosMayor(max))
                    max = elemento;
            return max;
        }

        public bool Contiene(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            foreach (IComp elemento in _cola)
                if (elemento.SosIgual(obj))
                    return true;
            return false;
        }

        /// <summary>Busca un Alumno por legajo. Retorna null si no lo encuentra.</summary>
        public Alumno? BuscarAlumno(int legajo)
        {
            foreach (IComp elemento in _cola)
                if (elemento is Alumno a && a.GetLegajo() == legajo)
                    return a;
            return null;
        }

        public override string ToString() => $"Cola [{Cuantos()} elemento(s)]";

        // ─── IIterable ──────────────────────────────────────────────────────

        /// <summary>
        /// Retorna un iterador que recorre la cola de frente a fondo (orden FIFO).
        /// La iteración opera sobre una instantánea, no sobre la estructura viva.
        /// </summary>
        public Iterator CrearIterador() => new ColaIterator(_cola);
    }
}
