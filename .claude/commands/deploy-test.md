---
description: Manually trigger the test-environment deploy workflow.
---

Run: `gh workflow run deploy-test.yml`

Then poll the latest run: `gh run watch $(gh run list --workflow=deploy-test.yml --limit=1 --json databaseId --jq '.[0].databaseId')`

Report: which image tag deployed, the short SHA, and whether the health check passed.
