# Sistema de Gestión y Renta de Vehículos

Aplicación de consola en **C# (.NET 8.0)** orientada a objetos para la administración, control y renta de una flota heterogénea de vehículos (Automóviles, Camionetas y Motocicletas).

---

## 🏗️ Estructura del Proyecto

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

## 🚀 Cómo Ejecutar el Proyecto

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

## 🧠 Conceptos de POO Aplicados

| Pilar de POO | Implementación en el Código | Propósito Técnico |
| :--- | :--- | :--- |
| **Abstracción** | `public abstract class Vehiculo` | Modela las características esenciales comunes e impide instanciar un vehículo genérico sin especializar. |
| **Encapsulamiento** | Propiedades `{ get; set; }` y campos `private` en `GestorFlota` | Protege el estado interno; el arreglo `flota` y el contador `totalVehiculos` solo se modifican por métodos controlados. |
| **Herencia** | `Automovil`, `Camioneta`, `Motocicleta : Vehiculo` | Reutiliza los atributos comunes y la lógica del constructor padre mediante la cláusula `: base(...)`. |
| **Polimorfismo** | Arreglo `Vehiculo[]` y método `virtual`/`override` | Permite almacenar objetos heterogéneos y resolver dinámicamente en tiempo de ejecución la versión específica de `MostrarInformacion()`. |
| **Interfaces** | `public interface IMantenimiento` | Define un contrato operativo independiente que desacopla el mantenimiento del resto de la jerarquía. |

---

## 🗣️ Guía Maestra para la Explicación del Código

Esta sección está diseñada para guiarte en una sustentación o exposición paso a paso:

### Fase 1: El Contrato y la Clase Base Abstracta
1. **`Interfaces/IMantenimiento.cs`**:
   - **Qué explicar:** Define dos métodos sin cuerpo: `RealizarMantenimiento()` y `ConsultarMantenimiento()`.
   - **Punto clave:** Cumple con el principio de desacoplamiento e inversión de dependencias. Cualquier otra clase futura (por ejemplo, maquinaria o herramientas de taller) podrá implementar esta interfaz sin necesidad de heredar de `Vehiculo`.
2. **`Modelos/Vehiculo.cs`**:
   - **Qué explicar:** Es una clase `abstract` que implementa `IMantenimiento`. Contiene los atributos que comparten todos los vehículos: `Codigo`, `Marca`, `Modelo`, `Anio`, `Kilometros`, `PrecioRenta` y `Estado`.
   - **Punto clave:** Su método `MostrarInformacion()` es `virtual`. Esto significa que define una visualización base, pero autoriza a las clases hijas a extenderla según sus atributos propios.

