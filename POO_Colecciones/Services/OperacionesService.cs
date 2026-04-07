using POO_Colecciones.Domain.Collections;
using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
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

        /// <summary>Crea un Alumno con datos generados aleatoriamente.</summary>
        private static Alumno CrearAlumnoAleatorio(int dni)
        {
            string nombre   = RandomHelper.Elegir(Nombres);
            int    legajo   = RandomHelper.EnteroEntre(1000, 9999);
            double promedio = RandomHelper.DoubleEntre(0, 10);
            return new Alumno(nombre, dni, legajo, promedio);
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
    }
}
