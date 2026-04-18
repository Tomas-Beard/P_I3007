using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Interfaces
{
    /// <summary>
    /// Interfaz del patrón Strategy para comparación de Alumnos.
    /// Permite intercambiar el criterio de comparación en tiempo de ejecución,
    /// desacoplando el algoritmo de comparación de la clase Alumno.
    /// </summary>
    public interface EstrategiaComparacion
    {
        /// <summary>
        /// Compara dos alumnos según el criterio concreto de la estrategia.
        /// </summary>
        /// <returns>Negativo si a &lt; b, cero si a == b, positivo si a &gt; b.</returns>
        int Comparar(Alumno a, Alumno b);
    }
}
