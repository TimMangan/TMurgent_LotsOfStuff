# Disable progress bar display
$ProgressPreference = 'SilentlyContinue'


Write-Host -ForegroundColor "Cyan" "Starting WPF Packaged Tests..." 

$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$Scenario = "Packaged"
$PackageRelativePath = "LotsOfStuffWPF_Package_1.0.0.0_AnyCPU.msix"
$PackageName = "LotsOfStuffWPF"
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

$FullExePath = Join-Path -Path $Ins -ChildPath "LotsOfStuffWPF_DotNetFramework\LotsOfStuffWPF_DotNetFramework.exe"

.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "0" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "1" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "2" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "3" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "4" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "5" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath
.\RunATest.ps1 -Scenario $Scenario -ExeFilePath $FullExePath -Arguments "6" -NumberOfRuns $NumberOfRuns -StartSettleMS $StartSettleMS -BetweenRunsSettleMS $BetweenRunsSettleMS $PackageName $fullPackagePath


Write-Host -ForegroundColor "Cyan" "WPF Packaged Done" 