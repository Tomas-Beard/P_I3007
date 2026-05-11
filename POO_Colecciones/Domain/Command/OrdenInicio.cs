using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Command
{
    public class OrdenInicio : OrdenEnAula1
    {
        private readonly Aula _aula;

        public OrdenInicio(Aula aula)
        {
            _aula = aula;
        }

        public void ejecutar()
        {
            _aula.comenzar();
        }
    }
}
