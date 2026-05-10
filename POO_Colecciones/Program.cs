using POO_Colecciones.Domain.Collections;
using POO_Colecciones.Domain.Entities;
using POO_Colecciones.Domain.Strategies;
using POO_Colecciones.Services;

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
Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
Console.WriteLine("║              Fin de las prácticas 1, 2 y 3               ║");
Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();

