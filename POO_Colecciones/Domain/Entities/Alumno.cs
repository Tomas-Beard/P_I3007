using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Entities
{
    /// <summary>
    /// Representa un alumno. Hereda de Persona e implementa el patrón Strategy
    /// para la comparación: la lógica de comparación se delega a un objeto
    /// IEstrategiaComparacion intercambiable en tiempo de ejecución.
    /// Si no se asigna estrategia, se usa el criterio original (Legajo + Promedio).
    /// </summary>
    public class Alumno : Persona
    {
        // ─── Atributos propios ──────────────────────────────────────────────
        private readonly int    _legajo;
        private readonly double _promedio;

        // ─── Patrón Strategy ────────────────────────────────────────────────
        /// <summary>
        /// Estrategia de comparación actualmente asignada.
        /// null → se aplica el criterio por defecto (Legajo + Promedio).
        /// </summary>
        private EstrategiaComparacion? _estrategia;

        // ─── Constructor ────────────────────────────────────────────────────
        public Alumno(string nombre, int dni, int legajo, double promedio)
            : base(nombre, dni)
        {
            if (legajo <= 0)
                throw new ArgumentException("El legajo debe ser positivo.");
            if (promedio < 0 || promedio > 10)
                throw new ArgumentException("El promedio debe estar entre 0 y 10.");

            _legajo   = legajo;
            _promedio = promedio;
        }

        // ─── Getters ────────────────────────────────────────────────────────
        public int    GetLegajo()   => _legajo;
        public double GetPromedio() => _promedio;

        // ─── Patrón Strategy: asignación de estrategia ──────────────────────
        /// <summary>
        /// Asigna la estrategia de comparación a usar en SosIgual/SosMenor/SosMayor.
        /// </summary>
        public void SetEstrategia(EstrategiaComparacion estrategia)
        {
            _estrategia = estrategia ?? throw new ArgumentNullException(nameof(estrategia));
        }

        /// <summary>Retorna la estrategia actualmente asignada (puede ser null).</summary>
        public EstrategiaComparacion? GetEstrategia() => _estrategia;

        // ─── Redefinición de comparación ────────────────────────────────────
        /// <summary>
        /// Delega la comparación a la estrategia asignada si existe.
        /// Si no hay estrategia, usa el criterio original: Legajo (+ Promedio como desempate).
        /// Esto preserva compatibilidad con código de la Práctica 1.
        /// </summary>
        protected override int CompararCon(IComp obj)
        {
            if (obj is not Alumno otro)
                throw new ArgumentException($"No se puede comparar Alumno con {obj.GetType().Name}");

            // ── Strategy activo: delegar ──
            if (_estrategia is not null)
                return _estrategia.Comparar(this, otro);

            // ── Fallback (criterio original de Práctica 1): Legajo + Promedio ──
            int cmpLegajo = _legajo.CompareTo(otro._legajo);
            return cmpLegajo != 0 ? cmpLegajo : _promedio.CompareTo(otro._promedio);
        }

        // ─── Representación textual ─────────────────────────────────────────
        public override string ToString() =>
            $"Alumno {{ Legajo: {_legajo,6} | Promedio: {_promedio:F2} | {base.ToString()} }}";
    }
}
