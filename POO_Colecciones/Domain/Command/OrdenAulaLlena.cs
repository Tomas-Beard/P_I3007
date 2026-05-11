using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Command
{
    public class OrdenAulaLlena : OrdenEnAula1
    {
        private readonly Aula _aula;

        public OrdenAulaLlena(Aula aula)
        {
            _aula = aula;
        }

        public void ejecutar()
        {
            _aula.claseLista();
        }
    }
}
