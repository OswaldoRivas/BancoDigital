BancoDigital - Backend API .NET 10



Descripción



BancoDigital es una API REST desarrollada con ASP.NET Core .NET 10 para gestionar operaciones bancarias básicas.



El proyecto permite registrar clientes, crear cuentas bancarias, consultar saldos, realizar depósitos y retiros, y consultar el historial de transacciones.



Se implementó una arquitectura por capas para separar las responsabilidades de presentación, lógica de negocio y persistencia de datos.



Tecnologías utilizadas



\- C# y .NET 10

\- ASP.NET Core Web API

\- Entity Framework Core

\- SQLite

\- xUnit para pruebas unitarias

\- Git y GitHub



Arquitectura del proyecto



La solución está organizada en cinco proyectos:



BancoDigital.API

\- Controladores y endpoints HTTP.

\- DTOs para solicitudes y respuestas.

\- Manejo global de excepciones.



BancoDigital.Application

\- Interfaces de servicios y repositorios.

\- Servicios que coordinan las operaciones bancarias.

\- Validaciones de aplicación.



BancoDigital.Domain

\- Entidades Customer, BankAccount y BankTransaction.

\- Reglas de negocio para depósitos y retiros.

\- Generación de números de cuenta.

\- Excepciones de negocio.



BancoDigital.Infrastructure

\- DbContext de Entity Framework Core.

\- Repositorios.

\- Persistencia en SQLite.

\- Migraciones de base de datos.



BancoDigital.Tests

\- Pruebas unitarias con xUnit.

\- Validación de depósitos.

\- Validación de retiros con fondos suficientes e insuficientes.

\- Validación del formato y generación de números de cuenta.



Funcionalidades



Clientes



Permite registrar un cliente con:



\- Nombre completo.

\- Fecha de nacimiento.

\- Sexo.

\- Ingreso mensual.



Cuentas bancarias



Permite crear una cuenta asociada a un cliente existente, indicando un saldo inicial.



El número de cuenta se genera con el formato:



`ACC-YYYYMMDD-XXXX`



Ejemplo:



`ACC-20261022-0104`



El sistema comprueba si el número generado ya existe y la base de datos dispone de un índice único para evitar duplicados.



Operaciones bancarias



\- Consultar saldo.

\- Realizar depósitos.

\- Realizar retiros.

\- Rechazar montos menores o iguales a cero.

\- Impedir retiros cuando el saldo es insuficiente.



Historial de transacciones



Cada movimiento registra:



\- Identificador de transacción.

\- Tipo de operación.

\- Monto.

\- Fecha y hora.

\- Saldo resultante después de la operación.



Endpoints de cuentas bancarias



| Método | Endpoint | Descripción |

|---|---|---|

| POST | `/api/accounts` | Crear cuenta bancaria |

| GET | `/api/accounts/{accountNumber}/balance` | Consultar saldo |

| POST | `/api/accounts/{accountNumber}/deposit` | Realizar depósito |

| POST | `/api/accounts/{accountNumber}/withdraw` | Realizar retiro |

| GET | `/api/accounts/{accountNumber}/transactions` | Consultar historial |



Requisitos para ejecutar el proyecto



\- SDK de .NET 10.

\- Visual Studio compatible con .NET 10 o Visual Studio Code.

\- Acceso a los paquetes NuGet utilizados por la solución.



Instrucciones de ejecución



1\. Clonar el repositorio:



```bash

git clone https://github.com/OswaldoRivas/BancoDigital.git

```



2\. Acceder a la carpeta del proyecto:



```bash

cd BancoDigital

```



3\. Restaurar dependencias:



```bash

dotnet restore

```



4\. Aplicar las migraciones de Entity Framework Core desde la carpeta principal:



```bash

dotnet ef database update --project BancoDigital.Infrastructure --startup-project BancoDigital.API

```



5\. Ejecutar la API:



```bash

dotnet run --project BancoDigital.API

```



La API utiliza SQLite como base de datos local. La cadena de conexión se encuentra en `appsettings.json`.



Ejemplo de creación de cuenta



Solicitud:



`POST /api/accounts`



```json

{

&#x20; "customerId": 1,

&#x20; "initialBalance": 2500

}

```



La respuesta incluye el identificador de la cuenta, su número único, el cliente asociado y el saldo inicial.



Ejemplo de depósito



Solicitud:



`POST /api/accounts/{accountNumber}/deposit`



```json

{

&#x20; "amount": 500

}

```



Ejemplo de retiro



Solicitud:



`POST /api/accounts/{accountNumber}/withdraw`



```json

{

&#x20; "amount": 300

}

```



Manejo global de excepciones



La API implementa un manejador global de excepciones mediante `IExceptionHandler`.



Entre las respuestas HTTP contempladas están:



\- 400 Bad Request:\*\* operaciones inválidas o errores de negocio.

\- 404 Not Found:\*\* cuenta bancaria inexistente.

\- 500 Internal Server Error:\*\* errores internos no previstos.



Los errores se devuelven mediante respuestas JSON sin exponer detalles internos del servidor.



Pruebas unitarias



Para ejecutar las pruebas desde la carpeta principal de la solución:



```bash

dotnet test

```



Las pruebas implementadas verifican:



\- Incremento del saldo después de un depósito.

\- Disminución del saldo después de un retiro válido.

\- Rechazo de retiros por fondos insuficientes.

\- Formato del número de cuenta.

\- Generación de números de cuenta diferentes.



Consideraciones técnicas



\- Los controladores utilizan interfaces de servicios y no acceden directamente al DbContext.

\- Se utiliza inyección de dependencias.

\- Entity Framework Core gestiona la persistencia.

\- La base de datos establece unicidad sobre el número de cuenta.

\- Los archivos temporales, resultados de compilación y bases de datos locales se excluyen mediante `.gitignore`.



Autor



Holvin Rivas



Proyecto desarrollado como prueba técnica para el puesto de Programador Backend.



Tecnología principal: ASP.NET Core .NET 10.

