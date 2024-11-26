[Setup]
AppName=Ticksy
AppVersion=1.0
DefaultDirName={pf}\ticksy
DefaultGroupName=Ticksy
OutputDir=output\installer
OutputBaseFilename=installer
Compression=lzma
SolidCompression=yes

[Files]
Source: "publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Ticksy"; Filename: "{app}\Ticksy.exe"
Name: "{commondesktop}\Ticksy"; Filename: "{app}\Ticksy.exe"