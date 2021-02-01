@echo off

echo verificando instalacao Gulp CLI
call  %AppData%\npm\gulp -v
if %ERRORLEVEL% == 0 goto :next
goto :cliError

:cliError
echo Iniciando instalacao Gulp CLI
start /high /wait cmd.exe /c ../../../Setup/nodejs/npm install -g gulp-cli

:next
echo
echo Iniciando server nodered
start cmd.exe /c %AppData%\npm\gulp -b "..\SauronEyePortal.Web" --color --gulpfile ".\Gulpfile.js" nodeRed
EXIT /B 0