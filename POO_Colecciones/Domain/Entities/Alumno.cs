namespace POO_Colecciones.Domain.Entities
{
    /// <summary>
    /// Representa un alumno. Hereda de Persona y redefine la comparación
    /// usando el PROMEDIO como criterio principal (desempata por legajo).
    /// </summary>
    public class Alumno : Persona
    {
        // ─── Atributos propios ──────────────────────────────────────────────
        private readonly int    _legajo;
        private readonly double _promedio;

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

        // ─── Redefinición de comparación ────────────────────────────────────
        /// <summary>
        /// Compara alumnos por LEGAJO (criterio principal).
        /// Si los legajos son iguales, desempata por PROMEDIO.
        /// Esto permite usar Contiene() buscando por legajo de forma efectiva.
        /// </summary>
        protected override int CompararCon(IComp obj)
        {
            if (obj is not Alumno otro)
                throw new ArgumentException($"No se puede comparar Alumno con {obj.GetType().Name}");

            int cmpLegajo = _legajo.CompareTo(otro._legajo);
            return cmpLegajo != 0 ? cmpLegajo : _promedio.CompareTo(otro._promedio);
        }

        // ─── Representación textual ─────────────────────────────────────────
        public override string ToString() =>
            $"Alumno {{ Legajo: {_legajo,6} | Promedio: {_promedio:F2} | {base.ToString()} }}";
    }
}
