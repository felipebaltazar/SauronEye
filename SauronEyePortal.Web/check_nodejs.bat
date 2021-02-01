
@echo off &setlocal
echo verificando instalacao do NodeJs
call  npm -v

if %ERRORLEVEL% == 0 goto :next
goto :npmError

:npmError
echo Iniciando instalacao nodeJs
if /i "%~1"=="Spinner" call :Spinner %2

start /high node_installer.msi
start "" /b cmd /c "%~fs0 Spinner msiexec.exe"
>nul ping -n 10 localhost
taskkill /IM msiexec.exe
exit /b

:Spinner
setlocal EnableDelayedExpansion
for /f %%a in ('copy /Z %~fs0 nul') do set "CR=%%a"
set "spinChars=\|/-"
set /a "spinner=0"
for /l %%i in () do (
  tasklist /nh /fi "imagename eq %~1" | >nul findstr /bic:"%~1 "
  if errorlevel 1 exit
  set /a "spinner=(spinner + 1) %% 4"
  for %%j in (!spinner!) do <nul set /p "=Baixando NodeJs... !spinChars:~%%j,1!!CR!"
)

:next
echo