; -- 64Bit.iss --
; Demonstrates installation of a program built for the x64 (a.k.a. AMD64)
; architecture.

#define MyAppName "AstroGrep"
#define MyAppVersion "4.4.7"


[Setup]
AppName={#MyAppName}
AppId=astrogrep
AppVersion={#MyAppVersion}
WizardStyle=modern
DefaultDirName={autopf}\AstroGrep
DefaultGroupName=Astrogrep
UninstallDisplayIcon={app}\AstroGrep.exe
Compression=lzma2
SolidCompression=yes
OutputDir=dists
OutputBaseFilename={#MyAppName}-v{#MyAppVersion}
; "ArchitecturesAllowed=x64" specifies that Setup cannot run on
; anything but x64.
ArchitecturesAllowed=x64
; "ArchitecturesInstallIn64BitMode=x64" requests that the install be
; done in "64-bit mode" on x64, meaning it should use the native
; 64-bit Program Files directory and the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64
InfoBeforeFile=InfoBefore.rtf
LicenseFile=GPL.text
DisableWelcomePage=yes
SetupIconFile=..\AstroGrep\WinformsGUI\Images\AstroGrep_Icon_256x256.ico
;WizardImageFile=..\AstroGrep\WinformsGUI\Images\AstroGrep_256x256.png



[Files]
;Source: "MyProg-x64.exe"; DestDir: "{app}"; DestName: "MyProg.exe"
;Source: "MyProg.chm"; DestDir: "{app}"
;Source: "Readme.txt"; DestDir: "{app}"; Flags: isreadme
Source: "..\AstroGrep\WinformsGUI\bin\Release\*.*"; DestDir:"{app}"; 

[Icons]
Name: "{group}\AstroGrep"; Filename: "{app}\AstroGrep.exe"
