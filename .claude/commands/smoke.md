---
description: Run local smoke test against the running container.
---

1. Ensure container is up: `docker compose ps | grep {{PROJECT_NAME}}`. If not, `docker compose up -d --build`.
2. Curl `/health`: expect 200.
3. Curl `/`: expect 200 with JSON `{"service":"{{PROJECT_NAME}}","status":"ok"}`.
4. Tail last 20 lines of container logs.
5. Report pass/fail per step.
