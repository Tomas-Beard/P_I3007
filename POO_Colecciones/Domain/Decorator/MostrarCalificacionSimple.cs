using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Decorator
{
    /// <summary>
    /// P4 — Componente Concreto del patrón Decorator.
    /// </summary>
    public class MostrarCalificacionSimple : IMostrarCalificacion
    {
        private readonly Alumno _alumno;

        public MostrarCalificacionSimple(Alumno alumno)
        {
            _alumno = alumno;
        }

        public string MostrarCalificacion() => _alumno.MostrarCalificacion();
    }
}
