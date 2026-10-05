using System;
using GestionVehiculos.Interfaces;
using GestionVehiculos.Modelos;

namespace GestionVehiculos.Servicios
{
    public class GestorFlota
    {
        // Rubro 2: Almacena y procesa objetos heterogéneos en un arreglo Vehiculo[]
        private Vehiculo[] flota;
        private int totalVehiculos;

        public GestorFlota(int capacidadInicial = 10)
        {
            flota = new Vehiculo[capacidadInicial];
            totalVehiculos = 0;
            CargarDatosIniciales();
        }

        public int TotalRegistrados => totalVehiculos;

        // Carga de datos de prueba para demostración y evaluación rápida
        private void CargarDatosIniciales()
        {
            RegistrarVehiculo(new Automovil(
                codigo: "AUT-001",
                marca: "Toyota",
                modelo: "Corolla",
                anio: 2022,
                kilometros: 35400,
                precioRenta: 45.0,
                numeroPuertas: 4,
                tipoTransmision: "Automática",
                tipoCombustible: "Gasolina",
                tieneAireAcondicionado: true,
                numeroPasajeros: 5
            ));

            RegistrarVehiculo(new Camioneta(
                codigo: "CAM-001",
                marca: "Toyota",
                modelo: "Hilux",
                anio: 2021,
                kilometros: 68200,
                precioRenta: 85.0,
                capacidadCarga: 1.2,
                tipoTraccion: "4x4",
                tieneDobleCabina: true,
                numeroPasajeros: 5
            ));

            RegistrarVehiculo(new Motocicleta(
                codigo: "MOT-001",
                marca: "Yamaha",
                modelo: "MT-07",
                anio: 2023,
                kilometros: 12500,
                precioRenta: 30.0,
                cilindrada: 689,
                tipoMotocicleta: "Pistera",
                tieneBaul: false,
                numeroCascos: 2
            ));
        }

        // 1. Registrar vehículo
        public bool RegistrarVehiculo(Vehiculo nuevo)
        {
            if (nuevo == null) return false;

            // Validación de código único (no duplicados)
            if (BuscarPorCodigo(nuevo.Codigo) != null)
            {
                return false;
            }

            // Expansión dinámica del arreglo si se alcanza la capacidad máxima
            if (totalVehiculos >= flota.Length)
            {
                int nuevaCapacidad = flota.Length == 0 ? 4 : flota.Length * 2;
                Array.Resize(ref flota, nuevaCapacidad);
            }

            flota[totalVehiculos] = nuevo;
            totalVehiculos++;
            return true;
        }

        // 2. Mostrar todos los vehículos (Polimorfismo en ejecución)
        public void MostrarTodos()
        {
            if (totalVehiculos == 0)
            {
                Console.WriteLine("No hay vehículos registrados en la flota.");
                return;
            }

            Console.WriteLine($"\n=== LISTADO GENERAL DE LA FLOTA ({totalVehiculos} Vehículos) ===");
            for (int i = 0; i < totalVehiculos; i++)
            {
                flota[i].MostrarInformacion();
            }
            Console.WriteLine("----------------------------------------\n");
        }

