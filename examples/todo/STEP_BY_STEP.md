# Guía Paso a Paso: Construcción de la ToDo API (Contract-First, .NET 9)

Esta guía te acompaña desde la estructura vacía actual hasta una API funcional alineada con `openapi/todo-api.v1.yaml`. Está pensada para que entiendas el POR QUÉ de cada paso, no solo el QUÉ. Evita copiar/pegar sin reflexionar.

> Principio central: el contrato (OpenAPI) gobierna; el código lo implementa.

---

## Tabla de Fases

| Fase | Objetivo | Resultado "Definition of Done" |
|------|----------|---------------------------------|
| 0 | Verificar entorno | `dotnet --version` >= 9, spec válida |
| 1 | Solución y capas | Proyectos creados y referenciados |
| 2 | Modelado dominio | Entidades + enums + reglas básicas documentadas |
| 3 | Interfaces repos | Interfaces en Application, sin implementación real |
| 4 | Infra in-memory | Repos in-memory funcionando |
| 5 | Casos de uso | Servicios Create/List/Get listos |
| 6 | Endpoints mínimos | Rutas POST/GET project/task responden 201/200 |
| 7 | Validación | Reglas contractuales y de negocio separadas |
| 8 | Tests integración | Flujo crear+listar cubierto, build verde |
| 9 | Persistencia EF Core | SQLite local + migraciones iniciales |
| 10 | Autenticación JWT | Endpoints protegidos salvo health/swagger |
| 11 | Errores & ProblemDetails | Respuestas 4xx/404 unificadas |
| 12 | Observabilidad | Logging estructurado + métricas básicas |
| 13 | Auditoría | Registro cambios de estado/task title |
| 14 | Filtros avanzados | Paginación estable y ordenación múltiple |
| 15 | Hardening | Rate limiting, headers seguridad, timeouts |
| 16 | Refactor & limpieza | Código consistente, sin duplicaciones |

---

## Fase 0: Verificar Entorno

**Objetivo:** Asegurar base técnica antes de escribir código.

1. Comprueba .NET: `dotnet --version`.
2. Valida tu OpenAPI (en raíz): usar Swagger Editor o herramienta online.
3. Crea commit inicial limpio (si no existe): solo estructura.

Checklist Done:

- ✅ OpenAPI sin errores sintácticos
- ✅ Git limpio

---

## Fase 1: Solución y Capas

**Objetivo:** Arquitectura mínima limpia (sin lógica). Proyectos: Domain, Application, Infrastructure, Api, Tests.

Estructura esperada (ya tienes carpetas vacías; añade proyectos):

```text
ToDo.sln
ToDo.Domain/
ToDo.Application/
ToDo.Infrastructure/
ToDo.Api/
ToDo.Api.Tests/
openapi/todo-api.v1.yaml
```

Referencias (solo direcciones permitidas):

- Api → Application, Infrastructure
- Infrastructure → Domain, Application
- Application → Domain
- Domain → (ninguna)
- Tests → Api

Definition of Done:

- Todos los .csproj apuntan a net9.0
- Referencias agregadas sin ciclos
- Build base OK

---

## Fase 2: Modelado de Dominio

**Objetivo:** Entender reglas antes de persistencia.

Acciones:

1. Define enums `TaskStatus` (OPEN, IN_PROGRESS, BLOCKED, DONE) y `TaskPriority` (LOW, MEDIUM, HIGH, URGENT) en Domain.
2. Clase `Project`: Id, Name, Description?, OwnerId, CreatedAt.
3. Clase `Task`: Id, ProjectId, Title, Description?, Status, Priority, DueDate?, AssigneeId?, CreatedAt, UpdatedAt, CompletedAt?.
4. Encapsula invariantes (ej.: método para cambiar estado que valide transición y setee CompletedAt si pasa a DONE).
5. Documenta (en comentarios) reglas que AÚN no implementas para no olvidarlas.

Definition of Done:

- Entidades sin referencias a EF ni a JSON
- Métodos de comportamiento básicos (p.ej. `SetStatus`) en lugar de mutaciones directas

---

## Fase 3: Interfaces de Repositorio (Application)

**Objetivo:** Abstracción clara de almacenamiento.

Interfaces mínimas:

