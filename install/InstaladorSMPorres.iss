; Controla las versión instalada del SMPorres con la nueva disponible                  

#define MyAppName "Sistema de Cobranzas"    
#define MyAppVersion "1.1.0.0"          
#define MyAppPublisher "Instituto San Martín de Porres"
#define MyAppURL "http://www.ismp.edu.ar"
#define MyAppExeName "Installer.exe"
#define MyAppId "C9EDC8CC-7980-4AFC-8285-00BEE64F96DF"
#define MyAppSource "D:\Proyectos\SMPorres\other\Installer\Installer\bin\Release"     
#define MyAppDestino "C:\Program Files\ISMP\Instalador"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={#MyAppId}
AppName={#MyAppName}
AppVersion={#MyAppVersion}               
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={#MyAppDestino}\{#MyAppName}      
DisableDirPage=yes
DisableProgramGroupPage=yes        
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest             
OutputDir=D:\Proyectos\SMPorres\install\InstaladorSMPorres
SetupIconFile=D:\Proyectos\SMPorres\src\SMPorres\app2.ico
OutputBaseFilename={#MyAppName}
;ISMP
Compression=lzma                               
SolidCompression=yes
WizardStyle=modern
DisableFinishedPage=yes

[Languages]                                                            
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]    
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
;Source: "D:\Proyectos\SMPorres\other\Installer\Installer\bin\Release\Installer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppSource}\Installer.exe"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\Installer.exe.config"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\Installer.pdb"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion                                                           
Source: "{#MyAppSource}\Installer.vshost.exe"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\Installer.vshost.exe.config"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName} "; Filename: "{#MyAppDestino}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{#MyAppDestino}\{#MyAppExeName}"; Tasks: desktopicon

