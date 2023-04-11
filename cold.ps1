param
(
    [Parameter(Mandatory=$true)]
    [string] $version
)

for (($i = 0), ($j = 0); $i -lt 5; $i++)
{
    & dotnet build ./DotNetRunner/DotNetRunner.csproj -bl -c Release -r android-arm64 -t:Benchmark -p:Targeting=$version -p:ColdStart=true | grep 'Info_Invoke \|' -CaseSensitive
}