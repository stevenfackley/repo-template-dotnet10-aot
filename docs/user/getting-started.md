# Getting started with {{PROJECT_NAME}}

## Prerequisites
- Docker Desktop, or .NET 10 SDK.

## Run locally (Docker)

```bash
docker compose up --build
curl http://localhost:8080/health
```

## Run locally (SDK)

```bash
dotnet run --project src/{{PROJECT_NAME}}
```

## Configuration

Copy `.env.example` → `.env`, adjust values. See `docs/architecture/SDD.md` for what each var does.