- `IProjectRepository`: AddAsync, GetByIdAsync, ListPagedAsync
- `ITaskRepository`: AddAsync, GetByIdAsync, ListByProjectAsync (con objeto filtro), UpdateAsync
- Filtro `TaskQuery` (Status[], Priority[], DueBefore, DueAfter, TagIds[], Search, Page, PageSize, SortCriteria[])

Evita premature optimization: filtros que no uses aún pueden estar en backlog.

Definition of Done:

- No hay implementación concreta
- Interfaces sólo devuelven tipos Domain o colecciones

---

## Fase 4: Infraestructura In-Memory

**Objetivo:** Iterar sin base de datos.

Implementaciones:

- List internas (ConcurrentDictionary opcional) para Projects y Tasks.
- Paginación manual (Skip/Take).
- Filtro simple (aplica sólo criterios no nulos).

Nota: In-memory NO garantiza consistencia transaccional; aceptable para aprendizaje.

Definition of Done:

- Crear project + listar funciona con datos volátiles
- Crear task referenciada a un project existente valida la existencia

---

## Fase 5: Casos de Uso (Application Services)

**Objetivo:** Orquestar lógica sin tocar HTTP directamente.

Servicios ejemplares:

- CreateProjectService (input: Name, Description, OwnerId) → Project
- ListProjectsService (input: page, size) → PageResult<Project>
- CreateTaskService (valida reglas + crea Task)
- ListTasksService (aplica filtros y paginación)
- GetTaskService
- UpdateTaskPartialService (aplicar cambios parciales y actualizar UpdatedAt)
- CompleteTaskService (delegar a SetStatus DONE)

Principios:

- Input/Output DTO internos (Commands/Queries) separados de modelos HTTP externos.
- Validaciones de negocio viven aquí (o en Domain si son invariantes).

Definition of Done:

- Cada servicio probado mentalmente sin framework
- No referencias a `HttpContext`, `IResult`, etc.

---

## Fase 6: Endpoints Mínimos (Minimal API)

**Objetivo:** Exponer las primeras rutas contractuales.

Implementa primero sólo:

- POST /v1/projects
- GET /v1/projects
- POST /v1/projects/{projectId}/tasks
- GET /v1/projects/{projectId}/tasks

Mapeo: Request → Command → Servicio → Domain → Response DTO.

Deja endpoints restantes (update, complete, audit, tags) para después.

Definition of Done:

- 201 al crear recursos, 200 al listar
- 404 cuando project no existe
- 400 en datos inválidos

---

## Fase 7: Validación

**Objetivo:** Separar “datos mal formados” de “reglas de negocio”.

Capas:

- Validación de contrato: título vacío, longitud, enum fuera de rango → 400 antes de servicio.
- Validación de negocio: transiciones ilegales, project inexistente → excepciones traducidas a 404 / 400.

Puedes introducir FluentValidation o implementar validadores simples internamente.

Definition of Done:

- No hay validaciones duplicadas en varias capas
- Mensajes de error consistentes

---

## Fase 8: Tests de Integración Básicos

**Objetivo:** Asegurar regresiones mínimas.

Casos recomendados:

1. Crear Project → 201 → luego List Projects contiene el item.
2. Crear Task → 201 → List Tasks filtrando por project la incluye.
3. Task con título >120 → 400.
4. Crear Task en project inexistente → 404.

Definition of Done:

- Tests pasan localmente
- Se pueden ejecutar repetidamente (in-memory reseteado por test fixture)

---

## Fase 9: Persistencia con EF Core (SQLite)

**Objetivo:** Sustituir gradualmente in-memory.

Pasos conceptuales:

1. Añadir paquetes EF Core.
2. DbContext (DbSet<Project>, DbSet<Task>, Tag si luego lo implementas).
3. Configuración (Value Conversions para enums si necesario).
4. Migración inicial.
5. Implementaciones repos EF (reutilizan queries).
6. Arranque configurable: usar parameter/flag para elegir InMemory vs EF.

Definition of Done:

- Migración aplica y endpoints siguen funcionando
- Tests pueden usar SQLite en memoria (connection keep-alive)

---

## Fase 10: Autenticación JWT

**Objetivo:** Proteger endpoints.

Pasos conceptuales:

1. Agregar autenticación Bearer.
2. Middleware para rechazar sin token.
3. Determinar OwnerId desde claims (sub).
4. Proteger todas las rutas salvo /health, /swagger.

