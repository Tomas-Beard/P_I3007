using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Strategies
{
    /// <summary>
    /// Estrategia: compara Alumnos por Promedio en orden numérico ascendente.
    /// En caso de empate desempata por Legajo.
    /// </summary>
    public class ComparacionPorPromedio : EstrategiaComparacion
    {
        public int Comparar(Alumno a, Alumno b)
        {
            int cmpPromedio = a.GetPromedio().CompareTo(b.GetPromedio());
            return cmpPromedio != 0 ? cmpPromedio : a.GetLegajo().CompareTo(b.GetLegajo());
        }

        public override string ToString() => "Estrategia por Promedio";
    }
}
