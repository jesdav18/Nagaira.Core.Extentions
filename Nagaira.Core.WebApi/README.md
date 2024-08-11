# Nagaira.Core.WebApi

Esta biblioteca proporciona extensiones y utilidades para trabajar con JWT (JSON Web Tokens) en aplicaciones web ASP.NET Core. Facilita la implementación de autenticación y autorización basada en tokens JWT.

## Instalación

Para utilizar esta biblioteca en tu proyecto, puedes instalarla a través de NuGet:

```bash
Install-Package Nagaira.Core.WebApi
```
# Documentación de `SwaggerConfig`

La clase `SwaggerConfig` en el espacio de nombres `Nagaira.Core.WebApi.Extensions` proporciona una configuración para integrar Swagger en aplicaciones ASP.NET Core. Swagger es una herramienta que ayuda a documentar y probar las API RESTful. La configuración permite habilitar la documentación de la API y agregar soporte para autenticación con JWT (JSON Web Tokens).

## Clase `SwaggerConfig`

La clase `SwaggerConfig` contiene métodos estáticos que se utilizan para configurar Swagger en el servicio de dependencias de ASP.NET Core.

### Método `ConfigureSwaggerGen`

Este método configura Swagger para generar documentación de la API y agregar autenticación basada en JWT.
### Parámetros
-   `services` (`IServiceCollection`): La colección de servicios en la que se configura Swagger.
-   `assemblyName` (`string`): El nombre del ensamblaje que se usará como título en la documentación de Swagger.
-   `version` (`string`): La versión de la API que se mostrará en la documentación.

### Uso
Para utilizar la configuración proporcionada por `SwaggerConfig`, debes llamar al método `ConfigureSwaggerGen` desde el método `ConfigureServices` en tu archivo `Startup.cs`:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    SwaggerConfig.ConfigureSwaggerGen(services, "MiApi", "v1");
    // Otras configuraciones...
}

```
Asegúrate de que Swagger esté habilitado en tu aplicación ASP.NET Core configurando el middleware en el método `Configure` de `Startup.cs`:
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiApi v1");
    });
    // Otras configuraciones...
}
```
# Documentación de `CorsOptionExtension`

La clase `CorsOptionExtension` proporciona un método de extensión para configurar políticas de CORS (Cross-Origin Resource Sharing) en aplicaciones ASP.NET Core. Esta extensión permite definir fácilmente políticas de CORS con orígenes específicos o permitir cualquier origen.


## Clase `CorsOptionExtension`

La clase `SwaggerConfig` contiene métodos estáticos que se utilizan para configurar Swagger en el servicio de dependencias de ASP.NET Core.

### Método `AddPolicyCors`

El método `AddPolicyCors` añade una política de CORS al objeto `CorsOptions`. Configura la política según los orígenes proporcionados:

-   Si no se especifican orígenes (`origins.Length == 0`), la política permite cualquier origen.
-   Si se especifican uno o más orígenes, solo esos orígenes están permitidos.

Además, la política permite cualquier encabezado, cualquier método y credenciales.

### Parámetros

-   `name`: Una cadena que representa el nombre de la política de CORS.
-   `origins`: Un arreglo de cadenas que representa los orígenes permitidos. Si no se proporcionan orígenes, la política permitirá cualquier origen.

### Uso
Para utilizar la configuración proporcionada por `SwaggerConfig`, se debe definir desde el método `ConfigureServices` en tu archivo `Startup.cs` como extensión de `options`:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicyCors("MiPolitica", "https://example.com", "https://anotherexample.com");
    });
    // Otras configuraciones de servicios...
}
```
En el método `Configure` de `Startup.cs`, habilita el middleware de CORS y especifica la política a usar (esta debe ser la misma que se escribió en `AddPolicyCors`):
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors("MiPolitica");
    // Otras configuraciones de middleware...
}
```
#### Importante
Para todos los origenes que se registren, se permitirán todas las cabeceras, todos los métodos y todas las credenciales

# Nagaira.Core.WebApi

This library provides extensions and utilities for working with JWT (JSON Web Tokens) in ASP.NET Core web applications. It facilitates the implementation of authentication and authorization based on JWT tokens.

## Installation

To use this library in your project, you can install it via NuGet:

```shell
Install-Package Nagaira.Core.WebApi` 
```

# Documentation for `SwaggerConfig`

The `SwaggerConfig` class in the `Nagaira.Core.WebApi.Extensions` namespace provides configuration for integrating Swagger into ASP.NET Core applications. Swagger is a tool that helps document and test RESTful APIs. This configuration enables API documentation and adds support for JWT (JSON Web Tokens) authentication.

## Class `SwaggerConfig`

The `SwaggerConfig` class contains static methods used to configure Swagger in the ASP.NET Core dependency service.

### Method `ConfigureSwaggerGen`

This method configures Swagger to generate API documentation and add JWT-based authentication.

#### Parameters

-   **`services`** (`IServiceCollection`): The service collection in which Swagger is configured.
-   **`assemblyName`** (`string`): The name of the assembly that will be used as the title in the Swagger documentation.
-   **`version`** (`string`): The version of the API to be displayed in the documentation.

### Usage

To use the configuration provided by `SwaggerConfig`, call the `ConfigureSwaggerGen` method from the `ConfigureServices` method in your `Startup.cs` file:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    SwaggerConfig.ConfigureSwaggerGen(services, "MyApi", "v1");
    // Other configurations...
}` 
```

Ensure Swagger is enabled in your ASP.NET Core application by configuring the middleware in the `Configure` method of `Startup.cs`:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApi v1");
    });
    // Other configurations...
}
```

# Documentation for `CorsOptionExtension`

The `CorsOptionExtension` class provides an extension method for configuring CORS (Cross-Origin Resource Sharing) policies in ASP.NET Core applications. This extension allows you to easily define CORS policies with specific origins or allow any origin.

## Class `CorsOptionExtension`

The `CorsOptionExtension` class contains static methods used to configure CORS options.

### Method `AddPolicyCors`

The `AddPolicyCors` method adds a CORS policy to the `CorsOptions` object. It configures the policy based on the provided origins:

-   If no origins are specified (`origins.Length == 0`), the policy allows any origin.
-   If one or more origins are specified, only those origins are allowed.

Additionally, the policy allows any header, any method, and credentials.

#### Parameters

-   **`name`**: A string representing the name of the CORS policy.
-   **`origins`**: An array of strings representing the allowed origins. If no origins are provided, the policy will allow any origin.

### Usage
To use the configuration provided by `CorsOptionExtension`, define it from the `ConfigureServices` method in your `Startup.cs` file as an extension to `options`:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicyCors("MyPolicy", "https://example.com", "https://anotherexample.com");
    });
    // Other service configurations...
} 
```
In the `Configure` method of `Startup.cs`, enable the CORS middleware and specify the policy to use (this should match the policy name used in `AddPolicyCors`):

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors("MyPolicy");
    // Other middleware configurations...
}
```
#### Important
All registered origins will allow all headers, all methods, and all credentials.