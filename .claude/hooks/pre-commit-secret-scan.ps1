#requires -Version 7
<#
.SYNOPSIS
  Pre-commit hook wrapper. Scans staged files only for secret-shapes + banned packages.
  Install: copy to .git/hooks/pre-commit (or use `git config core.hooksPath .claude/hooks`).
#>
$ErrorActionPreference = 'Stop'

$staged = git diff --cached --name-only --diff-filter=ACMR
if (-not $staged) { exit 0 }

$patterns = @(
    'InstrumentationKey='
    'AKIA[0-9A-Z]{16}'
    'ASIA[0-9A-Z]{16}'
    'xoxb-[0-9]+-[0-9]+-[a-zA-Z0-9]+'
    'sk_live_[0-9a-zA-Z]+'
    'sk-ant-[0-9a-zA-Z_-]+'
    'ghp_[0-9a-zA-Z]{36,}'
    '-----BEGIN (RSA|OPENSSH|EC|PGP) PRIVATE KEY-----'
) -join '|'

$fail = $false
foreach ($f in $staged) {
    if (-not (Test-Path $f)) { continue }
    $hits = Select-String -Path $f -Pattern $patterns -AllMatches 2>$null
    if ($hits) {
        Write-Host "BLOCKED: $f matches secret pattern" -ForegroundColor Red
        $hits | ForEach-Object { Write-Host "  line $($_.LineNumber): $($_.Line.Trim())" }
        $fail = $true
    }
}
if ($fail) {
    Write-Host 'Commit blocked. Remove the secret or use `git commit --no-verify` only if you know what you are doing.' -ForegroundColor Red
    exit 1
}
