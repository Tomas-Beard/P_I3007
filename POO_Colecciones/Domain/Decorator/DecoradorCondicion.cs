using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Decorator
{
    /// <summary>
    /// P4 — Decorador Concreto: agrega la condición académica del alumno.
    ///
    /// Reglas:
    ///   nota >= 7  → PROMOCIÓN
    ///   nota >= 4  → APROBADO
    ///   nota  < 4  → DESAPROBADO
    /// </summary>
    public class DecoradorCondicion : DecoradorCalificacion
    {
        private readonly Alumno _alumno;

        public DecoradorCondicion(IMostrarCalificacion componente, Alumno alumno)
            : base(componente)
        {
            _alumno = alumno;
        }

        public override string MostrarCalificacion()
        {
            string resultado = _componente.MostrarCalificacion();
            int nota = _alumno.GetCalificacion();

            string condicion = nota >= 7 ? "PROMOCIÓN"
                             : nota >= 4 ? "APROBADO"
                             : "DESAPROBADO";

            return $"{resultado} - {condicion}";
        }
    }
}
