---
description: Full pre-push gate — format, test, vuln audit, secret scan.
---

Run the following in order. Stop on first failure, report the failure, do NOT attempt a push:

1. `dotnet format --verify-no-changes`
2. `dotnet test --nologo --verbosity minimal`
3. `dotnet list package --vulnerable`
4. `pwsh scripts/scan-secrets.ps1`
5. Print short SHA: `git rev-parse --short HEAD`

If all pass, print a green summary with the short SHA and `git status --short`.
