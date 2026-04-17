# Ops patterns

## Image promotion
`sha-<short>` → `prod-<sha>` via `deploy-prod.yml` on a `YYYYMMDD_<project>_Release` tag. Prod images are immutable.

## Rollback
Re-run `deploy-prod.yml` with `workflow_dispatch` pointing at the previous release tag. See `runbooks/rollback.md`.

## Local-registry sync (K3s only)
See `gh-actions/.github/workflows/deploy-k3s-helm.yml` — GHCR pull → tag → push to `local-registry:30500` → Helm upgrade.
