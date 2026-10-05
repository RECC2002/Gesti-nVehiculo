using System;
using System.Globalization;
using GestionVehiculos.Modelos;
using GestionVehiculos.Servicios;

namespace GestionVehiculos
{
    internal class Program
    {
        private static GestorFlota gestor = new GestorFlota();

        static void Main(string[] args)
        {
            try { Console.Title = "Sistema de Gestión y Renta de Vehículos"; } catch { }
            int opcion = 0;

            do
            {
                MostrarMenuPrincipal();
                opcion = LeerEntero("Seleccione una opción (1-11): ", 1, 11);

                LimpiarPantalla();
                switch (opcion)
                {
                    case 1:
                        SubmenuRegistrarVehiculo();
                        break;
                    case 2:
                        gestor.MostrarTodos();
                        break;
                    case 3:
                        BuscarVehiculoPorCodigo();
                        break;
                    case 4:
                        gestor.MostrarPorEstado("Disponible");
                        break;
                    case 5:
                        gestor.MostrarPorEstado("Rentado");
                        break;
                    case 6:
                        CambiarEstadoVehiculo();
                        break;
                    case 7:
                        gestor.MostrarMayorKilometraje();
                        break;
                    case 8:
                        gestor.CalcularPrecioPromedio();
                        break;
                    case 9:
                        gestor.ContarPorTipo();
                        break;
                    case 10:
                        MenuMantenimiento();
                        break;
                    case 11:
                        MostrarMensajeColor("\n¡Gracias por utilizar el Sistema de Gestión y Renta de Vehículos! Hasta pronto.\n", ConsoleColor.Green);
                        break;
                }

                if (opcion != 11)
                {
                    Pausar();
                }

            } while (opcion != 11);
        }

