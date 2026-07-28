#
# RunATest.ps1
#	A powershell script to run an application multiple times while captureing the timing.
#
# Copyright 2026  TMurgent Technologies, LLP


[CmdletBinding()]
param(
  [Parameter(Position=0, Mandatory=$True)][string]$Scenario,
  [Parameter(Position=1, Mandatory=$True)][string]$ExeFilePath,
  [Parameter(Position=2, Mandatory=$True)][string]$Arguments,
  [Parameter(Position=3, Mandatory=$True)][int]$NumberOfRuns,
  
  [Parameter(Position=4, Mandatory=$False)][int]$StartSettleMS,
  [Parameter(Position=5, Mandatory=$False)][int]$BetweenRunsSettleMS,
  [Parameter(Position=6, Mandatory=$False)][string]$PackageName,
  [Parameter(Position=7, Mandatory=$False)][string]$MsixPackagePath

  ## NOTE: Call with -Verbose parameter to debug
)

function cleanup-package($package, $msixPackagePath, $timeouts)
{
	################################
	##### WARNING #####
	##### Currently (7/2026) there is an OS bug related to reset-appxpackage. The bug looses committed memory in the Appx svchost process, and also in the user's explorer process for the user desktop.
	##### Repeated calls will result in loosing virtual memory, eventually causing programs to crash and even OS reset.
	##### Monitor the use of committed memory in tests that call this function.
	################################
	#####Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 	
	#####reset-appxpackage -Package $package
	#####Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 

	remove-appxpackage $package
	Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 	
	add-appxpackage $msixPackagePath
	Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 
	Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 

}

function output-csv($numbers, $csvFilepath)
{
	# Convert the array into objects with a property name
	$numberObjects = $numbers | ForEach-Object{
		[PSCustomObject]@{
			MS = $_
		}
	}

	try 
	{
		# Export to CSV without type information
		$numberObjects | Export-Csv -Path $csvFilepath -NoTypeInformation -Force
		Write-Host -ForegroundColor "Blue" -BackgroundColor "Black" "CSV file created successfully at: $csvFilepath"
	}
	catch 
	{
		Write-Host "Error writing CSV file: $($_.Exception.Message)" -ForegroundColor Red
	}
}

Write-Host "Running $ExeFilePath $Arguments for $NumberOfRuns times"
$executingScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

## Adjust for both full and relative path provided
if ($ExeFilePath -like "C:\*") {
	$FullExeFilePath = $ExeFilePath
	$isPackaged = $true
}
else
{
	$FullExeFilePath = Join-Path -Path $executingScriptDirectory -ChildPath $ExeFilePath
	$isPackaged = $false
}
$ExtraPackageSleepMS = 500  
Start-Sleep  -Milliseconds $StartSettleMS

if ($isPackaged)
{
	$package = get-appxpackage -name $PackageName
	if ($package -ne $null)
	{
		cleanup-package $package $MsixPackagePath $ExtraPackageSleepMS 
	}
	else
	{
		add-appxpackage $MsixPackagePath
		Start-Sleep  -Milliseconds $ExtraPackageSleepMS 
	}
}

$arr = @()
for ($index= 1; $index -le $NumberOfRuns; $index++) {
  $argList = $Arguments + " " + $index
  $startTime = Get-Date
  
  Start-Process -Wait	 "$($FullExeFilePath)" -ArgumentList "$($argList)"

  $endTime = Get-Date

  $duration = $endTime - $startTime
  $arr += $duration.TotalMilliseconds
  
  ###Write-Verbose "Run $($index) Duration: $($duration.TotalMilliseconds) ms"


  ## Cleanups that must occur between runs

  if ($Arguments -eq 3 -or $Arguments -eq 4)
  {
	# These tests write to the registry, so clean that up between tests, depending on the type of
	# app package/lack thereof.
	  if ($isPackaged)
	  {
		  cleanup-package $package $MsixPackagePath $ExtraPackageSleepMS
      }
	  else
	  {
		remove-item -Path "HKCU\Software\Test3_BaseKey" -Recurse -Force -ErrorAction SilentlyContinue
	  }
  }
  if ($Arguments -eq 6)
  {
	# These tests write files to the file system, , so clean that up between tests, depending on the type of
	# app package/lack theirof.

	  if ($isPackaged)
	  {
		  cleanup-package $package $MsixPackagePath $ExtraPackageSleepMS
	  }
	  else
	  {
		  $testfolder = Join-Path -Path $ExeFilePath.Parent -ChildPath "Test6Folder"
		  remove-item -Path $testfolder -Recurse -Force 
		  Start-Sleep  -Milliseconds $ExtraPackageSleepMS     ## added extra sleep to ensure system settles 
	  }
  }
  

  if ($index -lt $NumberOfRuns) {
	Start-Sleep  -Milliseconds $BetweenRunsSettleMS
  }
}

#############################
### END OF RUNS REPORTING
#############################
$min = $arr | Measure-Object -Minimum | Select-Object -ExpandProperty Minimum
$max = $arr | Measure-Object -Maximum | Select-Object -ExpandProperty Maximum


$tot=$arr | Measure-Object -Sum | Select-Object -ExpandProperty Sum
$avg = $tot/$NumberOfRuns
$tot -= $arr[0]
$skipfirstAvg = $tot / ($NumberOfRuns - 1)



$sorted = $arr | Sort-Object

##Write-Host "Sorted individual results:"
##for ($i = 0; $i -lt $sorted.Count; $i += 5) {
##    $chunk = $sorted[$i..([math]::Min($i+4, $sorted.Count-1))]
##    Write-Host -ForegroundColor "Blue" -BackgroundColor "Black" ($chunk -join ", ")
##}
Write-Host "Saving sorted individual results to: $name"
$name = "Results\$($Scenario)_$($Arguments)_Results.csv"
output-csv $sorted  $name


# Discard the top 10% result times as unreasable interference by Windows
$reasonableCount = 0;
$reasonableTot = 0;
$cutoff = $sorted.Count * 0.9
foreach ($val in $sorted)
{
	if ($reasonableCount -lt $cutoff)
	{
		$reasonableCount++;
		$reasonableTot += $val
	}
}
$reasonableAvg = $reasonableTot / $reasonableCount;

Write-Host -ForegroundColor "Green" "Summarized Results for test $($Arguments) on $($FullExeFilePath)"
Write-Host -ForegroundColor "Green" "Minimum:      $min ms"
Write-Host -ForegroundColor "Green" "Maximum:      $max ms"
Write-Host -ForegroundColor "Green" "Average:      $avg ms"
Write-Host -ForegroundColor "Green" "SkipFirstAvg: $skipfirstAvg ms"
Write-Host -ForegroundColor "Green" "90PctAvg:     $reasonableAvg ms"

$record=@()
$record += $Scenario
$record += $min
$record += $max
$record += $avg
$record += $reasonableAvg
$name = "Results\$($Scenario)_$($Arguments)_MinMaxAvg90.csv"
output-csv $record  $name

Write-Host "All runs completed."