### Fase 2: Las Clases Derivadas Especializadas
1. **`Automovil.cs`**, **`Camioneta.cs`** y **`Motocicleta.cs`**:
   - **Qué explicar:** Cada una representa una especialización del mundo real y agrega atributos particulares:
     - *Automóvil:* Número de puertas, transmisión, combustible, aire acondicionado y pasajeros.
     - *Camioneta:* Capacidad de carga (toneladas), tipo de tracción (4x2, 4x4, AWD), cabina y pasajeros.
     - *Motocicleta:* Cilindrada (cc), tipo (Scooter, Pistera, Enduro), baúl y cascos incluidos.
   - **Uso de `: base(...)`:** En los constructores, se invocan los parámetros de la superclase para inicializar los atributos generales sin duplicar líneas de código (principio DRY - *Don't Repeat Yourself*).
   - **Uso de `override`:** Cada clase sobrescribe `MostrarInformacion()`, llamando primero a `base.MostrarInformacion()` y luego imprimiendo sus características particulares.

### Fase 3: Lógica de Negocio y Colección Polimórfica
1. **`Servicios/GestorFlota.cs`**:
   - **Colección Polimórfica (`Vehiculo[] flota`):** Se utiliza un solo arreglo de tipo base `Vehiculo[]` capaz de almacenar objetos de cualquiera de las tres subclases sin necesidad de listas separadas.
   - **Redimensionamiento Dinámico (`Array.Resize`):** Cuando el arreglo se llena (`totalVehiculos >= flota.Length`), se duplica su tamaño automáticamente para admitir nuevos registros.
   - **Búsqueda por Código (`BuscarPorCodigo`):** Recorre el arreglo y compara ignorando mayúsculas/minúsculas (`StringComparison.OrdinalIgnoreCase`).
   - **Vehículo con Mayor Kilometraje (`MostrarMayorKilometraje`):** Algoritmo de búsqueda lineal del valor máximo que compara el kilometraje de cada vehículo registrado.
   - **Precio Promedio de Renta (`CalcularPrecioPromedio`):** Acumula la suma de las tarifas diarias registradas y calcula la media aritmética.
   - **Inspección de Tipos con Operador `is` (`ContarPorTipo`):** Utiliza `v is Automovil`, `v is Camioneta` y `v is Motocicleta` para determinar en tiempo de ejecución el tipo real del objeto y calcular la distribución porcentual.
   - **Mantenimiento Desacoplado (`AtenderMantenimiento`):** Hace un casteo seguro a la interfaz `IMantenimiento mantenimiento = v;` para consultar o ejecutar el mantenimiento.

### Fase 4: Interfaz de Consola y Control de Errores
1. **`Program.cs`**:
   - **Bucle Principal (`do-while` y `switch`):** Permite navegar entre las 11 opciones del menú de forma cíclica y limpia hasta seleccionar la opción 11 (Salir).
   - **Validación Robusta de Entradas:**
     - `LeerEntero()`: Valida que el dato sea numérico y se encuentre dentro de un rango permitido usando `int.TryParse`.
     - `LeerDouble()`: Admite separadores decimales tanto con punto (`.`) como con coma (`,`) mediante `CultureInfo.InvariantCulture`, evitando fallos en configuraciones regionales en español.
     - `LeerTexto()`: Impide entradas vacías o compuestas solo por espacios.
     - `LeerBooleano()`: Acepta respuestas flexibles como "S", "SI", "SÍ", "N", "NO".
   - **Tolerancia a Fallos en Consola:** Métodos auxiliares `LimpiarPantalla()` y `Pausar()` previenen excepciones de consola en entornos redirigidos o depuradores.

---

## 💻 Guion para la Demostración en Vivo ante el Evaluador

Sigue este orden al proyectar o mostrar la aplicación en vivo:

1. **Mostrar la Flota Inicial (Opción 2):**  
   - Ejecuta la opción 2 y muestra cómo el programa lista automáticamente un Automóvil, una Camioneta y una Motocicleta de prueba.
   - *Comentario oral:* "Aquí podemos observar el polimorfismo: recorremos un único arreglo `Vehiculo[]` y cada objeto sabe cómo imprimirse a sí mismo con sus datos específicos."
2. **Distribución por Tipos con operador `is` (Opción 9):**  
   - Ejecuta la opción 9.
   - *Comentario oral:* "En esta opción el sistema utiliza el operador `is` para verificar el tipo derivado de cada elemento en memoria y calcular la proporción de cada categoría."
3. **Cálculos Estadísticos (Opciones 7 y 8):**  
   - Ejecuta la opción 7 para mostrar el vehículo con mayor kilometraje (Toyota Hilux con 68,200 km) y la opción 8 para ver el promedio diario de renta ($53.33 USD).
4. **Validación contra Errores de Usuario (Opción 1):**  
   - Selecciona registrar un vehículo. En el año o precio, escribe letras a propósito.
   - *Comentario oral:* "El programa está protegido contra entradas inválidas mediante `TryParse`; si el usuario ingresa un formato erróneo, el sistema avisa en color rojo y solicita el dato nuevamente sin detener la aplicación."
5. **Ciclo de Mantenimiento (Opciones 6 y 10):**  
   - Cambia el estado de un auto a *"En Mantenimiento"* mediante la opción 6.
   - Luego ingresa a la opción 10: consulta el diagnóstico y ejecuta el mantenimiento correctivo para ver cómo pasa nuevamente a *"Disponible"*.

---

## 🎓 Banco de Preguntas y Respuestas para la Sustentación Individual

1. **¿Por qué la clase `Vehiculo` es abstracta?**
   - *Respuesta:* Porque representa un concepto genérico. En un negocio de renta real nunca se renta un "vehículo abstracto", sino un automóvil, una camioneta o una motocicleta concreta. Además, sirve de plantilla obligatoria que asegura que todas las subclases tengan la misma estructura base.

2. **¿Qué ventaja tiene usar `: base(...)` en los constructores derivados?**
   - *Respuesta:* Permite reutilizar el constructor de la clase base. De esta manera, la asignación y validación de los atributos comunes (código, marca, modelo, año, kilometraje, precio) se centraliza en un único punto, evitando redundancia.

3. **¿Cómo funciona el polimorfismo al invocar `MostrarInformacion()`?**
   - *Respuesta:* Ocurre mediante *despacho dinámico* (*dynamic dispatch*). Aunque la variable en el ciclo sea de tipo `Vehiculo`, el entorno de ejecución de .NET detecta el tipo instanciado real y ejecuta el método `override` correspondiente, imprimiendo primero los datos generales con `base.MostrarInformacion()` y luego los detalles específicos.

4. **¿Por qué usar la interfaz `IMantenimiento` en lugar de programar los métodos directamente en `Vehiculo`?**
   - *Respuesta:* Por el principio de segregación de interfaces y bajo acoplamiento. Si el día de mañana la empresa adquiere elevadores hidráulicos, herramientas de taller o servidores que también requieren mantenimiento pero no son vehículos, pueden implementar `IMantenimiento` sin necesidad de forzar una herencia con `Vehiculo`.

5. **¿Qué función cumple el operador `is` en la opción 9?**
   - *Respuesta:* Realiza una comprobación de tipo en tiempo de ejecución de forma segura (*type checking*), verificando si la instancia actual implementa o desciende de una subclase específica sin necesidad de conversiones forzadas (*casting*) que puedan lanzar excepciones.

---

## 👥 Guía de Commits para el Equipo (3 - 4 Integrantes)

Para evidenciar el trabajo colaborativo en Git y GitHub conforme a la rúbrica de evaluación:

- **Integrante 1 (Arquitectura y Modelos Base):**
  - `git commit -m "feat: crear interfaz IMantenimiento y clase base abstracta Vehiculo"`
  - `git commit -m "feat: implementar clase Automovil heredando de Vehiculo"`
- **Integrante 2 (Modelos Derivados Especializados):**
  - `git commit -m "feat: implementar clase Camioneta con soporte para carga y tracción"`
  - `git commit -m "feat: implementar clase Motocicleta con cilindrada y accesorios"`
- **Integrante 3 (Lógica de Negocio y Colección Polimórfica):**
  - `git commit -m "feat: implementar GestorFlota con arreglo polimórfico Vehiculo[]"`
  - `git commit -m "feat: implementar algoritmos de búsqueda, kilometraje y conteo con operador is"`
- **Integrante 4 (Interfaz de Consola, Validaciones y Documentación):**
  - `git commit -m "feat: implementar menú interactivo y lectura segura en Program.cs"`
  - `git commit -m "docs: documentar arquitectura, guía de sustentación y demostración en README"`
