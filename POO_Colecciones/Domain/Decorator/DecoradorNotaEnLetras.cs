using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Decorator
{
    /// <summary>
    /// P4 — Decorador Concreto: agrega la nota expresada en letras.
    /// </summary>
    public class DecoradorNotaEnLetras : DecoradorCalificacion
    {
        private readonly Alumno _alumno;

        private static readonly string[] _palabras =
        {
            "CERO", "UNO", "DOS", "TRES", "CUATRO",
            "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ"
        };

        public DecoradorNotaEnLetras(IMostrarCalificacion componente, Alumno alumno)
            : base(componente)
        {
            _alumno = alumno;
        }

        public override string MostrarCalificacion()
        {
            string resultado = _componente.MostrarCalificacion();
            int nota = _alumno.GetCalificacion();
            string enLetras = nota >= 0 && nota <= 10
                ? _palabras[nota]
                : nota.ToString();

            return $"{resultado} ({enLetras})";
        }
    }
}
