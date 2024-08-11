# Nagaira.Core.WebApi

Esta biblioteca proporciona extensiones y utilidades para trabajar con JWT (JSON Web Tokens) en aplicaciones web ASP.NET Core. Facilita la implementaci�n de autenticaci�n y autorizaci�n basada en tokens JWT.

## Instalaci�n

Para utilizar esta biblioteca en tu proyecto, puedes instalarla a trav�s de NuGet:

```bash
Install-Package Nagaira.Core.WebApi
```
# Documentaci�n de `SwaggerConfig`

La clase `SwaggerConfig` en el espacio de nombres `Nagaira.Core.WebApi.Extensions` proporciona una configuraci�n para integrar Swagger en aplicaciones ASP.NET Core. Swagger es una herramienta que ayuda a documentar y probar las API RESTful. La configuraci�n permite habilitar la documentaci�n de la API y agregar soporte para autenticaci�n con JWT (JSON Web Tokens).

## Clase `SwaggerConfig`

La clase `SwaggerConfig` contiene m�todos est�ticos que se utilizan para configurar Swagger en el servicio de dependencias de ASP.NET Core.

### M�todo `ConfigureSwaggerGen`

Este m�todo configura Swagger para generar documentaci�n de la API y agregar autenticaci�n basada en JWT.
### Par�metros
-   `services` (`IServiceCollection`): La colecci�n de servicios en la que se configura Swagger.
-   `assemblyName` (`string`): El nombre del ensamblaje que se usar� como t�tulo en la documentaci�n de Swagger.
-   `version` (`string`): La versi�n de la API que se mostrar� en la documentaci�n.

### Uso
Para utilizar la configuraci�n proporcionada por `SwaggerConfig`, debes llamar al m�todo `ConfigureSwaggerGen` desde el m�todo `ConfigureServices` en tu archivo `Startup.cs`:
```csharp
public void ConfigureServices(IServiceCollection services)
{
    SwaggerConfig.ConfigureSwaggerGen(services, "MiApi", "v1");
    // Otras configuraciones...
}

```
Aseg�rate de que Swagger est� habilitado en tu aplicaci�n ASP.NET Core configurando el middleware en el m�todo `Configure` de `Startup.cs`:
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
# Documentaci�n de `CorsOptionExtension`

La clase `CorsOptionExtension` proporciona un m�todo de extensi�n para configurar pol�ticas de CORS (Cross-Origin Resource Sharing) en aplicaciones ASP.NET Core. Esta extensi�n permite definir f�cilmente pol�ticas de CORS con or�genes espec�ficos o permitir cualquier origen.


## Clase `CorsOptionExtension`

La clase `SwaggerConfig` contiene m�todos est�ticos que se utilizan para configurar Swagger en el servicio de dependencias de ASP.NET Core.

### M�todo `AddPolicyCors`

El m�todo `AddPolicyCors` a�ade una pol�tica de CORS al objeto `CorsOptions`. Configura la pol�tica seg�n los or�genes proporcionados:

-   Si no se especifican or�genes (`origins.Length == 0`), la pol�tica permite cualquier origen.
-   Si se especifican uno o m�s or�genes, solo esos or�genes est�n permitidos.

Adem�s, la pol�tica permite cualquier encabezado, cualquier m�todo y credenciales.

### Par�metros

-   `name`: Una cadena que representa el nombre de la pol�tica de CORS.
-   `origins`: Un arreglo de cadenas que representa los or�genes permitidos. Si no se proporcionan or�genes, la pol�tica permitir� cualquier origen.

### Uso
Para utilizar la configuraci�n proporcionada por `SwaggerConfig`, se debe definir desde el m�todo `ConfigureServices` en tu archivo `Startup.cs` como extensi�n de `options`:
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
En el m�todo `Configure` de `Startup.cs`, habilita el middleware de CORS y especifica la pol�tica a usar (esta debe ser la misma que se escribi� en `AddPolicyCors`):
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseCors("MiPolitica");
    // Otras configuraciones de middleware...
}
```
#### Importante
Para todos los origenes que se registren, se permitir�n todas las cabeceras, todos los m�todos y todas las credenciales

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