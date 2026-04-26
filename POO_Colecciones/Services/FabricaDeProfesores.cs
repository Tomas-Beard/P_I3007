using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Utils;

namespace POO_Colecciones.Services
{
    /// <summary>
    /// Fábrica concreta para objetos <see cref="Profesor"/>.
    /// Hereda de <see cref="FabricaDeComparables"/> e implementa los dos
    /// métodos abstractos de instancia para crear Profesores aleatorios o por teclado.
    /// </summary>
    public class FabricaDeProfesores : FabricaDeComparables
    {
        private readonly GeneradorDeDatosAleatorios _gen    = new();
        private readonly LectorDeDatos             _lector = new();

        private static readonly string[] _nombres =
        {
            "Carlos", "María", "Roberto", "Liliana", "Alejandro",
            "Claudia", "Fernando", "Patricia", "Marcelo", "Graciela"
        };

        /// <summary>Crea un Profesor con datos completamente aleatorios.</summary>
        public override IComp CrearAleatorio()
        {
            string nombre     = RandomHelper.Elegir(_nombres);
            int    dni        = RandomHelper.EnteroEntre(10_000_000, 40_000_000);
            int    antiguedad = _gen.NumeroAleatorio(40) + 1;   // [1..41]
            return new Profesor(nombre, dni, antiguedad);
        }

        /// <summary>Crea un Profesor con datos ingresados por teclado.</summary>
        public override IComp CrearPorTeclado()
        {
            Console.WriteLine("  [Fábrica de Profesores]");

            Console.Write("    Nombre     : ");
            string nombre = _lector.StringPorTeclado();

            Console.Write("    DNI        : ");
            int dni = Math.Abs(_lector.NumeroPorTeclado());

            Console.Write("    Antigüedad : ");
            int antiguedad = Math.Abs(_lector.NumeroPorTeclado());

            return new Profesor(nombre, Math.Max(1, dni), Math.Max(1, antiguedad));
        }
    }
}
