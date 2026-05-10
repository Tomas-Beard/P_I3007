namespace POO_Colecciones.Services
{
    /// <summary>
    /// Ej 3 — Lee datos primitivos ingresados por el usuario desde la consola.
    /// Encapsula la interacción con Console para que las fábricas no dependan
    /// directamente de System.Console.
    /// </summary>
    public class LectorDeDatos
    {
        /// <summary>
        /// Solicita un número entero por teclado y lo retorna.
        /// Repite la solicitud hasta que el usuario ingrese un valor válido.
        /// </summary>
        public int NumeroPorTeclado()
        {
            while (true)
            {
                Console.Write("    Ingrese un número entero: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int resultado))
                    return resultado;
                Console.WriteLine("    [!] Valor inválido. Intente de nuevo.");
            }
        }

        /// <summary>
        /// Solicita un string por teclado y lo retorna.
        /// Repite la solicitud hasta que el usuario ingrese un valor no vacío.
        /// </summary>
        public string StringPorTeclado()
        {
            while (true)
            {
                Console.Write("    Ingrese un texto: ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();
                Console.WriteLine("    [!] El texto no puede estar vacío. Intente de nuevo.");
            }
        }
    }
}
