# {{PROJECT_NAME}}

## Stack
{{FLAVOR}}. .NET 10 Native AOT. See `docs/architecture/SDD.md`.

## Standard flow
analyze -> read docs/ -> read Directory.Build.props -> plan -> work.

## Conventions
- Image tags: `sha-{SHORT_SHA}` (test); `sha-{SHORT_SHA}` + `latest` (prod, deployed by immutable digest).
- Release tags: `YYYYMMDD_{{PROJECT_NAME}}_Release`.
- Secrets: never in `src/`. See `.env.example`.
- Tests: progressive pyramid. E2E smoke on PR, integration on main.
- Commits: Conventional Commits (feat/fix/chore/docs/refactor/test/ci).
- Branches: `main` protected. Squash-merge via PR.

## Deploy target
{{DEPLOY_TARGET}}

## Commands
- `/ship` — full pre-push sequence (format + test + build + secret-scan)
- `/deploy-test` — trigger test deploy
- `/add-adr` — new DECISIONS.md entry
- `/bump-image` — check a `sha-<short>` image exists and how to roll to it
- `/smoke` — run E2E smoke locally

## Do not
- Add static AWS keys. Use OIDC.
- Skip `scripts/scan-secrets.ps1` (run by `/ship`) before pushing.
- Bump major deps without an ADR.
- Disable `TreatWarningsAsErrors`.
- Add telemetry SDKs (ApplicationInsights/Sentry/Datadog/etc). Banned by CI.
