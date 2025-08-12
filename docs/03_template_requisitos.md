# Template de Requisitos del API

Completa y guarda este archivo como `requisitos.md` en la raíz o dentro de `docs/`.

## 1. Objetivos de negocio

- (Ej.) Permitir a clientes externos consultar su historial de pedidos
- ...

## 2. Actores / Clientes

| Actor | Descripción | Necesidades principales |
|-------|-------------|-------------------------|
| Frontend Web | SPA React | CRUD de recursos, auth |
| ... | ... | ... |

## 3. Recursos iniciales

| Recurso | Descripción | Operaciones CRUD necesarias | Sensible (S/N) |
|---------|-------------|------------------------------|----------------|
| users | Usuarios finales | C,R,U,D | S |
| ... | ... | ... | ... |

## 4. Requisitos funcionales clave

1.
2.
3.

## 5. Requisitos no funcionales

| Categoría | Métrica / Objetivo |
|-----------|--------------------|
| Latencia p95 | <= 300 ms |
| Error rate | < 1% |
| Uptime | 99.5% |
| ... | ... |

## 6. Seguridad

- Tipo de autenticación (OAuth2, JWT, API Key, etc.)
- Datos sensibles (PII, PCI, salud) y necesidades de cifrado

## 7. Observabilidad

- Logs estructurados (JSON) con trace_id
- Métricas: requests_total, request_duration_seconds (histogram)
- Trazas distribuidas: (Sí/No)

## 8. Escalabilidad y capacidad

- RPS inicial / pico
- Estrategia de escalado (horizontal, serverless)

## 9. Integraciones externas

| Sistema | Tipo | Propósito |
|---------|------|-----------|
| Stripe | Pago | Cobros |
| ... | ... | ... |

## 10. Versionado y deprecación

- Estrategia propuesta (URL / header / media type)
- Política de soporte de versiones

## 11. Riesgos y supuestos

- Riesgo: ... Mitigación: ...
- Supuesto: ...

## 12. Roadmap inicial (alto nivel)

| Fase | Entregable | ETA |
|------|------------|-----|
| Fase 1 | MVP auth + CRUD base | 2 semanas |
| Fase 2 | Observabilidad + Hardening | 1 semana |
| ... | ... | ... |

## 13. Criterios de éxito

- Métricas clave que indicarán que el MVP es aceptable.

---

Completa esto y podremos construir la matriz de decisión del stack.
