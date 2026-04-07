namespace POO_Colecciones.Utils
{
    /// <summary>
    /// Utilidad centralizada para generación de valores aleatorios.
    /// Usa una instancia compartida de Random para evitar secuencias repetidas.
    /// </summary>
    public static class RandomHelper
    {
        // Instancia única y compartida (thread-safe en .NET 6+)
        private static readonly Random _rnd = new();

        /// <summary>Entero aleatorio en [min, max].</summary>
        public static int EnteroEntre(int min, int max) => _rnd.Next(min, max + 1);

        /// <summary>Double aleatorio en [min, max] con dos decimales.</summary>
        public static double DoubleEntre(double min, double max)
        {
            double raw = min + _rnd.NextDouble() * (max - min);
            return Math.Round(raw, 2);
        }

        /// <summary>Selecciona un elemento aleatorio de un arreglo de strings.</summary>
        public static string Elegir(params string[] opciones)
        {
            if (opciones.Length == 0)
                throw new ArgumentException("Se necesita al menos una opción.");
            return opciones[_rnd.Next(opciones.Length)];
        }
    }
}
