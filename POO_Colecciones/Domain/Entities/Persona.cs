namespace POO_Colecciones.Domain.Entities
{
    /// <summary>
    /// Clase abstracta que representa una persona con nombre y DNI.
    /// Implementa IComp (alias de nuestra IComparable) comparando por DNI.
    /// Las subclases pueden sobreescribir la comparación mediante CompararCon().
    /// </summary>
    public abstract class Persona : IComp
    {
        // ─── Atributos encapsulados ─────────────────────────────────────────
        private readonly string _nombre;
        private readonly int    _dni;

        // ─── Constructor ────────────────────────────────────────────────────
        protected Persona(string nombre, int dni)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");
            if (dni <= 0)
                throw new ArgumentException("El DNI debe ser un número positivo.");

            _nombre = nombre;
            _dni    = dni;
        }

        // ─── Getters ────────────────────────────────────────────────────────
        public string GetNombre() => _nombre;
        public int    GetDni()    => _dni;

        // ─── Lógica de comparación (virtual → subclases pueden redefinirla) ─
        /// <summary>
        /// Compara por DNI. Las subclases pueden sobrescribir este método.
        /// </summary>
        protected virtual int CompararCon(IComp obj)
        {
            if (obj is not Persona otra)
                throw new ArgumentException($"No se puede comparar Persona con {obj.GetType().Name}");
            return _dni.CompareTo(otra._dni);
        }

        // ─── Implementación de IComp ─────────────────────────────────────────
        public bool SosIgual(IComp obj) => CompararCon(obj) == 0;
        public bool SosMenor(IComp obj) => CompararCon(obj) <  0;
        public bool SosMayor(IComp obj) => CompararCon(obj) >  0;

        // ─── Representación textual ─────────────────────────────────────────
        public override string ToString() => $"[DNI: {_dni} | Nombre: {_nombre}]";
    }
}
