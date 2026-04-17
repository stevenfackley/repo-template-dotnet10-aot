# Data flow

```mermaid
flowchart LR
    client -->|HTTP| service[{{PROJECT_NAME}}]
    service -->|SQL| db[(Postgres)]
```

<!-- Replace diagram with the real flow once the stack is concrete. -->
