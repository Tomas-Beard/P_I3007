using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Services
{
    /// <summary>
    /// Fábrica concreta para objetos <see cref="Numero"/>.
    /// Hereda de <see cref="FabricaDeComparables"/> e implementa los dos
    /// métodos abstractos de instancia para crear Numeros aleatorios o por teclado.
    /// </summary>
    public class FabricaDeNumeros : FabricaDeComparables
    {
        private readonly GeneradorDeDatosAleatorios _gen    = new();
        private readonly LectorDeDatos             _lector = new();

        /// <summary>Crea un Numero con valor aleatorio entre 1 y 999.</summary>
        public override IComp CrearAleatorio()
        {
            int valor = _gen.NumeroAleatorio(999) + 1;   // [1..999]
            return new Numero(valor);
        }

        /// <summary>Crea un Numero con el valor ingresado por teclado.</summary>
        public override IComp CrearPorTeclado()
        {
            Console.WriteLine("  [Fábrica de Números]");
            int valor = _lector.NumeroPorTeclado();
            return new Numero(valor);
        }
    }
}
