# Nagaira.WebApi.Utilities

Esta biblioteca contiene utilidades y extensiones para simplificar la configuración y el arranque de aplicaciones web ASP.NET Core. Incluye funcionalidades para el registro de logs, manejo de solicitudes HTTP y otras tareas comunes en el desarrollo de API web. Consulta la documentación específica dentro del directorio de la biblioteca para obtener más detalles.

## Instalación

Para utilizar esta biblioteca en tu proyecto, puedes instalarla a través de NuGet:

```bash
Install-Package Nagaira.Core.WebApi
```
# Documentación de `AsemblyExtension`

La clase `AsemblyExtension` en el espacio de nombres `Nagaira.WebApi.Utilities` contiene métodos estáticos para abstraer la información de ensamblado del aplicativo cuando se requiera, se devuelve en un simple objeto de transferencia de datos (DTO)

### Método `GetAssemblyInfo`

El método `GetAssemblyInfo` obtiene la información de un ensamblado proporcionado. Devuelve un objeto `AssemblyInfo` que contiene el nombre y la versión del ensamblado.

### Parámetros
- `assembly` (`System.Reflection.Assembly`): El ensamblado del cual se desea obtener la información. 

### Uso
```csharp
using System.Reflection; 
using Nagaira.WebApi.Utilities.Extensions;

AssemblyInfo assemblyInfo = AsemblyExtension.GetAssemblyInfo(assembly); 
Console.WriteLine($"Nombre: {assemblyInfo.Name}"); 
Console.WriteLine($"Versión: {assemblyInfo.Version}");
```


# Nagaira.WebApi.Utilities

This library contains utilities and extensions to simplify the configuration and startup of ASP.NET Core web applications. It includes functionalities for logging, handling HTTP requests, and other common tasks in web API development. Check the specific documentation within the library directory for more details.

## Installation

To use this library in your project, you can install it via NuGet:

```bash
Install-Package Nagaira.Core.WebApi

```
# Documentación de `AsemblyExtension`

The `AsemblyExtension` class in the `Nagaira.WebApi.Utilities` namespace contains static methods to abstract assembly information from the application when needed, returning it in a simple Data Transfer Object (DTO).

### Method `GetAssemblyInfo`

The `GetAssemblyInfo` method retrieves information from a provided assembly. It returns an `AssemblyInfo` object containing the assembly's name and version.

### Parameters

-   `assembly` (`System.Reflection.Assembly`): The assembly from which to obtain the information. 

### Usage
```csharp
using System.Reflection; 
using Nagaira.WebApi.Utilities.Extensions;

AssemblyInfo assemblyInfo = AsemblyExtension.GetAssemblyInfo(assembly); 
Console.WriteLine($"Name: {assemblyInfo.Name}"); 
Console.WriteLine($"Version: {assemblyInfo.Version}");
```