Definition of Done:

- 401 cuando falta token
- OwnerId se propaga correctamente a CreateProject

---

## Fase 11: Manejo de Errores Uniforme (ProblemDetails)

**Objetivo:** Respuestas consistentes.

1. Middleware global try/catch.
2. Mapear excepciones de dominio a códigos (NotFoundException → 404, BusinessRuleException → 400).
3. Estructura JSON alineada al schema `Error` del OpenAPI.

Definition of Done:

- No hay stack traces en output
- Todos los errores pasan por un solo punto

---

## Fase 12: Observabilidad Inicial

**Objetivo:** Visibilidad.

1. Logging estructurado (Serilog) con `RequestId/TraceId`.
2. Métrica contador: tasks_created_total.
3. (Opcional) OpenTelemetry exporter consola.

Definition of Done:

- Logs en JSON
- Métrica crece tras crear tasks

---

## Fase 13: Auditoría

**Objetivo:** Registrar cambios clave.

Estrategia simple:

- Decorador sobre repositorio Task que detecta cambios en Update y escribe AuditLog (en memoria o tabla).
- Registro de: antiguo status, nuevo status, usuario, timestamp.

Definition of Done:

- GET /v1/tasks/{id}/audit devuelve al menos un evento tras un cambio

---

## Fase 14: Filtros Avanzados y Paginación Robusta

**Objetivo:** Mejora de consultas.

1. Soporte múltiple de status y prioridad.
2. Orden por varios campos: parsear sort param.
3. Paginación estable (orden determinista por Id secundario si necesario).
4. Índices DB (cuando pases a EF) para columnas consultadas.

Definition of Done:

- Respuestas consistentes bajo distintos criterios
- No se repiten ni omiten elementos entre páginas consecutivas

---

## Fase 15: Hardening

**Objetivo:** Resiliencia y seguridad básica.

- Rate limiting (por IP o usuario).
- Secure headers (Strict-Transport-Security, X-Content-Type-Options, etc.).
- Timeout global / cancelación cooperativa.
- Input size limits.

Definition of Done:

- Peticiones excesivas reciben 429
- Headers de seguridad presentes

---

## Fase 16: Refactor & Limpieza

**Objetivo:** Reducir deuda técnica.

Checklist:

- Eliminar duplicación en mapping.
- Revisar nombres consistentes.
- Añadir comentarios TODO con fecha y dueño.
- Asegurar que Domain no gotea dependencias externas.

Definition of Done:

- Code review mental sin sorpresas

---

## Integración Continua (Cuando avances)

- Paso 1: `dotnet build`
- Paso 2: tests
- Paso 3: lint OpenAPI (Spectral)
- Paso 4: openapi-diff contra main (alerta breaking)
- Paso 5: publicar artefactos (spec + cobertura tests)

---

## Buenas Prácticas Clave

- Commits pequeños (1 feature = 1 commit lógico).
- Escribe el test de integración antes de añadir lógica compleja.
- No cambies la spec sin justificar el impacto en consumidores.
- Documenta decisiones (WHY) en README o comentarios encima de servicios.

---

## Checklist General Rápido (Auto‑evaluación)

- [ ] OpenAPI actualizado vs implementación
- [ ] Entidades sin dependencias de infraestructura
- [ ] Servicios sin lógica de transporte (HTTP)
- [ ] Errores unificados
- [ ] Tests cubren caminos felices + 1 fallo clave
- [ ] Seguridad y observabilidad mínimas presentes

---

## Apéndice A: Errores Frecuentes

| Problema | Prevención |
|----------|-----------|
| Lógica de negocio en endpoints | Extraer a servicios Application |
| Repos devolviendo DTOs | Repos solo Domain entities |
| Paginación inconsistente | Siempre orden + page + pageSize explícitos |
| Validaciones duplicadas | Centralizar en validadores + invariantes Domain |
| Cambios silenciosos en spec | PR diffs + openapi-diff obligatorio |

---

## Próximo Paso Inmediato

Inicia Fase 2: modela enums y entidades en Domain, escribe reglas de transición en comentarios y confirma dudas antes de codificar casos de uso.

> Cuando termines Fase 2, vuelve y revisamos las invariantes antes de continuar.

---

Fin de la guía. Ajusta, itera y agrega notas propias conforme avances.
