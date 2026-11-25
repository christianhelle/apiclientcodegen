# Prints free disk space and Gradle cache sizes, to track CI headroom
Get-PSDrive -PSProvider FileSystem |
    Format-Table Name,
        @{ n = 'UsedGB'; e = { [math]::Round($_.Used / 1GB, 1) } },
        @{ n = 'FreeGB'; e = { [math]::Round($_.Free / 1GB, 1) } }

$gradleHome = if ($env:GRADLE_USER_HOME) { $env:GRADLE_USER_HOME } else { Join-Path $HOME '.gradle' }
Get-ChildItem -Path (Join-Path $gradleHome 'caches/*') -Directory -ErrorAction SilentlyContinue |
    ForEach-Object {
        $size = (Get-ChildItem -LiteralPath $_.FullName -Recurse -File -Force -ErrorAction SilentlyContinue |
            Measure-Object -Property Length -Sum).Sum
        [pscustomobject]@{ Directory = $_.FullName; SizeGB = [math]::Round($size / 1GB, 2) }
    } |
    Format-Table -AutoSize
