#
# RunATest.ps1
#	A powershell script to run an application multiple times while captureing the timing.
#
# Copyright 2026  TMurgent Technologies, LLP


[CmdletBinding()]
param(
  [Parameter(Position=0, Mandatory=$True)][string]$ExeFilePath,
  [Parameter(Position=1, Mandatory=$True)][string]$Arguments,
  [Parameter(Position=2, Mandatory=$True)][int]$NumberOfRuns,
  
  [Parameter(Position=3, Mandatory=$False)][int]$StartSettleMS,
  [Parameter(Position=4, Mandatory=$False)][int]$BetweenRunsSettleMS

  ## NOTE: Call with -Verbose parameter to debug
)

function cleanup-package($package, $timeouts)
{
	################################
	##### WARNING #####
	##### Currently (7/2026) there is an OS bug related to reset-appxpackage. The bug looses committed memory in the Appx svchost process, and also in the user's explorer process for the user desktop.
	##### Repeated calls will result in loosing virtual memory, eventually causing programs to crash and even OS reset.
	##### Monitor the use of committed memory in tests that call this function.
	################################
	Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 	
	reset-appxpackage -Package $package
	Start-Sleep  -Milliseconds $timeouts  ## added extra sleep to ensure system settles when we 
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

$package = get-appxpackage -name "LotsOfStuff"

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
		  cleanup-package $package $ExtraPackageSleepMS
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
		  cleanup-package $package $ExtraPackageSleepMS
	  }
	  else
	  {
		  $testfolder = Join-Path -Path $executingScriptDirectory -ChildPath "Test6Folder"
		  remove-item -Path $testfolder -Recurse -Force 
		  Start-Sleep  -Milliseconds $ExtraPackageSleepMS     ## added extra sleep to ensure system settles 
	  }
  }
  

  if ($index -lt $NumberOfRuns) {
	Start-Sleep  -Milliseconds $BetweenRunsSettleMS
  }
}

$min = $arr | Measure-Object -Minimum | Select-Object -ExpandProperty Minimum
$tot=$arr | Measure-Object -Sum | Select-Object -ExpandProperty Sum
$avg = $tot/$NumberOfRuns
$tot -= $arr[0]
$skipfirstAvg = $tot / ($NumberOfRuns - 1)

Write-Host -ForegroundColor "Green" "Summarized Results for test $($Arguments) on $($FullExeFilePath)"
Write-Host -ForegroundColor "Green" "Minimum:      $min ms"
Write-Host -ForegroundColor "Green" "Average:      $avg ms"
Write-Host -ForegroundColor "Green" "SkipFirstAvg: $skipfirstAvg ms"

Write-Host "All runs completed."
