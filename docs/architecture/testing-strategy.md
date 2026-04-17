# Testing strategy

Progressive pyramid. Each layer is cheaper + faster than the one above.

## Unit
- xUnit in `tests/{{PROJECT_NAME}}.Tests`. Pure logic, no IO.
- Run on every commit.

## Architecture
- `ArchitectureTests.cs` with NetArchTest + reflection.
- Enforces: no banned telemetry refs, no leaking types into root namespace.

## Integration
- `WebApplicationFactory<Program>` in-process. Hits routes with real DI container.
- Run on PR to `main`.

## E2E smoke
- Curl-based against a deployed container. Health + one happy path.
- Run post-deploy in `deploy-test.yml`.

## E2E integration
- Full scenario coverage. Slow. Nightly schedule.

## Coverage
- Target: 70% line, 60% branch on `src/`. Enforced softly — trending direction matters more than hitting the number exactly.
