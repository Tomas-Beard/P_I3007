namespace POO_Colecciones.Domain.Entities
{
    /// <summary>
    /// Representa un número entero con capacidad de comparación polimórfica.
    /// Implementa IComp (alias de nuestra IComparable) para integrarse
    /// con las colecciones del sistema.
    /// </summary>
    public class Numero : IComp
    {
        // ─── Atributo privado ───────────────────────────────────────────────
        private readonly int _valor;

        // ─── Constructor ────────────────────────────────────────────────────
        public Numero(int valor) => _valor = valor;

        // ─── Getter ─────────────────────────────────────────────────────────
        public int GetValor() => _valor;

        // ─── Helper interno de comparación ──────────────────────────────────
        /// <summary>
        /// Verifica que el objeto recibido sea un Numero y compara los valores.
        /// </summary>
        private int CompararCon(IComp obj)
        {
            if (obj is not Numero otro)
                throw new ArgumentException($"No se puede comparar Numero con {obj.GetType().Name}");
            return _valor.CompareTo(otro._valor);
        }

        // ─── Implementación de IComp ─────────────────────────────────────────
        public bool SosIgual(IComp obj) => CompararCon(obj) == 0;
        public bool SosMenor(IComp obj) => CompararCon(obj) <  0;
        public bool SosMayor(IComp obj) => CompararCon(obj) >  0;

        // ─── Representación textual ─────────────────────────────────────────
        public override string ToString() => $"Numero({_valor})";
    }
}
