using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Strategies
{
    /// <summary>
    /// Estrategia: compara Alumnos por Nombre en orden alfabético
    /// sin distinción de mayúsculas/minúsculas.
    /// </summary>
    public class ComparacionPorNombre : EstrategiaComparacion
    {
        public int Comparar(Alumno a, Alumno b) =>
            string.Compare(a.GetNombre(), b.GetNombre(), StringComparison.OrdinalIgnoreCase);

        public override string ToString() => "Estrategia por Nombre";
    }
}
