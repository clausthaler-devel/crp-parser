; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "CRP-Extractor"
#define MyAppVersion "0.1"
#define MyAppPublisher "Claus Thaler"
#define MyAppURL "https://github.com/clausthaler-devel/crp-parser"
#define MyAppExeName "CRPExtractorW.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B9ABF8CB-0870-4F98-8F26-DCA6C265DB24}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Installer
OutputBaseFilename={#MyAppName}-{#MyAppVersion}-Setup
SetupIconFile=CRPExtractor\crpextractor.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "CRPExtractorW\bin\Release\CRPExtractorW.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CRPExtractorW.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CRPExtractorW.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CRPExtractorW.vshost.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CRPExtractorW.vshost.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CRPExtractorW.vshost.exe.manifest"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CrpParser.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\CrpParser.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\Magick.NET-Q16-AnyCPU.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\Magick.NET-Q16-AnyCPU.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractorW\bin\Release\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "CRPExtractor\bin\Release\CRPExtractor.exe"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
