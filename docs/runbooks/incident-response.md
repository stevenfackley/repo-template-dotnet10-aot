# Incident response

## 1. Declare
Open an issue using the `Ops incident` template. Severity per template.

## 2. Stabilize
- SEV-1: roll back first, debug second. See `rollback.md`.
- SEV-2/3: gather logs, form hypothesis.

## 3. Investigate
- `docker logs <container> --tail 200`
- `gh run list --status failure --limit 5` — recent CI / deploy history
- `kubectl describe pod` (K3s) or EC2 `journalctl -u docker`

## 4. Resolve
- Patch, test, deploy, verify.

## 5. Post-mortem
- Within 48h of resolution. Blameless. Action items → tracked issues.
