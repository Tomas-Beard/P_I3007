using POO_Colecciones.Domain.Collections;
using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Strategies;
using POO_Colecciones.Services;
// P4 ─ Adapter y Decorator
using MetodologíasDeProgramaciónI;
using POO_Colecciones.Domain.Adapter;
using POO_Colecciones.Domain.Decorator;
using POO_Colecciones.Domain.Proxy;
using POO_Colecciones.Domain.Command;
using POO_Colecciones.Domain.Interfaces;

// ════════════════════════════════════════════════════════════════════════════
//  PROGRAMA PRINCIPAL
//  Demuestra el uso de Pila, Cola y ColeccionMultiple con Alumnos.
// ════════════════════════════════════════════════════════════════════════════

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║        POO - Colecciones, Interfaces y Polimorfismo      ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine();

// ─── Crear colecciones ───────────────────────────────────────────────────────
Pila pila = new();
Cola cola = new();
ColeccionMultiple colMultiple = new(pila, cola);

// ─── Servicio de operaciones ─────────────────────────────────────────────────
OperacionesService servicio = new();

// ─── Llenar con Alumnos aleatorios ───────────────────────────────────────────
Console.WriteLine("► Llenando Pila y Cola con 20 alumnos aleatorios cada una...");
servicio.LlenarAlumnos(colMultiple);
Console.WriteLine($"  Pila:          {pila.Cuantos()} alumnos");
Console.WriteLine($"  Cola:          {cola.Cuantos()} alumnos");
Console.WriteLine($"  ColMultiple:   {colMultiple.Cuantos()} alumnos (total)");
Console.WriteLine();

// ─── Informar sobre la ColeccionMultiple ─────────────────────────────────────
Console.WriteLine("► Informando sobre ColeccionMultiple:");
servicio.Informar(colMultiple);

// ─── Informar sobre Pila y Cola por separado ─────────────────────────────────
Console.WriteLine("► Informando sobre la Pila:");
servicio.Informar(pila);

Console.WriteLine("► Informando sobre la Cola:");
servicio.Informar(cola);

// ════════════════════════════════════════════════════════════════════════════
//  PRÁCTICA 2 — Patrones Strategy e Iterator (Ejercicios 1–9)
// ════════════════════════════════════════════════════════════════════════════
Console.WriteLine();
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║    PRÁCTICA 2 — Strategy e Iterator                      ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine();

// ─── Ej 7: Pila, Cola y Conjunto con ImprimirElementos (Iterator) ─────────────────────
Console.WriteLine("══ Ejercicio 7: Pila / Cola / Conjunto + ImprimirElementos ═");
Console.WriteLine();

Pila pilaP2 = new();
Cola colaP2 = new();
Conjunto conjP2 = new();

servicio.LlenarAlumnos(pilaP2);
servicio.LlenarAlumnos(colaP2);
servicio.LlenarAlumnos(conjP2);

Console.WriteLine($"  Pila   : {pilaP2.Cuantos()} alumnos");
Console.WriteLine($"  Cola   : {colaP2.Cuantos()} alumnos");
Console.WriteLine($"  Conjunto: {conjP2.Cuantos()} alumnos únicos (estrategia por Nombre → duplicados descartados)");
Console.WriteLine();

Console.WriteLine("▼ Elementos de la PILA (cima → base):");
servicio.ImprimirElementos(pilaP2);
Console.WriteLine();

Console.WriteLine("▼ Elementos de la COLA (frente → fondo):");
servicio.ImprimirElementos(colaP2);
Console.WriteLine();

Console.WriteLine("▼ Elementos del CONJUNTO (orden de inserción):");
servicio.ImprimirElementos(conjP2);
Console.WriteLine();

// ─── Ej 9: Cambio de estrategia múltiple → Mínimo & Máximo ───────────────────────────
Console.WriteLine("══ Ejercicio 9: CambiarEstrategia → Mín/Máx por cada criterio ═");
Console.WriteLine();

Pila pilaEj9 = new();
servicio.LlenarAlumnos(pilaEj9);
Console.WriteLine($"  Pila Ej9: {pilaEj9.Cuantos()} alumnos cargados.");
servicio.ImprimirElementos(pilaEj9);
Console.WriteLine();

// ─ Por Nombre
servicio.CambiarEstrategia(pilaEj9, new ComparacionPorNombre());
servicio.MostrarMinMax(pilaEj9, "por Nombre");

// ─ Por Legajo
servicio.CambiarEstrategia(pilaEj9, new ComparacionPorLegajo());
servicio.MostrarMinMax(pilaEj9, "por Legajo");

