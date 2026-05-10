using MetodologíasDeProgramaciónI;
using POO_Colecciones.Domain.Decorator;
using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Adapter
{
    /// <summary>
    /// P4 — Patrón Adapter (mediante COMPOSICIÓN).
    /// </summary>
    public class AlumnoAdapter : Student
    {
        // ─── Adaptable (composición) ─────────────────────────────────────────
        private readonly Alumno _alumno;
        // ─── Decorador inyectado para showResult() ───────────────────────────
        private readonly IMostrarCalificacion _decorador;

        // ─── Constructor ─────────────────────────────────────────────────────
        public AlumnoAdapter(Alumno alumno, IMostrarCalificacion decorador)
        {
            _alumno = alumno;
            _decorador = decorador;
        }

        // ─── Helper interno para comparaciones entre adapters ────────────────
        internal Alumno GetAlumno() => _alumno;

        // ═════════════════════════════════════════════════════════════════════
        //  Implementación de la interfaz Student (traducción de llamadas)
        // ═════════════════════════════════════════════════════════════════════

        public string getName() => _alumno.GetNombre();

        public int yourAnswerIs(int question) => _alumno.ResponderPregunta(question);

        public void setScore(int score) => _alumno.SetCalificacion(score);

        public string showResult() => _decorador.MostrarCalificacion();

        public bool equals(Student student)
        {
            if (student is AlumnoAdapter otro)
                return _alumno.SosIgual(otro.GetAlumno());
            return false;
        }

        public bool lessThan(Student student)
        {
            if (student is AlumnoAdapter otro)
                return _alumno.SosMenor(otro.GetAlumno());
            return false;
        }

        public bool greaterThan(Student student)
        {
            if (student is AlumnoAdapter otro)
                return _alumno.SosMayor(otro.GetAlumno());
            return false;
        }
    }
}
