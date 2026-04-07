using POO_Colecciones.Domain.Collections;
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
Cola cola  = new();
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

Console.WriteLine("Presione cualquier tecla para salir...");
Console.ReadKey();
