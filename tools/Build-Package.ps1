#requires -Version 7.0
param(
    [Parameter(Mandatory)] [string]$ManagedDirectory,
    [Parameter(Mandatory)] [string]$CompilerPath,
    [string]$OutputDirectory = (Join-Path $PSScriptRoot '../artifacts')
)

$ErrorActionPreference = 'Stop'
$repository = (Resolve-Path -LiteralPath (Join-Path $PSScriptRoot '..')).ProviderPath
$source = Join-Path $repository 'VanillaMapMod'
$managed = (Resolve-Path -LiteralPath $ManagedDirectory).ProviderPath
$compiler = (Resolve-Path -LiteralPath $CompilerPath).ProviderPath
$outputPath = $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($OutputDirectory)
[xml]$project = Get-Content -LiteralPath (Join-Path $source 'VanillaMapMod.csproj') -Raw
$properties = $project.Project.PropertyGroup[0]

New-Item -ItemType Directory -Path $outputPath -Force | Out-Null
$dll = Join-Path $outputPath 'VanillaMapMod.dll'
$pdb = Join-Path $outputPath 'VanillaMapMod.pdb'
$info = Join-Path $outputPath 'VanillaMapMod.AssemblyInfo.cs'
$responseFile = Join-Path $outputPath 'VanillaMapMod.rsp'

$attributes = [ordered]@{
    AssemblyTitle = $properties.AssemblyTitle
    AssemblyProduct = $properties.Product
    AssemblyCopyright = $properties.Copyright
    AssemblyVersion = $properties.AssemblyVersion
    AssemblyFileVersion = $properties.FileVersion
    AssemblyInformationalVersion = $properties.Version
}
$assemblyInfo = foreach ($entry in $attributes.GetEnumerator()) {
    $literal = ConvertTo-Json -InputObject ([string]$entry.Value) -Compress
    "[assembly: System.Reflection.$($entry.Key)($literal)]"
}
$assemblyInfo | Set-Content -LiteralPath $info -Encoding utf8

$arguments = [System.Collections.Generic.List[string]]::new()
$arguments.AddRange([string[]]@(
    '/nologo', '/nostdlib+', '/target:library', '/langversion:preview', '/optimize+',
    '/deterministic+', '/debug:portable', '/define:TRACE;RELEASE',
    "/out:`"$dll`"", "/pdb:`"$pdb`"",
    "/pathmap:`"$source=VanillaMapMod,$outputPath=build`""
))
foreach ($name in @('mscorlib.dll', 'System.dll', 'System.Core.dll', 'netstandard.dll', 'Assembly-CSharp-firstpass.dll')) {
    $file = (Resolve-Path -LiteralPath (Join-Path $managed $name)).ProviderPath
    $arguments.Add("/reference:`"$file`"")
}
foreach ($reference in $project.GetElementsByTagName('Reference')) {
    $file = (Resolve-Path -LiteralPath $reference.HintPath.Replace('$(HollowKnightRefs)', $managed)).ProviderPath
    $arguments.Add("/reference:`"$file`"")
}
foreach ($resource in $project.GetElementsByTagName('EmbeddedResource')) {
    $file = (Resolve-Path -LiteralPath (Join-Path $source $resource.Include)).ProviderPath
    $name = $properties.RootNamespace + '.' + ($resource.Include -replace '[/\\]', '.')
    $arguments.Add("/resource:`"$file`",$name")
}
$arguments.Add("`"$info`"")
foreach ($file in (Get-ChildItem -LiteralPath $source -Recurse -File -Filter '*.cs' | Sort-Object FullName)) {
    if ($file.FullName -notmatch '[/\\](bin|obj)[/\\]') {
        $arguments.Add("`"$($file.FullName)`"")
    }
}
$arguments | Set-Content -LiteralPath $responseFile -Encoding utf8BOM
& $compiler /noconfig "@$responseFile"
if ($LASTEXITCODE -ne 0) { throw "C# compiler exited with status $LASTEXITCODE." }

$archive = Join-Path $outputPath "VanillaMapMod-ZH-$($properties.Version).zip"
$files = @($dll, $pdb, (Join-Path $source 'language.json'), (Join-Path $repository 'README.md'), (Join-Path $repository 'LICENSE'))
Compress-Archive -LiteralPath $files -DestinationPath $archive -CompressionLevel Optimal -Force
$hash = (Get-FileHash -LiteralPath $archive -Algorithm SHA256).Hash.ToLowerInvariant()
"$hash  $([IO.Path]::GetFileName($archive))" | Set-Content -LiteralPath (Join-Path $outputPath 'SHA256SUMS.txt') -Encoding ascii
Get-Item -LiteralPath $archive
