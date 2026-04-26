using POO_Colecciones.Domain.Collections;
using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Strategies;
using POO_Colecciones.Utils;

namespace POO_Colecciones.Services
{
    /// <summary>
    /// Servicio de operaciones sobre colecciones.
    /// Responsabilidad única: llenado e informe.
    /// No conoce los detalles internos de ninguna colección concreta.
    /// </summary>
    public class OperacionesService
    {
        private const int CantidadRelleno = 20;

        private static readonly string[] Nombres =
        {
            "Ana", "Bruno", "Camila", "Diego", "Elena",
            "Facundo", "Gabriela", "Hernán", "Iris", "Joaquín",
            "Karen", "Lucas", "Marta", "Nicolás", "Olivia"
        };

        // ════════════════════════════════════════════════════════════════════
        //  LLENAR con Números
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Agrega <see cref="CantidadRelleno"/> objetos Numero con valores
        /// aleatorios entre 1 y 100 a la colección recibida.
        /// </summary>
        public void Llenar(Coleccionable col)
        {
            ArgumentNullException.ThrowIfNull(col);
            for (int i = 0; i < CantidadRelleno; i++)
                col.Agregar(new Numero(RandomHelper.EnteroEntre(1, 100)));
        }

        // ════════════════════════════════════════════════════════════════════
        //  LLENAR con Alumnos
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Agrega <see cref="CantidadRelleno"/> alumnos aleatorios a la Pila
        /// y otros <see cref="CantidadRelleno"/> a la Cola de la ColeccionMultiple.
        /// Se trabaja directamente sobre las sub-colecciones porque
        /// ColeccionMultiple.Agregar() es un Null Object.
        /// </summary>
        public void LlenarAlumnos(ColeccionMultiple col)
        {
            ArgumentNullException.ThrowIfNull(col);
            int dniBase = RandomHelper.EnteroEntre(10_000_000, 20_000_000);

            for (int i = 0; i < CantidadRelleno; i++)
            {
                col.GetPila().Agregar(CrearAlumnoAleatorio(dniBase + i));
                col.GetCola().Agregar(CrearAlumnoAleatorio(dniBase + CantidadRelleno + i));
            }
        }

        /// <summary>Crea un Alumno con datos generados aleatoriamente.
        /// Ej 2: se asigna ComparacionPorNombre como estrategia por defecto.
        /// </summary>
        private static Alumno CrearAlumnoAleatorio(int dni)
        {
            string nombre   = RandomHelper.Elegir(Nombres);
            int    legajo   = RandomHelper.EnteroEntre(1000, 9999);
            double promedio = RandomHelper.DoubleEntre(0, 10);
            var alumno = new Alumno(nombre, dni, legajo, promedio);
            alumno.SetEstrategia(new ComparacionPorNombre()); // estrategia por defecto (Ej 2)
            return alumno;
        }

        // ════════════════════════════════════════════════════════════════════
        //  LLENAR genérico (Ej 7) — cualquier Coleccionable (Pila, Cola, Conjunto)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Sobrecarga genérica: agrega <see cref="CantidadRelleno"/> alumnos
        /// aleatorios a cualquier Coleccionable que implemente Agregar().
        /// Util para llenar Pila, Cola o Conjunto independientemente.
        /// </summary>
        public void LlenarAlumnos(Coleccionable col)
        {
            ArgumentNullException.ThrowIfNull(col);
            int dniBase = RandomHelper.EnteroEntre(10_000_000, 20_000_000);
            for (int i = 0; i < CantidadRelleno; i++)
                col.Agregar(CrearAlumnoAleatorio(dniBase + i));
        }

        // ════════════════════════════════════════════════════════════════════
        //  INFORMAR
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Imprime por consola: cantidad, mínimo, máximo y búsqueda interactiva.
        /// </summary>
        public void Informar(Coleccionable col)
        {
            ArgumentNullException.ThrowIfNull(col);

            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"  Colección : {col}");
            Console.WriteLine(new string('─', 60));
            Console.WriteLine($"  Cantidad  : {col.Cuantos()}");

            try
            {
                Console.WriteLine($"  Mínimo    : {col.Minimo()}");
                Console.WriteLine($"  Máximo    : {col.Maximo()}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  [!] {ex.Message}");
            }

            // Búsqueda interactiva
            BuscarEImprimir(col);

            Console.WriteLine(new string('═', 60));
            Console.WriteLine();
        }

