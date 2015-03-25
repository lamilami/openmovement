@echo off

SET MATLAB_EXE=
for %%i in (matlab.exe) do set MATLAB_EXE=%%~$PATH:i
if not defined MATLAB_EXE (
    echo Matlab executable not found in path.
    goto error
)
echo Matlab executable found in path at: "%MATLAB_EXE%"

set MCC=%MATLAB_EXE:~0,-11%\mcc.bat
if not exist "%MCC%" (
    echo MCC not found at: %MCC%
    goto error
)

echo MCC: %MCC%

rem cd /d %~dp0

rem -o "Name" -m "C:\matlab_code\main.m" -I "C:\matlab_code\lib" -d "C:\matlab_code\distrib"
@echo on
"%MCC%" -win32 -T link:exe %*
@echo off

goto end

:error
pause

:end
