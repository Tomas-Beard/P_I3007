namespace POO_Colecciones.Domain.Decorator
{
    /// <summary>
    /// P4 — Decorador Abstracto del patrón Decorator.
    /// </summary>
    public abstract class DecoradorCalificacion : IMostrarCalificacion
    {
        protected readonly IMostrarCalificacion _componente;

        protected DecoradorCalificacion(IMostrarCalificacion componente)
        {
            _componente = componente;
        }

        public virtual string MostrarCalificacion() =>
            _componente.MostrarCalificacion();
    }
}
