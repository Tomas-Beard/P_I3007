using POO_Colecciones.Domain.Command;
using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Iterators;

namespace POO_Colecciones.Domain.Collections
{
    /// <summary>
    /// Colección LIFO (Last In, First Out) que implementa IColeccionable e Iterable.
    /// Internamente usa Stack&lt;IComp&gt; de .NET.
    ///
    /// P5 — Implementa Ordenable: permite inyectar comandos Command que se
    /// disparan en momentos clave sin que Pila conozca la lógica concreta del Aula.
    /// </summary>
    public class Pila : Coleccionable, Iterable, Ordenable
    {
        // ─── Estructura interna ─────────────────────────────────────────────
        private readonly Stack<IComp> _pila = new();

        // ─── P5: Comandos inyectados (Patrón Command) ────────────────────────
        private OrdenEnAula1? _ordenInicio;
        private OrdenEnAula2? _ordenLlegaAlumno;
        private OrdenEnAula1? _ordenAulaLlena;

        // ─── P5: Implementación de Ordenable ────────────────────────────────
        public void setOrdenInicio(OrdenEnAula1 orden) => _ordenInicio = orden;
        public void setOrdenLlegaAlumno(OrdenEnAula2 orden) => _ordenLlegaAlumno = orden;
        public void setOrdenAulaLlena(OrdenEnAula1 orden) => _ordenAulaLlena = orden;

        // ─── IColeccionable ─────────────────────────────────────────────────

        public int Cuantos() => _pila.Count;

        /// <summary>
        /// Agrega un elemento a la pila y dispara los comandos correspondientes:
        ///  - 1er elemento   → OrdenInicio + OrdenLlegaAlumno(obj)
        ///  - Cualquier otro → OrdenLlegaAlumno(obj)
        ///  - Al llegar a 40 → OrdenAulaLlena()
        /// </summary>
        public void Agregar(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            // ── ¿Es el primer elemento? → disparar inicio ──────────────────
            bool esPrimero = _pila.Count == 0;
            if (esPrimero)
                _ordenInicio?.ejecutar();

            // ── Insertar en la pila ────────────────────────────────────────
            _pila.Push(obj);

            // ── Notificar llegada del alumno ───────────────────────────────
            _ordenLlegaAlumno?.ejecutar(obj);

            // ── ¿Se llegó a 40 elementos? → disparar aula llena ───────────
            if (_pila.Count == 40)
                _ordenAulaLlena?.ejecutar();
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