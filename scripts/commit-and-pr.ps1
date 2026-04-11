Param(
    [string]$BranchName = "feature/short-description",
    [string]$CommitMessage = "",
    [string]$BaseBranch = "main",
    [string]$PrTitle = "",
    [string]$PrBody = ""
)

Set-StrictMode -Version Latest

function Exec($args) {
    Write-Host "> $args"
    & cmd /c $args
    $code = $LASTEXITCODE
    if ($code -ne 0) { throw "Command failed with exit code $code: $args" }
}

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Error "git is not installed or not in PATH. Aborting."
    exit 1
}

$ghAvailable = $true
if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    Write-Warning "GitHub CLI (gh) not found. PR creation will be skipped. Install gh to enable automatic PR creation."
    $ghAvailable = $false
}

if (-not $CommitMessage -or $CommitMessage.Trim() -eq "") {
    $CommitMessage = Read-Host "Enter one-line commit message"
}

if (-not $BranchName -or $BranchName.Trim() -eq "") { $BranchName = "feature/short-description" }

try {
    # Create or switch to branch
    Write-Host "Switching to branch $BranchName"
    & git checkout -b $BranchName 2>$null
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Branch exists, checking out $BranchName"
        Exec "git checkout $BranchName"
    }

    # Stage hunks interactively
    Write-Host "Staging changes interactively (git add -p). Review hunks and stage desired ones."
    Exec "git add -p"

    # Commit
    Write-Host "Committing: $CommitMessage"
    & git commit -m "$CommitMessage"
    if ($LASTEXITCODE -ne 0) {
        Write-Warning "Nothing to commit or commit failed. Exiting."
        exit 0
    }

    # Push
    Write-Host "Pushing branch $BranchName to origin"
    Exec "git push -u origin $BranchName"

    # Create PR if gh available
    if ($ghAvailable) {
        if (-not $PrTitle -or $PrTitle.Trim() -eq "") { $PrTitle = $CommitMessage }
        Write-Host "Creating PR: title=[$PrTitle] base=[$BaseBranch]"
        $bodyArg = ""
        if ($PrBody -and $PrBody.Trim() -ne "") { $bodyArg = "--body \"$PrBody\"" }
        Exec "gh pr create --base $BaseBranch --title \"$PrTitle\" $bodyArg"
    } else {
        Write-Host "PR not created. Run: gh pr create --base $BaseBranch --title \"$CommitMessage\" --body \"(details)\""
    }

    Write-Host "Done."
} catch {
    Write-Error $_.Exception.Message
    exit 1
}