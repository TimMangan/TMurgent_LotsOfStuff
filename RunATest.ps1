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
  
Start-Sleep  -Milliseconds $StartSettleMS

$arr = @()
for ($index= 1; $index -le $NumberOfRuns; $index++) {
  $argList = $Arguments + " " + $index
  $startTime = Get-Date
  
  Start-Process -Wait	 "$($FullExeFilePath)" -ArgumentList "$($argList)"

  $endTime = Get-Date

  $duration = $endTime - $startTime
  $arr += $duration.TotalMilliseconds
  
  Write-Verbose "Run $($index) Duration: $($duration.TotalMilliseconds) ms"


  ## Cleanups that must occur between runs
  if ($Arguments -eq 3 -or $Arguments -eq 4)
  {
	  if ($isPackaged)
	  {
		  get-appxpackage -name "LotsOfStuff" | reset-appxpackage 
		  Start-Sleep  -Milliseconds $StartSettleMS  ## added extra sleep to ensure system settles when we do this
	  }
	  else
	  {
		remove-item -Path "HKCU\Test3_BaseKey" -Recurse -Force -ErrorAction SilentlyContinue
	  }
  }
  if ($Arguments -eq 6)
  {
	  if ($isPackaged)
	  {
		  get-appxpackage -name "LotsOfStuff" | reset-appxpackage 
		  Start-Sleep  -Milliseconds $StartSettleMS  ## added extra sleep to ensure system settles when we do this
	  }
	  else
	  {
		  $testfolder = Join-Path -Path $executingScriptDirectory -ChildPath "Test6Folder"
		  remove-item -Path $testfolder -Recurse -Force 
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
