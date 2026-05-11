using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Command
{
    public class OrdenLlegaAlumno : OrdenEnAula2
    {
        private readonly Aula _aula;

        public OrdenLlegaAlumno(Aula aula)
        {
            _aula = aula;
        }

        public void ejecutar(IComp c)
        {
            if (c is Alumno alumno)
            {
                _aula.nuevoAlumno(alumno);
            }
        }
    }
}
