; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "ISMP"
#define MyAppVersion "1.3.0.57"
#define MyAppPublisher "Instituto San Mart�n de Porres"
#define MyAppURL "http://www.ismp.edu.ar"
#define MyAppExeName "SMPorres.exe"
#define MyAppId "4176C0F8-3F7C-4BD4-B2A1-99D8E74C4057"
#define MyAppSource "D:\Proyectos\SMPorres\src\SMPorres\bin\JHC"
#define MyAppDestino "C:\Program Files\ISMP"


[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={#MyAppId}
AppName={#MyAppName}
AppVersion={#MyAppVersion}        
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={#MyAppDestino}\{#MyAppName} 
//{autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=D:\Proyectos\SMPorres\install\SMPorres
OutputBaseFilename=Setup
SetupIconFile=D:\Proyectos\SMPorres\src\SMPorres\app2.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
DisableDirPage=yes
DisableFinishedPage=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

//[Tasks]
//Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; 

[Files]
;Source: "D:\Proyectos\SMPorres\src\SMPorres\bin\Debug\SMPorres.exe"; DestDir: "C:\Program Files\ISMP"; Flags: ignoreversion
;Source: "{#MyAppSource}\SMPorres.exe"; DestDir: "C:\Program Files\ISMP"; Flags: ignoreversion
Source: "{#MyAppSource}\SMPorres.exe"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.CrystalReports.Engine.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.ClientDoc.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.CommLayer.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.CommonControls.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.CommonObjectModel.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.Controllers.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.CubeDefModel.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.DataDefModel.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.DataSetConversion.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.ObjectFactory.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.Prompting.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.ReportDefModel.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportAppServer.XmlSerialize.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.ReportSource.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.Shared.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CrystalDecisions.Windows.Forms.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\CustomLibrary.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\EntityFramework.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\EntityFramework.SqlServer.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\EntityFramework.SqlServer.xml"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\EntityFramework.xml"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\FlashControlV71.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\log4net.config"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\log4net.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\log4net.xml"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\ShockwaveFlashObjects.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\SMPorres.exe"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\SMPorres.exe.config"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\SMPorres.pdb"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
;Source: "{#MyAppSource}\SMPorres.upload.xml"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\SMPorres.vshost.exe"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\SMPorres.vshost.exe.config"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
;Source: "{#MyAppSource}\SMPorres.vshost.exe.manifest"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
Source: "{#MyAppSource}\stdole.dll"; DestDir: "{#MyAppDestino}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
;Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autoprograms}\{#MyAppName}"; Filename: "{#MyAppDestino}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{#MyAppDestino}\{#MyAppExeName}"; 
//Tasks: desktopicon

[Code]
function GetNumber(var temp: String): Integer;
var
  part: String;
  pos1: Integer;
begin
  if Length(temp) = 0 then
  begin
    Result := -1;
    Exit;
  end;
    pos1 := Pos('.', temp);
    if (pos1 = 0) then
    begin
      Result := StrToInt(temp);
    temp := '';
    end
    else
    begin
    part := Copy(temp, 1, pos1 - 1);
      temp := Copy(temp, pos1 + 1, Length(temp));
      Result := StrToInt(part);
    end;
end;    

function CompareInner(var temp1, temp2: String): Integer;
var
  num1, num2: Integer;
begin
    num1 := GetNumber(temp1);
  num2 := GetNumber(temp2);
  if (num1 = -1) or (num2 = -1) then
  begin
    Result := 0;
    Exit;
  end;
      if (num1 > num2) then
      begin
        Result := 1;
      end
      else if (num1 < num2) then
      begin
        Result := -1;
      end
      else
      begin
        Result := CompareInner(temp1, temp2);
      end;
end;

//-1 ==> str1 < str2 
// 1 ==> str1 > str2
function CompareVersion(str1, str2: String): Integer;
var
  temp1, temp2: String;
begin
    temp1 := str1;
    temp2 := str2;
    Result := CompareInner(temp1, temp2);
end;

//Determina si se ejecuta el [Setup]
function InitializeSetup(): Boolean;
var
  oldVersion: String;
  uninstaller: String;
  ErrorCode: Integer;
begin
 
  if RegKeyExists(HKEY_LOCAL_MACHINE,
  'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1') then
  begin
    RegQueryStringValue(HKEY_LOCAL_MACHINE,
      'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1',
      'DisplayVersion', oldVersion);
  
    if (CompareVersion(oldVersion, '{#MyAppVersion}') < 0) then
    begin
     //Desintalar version {#MyAppInstalled}
      RegQueryStringValue(HKEY_LOCAL_MACHINE,
        'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{#MyAppID}_is1',
        'UninstallString', uninstaller);
      ShellExec('runas', uninstaller, '/VERYSILENT', '', SW_HIDE, ewWaitUntilTerminated, ErrorCode);
      if (ErrorCode <> 0) then
      begin
        //MsgBox( 'Failed to uninstall Code Beautifier Collection version ' + oldVersion + '. Please restart Windows and run setup again.',
        //mbError, MB_OK );
        Result := False;
      end
      else
      begin
        Result := True;
      end;
  
    end;

  end
  else  //  no est� instalado el programa. Instalar por 1ra vez
 begin
    Result := True;
 end;  
end;

//[Registry]
//Root: HKLM; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "Mi_Programa"; ValueData: "{app}\Programa.exe";
//Root: HKLM; Subkey: "HKEY_LOCAL_MACHINE\\SOFTWARE\\SMP"; ValueType: string; ValueName: "Cs"; ValueData: "tg9zuyIT7AfhuZ5AyFPzCsQHjYPeBmAsCVy4FD6OAmQ1Qhvbj3To8pXb07VdoF3mt0QHrfzmPB+VUL72vXwnZ3gJbJYlf7yn7QWZipifM7vuMP60ZyBuLU+3AWzgcnIUUFi6VA5HIYDuN3LTeqBp6UdXgLQavmOeCqrP+DoVq1odSH6ZGoWk4SaG+/eYFQ4iGuTekj2B+1wAk2sLSym/1L0sBGqSEaX8E1MBHmkbGywbdUe9sJvXnQPt9uVefU8K";