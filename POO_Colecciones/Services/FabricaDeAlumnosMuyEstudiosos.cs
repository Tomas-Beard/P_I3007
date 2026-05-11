using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Interfaces;
using POO_Colecciones.Domain.Proxy;
using POO_Colecciones.Domain.Strategies;
using POO_Colecciones.Utils;

namespace POO_Colecciones.Services
{

    public class FabricaDeAlumnosMuyEstudiosos : FabricaDeComparables
    {
        private readonly GeneradorDeDatosAleatorios _gen = new();
        private readonly LectorDeDatos _lector = new();

        private static readonly string[] _nombres =
        {
            "Ratón Pérez", "Rosa Blanco", "Felipe Castro", "Carmen Ruiz", "Jorge Morales",
            "Isabel Díaz", "Luis Vargas", "Patricia Romero", "Andrés Jiménez", "Teresa Molina"
        };

        public override IComp CrearAleatorio()
        {
            string nombre = RandomHelper.Elegir(_nombres);
            int dni = RandomHelper.EnteroEntre(10_000_000, 40_000_000);
            int legajo = _gen.NumeroAleatorio(8999) + 1000;
            double promedio = RandomHelper.DoubleEntre(0, 10);

            // Crea un Proxy con esMuyEstudioso = true
            var alumno = new AlumnoProxy(nombre, dni, legajo, promedio, true);
            alumno.SetEstrategia(new ComparacionPorNombre());
            return alumno;
        }

        public override IComp CrearPorTeclado()
        {
            Console.WriteLine("  [Fábrica de Alumnos Muy Estudiosos]");

            Console.Write("    Nombre   : ");
            string nombre = _lector.StringPorTeclado();

            Console.Write("    DNI      : ");
            int dni = Math.Abs(_lector.NumeroPorTeclado());

            Console.Write("    Legajo   : ");
            int legajo = Math.Abs(_lector.NumeroPorTeclado());

            Console.Write("    Promedio : ");
            int promInt = _lector.NumeroPorTeclado();
            double promedio = Math.Max(0, Math.Min(10, promInt));

            var alumno = new AlumnoProxy(nombre, Math.Max(1, dni), Math.Max(1, legajo), promedio, true);
            alumno.SetEstrategia(new ComparacionPorNombre());
            return alumno;
        }
    }
}
