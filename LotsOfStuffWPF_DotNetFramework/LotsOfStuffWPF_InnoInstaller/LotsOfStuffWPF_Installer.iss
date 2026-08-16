; Inno Setup script for LotsOfStuffWPF_DotNetFramework

#define AppPublishDir "bin\Release\net7.0"

[Setup]
AppName=LotsOfStuffWPF_DotNetFramework
AppVersion=1.0.0
DefaultDirName={pf64}\LotsOfStuffWPF_DotNetFramework
DefaultGroupName=LotsOfStuffWPF_DotNetFramework
PrivilegesRequired=admin
Compression=lzma
SolidCompression=yes
OutputDir=..\\..\\Releases
OutputBaseFilename=LotsOfStuffWPF_Installer

[Files]
; Publish output of the app (place publish output into the Release folder under Inno)
Source: "{#AppPublishDir}\\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs ignoreversion

[Icons]
Name: "{group}\\LotsOfStuffWPF_DotNetFramework"; Filename: "{app}\\LotsOfStuffWPF_DotNetFramework.exe"

[Tasks]
Name: desktopicon; Description: "Create a &desktop icon"; GroupDescription: "Additional tasks:"; Flags: unchecked
