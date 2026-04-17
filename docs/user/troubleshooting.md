# Troubleshooting

## `dotnet publish` fails with AOT errors
Usually a library that uses reflection. Check the warning, add `[DynamicallyAccessedMembers]` or remove the lib.

## Container exits immediately
Check `docker logs <container>`. Likely a missing env var. Compare against `.env.example`.

## CI fails on `dependency-audit`
Either a known-vuln package (bump it) or a banned-telemetry match (remove it).
