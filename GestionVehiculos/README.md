# Sistema de Gestión y Renta de Vehículos

Aplicación de consola en **C# (.NET 8.0)** orientada a objetos para la administración, control y renta de una flota heterogénea de vehículos (Automóviles, Camionetas y Motocicletas).

---

## Estructura del Proyecto

```text
GestionVehiculos/
├── Interfaces/
│   └── IMantenimiento.cs       # Define el contrato de mantenimiento
├── Modelos/
│   ├── Vehiculo.cs             # Clase base abstracta con atributos comunes
│   ├── Automovil.cs            # Clase derivada con atributos de autos
│   ├── Camioneta.cs            # Clase derivada con atributos de carga/tracción
│   └── Motocicleta.cs          # Clase derivada con atributos de motos
├── Servicios/
│   └── GestorFlota.cs          # Lógica de negocio y colección polimórfica (Vehiculo[])
├── Program.cs                  # Menú interactivo (11 opciones) y validaciones seguras
├── GestionVehiculos.csproj     # Configuración del proyecto .NET 8.0
└── README.md                   # Documentación y guía completa de sustentación
```

---

## Cómo Ejecutar el Proyecto

1. Abrir una terminal en la carpeta del proyecto:
   ```bash
   cd GestionVehiculos
   ```
2. Ejecutar con el CLI de .NET:
   ```bash
   dotnet run
   ```
   *(Si el comando `dotnet` no es reconocido en tu terminal actual, abre una nueva ventana de terminal o ejecuta directamente usando la ruta completa: `& "C:\Program Files\dotnet\dotnet.exe" run`).*

---

## Conceptos de POO Aplicados

| Pilar de POO | Implementación en el Código | Propósito Técnico |
| :--- | :--- | :--- |
| **Abstracción** | `public abstract class Vehiculo` | Modela las características esenciales comunes e impide instanciar un vehículo genérico sin especializar. |
| **Encapsulamiento** | Propiedades `{ get; set; }` y campos `private` en `GestorFlota` | Protege el estado interno; el arreglo `flota` y el contador `totalVehiculos` solo se modifican por métodos controlados. |
| **Herencia** | `Automovil`, `Camioneta`, `Motocicleta : Vehiculo` | Reutiliza los atributos comunes y la lógica del constructor padre mediante la cláusula `: base(...)`. |
| **Polimorfismo** | Arreglo `Vehiculo[]` y método `virtual`/`override` | Permite almacenar objetos heterogéneos y resolver dinámicamente en tiempo de ejecución la versión específica de `MostrarInformacion()`. |
| **Interfaces** | `public interface IMantenimiento` | Define un contrato operativo independiente que desacopla el mantenimiento del resto de la jerarquía. |

---

