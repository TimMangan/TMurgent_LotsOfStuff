; Inno Setup script for LotsOfStuffWinUI_DotNet
; Adjust AppPublishDir if your publish output is in a different location

#define AppPublishDir "bin\x64\Release\net10.0-windows10.0.22000.0\win-x64"

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

; Embed the Windows App Runtime redistributable so it can be optionally installed by the user.
Source: "{#Redistributable}"; DestDir: "{tmp}"; Flags: dontcopy

[Icons]
Name: "{group}\\LotsOfStuffWinUI_DotNet"; Filename: "{app}\\LotsOfStuffWinUI_DotNet.exe"

[Tasks]
Name: desktopicon; Description: "Create a &desktop icon"; GroupDescription: "Additional tasks:"; Flags: unchecked
Name: winappruntime; Description: "Install the Windows App Runtime x64 (required for the app to run)"; GroupDescription: "Additional tasks:"; Flags: unchecked


