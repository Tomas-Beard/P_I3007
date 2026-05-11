using POO_Colecciones.Domain.Entities;

namespace POO_Colecciones.Domain.Proxy
{
    /// <summary>
    /// Implementación del patrón Proxy.
    /// Actúa como sustituto de Alumno o AlumnoMuyEstudioso,
    /// retrasando su creación hasta que se invoque ResponderPregunta().
    /// </summary>
    public class AlumnoProxy : Alumno
    {
        private Alumno? _alumnoReal;
        private readonly bool _esMuyEstudioso;

        public AlumnoProxy(string nombre, int dni, int legajo, double promedio, bool esMuyEstudioso)
            : base(nombre, dni, legajo, promedio)
        {
            _alumnoReal = null;
            _esMuyEstudioso = esMuyEstudioso;
        }

        public override int ResponderPregunta(int pregunta)
        {
            if (_alumnoReal == null)
            {
                if (_esMuyEstudioso)
                {
                    Console.WriteLine($"    [Proxy] Creando alumno muy estudioso real ({GetNombre()})...");
                    _alumnoReal = new AlumnoMuyEstudioso(GetNombre(), GetDni(), GetLegajo(), GetPromedio());
                }
                else
                {
                    Console.WriteLine($"    [Proxy] Creando alumno real ({GetNombre()})...");
                    _alumnoReal = new Alumno(GetNombre(), GetDni(), GetLegajo(), GetPromedio());
                }
            }

            return _alumnoReal.ResponderPregunta(pregunta);
        }
    }
}