// ─ Por Promedio
servicio.CambiarEstrategia(pilaEj9, new ComparacionPorPromedio());
servicio.MostrarMinMax(pilaEj9, "por Promedio");

// ─ Por DNI
servicio.CambiarEstrategia(pilaEj9, new ComparacionPorDni());
servicio.MostrarMinMax(pilaEj9, "por DNI");

Console.WriteLine("Presione cualquier tecla para continuar a la Práctica 3...");
Console.ReadKey();

// ════════════════════════════════════════════════════════════════════════════
//  PRÁCTICA 3 — Factory Method + Observer (Ejercicios 2–14)
// ════════════════════════════════════════════════════════════════════════════
Console.WriteLine();
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║    PRÁCTICA 3 — Factory Method + Observer                ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine();

// ─── Ej 6: llenar unificado con Factory Method ───────────────────────────────
Console.WriteLine("══ Ej 6: Llenar con Factory Method (opción 1 = Numero) ════");
Pila pilaNumeros = new();
servicio.Llenar(pilaNumeros, 1);       // usa FabricaDeComparables.CrearAleatorio(1)
Console.WriteLine($"  Pila llenada con Números: {pilaNumeros.Cuantos()} elementos");
Console.WriteLine($"  Mínimo: {pilaNumeros.Minimo()}");
Console.WriteLine($"  Máximo: {pilaNumeros.Maximo()}");
Console.WriteLine();

Console.WriteLine("══ Ej 6: Llenar con Factory Method (opción 2 = Alumno) ════");
Cola colaAlumnos = new();
servicio.Llenar(colaAlumnos, 2);       // usa FabricaDeComparables.CrearAleatorio(2)
Console.WriteLine($"  Cola llenada con Alumnos: {colaAlumnos.Cuantos()} elementos");
Console.WriteLine($"  Mínimo: {colaAlumnos.Minimo()}");
Console.WriteLine($"  Máximo: {colaAlumnos.Maximo()}");
Console.WriteLine();

Console.WriteLine("══ Ej 6: Llenar con Factory Method (opción 3 = Profesor) ══");
Conjunto conjProfesores = new();
servicio.Llenar(conjProfesores, 3);    // usa FabricaDeComparables.CrearAleatorio(3)
Console.WriteLine($"  Conjunto llenado con Profesores: {conjProfesores.Cuantos()} elementos únicos");
Console.WriteLine($"  Mínimo (por antigüedad): {conjProfesores.Minimo()}");
Console.WriteLine($"  Máximo (por antigüedad): {conjProfesores.Maximo()}");
Console.WriteLine();

// ─── Ej 14: Observer — Profesor + 20 Alumnos ─────────────────────────────────
Console.WriteLine("══ Ej 14: Observer — DictadoDeClases ══════════════════════");
Console.WriteLine();

// Crear un Profesor vía Factory Method (opción 3)
Profesor profesor = (Profesor)FabricaDeComparables.CrearAleatorio(3);
Console.WriteLine($"  Profesor creado: {profesor}");
Console.WriteLine();

// Crear 20 Alumnos y registrarlos como observadores del profesor
Console.WriteLine("  Registrando 20 alumnos como observadores...");
for (int i = 0; i < 20; i++)
{
    Alumno alumnoObs = (Alumno)FabricaDeComparables.CrearAleatorio(2);
    profesor.AgregarObservador(alumnoObs);
}
Console.WriteLine("  ✓ 20 alumnos registrados.");
Console.WriteLine();

// Ejecutar el dictado de clases (Ej 13): 5 rondas de hablar + escribir
Console.WriteLine("  ► Iniciando dictado de clases (5 rondas):");
Console.WriteLine();
servicio.DictadoDeClases(profesor);

Console.WriteLine();
Console.WriteLine("Presione cualquier tecla para continuar a la Práctica 4...");
Console.ReadKey();

// ════════════════════════════════════════════════════════════════════════════
//  PRÁCTICA 4 — Adapter + Decorator (Ejercicios 1–8)
// ════════════════════════════════════════════════════════════════════════════
Console.WriteLine();
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║    PRÁCTICA 4 — Adapter + Decorator                      ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine();

