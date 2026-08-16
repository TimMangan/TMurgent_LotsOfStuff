# Disable progress bar display
$ProgressPreference = 'SilentlyContinue'

Write-Host -ForegroundColor "Cyan" "Starting WinUI Native Tests..." 

$Scenario = "Native"
$exeFullPath = "$($env:ProgramFilesX64)\LotsOfStuffWinUI_DotNet\LotsOfStuffWinUI_DotNet.exe"

$NumberOfRuns = 500
$StartSettleMS = 5000
$BetweenRunsSettleMS = 1000

$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

 
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeFullPath -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS


Write-Host -ForegroundColor "Cyan" "WInUI Native Tests Done" 