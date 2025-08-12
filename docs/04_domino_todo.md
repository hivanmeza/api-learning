# Dominio: Gestor de Tareas (Tasks / ToDo Enriquecido)

> [Inicio](../README.md) · [Fundamentos](01_fundamentos_apis.md) · [Criterios](02_eleccion_stack.md) · **Dominio Tasks**

## 1. Objetivo

Un API para gestionar proyectos, tareas y etiquetas, con soporte para filtrado avanzado, prioridades y auditoría básica.

## 2. Entidades principales

| Entidad | Campos clave | Notas |
|---------|--------------|-------|
| User | Id, Email, DisplayName, CreatedAt | Autenticación básica JWT (futuro) |
| Project | Id, Name, Description, CreatedAt, OwnerId | Owner -> User |
| Task | Id, ProjectId, Title, Description, Status, Priority, DueDate, Tags[], AssigneeId, CreatedAt, UpdatedAt, CompletedAt? | Estados: OPEN, IN_PROGRESS, BLOCKED, DONE |
| Tag | Id, Name, Color | Nombre único por usuario |
| TaskTag (join) | TaskId, TagId | Relación N:M |
| AuditLog | Id, EntityType, EntityId, Action, ActorUserId, At, DiffJson | Para cambios relevantes |

## 3. Casos de uso (alto nivel)

- Crear proyecto
- Listar proyectos del usuario
- Crear tarea en un proyecto
- Actualizar estado / prioridad / asignado
- Agregar / quitar etiquetas
- Filtrar tareas: por estado, prioridad, rango fechas, texto, etiquetas (AND/OR)
- Completar tarea
- Ver historial (audit log) de una tarea

## 4. Reglas / Invariantes

- Task debe pertenecer a un Project existente y el usuario debe tener acceso.
- No se puede marcar DONE sin CompletedAt.
- Status transiciones permitidas: OPEN -> IN_PROGRESS -> DONE; cualquier -> BLOCKED; BLOCKED -> IN_PROGRESS.
- Nombre de Tag único por Owner (case-insensitive).
- Borrado lógico opcional (no implementado inicio).

## 5. Modelo inicial (simplificado JSON)

```json
Task: {
  "id": "uuid",
  "projectId": "uuid",
  "title": "string <= 120",
  "description": "string?",
  "status": "OPEN|IN_PROGRESS|BLOCKED|DONE",
  "priority": "LOW|MEDIUM|HIGH|URGENT",
  "dueDate": "ISO8601?",
  "tags": ["uuid"],
  "assigneeId": "uuid?",
  "createdAt": "ISO8601",
  "updatedAt": "ISO8601",
  "completedAt": "ISO8601?"
}
```

## 6. Endpoints iniciales (v1)

| Método | Path | Descripción |
|--------|------|-------------|
| POST | /v1/projects | Crear proyecto |
| GET | /v1/projects | Listar proyectos (paginado) |
| POST | /v1/projects/{projectId}/tasks | Crear task |
| GET | /v1/projects/{projectId}/tasks | Listar + filtros |
| GET | /v1/tasks/{taskId} | Obtener detalle |
| PATCH | /v1/tasks/{taskId} | Actualizar campos parciales (estado, título, etc.) |
| POST | /v1/tasks/{taskId}/complete | Marcar completada |
| GET | /v1/tasks/{taskId}/audit | Historial cambios |
| POST | /v1/tags | Crear tag |
| GET | /v1/tags | Listar tags usuario |

## 7. Filtros (query params en listado tasks)

- status=OPEN,IN_PROGRESS (lista)
- priority=HIGH (single or list)
- dueBefore=2025-09-01
- dueAfter=2025-08-01
- tag=uuid (repetible)
- search=texto libre (título/desc)
- page=1&pageSize=20 (paginación)
- sort=-priority,dueDate

## 8. Errores estándar

```json
{
  "error": {
    "code": "TASK_NOT_FOUND",
    "message": "Task not found",
    "traceId": "..."
  }
}
```

## 9. Extensiones futuras

- Subtareas
- Comentarios
- Borrado lógico y restauración
- Compartir proyectos (colaboración)
- Webhooks (task.completed)

## 10. Próximo paso

Definir OpenAPI inicial (`openapi/todo-api.v1.yaml`) con esquemas Project, Task, Tag y endpoints CRUD básicos.

---
🔗 Navegación: [Inicio](../README.md) · [Fundamentos](01_fundamentos_apis.md) · [Criterios](02_eleccion_stack.md)
