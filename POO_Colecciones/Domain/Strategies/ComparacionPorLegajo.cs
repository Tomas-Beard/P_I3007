using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Strategies
{
    /// <summary>
    /// Estrategia: compara Alumnos por Legajo en orden numérico ascendente.
    /// </summary>
    public class ComparacionPorLegajo : EstrategiaComparacion
    {
        public int Comparar(Alumno a, Alumno b) =>
            a.GetLegajo().CompareTo(b.GetLegajo());

        public override string ToString() => "Estrategia por Legajo";
    }
}