// ─── Datos para crear los 20 Students adaptados ──────────────────────────────
// Nombres con apellido para que el formato "Nombre Apellido  nota" quede legible.
(string nombre, int dni, int legajo, double promedio)[] datosAlumno =
{
    ("Juan García",        12345678, 1001, 7.5),
    ("María López",        23456789, 2002, 6.0),
    ("Carlos Rodríguez",   34567890, 3003, 8.2),
    ("Ana Martínez",       45678901, 4004, 5.5),
    ("Pedro Sánchez",      56789012, 5005, 9.0),
    ("Lucía Fernández",    67890123, 6006, 4.8),
    ("Diego Pérez",        78901234, 7007, 3.2),
    ("Valentina Torres",   89012345, 8008, 7.0),
    ("Mateo Ramírez",      90123456, 9009, 6.7),
    ("Sofía González",     11223344, 1234, 8.9),
};

(string nombre, int dni, int legajo, double promedio)[] datosMuyEstudioso =
{
    ("Ratón Pérez",        19283746, 5671, 9.5),
    ("Rosa Blanco",        28374659, 6782, 8.0),
    ("Felipe Castro",      37465827, 7893, 7.3),
    ("Carmen Ruiz",        46572938, 8904, 6.1),
    ("Jorge Morales",      55647382, 9015, 9.8),
    ("Isabel Díaz",        64738291, 1126, 7.7),
    ("Luis Vargas",        73829104, 2237, 8.4),
    ("Patricia Romero",    82910473, 3348, 5.9),
    ("Andrés Jiménez",     91047382, 4459, 6.5),
    ("Teresa Molina",      10293847, 5560, 7.1),
};

// ─── Instanciar Teacher (del sistema MDPI — sin modificar) ───────────────────
Teacher teacher = new Teacher();

Console.WriteLine("── Creando 10 Alumno adaptados con cadena de decoradores ──");
foreach (var (nombre, dni, legajo, promedio) in datosAlumno)
{
    AlumnoProxy a = new AlumnoProxy(nombre, dni, legajo, promedio, false);
    a.SetEstrategia(new ComparacionPorNombre());

    // ── Encadenamiento dinámico de decoradores (OBLIGATORIO, según enunciado) ──
    IMostrarCalificacion decorador =
        new DecoradorAsteriscos(
            new DecoradorCondicion(
                new DecoradorNotaEnLetras(
                    new DecoradorLegajo(
                        new MostrarCalificacionSimple(a),
                    a),
                a),
            a));

    teacher.goToClass(new AlumnoAdapter(a, decorador));
}

Console.WriteLine("── Creando 10 AlumnoMuyEstudioso adaptados ─────────────────");
foreach (var (nombre, dni, legajo, promedio) in datosMuyEstudioso)
{
    AlumnoProxy ame = new AlumnoProxy(nombre, dni, legajo, promedio, true);
    ame.SetEstrategia(new ComparacionPorNombre());

    IMostrarCalificacion decorador =
        new DecoradorAsteriscos(
            new DecoradorCondicion(
                new DecoradorNotaEnLetras(
                    new DecoradorLegajo(
                        new MostrarCalificacionSimple(ame),
                    ame),
                ame),
            ame));

    teacher.goToClass(new AlumnoAdapter(ame, decorador));
}

Console.WriteLine();
Console.WriteLine("── Teacher.teachingAClass() ─────────────────────────────────");
Console.WriteLine();

// ─── El Teacher toma lista, examina y publica resultados con decoradores ──────
teacher.teachingAClass();

Console.WriteLine();
Console.WriteLine("Presione cualquier tecla para continuar a la PARTE FINAL (Proxy + Command)...");
Console.ReadKey();

// ════════════════════════════════════════════════════════════════════════════
//  PARTE 8 — MAIN FINAL (Proxy + Command)
// ════════════════════════════════════════════════════════════════════════════
Console.WriteLine();
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║    PARTE FINAL — Proxy + Command + Aula                  ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine();

// 1. Crear Aula
Aula aula = new Aula();

// 2. Crear Pila
Pila pilaComandos = new Pila();

// 3. Configurar órdenes
pilaComandos.setOrdenInicio(new OrdenInicio(aula));
pilaComandos.setOrdenLlegaAlumno(new OrdenLlegaAlumno(aula));
pilaComandos.setOrdenAulaLlena(new OrdenAulaLlena(aula));

// 4. Llenar Pila (20 Alumno + 20 AlumnoMuyEstudioso)
Console.WriteLine("► Agregando 20 alumnos regulares...");
servicio.Llenar(pilaComandos, 2); // Opción 2: Alumno

Console.WriteLine("► Agregando 20 alumnos muy estudiosos...");
servicio.Llenar(pilaComandos, 4); // Opción 4: Alumno Muy Estudioso

Console.WriteLine();
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║          Fin del Programa                               ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();