        private static void MostrarMenuPrincipal()
        {
            LimpiarPantalla();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("===== SISTEMA DE RENTA DE VEHÍCULOS =====");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Console.WriteLine("1.  Registrar vehículo");
            Console.WriteLine("2.  Mostrar todos los vehículos");
            Console.WriteLine("3.  Buscar vehículo por código");
            Console.WriteLine("4.  Mostrar vehículos disponibles");
            Console.WriteLine("5.  Mostrar vehículos rentados");
            Console.WriteLine("6.  Cambiar estado");
            Console.WriteLine("7.  Vehículo con mayor kilometraje");
            Console.WriteLine("8.  Calcular precio promedio de renta");
            Console.WriteLine("9.  Cantidad de vehículos por tipo");
            Console.WriteLine("10. Realizar mantenimiento");
            Console.WriteLine("11. Salir");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        // Submenú y registro polimórfico
        private static void SubmenuRegistrarVehiculo()
        {
            MostrarEncabezado("REGISTRO POLIMÓRFICO DE VEHÍCULO");
            Console.WriteLine("1. Automóvil");
            Console.WriteLine("2. Camioneta");
            Console.WriteLine("3. Motocicleta");
            Console.WriteLine("4. Cancelar");

            int tipo = LeerEntero("Seleccione el tipo de vehículo a registrar (1-4): ", 1, 4);
            if (tipo == 4) return;

            Console.WriteLine("\n--- DATOS GENERALES ---");
            string codigo = "";
            while (true)
            {
                codigo = LeerTexto("Código de identificación (ej. AUT-101): ").ToUpper();
                if (gestor.BuscarPorCodigo(codigo) != null)
                {
                    MostrarMensajeColor("¡Error! Ya existe un vehículo con ese código. Ingrese otro.", ConsoleColor.Red);
                }
                else
                {
                    break;
                }
            }

            string marca = LeerTexto("Marca: ");
            string modelo = LeerTexto("Modelo: ");
            int anio = LeerEntero("Año de fabricación (1980 - 2026): ", 1980, 2026);
            double km = LeerDouble("Kilometraje actual (>= 0): ", 0);
            double precio = LeerDouble("Precio de renta por día en USD (> 0): ", 0.01);

            Vehiculo? nuevoVehiculo = null;

            switch (tipo)
            {
                case 1:
                    Console.WriteLine("\n--- ESPECIFICACIONES DE AUTOMÓVIL ---");
                    int puertas = LeerEntero("Número de puertas (2 - 5): ", 2, 5);
                    string transmision = LeerOpcionMultiple("Tipo de transmisión", new[] { "Manual", "Automática" });
                    string combustible = LeerOpcionMultiple("Tipo de combustible", new[] { "Gasolina", "Diésel", "Híbrido", "Eléctrico" });
                    bool aire = LeerBooleano("¿Tiene aire acondicionado? (S/N): ");
                    int pasajerosAuto = LeerEntero("Capacidad de pasajeros (1 - 9): ", 1, 9);

                    nuevoVehiculo = new Automovil(codigo, marca, modelo, anio, km, precio, puertas, transmision, combustible, aire, pasajerosAuto);
                    break;

                case 2:
                    Console.WriteLine("\n--- ESPECIFICACIONES DE CAMIONETA ---");
                    double carga = LeerDouble("Capacidad de carga útil en toneladas (ej. 1.5): ", 0.1);
                    string traccion = LeerOpcionMultiple("Tipo de tracción", new[] { "4x2", "4x4", "AWD" });
                    bool cabina = LeerBooleano("¿Tiene doble cabina? (S/N): ");
                    int pasajerosCamioneta = LeerEntero("Capacidad de pasajeros (2 - 9): ", 2, 9);

                    nuevoVehiculo = new Camioneta(codigo, marca, modelo, anio, km, precio, carga, traccion, cabina, pasajerosCamioneta);
                    break;

                case 3:
                    Console.WriteLine("\n--- ESPECIFICACIONES DE MOTOCICLETA ---");
                    int cilindrada = LeerEntero("Cilindrada en cc (50 - 2500): ", 50, 2500);
                    string tipoMoto = LeerOpcionMultiple("Tipo de motocicleta", new[] { "Scooter", "Enduro", "Pistera", "Chopper", "Turismo" });
                    bool baul = LeerBooleano("¿Tiene baúl / top case? (S/N): ");
                    int cascos = LeerEntero("Número de cascos incluidos (0 - 2): ", 0, 2);

                    nuevoVehiculo = new Motocicleta(codigo, marca, modelo, anio, km, precio, cilindrada, tipoMoto, baul, cascos);
                    break;
            }

            if (nuevoVehiculo != null && gestor.RegistrarVehiculo(nuevoVehiculo))
            {
                MostrarMensajeColor($"\n¡Éxito! El vehículo {codigo} ({nuevoVehiculo.Tipo}) fue registrado correctamente.", ConsoleColor.Green);
            }
            else
            {
                MostrarMensajeColor("\nNo se pudo registrar el vehículo.", ConsoleColor.Red);
            }
        }

        // Búsqueda por código
        private static void BuscarVehiculoPorCodigo()
        {
            MostrarEncabezado("BÚSQUEDA DE VEHÍCULO POR CÓDIGO");
            string codigo = LeerTexto("Ingrese el código del vehículo a buscar: ");
            Vehiculo? v = gestor.BuscarPorCodigo(codigo);

            if (v != null)
            {
                Console.WriteLine("\nVehículo localizado exitosamente:");
                v.MostrarInformacion();
            }
            else
            {
                MostrarMensajeColor($"No se encontró ningún vehículo registrado con el código '{codigo}'.", ConsoleColor.Yellow);
            }
        }

        // Cambio de estado
        private static void CambiarEstadoVehiculo()
        {
            MostrarEncabezado("CAMBIO DE ESTADO DE VEHÍCULO");
            string codigo = LeerTexto("Ingrese el código del vehículo: ");
            Vehiculo? v = gestor.BuscarPorCodigo(codigo);

            if (v == null)
            {
                MostrarMensajeColor($"Vehículo '{codigo}' no encontrado.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine($"\nVehículo actual: {v.Marca} {v.Modelo} ({v.Tipo}) | Estado actual: {v.Estado}");
            Console.WriteLine("Nuevo estado:");
            Console.WriteLine("1. Disponible");
            Console.WriteLine("2. Rentado");
            Console.WriteLine("3. En Mantenimiento");

            int seleccion = LeerEntero("Seleccione el nuevo estado (1-3): ", 1, 3);
            string nuevoEstado = seleccion switch
            {
                1 => "Disponible",
                2 => "Rentado",
                3 => "En Mantenimiento",
                _ => "Disponible"
            };

            if (gestor.CambiarEstado(codigo, nuevoEstado))
            {
                MostrarMensajeColor($"Estado del vehículo '{codigo}' actualizado a '{nuevoEstado}' correctamente.", ConsoleColor.Green);
            }
        }

        // Módulo de Mantenimiento usando la interfaz IMantenimiento
        private static void MenuMantenimiento()
        {
            MostrarEncabezado("GESTIÓN DE MANTENIMIENTO (IMantenimiento)");
            string codigo = LeerTexto("Ingrese el código del vehículo: ");
            Vehiculo? v = gestor.BuscarPorCodigo(codigo);

            if (v == null)
            {
                MostrarMensajeColor($"Vehículo '{codigo}' no encontrado.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine($"\nVehículo seleccionado: {v.Codigo} - {v.Marca} {v.Modelo} ({v.Tipo})");
            Console.WriteLine("1. Consultar estado de mantenimiento");
            Console.WriteLine("2. Ejecutar mantenimiento correctivo/preventivo");
            int accion = LeerEntero("Seleccione la acción a realizar (1-2): ", 1, 2);

            Console.WriteLine();
            gestor.AtenderMantenimiento(codigo, accion);
        }

        // Helpers de lectura segura por consola
        public static int LeerEntero(string mensaje, int min = int.MinValue, int max = int.MaxValue)
        {
            int valor;
            Console.Write(mensaje);
            while (!int.TryParse(Console.ReadLine(), out valor) || valor < min || valor > max)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (min != int.MinValue && max != int.MaxValue)
                {
                    Console.Write($"Entrada inválida. Debe ser un número entre {min} y {max}: ");
                }
                else
                {
                    Console.Write("Entrada no válida. Ingrese un número entero: ");
                }
                Console.ResetColor();
            }
            return valor;
        }

        public static double LeerDouble(string mensaje, double min = double.MinValue)
        {
            double valor;
            Console.Write(mensaje);
            while (true)
            {
                string? entrada = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(entrada))
                {
                    // Reemplaza coma por punto para permitir ambos separadores decimales
                    string normalizada = entrada.Replace(',', '.');
                    if (double.TryParse(normalizada, NumberStyles.Float, CultureInfo.InvariantCulture, out valor) && valor >= min)
                    {
                        return valor;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Entrada no válida. Ingrese un valor numérico decimal válido (>= {min}): ");
                Console.ResetColor();
            }
        }

        public static string LeerTexto(string mensaje, bool permitirVacio = false)
        {
            string? entrada;
            Console.Write(mensaje);
            while (true)
            {
                entrada = Console.ReadLine();
                if (!permitirVacio && string.IsNullOrWhiteSpace(entrada))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("El texto no puede estar vacío. Intente nuevamente: ");
                    Console.ResetColor();
                    continue;
                }
                return entrada?.Trim() ?? string.Empty;
            }
        }

        public static bool LeerBooleano(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? resp = Console.ReadLine()?.Trim().ToUpper();
                if (resp == "S" || resp == "SI" || resp == "SÍ") return true;
                if (resp == "N" || resp == "NO") return false;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Respuesta no válida. Escriba 'S' para Sí o 'N' para No.");
                Console.ResetColor();
            }
        }

        public static string LeerOpcionMultiple(string prompt, string[] opciones)
        {
            Console.WriteLine($"{prompt}:");
            for (int i = 0; i < opciones.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {opciones[i]}");
            }
            int seleccion = LeerEntero($"Seleccione una opción (1-{opciones.Length}): ", 1, opciones.Length);
            return opciones[seleccion - 1];
        }

        private static void MostrarEncabezado(string titulo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n=== {titulo} ===");
            Console.ResetColor();
        }

        private static void MostrarMensajeColor(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        private static void LimpiarPantalla()
        {
            try
            {
                Console.Clear();
            }
            catch
            {
                // Silencioso en caso de redirección de flujo o entornos de prueba sin consola nativa
            }
        }

        private static void Pausar()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
            Console.ResetColor();
            try
            {
                Console.ReadKey();
            }
            catch
            {
                // En terminales sin lectura de teclas directas (o redirigidas) espera un enter
                Console.ReadLine();
            }
        }
    }
}
