; Inno Setup script for LotsOfStuffWinUI_DotNet
; Adjust AppPublishDir if your publish output is in a different location

#define AppPublishDir "bin\x64\Release\net10.0-windows10.0.22000.0\win-x64"
#define Redistributable "D:\\source\\repos\\TMurgent_LotsOfStuff\\LotsOfStuffWinUI_DotNet\\Redistributable\\WindowsAppRuntimeInstall-x64.exe"

[Setup]
AppName=LotsOfStuffWinUI_DotNet
AppVersion=1.0.0
DefaultDirName={pf64}\LotsOfStuffWinUI_DotNet
DefaultGroupName=LotsOfStuffWinUI_DotNet
PrivilegesRequired=admin
Compression=lzma
SolidCompression=yes
OutputDir=..\\..\\Releases
OutputBaseFilename=LotsOfStuffWinUI_Installer

[Files]
; Publish output of the app (adjust AppPublishDir if needed)
Source: "{#AppPublishDir}\\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs ignoreversion

; Embed the Windows App Runtime redistributable; it will be extracted and executed from {tmp}
Source: "{#Redistributable}"; DestDir: "{tmp}"; Flags: dontcopy

[Icons]
Name: "{group}\\LotsOfStuffWinUI_DotNet"; Filename: "{app}\\LotsOfStuffWinUI_DotNet.exe"

[Tasks]
Name: desktopicon; Description: "Create a &desktop icon"; GroupDescription: "Additional tasks:"; Flags: unchecked

[Code]
var
  TempRedistributable: string;

function ExecAndCheckExitCode(const FileName, Params: string; var ResultCode: Integer): Boolean;
begin
  Result := Exec(ExpandConstant(FileName), Params, '', SW_SHOW, ewWaitUntilTerminated, ResultCode);
end;

function InitializeSetup(): Boolean;
var
  ResultCode: Integer;
begin
  Result := True; // allow install to continue by default

  // Extract the embedded redistributable to {tmp}
  try
	ExtractTemporaryFile(ExtractFileName(ExpandConstant('{#Redistributable}')));
  except
	MsgBox('Failed to extract Windows App Runtime installer from the setup. Aborting.', mbError, MB_OK);
	Result := False;
	Exit;
  end;

  TempRedistributable := ExpandConstant('{tmp}\\' + ExtractFileName(ExpandConstant('{#Redistributable}')));

  // Try common silent install switches for the redistributable
  if ExecAndCheckExitCode(TempRedistributable, '/install /quiet /norestart', ResultCode) then
  begin
	if (ResultCode = 0) or (ResultCode = 3010) then
	begin
	  Result := True;
	  Exit;
	end;
  end;

  if ExecAndCheckExitCode(TempRedistributable, '/quiet /norestart', ResultCode) then
  begin
	if (ResultCode = 0) or (ResultCode = 3010) then
	begin
	  Result := True;
	  Exit;
	end;
  end;

  // If installation failed, prompt the user
  if MsgBox('Windows App Runtime installation failed (exit code: ' + IntToStr(ResultCode) + ').' + #13#10 +
			'The app requires the Windows App Runtime to run. Do you want to abort installation?', mbError, MB_YESNO) = IDYES then
  begin
	Result := False;
  end
  else
  begin
	Result := True; // user chose to continue despite failure
  end;
end;
