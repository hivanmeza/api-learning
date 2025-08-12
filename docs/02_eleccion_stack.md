# Criterios para elegir el stack del API

## 1. Dimensiones de evaluación

- Productividad (rapidez para iterar)
- Performance / Latencia
- Concurrencia / Escalabilidad
- Ecosistema (librerías, comunidad, soporte)
- Facilidad de contratación de talento
- Curva de aprendizaje
- Observabilidad y herramientas
- Seguridad y madurez
- Coste operativo (infraestructura y mantenimiento)

## 2. Perfiles típicos de stacks

| Lenguaje | Fortalezas | Consideraciones |
|----------|------------|-----------------|
| Python (FastAPI, Django REST) | Rapidez de desarrollo, tipado opcional, ecosistema ML | Menor rendimiento bruto comparado con Go/Java |
| Java (Spring Boot, Micronaut) | Madurez, robustez, herramientas enterprise | Verbosidad, arranque más pesado (Spring) |
| JavaScript/TypeScript (Express, NestJS, Hono) | Full-stack mismo lenguaje, enorme comunidad | Single-threaded (aunque event-loop eficiente) |
| Go (net/http, Gin, Echo, Fiber) | Rendimiento, binarios estáticos, simplicidad | Genéricos recientes, menos metaprogramación |
| C# (.NET) | Performance, tooling excelente, multiplataforma | Ecosistema algo más corporativo |
| Rust (Axum, Actix, Rocket) | Seguridad en memoria, alto rendimiento | Curva de aprendizaje pronunciada |
| Kotlin (Ktor, Spring) | Sintaxis moderna + JVM | Arranque JVM, menor base que Java |
| PHP (Laravel, Symfony) | Rapidez para CRUD, hosting accesible | Menor rendimiento en escenarios intensivos |
| Ruby (Rails + grape) | Convención sobre configuración, rapidez prototipos | Performance menor, consumo RAM |

## 3. Factores no funcionales claves

- SLA / SLO de latencia (p95/p99)
- Throughput esperado (RPS)
- Requisitos de seguridad / compliance (PCI, HIPAA, GDPR)
- Tolerancia a fallos y estrategia de resiliencia
- Expectativa de crecimiento (scaling plan)

## 4. Estrategia de decisión

1. Ponderar criterios (ej: 0-5) según prioridad de negocio
2. Crear matriz comparativa de 2-3 stacks finalistas
3. Desarrollar un spike / prototipo comparable (endpoint, auth, DB simple)
4. Medir: latencia, throughput, DevX (tiempo de implementación), complejidad
5. Escoger y documentar trade-offs

## 5. Próximo paso

Definirás tus requisitos y construiremos esa matriz. Luego elegiremos el lenguaje para continuar la parte práctica.
