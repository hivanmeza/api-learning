# Fundamentos de APIs

## 1. ¿Qué es una API?

Una API (Application Programming Interface) es un contrato que permite que dos sistemas de software se comuniquen mediante reglas claras. En el contexto web, solemos hablar de Web APIs sobre HTTP.

## 2. Objetivos principales

- Exponer capacidades o datos de un sistema de forma segura y consistente.
- Permitir integración entre servicios internos y externos.
- Desacoplar frontends y backends.

## 3. Estilos de APIs

| Estilo | Características | Cuándo usar |
|--------|-----------------|-------------|
| REST (Representational State Transfer) | Basado en recursos, usa HTTP estándar, stateless | CRUD clásico, interoperabilidad amplia |
| GraphQL | Consulta declarativa, un endpoint, tipos fuertemente tipados | Optimizar under/over fetching, clientes móviles |
| gRPC | Binario (Protobuf), streaming, alto rendimiento | Comunicaciones entre microservicios de baja latencia |
| Webhooks | Llamadas salientes ante eventos | Notificaciones/event-driven |
| WebSockets | Conexión bidireccional persistente | Tiempo real (chat, dashboards) |
| RPC/JSON-RPC | Llamadas a procedimientos, más directo | Simplicidad en internal APIs |

## 4. Componentes clave de una Web API

1. Transporte (HTTP/2, HTTP/1.1, QUIC)
2. Modelo de recursos / contratos (JSON, Protobuf, Avro)
3. Autenticación y Autorización (API Keys, OAuth2, JWT, mTLS)
4. Versionado
5. Manejo de errores y códigos de estado
6. Observabilidad (logs, métricas, trazas)
7. Seguridad (rate limiting, CORS, validación de entrada)
8. Performance (caché, compresión, paginación, ETags)

## 5. Ciclo de vida de diseño

1. Descubrir objetivos de negocio
2. Identificar actores y casos de uso
3. Modelar recursos / operaciones
4. Definir contratos (esquemas) y errores
5. Seleccionar estilo (REST, GraphQL, etc.)
6. Diseñar política de versionado
7. Estrategia de seguridad
8. Observabilidad y SLIs/SLOs
9. Pruebas (unitarias, integración, contract testing)
10. Documentación (OpenAPI, README, ejemplos)

## 6. Buenas prácticas REST resumidas

- Usar sustantivos plurales para recursos: /users, /orders
- Verbos implícitos vía método HTTP: GET, POST, PUT, PATCH, DELETE
- Filtrado, paginación y ordenación via query params: ?page=1&page_size=20&sort=-created_at
- Códigos de estado adecuados (200, 201, 204, 400, 401, 403, 404, 409, 422, 500)
- Idempotencia en PUT y DELETE; POST no idempotente; PATCH para cambios parciales
- HATEOAS (opcional) para descubribilidad

## 7. Versionado

Estrategías:

- En la URL: /v1/users
- En encabezado: Accept: application/vnd.miapi.v1+json
- En media type personalizado

Mantener versiones antiguas activas según política de deprecation.

## 8. Errores y modelado de respuestas de error

Formato sugerido JSON:

{
  "error": {
    "code": "RESOURCE_NOT_FOUND",
    "message": "User not found",
    "details": [ ... ],
    "trace_id": "..."
  }
}

## 9. Seguridad básica

- Validar y sanear input (evitar injection)
- Usar HTTPS siempre
- Evitar exponer detalles internos en errores
- Rate limiting y protección contra fuerza bruta
- Principio de privilegio mínimo
- Rotación de secretos / API keys

## 10. Observabilidad

- Logging estructurado (JSON)
- Correlación (trace_id, span_id)
- Métricas (latencia p95, tasa de errores, throughput)
- Tracing distribuido (OpenTelemetry)

## 11. Performance & escalabilidad

- Paginación (limit/offset, cursor-based)
- Caching (HTTP cache headers, CDN)
- Compresión (gzip, brotli)
- Asincronía y colas para procesos largos
- Circuit breakers y timeouts

## 12. Contratos y documentación

- OpenAPI/Swagger para REST
- GraphQL SDL para GraphQL
- Protobuf .proto para gRPC

## 13. Pruebas

- Unitarias: lógica aislada
- Integración: endpoints reales (testcontainers)
- Contract testing (Pact)
- Seguridad (fuzzing, dependencia de SAST)
- Performance (k6, Locust)

## 14. Monorepo vs Polyrepo vs Multi-service

Depende de organización, ciclo de vida y herramientas CI/CD.

## 15. Próximo paso

En el siguiente módulo evaluaremos criterios para elegir el lenguaje y framework adecuados (ecosistema, performance, productividad, comunidad, requisitos no funcionales).
