using POO_Colecciones.Domain.Collections;
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

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
