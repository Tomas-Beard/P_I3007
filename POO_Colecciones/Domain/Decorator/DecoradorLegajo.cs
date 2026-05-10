using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Decorator
{
    /// <summary>
    /// P4 — Decorador Concreto: agrega el legajo y la nota obtenida a la visualización.
    /// </summary>
    public class DecoradorLegajo : DecoradorCalificacion
    {
        private readonly Alumno _alumno;

        public DecoradorLegajo(IMostrarCalificacion componente, Alumno alumno)
            : base(componente)
        {
            _alumno = alumno;
        }

        public override string MostrarCalificacion()
        {
            string resultado = _componente.MostrarCalificacion();

            int idx = resultado.LastIndexOf("  ");
            if (idx >= 0)
            {
                string nombre = resultado[..idx];
                string nota = resultado[(idx + 2)..];
                int legajo = _alumno.GetLegajo();
                int score = _alumno.GetCalificacion();   // ← nota obtenida
                return $"{nombre} ({legajo}/{score}) {nota}";
            }

            return resultado;
        }
    }
}
