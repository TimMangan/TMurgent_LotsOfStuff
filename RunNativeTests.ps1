Write-Host -ForegroundColor "Cyan" "Starting Native Tests..." 

$NumberOfRuns = 100
$StartSettleMS = 5000
$BetweenRunsSettleMS = 1000

$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe" -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS


Write-Host -ForegroundColor "Cyan" "Native Tests Done" 