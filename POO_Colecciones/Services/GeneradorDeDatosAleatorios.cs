using POO_Colecciones.Utils;

namespace POO_Colecciones.Services
{
    /// <summary>
    /// Ej 2 — Genera datos aleatorios de tipos primitivos.
    /// Delega la aleatoriedad a <see cref="RandomHelper"/> para mantener
    /// una única fuente de entropía en todo el proyecto.
    /// </summary>
    public class GeneradorDeDatosAleatorios
    {
        private static readonly char[] _letras =
            "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <summary>
        /// Retorna un entero aleatorio en [0, max].
        /// </summary>
        public int NumeroAleatorio(int max) =>
            RandomHelper.EnteroEntre(0, max);

        /// <summary>
        /// Retorna un string de <paramref name="cant"/> letras minúsculas aleatorias.
        /// </summary>
        public string StringAleatorio(int cant)
        {
            if (cant <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cant));

            char[] chars = new char[cant];
            for (int i = 0; i < cant; i++)
                chars[i] = _letras[RandomHelper.EnteroEntre(0, _letras.Length - 1)];

            // Capitalizar la primera letra para que parezca un nombre
            chars[0] = char.ToUpper(chars[0]);
            return new string(chars);
        }
    }
}
