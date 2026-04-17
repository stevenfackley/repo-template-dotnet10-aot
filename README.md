# {{PROJECT_NAME}}

{{FLAVOR}} — .NET 10 Native AOT web service.

## Quick start

```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/{{PROJECT_NAME}}
```

Full dev-env setup: see `C:\Users\steve\projects\DEV_SETUP_GUIDE.md`.

## Docker (local)

```bash
docker compose up --build
curl http://localhost:8080/health
```

## Deploy

Target: `{{DEPLOY_TARGET}}`.

Push to `main` → CI builds → `.github/workflows/deploy-test.yml` auto-runs on success.
Tag `YYYYMMDD_{{PROJECT_NAME}}_Release` → `.github/workflows/deploy-prod.yml` runs.

## Layout

```
src/{{PROJECT_NAME}}/          minimal API entry
tests/{{PROJECT_NAME}}.Tests/  xUnit + NetArchTest
docs/                          product, architecture, user, runbooks
scripts/scan-secrets.ps1       invoked by CI secret-scan
```

## Conventions

- Image tags: `sha-{SHORT_SHA}` (test), `prod-{SHA}` (prod).
- Release tags: `YYYYMMDD_{{PROJECT_NAME}}_Release`.
- Commits: Conventional Commits.
- No telemetry SDKs — enforced by `secret-scan.yml`.

## License

{{YEAR}} {{AUTHOR}}. See `LICENSE`.
