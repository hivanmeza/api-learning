# api-learning: Ruta de Aprendizaje para Construcción de APIs

Este repositorio (antes llamado `apis_learning`) guía un proceso incremental para aprender a diseñar y construir APIs profesionales antes de decidir un stack específico.

## Etapas

1. Fundamentos teóricos de APIs (`docs/01_fundamentos_apis.md`)
2. Criterios de elección de stack (`docs/02_eleccion_stack.md`)
3. Definición de requisitos y matriz de decisión (pendiente)
4. Selección de lenguaje y framework
5. Diseño de contrato (OpenAPI / esquema)
6. Implementación mínima (healthcheck, versionado, logging)
7. Autenticación y validación
8. Persistencia y patrones de datos
9. Observabilidad (logs, métricas, tracing)
10. Hardening y performance

## Cómo usar este repo ahora

1. Lee primero `docs/01_fundamentos_apis.md`.
2. Completa tus requisitos de negocio (usa `docs/03_template_requisitos.md`).
3. Evalúa con `docs/02_eleccion_stack.md`.

## Pregunta para ti

Define (puedes responder en un issue o en un nuevo doc):

- 3 objetivos de negocio del API
- Tipos de clientes (web, mobile, partners, internos)
- Volumen estimado (RPS objetivo inicial)
- Latencia objetivo (p95)
- Requisitos de seguridad (auth requerida, datos sensibles?)

Con esa información pasaremos a la matriz de decisión y elección de stack.

## Próximo paso

Respóndeme con tus requisitos y continuamos.
