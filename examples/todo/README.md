# ToDo API Example

Dominio de gestión de proyectos y tareas. Este ejemplo acompaña el contrato OpenAPI y se implementará de forma incremental para enseñar buenas prácticas en APIs con .NET.

## Objetivos de aprendizaje

- Minimal API + estructura limpia
- Contract-first (OpenAPI) → implementación
- Paginación, filtrado y ordenación
- Enums, validación y manejo de errores estandarizado (ProblemDetails)
- Autenticación JWT (básica en iteraciones siguientes)
- Evolución del contrato sin breaking changes

## Estructura prevista

```text
examples/todo/
  README.md
  openapi/
    todo-api.v1.yaml
  src/
    ToDo.Api/        # Proyecto principal (endpoints, config)
    ToDo.Application/ # Casos de uso / servicios
    ToDo.Domain/      # Entidades y lógica de negocio
    ToDo.Infrastructure/ # Persistencia (EF Core) y adaptadores
  tests/
    ToDo.Api.Tests/
```

## Roadmap iterativo

1. Bootstrapping Minimal API + health + swagger ui sirviendo spec
2. Persistencia en memoria / repos fake (Project, Task)
3. EF Core + migraciones SQLite/local
4. Validación (FluentValidation) + filtros de exception → ProblemDetails
5. Autenticación JWT y autorización básica
6. Filtros avanzados y paginación robusta
7. Audit log (decorador / interceptor)
8. Caching selectivo y ETag products (si aplica a Task list) – opcional
9. Observabilidad (Serilog + OpenTelemetry)
10. Hardening (rate limiting, headers seguridad)

## Contrato

Ver `openapi/todo-api.v1.yaml`. Fuente original también disponible en raíz para referencia histórica.

## Cómo ejecutar (cuando exista la solución)

```bash
dotnet build
cd src/ToDo.Api
dotnet run
```

Luego visitar [http://localhost:5000/swagger](http://localhost:5000/swagger) o donde se configure.

## Próximo paso

Crear solución .NET y proyecto `ToDo.Api` mínimo.
