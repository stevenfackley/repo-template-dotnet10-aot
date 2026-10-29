# Ops patterns

## Image promotion
`deploy-prod.yml` builds on a `YYYYMMDD_<project>_Release` tag and pushes `sha-<short>` + `latest`. Prod deploys by the digest-pinned `image-ref`, so the running image is immutable.

## Rollback
Re-run `deploy-prod.yml` with `workflow_dispatch` pointing at the previous release tag. See `runbooks/rollback.md`.
