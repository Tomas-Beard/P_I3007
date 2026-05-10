namespace POO_Colecciones.Domain.Decorator
{
    /// <summary>
    /// P4 — Decorador Concreto: envuelve el resultado en un recuadro de asteriscos.
    /// </summary>
    public class DecoradorAsteriscos : DecoradorCalificacion
    {
        public DecoradorAsteriscos(IMostrarCalificacion componente)
            : base(componente) { }

        public override string MostrarCalificacion()
        {
            string contenido = _componente.MostrarCalificacion();
            string borde = new string('*', contenido.Length + 4);
            return $"{borde}\n* {contenido} *\n{borde}";
        }
    }
}
