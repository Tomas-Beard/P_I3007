using MetodologíasDeProgramaciónI;
using POO_Colecciones.Domain.Adapter;
using POO_Colecciones.Domain.Decorator;

namespace POO_Colecciones.Domain.Entities
{
    public class Aula
    {
        private Teacher? _teacher;

        public void comenzar()
        {
            Console.WriteLine("  [Aula] Comenzando la clase, profesor instanciado.");
            _teacher = new Teacher();
        }

        public void nuevoAlumno(Alumno alumno)
        {
            if (_teacher == null)
            {
                throw new InvalidOperationException("No se puede agregar alumnos antes de comenzar la clase.");
            }

            // Aplicamos la cadena de decoradores (igual que en Práctica 4)
            IMostrarCalificacion decorador =
                new DecoradorAsteriscos(
                    new DecoradorCondicion(
                        new DecoradorNotaEnLetras(
                            new DecoradorLegajo(
                                new MostrarCalificacionSimple(alumno),
                            alumno),
                        alumno),
                    alumno));

            _teacher.goToClass(new AlumnoAdapter(alumno, decorador));
        }

        public void claseLista()
        {
            Console.WriteLine("  [Aula] Clase lista, el profesor comienza a enseñar.");
            if (_teacher != null)
            {
                _teacher.teachingAClass();
            }
        }
    }
}
