namespace POO_Colecciones.Services
{
    /// <summary>
    /// Ej 5 — Clase abstracta que define el contrato del patrón Factory Method.
    ///
    ///
    /// El cliente siempre llama a FabricaDeComparables.CrearAleatorio(opcion),
    /// NUNCA sabe qué subclase se instancia internamente.
    /// </summary>
    public abstract class FabricaDeComparables
    {
        // ═══════════════════════════════════════════════════════════════════
        //  Selector estático — CREACIÓN ALEATORIA
        //  Instancia la fábrica concreta y llama a su método de instancia.
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Selecciona la fábrica concreta según la opción y delega la creación.
        /// </summary>
        /// <param name="opcion">1 = Numero | 2 = Alumno | 3 = Profesor</param>
        public static IComp CrearAleatorio(int opcion)
        {
            FabricaDeComparables fabrica = null!;

            if (opcion == 1) fabrica = new FabricaDeNumeros();
            if (opcion == 2) fabrica = new FabricaDeAlumnos();
            if (opcion == 3) fabrica = new FabricaDeProfesores();

            if (fabrica is null)
                throw new ArgumentOutOfRangeException(nameof(opcion),
                    $"Opción {opcion} no válida. Use 1 (Numero), 2 (Alumno) o 3 (Profesor).");

            return fabrica.CrearAleatorio();   // ← delega al método abstracto
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Método abstracto de instancia — CREACIÓN ALEATORIA
        //  Cada subclase implementa su propia lógica de construcción.
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Crea y retorna un objeto Comparable con datos aleatorios.
        /// Cada fábrica concreta implementa este método para su tipo.
        /// </summary>
        public abstract IComp CrearAleatorio();

        // ═══════════════════════════════════════════════════════════════════
        //  Selector estático — CREACIÓN POR TECLADO
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Selecciona la fábrica concreta según la opción y delega la creación
        /// interactiva (datos ingresados por el usuario).
        /// </summary>
        /// <param name="opcion">1 = Numero | 2 = Alumno | 3 = Profesor</param>
        public static IComp CrearPorTeclado(int opcion)
        {
            FabricaDeComparables fabrica = null!;

            if (opcion == 1) fabrica = new FabricaDeNumeros();
            if (opcion == 2) fabrica = new FabricaDeAlumnos();
            if (opcion == 3) fabrica = new FabricaDeProfesores();

            if (fabrica is null)
                throw new ArgumentOutOfRangeException(nameof(opcion),
                    $"Opción {opcion} no válida. Use 1 (Numero), 2 (Alumno) o 3 (Profesor).");

            return fabrica.CrearPorTeclado();  // ← delega al método abstracto
        }

        // ═══════════════════════════════════════════════════════════════════
        //  Método abstracto de instancia — CREACIÓN POR TECLADO
        // ═══════════════════════════════════════════════════════════════════

        /// <summary>
        /// Crea y retorna un objeto Comparable con datos leídos por teclado.
        /// Cada fábrica concreta implementa este método para su tipo.
        /// </summary>
        public abstract IComp CrearPorTeclado();

        // ───────────────────────────────────────────────────────────────────
        /// <summary>Muestra en consola las opciones disponibles.</summary>
        public static void MostrarOpciones()
        {
            Console.WriteLine("    1 → Numero");
            Console.WriteLine("    2 → Alumno");
            Console.WriteLine("    3 → Profesor");
        }
    }
}
