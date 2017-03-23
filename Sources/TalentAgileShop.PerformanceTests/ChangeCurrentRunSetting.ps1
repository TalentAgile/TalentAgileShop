param
(
    [string]$loadTestFilePath,
    [string]$runSettingName
)

$loadtestContent = [xml](Get-Content $loadTestFilePath)

$loadtestContent.LoadTest.CurrentRunConfig = $runSettingName

$loadtestContent.Save($loadTestFilePath)