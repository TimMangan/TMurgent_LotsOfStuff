# Disable progress bar display
$ProgressPreference = 'SilentlyContinue'


Write-Host -ForegroundColor "Cyan" "Starting WinUI Packaged with Full Psf Tests..." 

$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$Scenario = "FullPsf"
$PackageRelativePath = "LotsOfStuffWinUIPackage_1.0.0.1_FullPsf_x64.msix"
$PackageName = "LotsOfStuffWinUI"
$NumberOfRuns = 500
$StartSettleMS = 5000
$BetweenRunsSettleMS = 1000

$fullPackagePath = Join-Path -Path $executingScriptDirectory -ChildPath $PackageRelativePath

$Ins = (get-appxPackage -name $PackageName).InstallLocation
if ($Ins -eq $null) 
{
	get-appxpackage -name $PackageName | remove-appxPackage
	add-appxPackage $fullPackagePath
	$Ins = (get-appxPackage -name $PackageName).InstallLocation
}
if ($Ins -eq $null) 
{
	Write-Host -ForegroundColor "Red" "Could not find the installed package. Please install the package first."
	exit 1
}


$fullExePath = Join-Path -Path $Ins -ChildPath "LotsOfStuffWinUI_PsfLauncher1.exe"

.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath


Write-Host -ForegroundColor "Cyan" "WInUI Packaged with Full Psf Done" 