# Disable progress bar display
$ProgressPreference = 'SilentlyContinue'

Write-Host -ForegroundColor "Cyan" "Starting WinUI Native Tests..." 

$Scenario = "Native"
$exeRelativePath = "LotsOfStuffWinUI_DotNet\LotsOfStuffWinUI_DotNet\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\LotsOfStuffWinUI_DotNet.exe"
$NumberOfRuns = 10
$StartSettleMS = 5000
$BetweenRunsSettleMS = 1000

$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$FullExePath = 
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS


Write-Host -ForegroundColor "Cyan" "WInUI Native Tests Done" 