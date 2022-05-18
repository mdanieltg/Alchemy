# Alchemy

Mezclador de ingredientes para el juego [Elder Scrolls V: Skyrim](https://elderscrolls.bethesda.net/es/skyrim/).

## ¿Qué es?

Es un servicio que facilita saber qué ingredientes de tu inventario puedes mezclar para obtener de manera satisfactoria
uno o más efectos.

## ¿Cómo lo pruebo?

### Versión hospedada en Azure

Si deseas probar la aplicación en tiempo real, puedes ir
a [https://mdanieltg-alchemy.azurewebsites.net](https://mdanieltg-alchemy.azurewebsites.net/).

### Probar localmente

Si lo que quieres es probarlo en tu propio equipo, entonces sigue los siguientes pasos:

1. Descarga el [SDK de .NET 6.0](https://dotnet.microsoft.com/en-us/download).
2. Clona este repositorio.
3. En una consola o terminal accede al directorio raíz del repositorio y luego ejecuta los comandos:
    1. `dotnet build`.
    2. `dotnet run`.
4. ¡Disfruta de la aplicación!

Si cuentas con [Visual Studio](https://visualstudio.microsoft.com/es/) o [Rider](https://www.jetbrains.com/rider/)
instalados, puedes abrir el archivo `Alchemy.sln` y utilizar el IDE para compilar y ejecutar la aplicación.

## ¿Cómo funciona?

### Preparación de la información

De manera manual, extraje la información de
la [Wiki de Elder Scrolls](https://elderscrolls.fandom.com/wiki/The_Elder_Scrolls_Wiki) de Fandom a archivos CSV
almacenados en este mismo repositorio.

### Inicialización del servicio

Al iniciar, el servicio transfiere el contenido de los archivos CSV por medio de un `HttpClient`, para después
serializarlos con el paquete [CsvHelper](https://www.nuget.org/packages/CsvHelper/) en tres colecciones de
objetos: `HashSet<Dlc>`, `HashSet<Effect>`, `HashSet<Ingredient>` y `IAsyncEnumerable<IngredientEffects>`.

Posteriormente, en la clase `AlchemyContextFactory` se agrega la información de los DLC a los ingredientes, y después,
utilizando la información de `IAsyncEnumerable<IngredientEffects>`, se agregan las relaciones entre efectos e
ingredientes, mediante las propiedades `ICollection<Effect> Effects` en la clase `Ingredient`
y `ICollection<Ingredient> Ingredients` en la clase `Effect`.

### Consulta de la información

La consulta de los ingredientes y los efectos se realiza por medio de controladores y servicios que entregan
directamente la información, sin paginación ni limites.

### Haciendo mezclas

Para las mezclas primero se pide al usuario seleccionar los ingredientes que tiene en su inventario.

Después, los ID de estos ingredientes son enviados a la clase `Mixer` como `IEnumerable<int>`. Aquí, primero se obtiene
la lista de ingredientes correspondiente a los IDs recibidos. Luego, se extraen los efectos de esos mismos ingredientes
y se agregan a un diccionario `Dictionary<Effect, List<Ingredient>>`.

Finalmente, por medio de LINQ, se crea una colección sobre el diccionario, donde la cantidad de ingredientes por efecto
sea mayor a uno (1), y se ordene por cantidad de ingredientes por efecto, de mayor a menor. Esta colección se itera para
agregar los elementos resultantes, en forma de `Mix` a una lista `List<Mix>`, para finalmente devolverla como resultado.

Esta lista `List<Mix>` es utilizada como modelo por la vista de resultados, donde se muestra de manera amigable para
facilitar la lectura al usuario.

## Stack tecnológico

La aplicación completa está construida
con [ASP.NET Core MVC versión 6](https://docs.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-6.0).

Para el frontend utilicé la librería [Bootstrap 5](https://getbootstrap.com/).

La información utilizada está almacenada como CSV en este mismo repositorio de GitHub.
