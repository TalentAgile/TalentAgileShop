Param(
    [string]$SourcesDirectory,
    [string]$BuildNumber
)

# Check sources directory is defined
if(-not $SourcesDirectory)
{
    Write-Error "SourcesDirectory variable is missing."
    exit 1
}
elseif(-not (Test-Path $SourcesDirectory))
{
    Write-Error "SourcesDirectory does not exist: $SourcesDirectory"
    exit 1
}

Write-Host "SourcesDirectory: $SourcesDirectory"

# Make sure there is a build number
if(-not $BuildNumber)
{
    Write-Error "BuildNumber is missing."
    exit 1
}

Write-Host "BuildNumber: $BuildNumber"

# Prepare Regex to find assembly version
$VersionRegex = "\d+\.\d+\.\d+\.\d+"
$VersionData = [regex]::Matches($BuildNumber, $VersionRegex)
switch($VersionData.Count)
{
    0 {
        Write-Error "Could not find version number data in BuildNumber."
        exit 1
    }
    1 { }
    default {
        Write-Warning "Found more than one instance of version data in BuildNumber."
        Write-Warning "Will assume first instance is version."
    }
}
$NewVersion = $VersionData[0]
Write-Host "Version: $NewVersion"

# Find AssemblyInfo files in sources directory
$Files = Get-ChildItem -Path $SourcesDirectory -Recurse -Include AssemblyInfo.*

function ApplyTheVersionToTheAssemblyFiles($files)
{
    Write-Host "$($files.count) files to update with the new version '$NewVersion'"
    foreach ($file in $files)
    {
        Write-Host "Processing $file"
        $fileContent = Get-Content($file)
        $fileContent -replace $VersionRegex, $NewVersion | Out-File $file
    }
}

# Apply version transformation
ApplyTheVersionToTheAssemblyFiles $Files