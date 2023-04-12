param
(
    [Parameter(Mandatory=$true)]
    [string] $version
)

$params = "-bl", "-c", "Release", "-r", "android-arm64", "-p:Targeting=$version", "-p:ColdStart=true"

& dotnet build ./DotNetRunner/DotNetRunner.csproj -t:Rebuild $params
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

for ($i = 0; $i -lt 5; $i++)
{
    & dotnet build ./DotNetRunner/DotNetRunner.csproj -t:Benchmark $params | grep 'Info_Invoke \|' -CaseSensitive
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}