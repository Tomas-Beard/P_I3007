using POO_Colecciones.Domain.Strategies;

namespace POO_Colecciones.Domain.Entities
{
    /// <summary>
    /// P4 — Subclase de Alumno que representa a un alumno muy estudioso.
    /// </summary>
    public class AlumnoMuyEstudioso : Alumno
    {
        // ─── Constructor ────────────────────────────────────────────────────
        /// <summary>
        /// Crea un AlumnoMuyEstudioso con los mismos parámetros que Alumno.
        /// </summary>
        public AlumnoMuyEstudioso(string nombre, int dni, int legajo, double promedio)
            : base(nombre, dni, legajo, promedio) { }

        // ─── Sobreescritura del comportamiento de respuesta ─────────────────
        public override int ResponderPregunta(int pregunta) => pregunta % 3;

        // ─── Representación textual ─────────────────────────────────────────
        public override string ToString() =>
            $"AlumnoMuyEstudioso {{ {base.ToString()} }}";
    }
}
