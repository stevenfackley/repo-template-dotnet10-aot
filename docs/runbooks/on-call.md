# On-call

Solo maintainer project — the "rotation" is {{AUTHOR}}. If you're reading this as a second person, welcome; your first action is to document who does what.

## Alert sources
- GitHub Actions failures (CI / deploy workflows)
- Manual user reports → GitHub issues

## First 5 minutes
1. Check `gh run list --limit 10` — is the latest deploy green?
2. Curl the health endpoint — is the service up?
3. `docker logs <container> --tail 50` — any errors?
4. If yes to any failure: open an `Ops incident` and start `incident-response.md`.
