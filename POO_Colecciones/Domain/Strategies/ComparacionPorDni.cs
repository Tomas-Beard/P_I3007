using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;

namespace POO_Colecciones.Domain.Strategies
{
    /// <summary>
    /// Estrategia: compara Alumnos por DNI en orden numérico ascendente.
    /// </summary>
    public class ComparacionPorDni : EstrategiaComparacion
    {
        public int Comparar(Alumno a, Alumno b) =>
            a.GetDni().CompareTo(b.GetDni());

        public override string ToString() => "Estrategia por DNI";
    }
}