        // ─── Búsqueda interactiva e impresión del resultado ─────────────────
        private static void BuscarEImprimir(Coleccionable col)
        {
            Console.WriteLine();
            bool esDeAlumnos = EsColeccionDeAlumnos(col);

            try
            {
                if (esDeAlumnos)
                {
                    Console.Write("  Ingrese un legajo a buscar (entero): ");
                    string? input = Console.ReadLine();
                    if (!int.TryParse(input, out int legajo))
                    {
                        Console.WriteLine("  [!] Valor inválido. Se omite la búsqueda.");
                        return;
                    }
                    // Búsqueda directa por legajo (itera la colección)
                    bool encontrado = BuscarAlumnoPorLegajo(col, legajo, out Alumno? hallado);
                    Console.WriteLine(encontrado
                        ? $"  Contiene legajo {legajo}: SÍ ✓  →  {hallado}"
                        : $"  Contiene legajo {legajo}: NO ✗");
                }
                else
                {
                    Console.Write("  Ingrese un número a buscar (entero): ");
                    string? input = Console.ReadLine();
                    if (!int.TryParse(input, out int num))
                    {
                        Console.WriteLine("  [!] Valor inválido. Se omite la búsqueda.");
                        return;
                    }
                    Numero buscado = new(num);
                    bool encontrado = col.Contiene(buscado);
                    Console.WriteLine($"  Contiene {buscado}: {(encontrado ? "SÍ ✓" : "NO ✗")}");
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException or IOException)
            {
                // El Debug Console de VS Code no soporta ReadLine interactivo.
                // Ejecutar con: dotnet run --project POO_Colecciones
                Console.WriteLine($"  [!] Consola no interactiva. Use el terminal integrado. ({ex.GetType().Name})");
            }
        }

        /// <summary>
        /// Itera la colección buscando un Alumno cuyo legajo coincida.
        /// Necesario porque CompararCon usa legajo+promedio como clave compuesta
        /// y no existe un promedio "comodín" que garantice igualdad.
        /// </summary>
        private static bool BuscarAlumnoPorLegajo(Coleccionable col, int legajo, out Alumno? hallado)
        {
            hallado = null;

            // Recorre la colección usando Minimo/Maximo para detectar tipo.
            // Como no tenemos iterador público, usamos Contiene con sentinelas
            // de promedio 0..10 en pasos de 0.01 sería O(n*1000) → demasiado.
            // Solución: exponemos la búsqueda a través de las sub-colecciones
            // concretas si es ColeccionMultiple, o buscamos vía una pequeña
            // lista auxiliar construida por el servicio al llenar.
            // En este diseño optamos por buscar en las sub-colecciones directamente.
            if (col is ColeccionMultiple cm)
            {
                hallado = BuscarEnPila(cm.GetPila(), legajo)
                       ?? BuscarEnCola(cm.GetCola(), legajo);
            }
            else if (col is Pila p)
            {
                hallado = BuscarEnPila(p, legajo);
            }
            else if (col is Cola c)
            {
                hallado = BuscarEnCola(c, legajo);
            }

            return hallado is not null;
        }

        // Helpers de búsqueda directa en estructuras concretas
        private static Alumno? BuscarEnPila(Pila pila, int legajo)
            => pila.BuscarAlumno(legajo);

        private static Alumno? BuscarEnCola(Cola cola, int legajo)
            => cola.BuscarAlumno(legajo);

        /// <summary>
        /// Detecta si la colección contiene Alumnos (versus Números u otros).
        /// </summary>
        private static bool EsColeccionDeAlumnos(Coleccionable col)
        {
            if (col.Cuantos() == 0) return false;
            try   { return col.Maximo() is Alumno; }
            catch { return false; }
        }

        // ════════════════════════════════════════════════════════════════════
        //  IMPRIMIR ELEMENTOS con Iterator (Ej 6)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Recorre el iterable usando su propio Iterator e imprime cada elemento
        /// invocando ToString(). No conoce el tipo concreto de la colección.
        /// </summary>
        public void ImprimirElementos(Iterable iterable)
        {
            ArgumentNullException.ThrowIfNull(iterable);
            Iterator it = iterable.CrearIterador();
            int indice = 1;
            it.Primero();
            while (!it.Fin())
            {
                Console.WriteLine($"  [{indice++,2}] {it.Actual()}");
                it.Siguiente();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  CAMBIAR ESTRATEGIA con Iterator (Ej 8)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Recorre la colección usando iterator, castea cada elemento a Alumno
        /// (con seguridad) y le aplica la nueva estrategia de comparación.
        /// La colección debe implementar Iterable además de Coleccionable.
        /// </summary>
        public void CambiarEstrategia(Coleccionable col, EstrategiaComparacion estrategia)
        {
            ArgumentNullException.ThrowIfNull(col);
            ArgumentNullException.ThrowIfNull(estrategia);

            if (col is not Iterable iterable)
                throw new ArgumentException(
                    $"{col.GetType().Name} no implementa Iterable. " +
                    "Solo Pila, Cola y Conjunto soportan CambiarEstrategia.");

            Iterator it = iterable.CrearIterador();
            it.Primero();
            while (!it.Fin())
            {
                IComp elem = it.Actual();
                if (elem is Alumno alumno)
                    alumno.SetEstrategia(estrategia);
                it.Siguiente();
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  MOSTRAR MIN/MAX sin entrada interactiva (Ej 9)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Muestra mínimo y máximo de la colección según la estrategia activa,
        /// sin pedir entrada por consola. Ideal para demostrar el cambio de criterio.
        /// </summary>
        public void MostrarMinMax(Coleccionable col, string tituloEstrategia)
        {
            ArgumentNullException.ThrowIfNull(col);
            Console.WriteLine(new string('─', 60));
            Console.WriteLine($"  Estrategia  : {tituloEstrategia}");
            Console.WriteLine($"  Colección   : {col}");
            try
            {
                Console.WriteLine($"  Mínimo      : {col.Minimo()}");
                Console.WriteLine($"  Máximo      : {col.Maximo()}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  [!] {ex.Message}");
            }
            Console.WriteLine();
        }
        // ════════════════════════════════════════════════════════════════════
        //  LLENAR unificado con Factory Method (Ej 6)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Ej 6 — Versión unificada de llenar.
        /// Agrega <see cref="CantidadRelleno"/> objetos creados por
        /// <see cref="FabricaDeComparables.CrearAleatorio"/> según la opción.
        /// Reemplaza la duplicación entre Llenar(Numero) y LlenarAlumnos.
        /// </summary>
        /// <param name="opcion">1=Numero | 2=Alumno | 3=Profesor</param>
        public void Llenar(Coleccionable col, int opcion)
        {
            ArgumentNullException.ThrowIfNull(col);
            for (int i = 0; i < CantidadRelleno; i++)
                col.Agregar(FabricaDeComparables.CrearAleatorio(opcion));
        }

        // ════════════════════════════════════════════════════════════════════
        //  INFORMAR tipado con Factory Method (Ej 6)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Ej 6 — Versión tipada de informar.
        /// Muestra cantidad, mínimo y máximo. Luego crea un objeto por teclado
        /// (usando <see cref="FabricaDeComparables.CrearPorTeclado"/>) y busca
        /// si la colección lo contiene.
        /// </summary>
        /// <param name="opcion">1=Numero | 2=Alumno | 3=Profesor</param>
        public void Informar(Coleccionable col, int opcion)
        {
            ArgumentNullException.ThrowIfNull(col);

            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"  Colección : {col}");
            Console.WriteLine(new string('─', 60));
            Console.WriteLine($"  Cantidad  : {col.Cuantos()}");

            try
            {
                Console.WriteLine($"  Mínimo    : {col.Minimo()}");
                Console.WriteLine($"  Máximo    : {col.Maximo()}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  [!] {ex.Message}");
            }

            // Búsqueda usando Factory Method por teclado
            try
            {
                Console.WriteLine();
                Console.WriteLine("  Ingrese un objeto para buscar en la colección:");
                IComp buscado = FabricaDeComparables.CrearPorTeclado(opcion);
                bool encontrado = col.Contiene(buscado);
                Console.WriteLine($"  Contiene {buscado}: {(encontrado ? "SÍ ✓" : "NO ✗")}");
            }
            catch (Exception ex) when (ex is InvalidOperationException or IOException)
            {
                Console.WriteLine($"  [!] Consola no interactiva. ({ex.GetType().Name})");
            }

            Console.WriteLine(new string('═', 60));
            Console.WriteLine();
        }

        // ════════════════════════════════════════════════════════════════════
        //  DICTADO DE CLASES — Observer en acción (Ej 13)
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Ej 13 — Simula una clase dictada por el profesor.
        /// Repite 5 veces: hablarALaClase() + escribirEnElPizarron().
        /// Cada acción notifica a todos los alumnos observadores.
        /// </summary>
        public void DictadoDeClases(Profesor profesor)
        {
            ArgumentNullException.ThrowIfNull(profesor);
            for (int ronda = 1; ronda <= 5; ronda++)
            {
                Console.WriteLine($"  ── Ronda {ronda} ──────────────────────────────────────────");
                profesor.HablarALaClase();
                profesor.EscribirEnElPizarron();
            }
        }
    }
}
