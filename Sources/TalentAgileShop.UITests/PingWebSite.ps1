param(
     [string] $uri
)
$r = Invoke-WebRequest -Uri $uri -TimeoutSec 180 -UseBasicParsing

Write-Host ("Status code: {0}" -f $r.StatusCode)