        // 3. Buscar vehículo por código (Equals con OrdinalIgnoreCase)
        public Vehiculo? BuscarPorCodigo(string codigo)
        {
            for (int i = 0; i < totalVehiculos; i++)
            {
                if (flota[i] != null && flota[i].Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                {
                    return flota[i];
                }
            }
            return null;
        }

        // 4 y 5. Mostrar vehículos por estado ("Disponible" / "Rentado" / "En Mantenimiento")
        public void MostrarPorEstado(string estadoBuscado)
        {
            int encontrados = 0;
            Console.WriteLine($"\n=== VEHÍCULOS EN ESTADO: '{estadoBuscado.ToUpper()}' ===");
            for (int i = 0; i < totalVehiculos; i++)
            {
                if (flota[i] != null && flota[i].Estado.Equals(estadoBuscado, StringComparison.OrdinalIgnoreCase))
                {
                    flota[i].MostrarInformacion();
                    encontrados++;
                }
            }

            if (encontrados == 0)
            {
                Console.WriteLine($"No se encontraron vehículos con estado '{estadoBuscado}'.");
            }
            else
            {
                Console.WriteLine($"Total encontrados: {encontrados}");
            }
            Console.WriteLine("----------------------------------------\n");
        }

        // 6. Cambiar estado de un vehículo
        public bool CambiarEstado(string codigo, string nuevoEstado)
        {
            Vehiculo? v = BuscarPorCodigo(codigo);
            if (v == null) return false;

            v.Estado = nuevoEstado;
            if (nuevoEstado.Equals("En Mantenimiento", StringComparison.OrdinalIgnoreCase))
            {
                v.RequiereMantenimiento = true;
            }
            else if (nuevoEstado.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
            {
                v.RequiereMantenimiento = false;
            }
            return true;
        }

        // 7. Vehículo con mayor kilometraje (Algoritmo de búsqueda de máximo)
        public void MostrarMayorKilometraje()
        {
            if (totalVehiculos == 0)
            {
                Console.WriteLine("No hay vehículos en la flota para evaluar.");
                return;
            }

            Vehiculo mayor = flota[0];
            for (int i = 0; i < totalVehiculos; i++)
            {
                var v = flota[i];
                if (v != null && v.Kilometros > mayor.Kilometros)
                {
                    mayor = v;
                }
            }

            Console.WriteLine("\n=== VEHÍCULO CON MAYOR KILOMETRAJE ===");
            mayor.MostrarInformacion();
            Console.WriteLine($"-> Kilometraje registrado: {mayor.Kilometros:N0} km");
            Console.WriteLine("----------------------------------------\n");
        }

        // 8. Calcular precio promedio de renta
        public void CalcularPrecioPromedio()
        {
            if (totalVehiculos == 0)
            {
                Console.WriteLine("No hay vehículos para calcular el promedio.");
                return;
            }

            double suma = 0;
            for (int i = 0; i < totalVehiculos; i++)
            {
                suma += flota[i].PrecioRenta;
            }

            double promedio = suma / totalVehiculos;
            Console.WriteLine("\n=== PRECIO PROMEDIO DE RENTA DIARIA ===");
            Console.WriteLine($"Total de vehículos analizados: {totalVehiculos}");
            Console.WriteLine($"Suma total de tarifas diarias: ${suma:F2}");
            Console.WriteLine($"Precio promedio de renta por día: ${promedio:F2}");
            Console.WriteLine("----------------------------------------\n");
        }

        // 9. Cantidad de vehículos por tipo con operador 'is'
        public void ContarPorTipo()
        {
            int autos = 0, camionetas = 0, motos = 0;
            for (int i = 0; i < totalVehiculos; i++)
            {
                var v = flota[i];
                if (v is Automovil) autos++;
                else if (v is Camioneta) camionetas++;
                else if (v is Motocicleta) motos++;
            }

            Console.WriteLine("\n=== CONTEO DE VEHÍCULOS POR TIPO ===");
            Console.WriteLine($"🚗 Automóviles : {autos} ({(totalVehiculos > 0 ? (autos * 100.0 / totalVehiculos) : 0):F1}%)");
            Console.WriteLine($"🚙 Camionetas  : {camionetas} ({(totalVehiculos > 0 ? (camionetas * 100.0 / totalVehiculos) : 0):F1}%)");
            Console.WriteLine($"🏍️ Motocicletas: {motos} ({(totalVehiculos > 0 ? (motos * 100.0 / totalVehiculos) : 0):F1}%)");
            Console.WriteLine($"------------------------------------");
            Console.WriteLine($"Total en Flota : {totalVehiculos}");
            Console.WriteLine("------------------------------------\n");
        }

        // 10. Operaciones de mantenimiento mediante la interfaz IMantenimiento
        public bool AtenderMantenimiento(string codigo, int accion)
        {
            Vehiculo? v = BuscarPorCodigo(codigo);
            if (v == null) return false;

            // Uso de la interfaz IMantenimiento desacoplada
            IMantenimiento mantenimiento = v;

            if (accion == 1)
            {
                mantenimiento.ConsultarMantenimiento();
            }
            else if (accion == 2)
            {
                mantenimiento.RealizarMantenimiento();
            }

            return true;
        }
    }
}
