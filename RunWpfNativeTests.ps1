# Disable progress bar display
$ProgressPreference = 'SilentlyContinue'

Write-Host -ForegroundColor "Cyan" "Starting WPF Native Tests..." 

$Scenario = "Native"
$exeRelativePath = "LotsOfStuffWPF_DotNetFramework\bin\Release\LotsOfStuffWPF_DotNetFramework.exe"
$NumberOfRuns = 500
$StartSettleMS = 5000
$BetweenRunsSettleMS = 1000

$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
#.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $exeRelativePath -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS


Write-Host -ForegroundColor "Cyan" "WPF Native Tests Done" 