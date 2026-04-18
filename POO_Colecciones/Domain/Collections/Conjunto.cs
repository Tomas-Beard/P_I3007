using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Iterators;

namespace POO_Colecciones.Domain.Collections
{
    /// <summary>
    /// Colección que no admite elementos repetidos.
    /// La unicidad se verifica mediante SosIgual() — el criterio depende de la
    /// estrategia de comparación activa en cada elemento (patrón Strategy).
    /// Implementa tanto Coleccionable como Iterable (patrón Iterator).
    /// El orden de iteración es el orden de inserción.
    /// </summary>
    public class Conjunto : Coleccionable, Iterable
    {
        // ─── Estructura interna ─────────────────────────────────────────────
        private readonly List<IComp> _elementos = new();

        // ─── Coleccionable ──────────────────────────────────────────────────

        public int Cuantos() => _elementos.Count;

        /// <summary>
        /// Agrega el elemento sólo si no existe un equivalente (según SosIgual).
        /// Si el elemento ya existe, la operación se ignora silenciosamente.
        /// </summary>
        public void Agregar(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            if (!Contiene(obj))
                _elementos.Add(obj);
        }

        /// <summary>Retorna el elemento mínimo según el criterio de comparación activo.</summary>
        public IComp Minimo()
        {
            if (_elementos.Count == 0)
                throw new InvalidOperationException("El conjunto está vacío.");

            IComp min = _elementos[0];
            foreach (IComp elem in _elementos)
                if (elem.SosMenor(min)) min = elem;
            return min;
        }

        /// <summary>Retorna el elemento máximo según el criterio de comparación activo.</summary>
        public IComp Maximo()
        {
            if (_elementos.Count == 0)
                throw new InvalidOperationException("El conjunto está vacío.");

            IComp max = _elementos[0];
            foreach (IComp elem in _elementos)
                if (elem.SosMayor(max)) max = elem;
            return max;
        }

        /// <summary>Retorna true si existe un elemento igual según SosIgual().</summary>
        public bool Contiene(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);
            foreach (IComp elem in _elementos)
                if (elem.SosIgual(obj)) return true;
            return false;
        }

        // ─── IIterable ──────────────────────────────────────────────────────

        /// <summary>Retorna un iterador que recorre los elementos en orden de inserción.</summary>
        public Iterator CrearIterador() => new ConjuntoIterator(_elementos);

        // ─── ToString ───────────────────────────────────────────────────────

        public override string ToString() => $"Conjunto [{Cuantos()} elemento(s) único(s)]";
    }
}
