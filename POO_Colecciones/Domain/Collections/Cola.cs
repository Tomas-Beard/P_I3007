using POO_Colecciones.Domain.Command;
using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Iterators;

namespace POO_Colecciones.Domain.Collections
{
    /// <summary>
    /// Colección FIFO (First In, First Out) que implementa IColeccionable e Iterable.
    /// Internamente usa Queue&lt;IComp&gt; de .NET.
    ///
    /// P5 — Implementa Ordenable: permite inyectar comandos Command que se
    /// disparan en momentos clave sin que Cola conozca la lógica concreta del Aula.
    /// </summary>
    public class Cola : Coleccionable, Iterable, Ordenable
    {
        // ─── Estructura interna ─────────────────────────────────────────────
        private readonly Queue<IComp> _cola = new();

        // ─── P5: Comandos inyectados (Patrón Command) ────────────────────────
        private OrdenEnAula1? _ordenInicio;
        private OrdenEnAula2? _ordenLlegaAlumno;
        private OrdenEnAula1? _ordenAulaLlena;

        // ─── P5: Implementación de Ordenable ────────────────────────────────
        public void setOrdenInicio(OrdenEnAula1 orden) => _ordenInicio = orden;
        public void setOrdenLlegaAlumno(OrdenEnAula2 orden) => _ordenLlegaAlumno = orden;
        public void setOrdenAulaLlena(OrdenEnAula1 orden) => _ordenAulaLlena = orden;

        // ─── IColeccionable ─────────────────────────────────────────────────

        public int Cuantos() => _cola.Count;

        /// <summary>
        /// Agrega un elemento a la cola y dispara los comandos correspondientes:
        ///  - 1er elemento   → OrdenInicio + OrdenLlegaAlumno(obj)
        ///  - Cualquier otro → OrdenLlegaAlumno(obj)
        ///  - Al llegar a 40 → OrdenAulaLlena()
        /// </summary>
        public void Agregar(IComp obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            // ── ¿Es el primer elemento? → disparar inicio ──────────────────
            bool esPrimero = _cola.Count == 0;
            if (esPrimero)
                _ordenInicio?.ejecutar();

            // ── Insertar en la cola ────────────────────────────────────────
            _cola.Enqueue(obj);

            // ── Notificar llegada del alumno ───────────────────────────────
            _ordenLlegaAlumno?.ejecutar(obj);

            // ── ¿Se llegó a 40 elementos? → disparar aula llena ───────────
            if (_cola.Count == 40)
                _ordenAulaLlena?.ejecutar();
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