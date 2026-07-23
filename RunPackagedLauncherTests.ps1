# Disable progress bar display
$ProgressPreference = 'SilentlyContinue'


Write-Host -ForegroundColor "Cyan" "Starting Packaged Tests..." 

$NumberOfRuns = 500
$StartSettleMS = 5000
$BetweenRunsSettleMS = 1000

$Ins = (get-appxPackage -name "LotsOfStuff").InstallLocation
if ($Ins -eq $null) {
	Write-Host -ForegroundColor "Red" "Could not find the installed package. Please install the package first."
	exit 1
}

$FullExePath = Join-Path -Path $Ins -ChildPath "LotsOfStuff_PsfLauncher1.exe"

.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS
.\RunATest.ps1  -ExeFilePath $FullExePath -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS


Write-Host -ForegroundColor "Cyan" "Native Packaged Done" 