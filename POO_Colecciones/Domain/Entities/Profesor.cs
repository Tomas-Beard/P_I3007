using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Entities
{
    /// <summary>
    /// Ej 8 — Representa un profesor universitario.
    /// Hereda de <see cref="Persona"/> (nombre + DNI + IComp).
    ///
    /// Patrón Observer — IObservado :
    ///   • Mantiene lista de observadores (alumnos).
    ///   • Expone su estado actual mediante <see cref="GetEstado()"/>.
    ///   • Notificar() se pasa a sí mismo: el observador consulta el estado.
    ///   • NO envía datos en la notificación.
    ///
    /// Criterio de comparación: por antigüedad (años de servicio).
    /// </summary>
    public class Profesor : Persona, IObservado
    {
        // ─── Atributo propio ────────────────────────────────────────────────
        private readonly int _antiguedad;

        // ─── Estado interno (patrón Observer) ──────────────────────────────
        /// <summary>
        /// Describe la acción que el profesor está realizando en este momento.
        /// El observador lee este valor en su Actualizar() para decidir qué hacer.
        /// </summary>
        private string _estadoActual = string.Empty;

        // ─── Lista de observadores ──────────────────────────────────────────
        private readonly List<IObservador> _observadores = new();

        // ─── Constructor ────────────────────────────────────────────────────
        /// <param name="nombre">Nombre del profesor.</param>
        /// <param name="dni">DNI del profesor.</param>
        /// <param name="antiguedad">Años de antigüedad (debe ser ≥ 0).</param>
        public Profesor(string nombre, int dni, int antiguedad)
            : base(nombre, dni)
        {
            if (antiguedad < 0)
                throw new ArgumentException("La antigüedad no puede ser negativa.", nameof(antiguedad));
            _antiguedad = antiguedad;
        }

        // ─── Getters ────────────────────────────────────────────────────────
        public int GetAntiguedad() => _antiguedad;

        /// <summary>
        /// Retorna el estado actual del profesor.
        /// Los observadores lo consultan desde su Actualizar().
        /// Valores posibles: "hablando" | "escribiendo" | ""
        /// </summary>
        public string GetEstado() => _estadoActual;

        // ═══════════════════════════════════════════════════════════════════
        //  Acciones del profesor (Ej 8 + Ej 12)
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Ej 8 / Ej 12 — El profesor habla a la clase.
        /// Actualiza el estado interno y notifica a todos los observadores.
        /// </summary>
        public void HablarALaClase()
        {
            Console.WriteLine($"  [Prof. {GetNombre()}] Hablando de algún tema.");
            _estadoActual = "hablando";
            Notificar();
        }

        /// <summary>
        /// Ej 8 / Ej 12 — El profesor escribe en el pizarrón.
        /// Actualiza el estado interno y notifica a todos los observadores.
        /// </summary>
        public void EscribirEnElPizarron()
        {
            Console.WriteLine($"  [Prof. {GetNombre()}] Escribiendo en el pizarrón.");
            _estadoActual = "escribiendo";
            Notificar();
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Patrón Observer — IObservado (Ej 12)
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>Agrega un observador a la lista de suscriptores.</summary>
        public void AgregarObservador(IObservador o)
        {
            ArgumentNullException.ThrowIfNull(o);
            if (!_observadores.Contains(o))
                _observadores.Add(o);
        }

        /// <summary>Quita un observador de la lista de suscriptores.</summary>
        public void QuitarObservador(IObservador o)
        {
            ArgumentNullException.ThrowIfNull(o);
            _observadores.Remove(o);
        }

        /// <summary>
        /// Notifica a todos los observadores pasándose a sí mismo como IObservado.
        /// Cada observador consultará GetEstado() para conocer la acción.
        /// Itera sobre una copia para evitar problemas si se modifica la lista.
        /// </summary>
        public void Notificar()
        {
            foreach (IObservador obs in _observadores.ToList())
                obs.Actualizar(this);
        }

        // ─── Redefinición de comparación (Ej 8) ────────────────────────────
        /// <summary>Compara profesores por antigüedad.</summary>
        protected override int CompararCon(IComp obj)
        {
            if (obj is not Profesor otro)
                throw new ArgumentException(
                    $"No se puede comparar Profesor con {obj.GetType().Name}");
            return _antiguedad.CompareTo(otro._antiguedad);
        }

        // ─── Representación textual ─────────────────────────────────────────
        public override string ToString() =>
            $"Profesor {{ Antigüedad: {_antiguedad} año(s) | {base.ToString()} }}";
    }
}
