using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Strategies;
using POO_Colecciones.Utils;

namespace POO_Colecciones.Services
{
    /// <summary>
    /// Fábrica concreta para objetos <see cref="Alumno"/>.
    /// Hereda de <see cref="FabricaDeComparables"/> e implementa los dos
    /// métodos abstractos de instancia para crear Alumnos aleatorios o por teclado.
    /// </summary>
    public class FabricaDeAlumnos : FabricaDeComparables
    {
        private readonly GeneradorDeDatosAleatorios _gen    = new();
        private readonly LectorDeDatos             _lector = new();

        private static readonly string[] _nombres =
        {
            "Ana", "Bruno", "Camila", "Diego", "Elena",
            "Facundo", "Gabriela", "Hernán", "Iris", "Joaquín",
            "Karen", "Lucas", "Marta", "Nicolás", "Olivia",
            "Pablo", "Renata", "Sebastián", "Tamara", "Valentina"
        };

        /// <summary>Crea un Alumno con datos completamente aleatorios.</summary>
        public override IComp CrearAleatorio()
        {
            string nombre   = RandomHelper.Elegir(_nombres);
            int    dni      = RandomHelper.EnteroEntre(10_000_000, 40_000_000);
            int    legajo   = _gen.NumeroAleatorio(8999) + 1000;  // [1000..9999]
            double promedio = RandomHelper.DoubleEntre(0, 10);

            var alumno = new Alumno(nombre, dni, legajo, promedio);
            alumno.SetEstrategia(new ComparacionPorNombre());
            return alumno;
        }

        /// <summary>Crea un Alumno con datos ingresados por teclado.</summary>
        public override IComp CrearPorTeclado()
        {
            Console.WriteLine("  [Fábrica de Alumnos]");

            Console.Write("    Nombre   : ");
            string nombre = _lector.StringPorTeclado();

            Console.Write("    DNI      : ");
            int dni = Math.Abs(_lector.NumeroPorTeclado());

            Console.Write("    Legajo   : ");
            int legajo = Math.Abs(_lector.NumeroPorTeclado());

            Console.Write("    Promedio : ");
            int promInt = _lector.NumeroPorTeclado();
            double promedio = Math.Max(0, Math.Min(10, promInt));

            var alumno = new Alumno(nombre, Math.Max(1, dni), Math.Max(1, legajo), promedio);
            alumno.SetEstrategia(new ComparacionPorNombre());
            return alumno;
        }
    }
}
