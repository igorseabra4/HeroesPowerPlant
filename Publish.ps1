param(
    [string]$OutDir = (Join-Path $PSScriptRoot 'artifacts'),
    [string]$Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'

$project = Join-Path $PSScriptRoot 'HeroesPowerPlant\HeroesPowerPlant.csproj'
$version = dotnet msbuild $project -getProperty:Version
$name = "HeroesPowerPlant_$version"
$stage = Join-Path $OutDir $name
$zip = Join-Path $OutDir "$name.zip"

Remove-Item $stage, $zip -Recurse -Force -ErrorAction SilentlyContinue

dotnet publish $project -c $Configuration -o $stage -p:DebugType=none
if ($LASTEXITCODE -ne 0) { throw "Publish failed ($LASTEXITCODE)" }

Remove-Item (Join-Path $stage 'csharp-prs.targets')
Get-ChildItem (Join-Path $stage 'runtimes') -Directory | Where-Object Name -ne 'win-x64' | Remove-Item -Recurse -Force

& "$env:SystemRoot\System32\tar.exe" -a -cf $zip -C $stage *
if ($LASTEXITCODE -ne 0) { throw "Zip failed ($LASTEXITCODE)" }

Write-Host "Created $zip"