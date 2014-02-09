@Echo Installing DirectX...
:DXSETUP1
cd
cd data\files\support\DirectX\
start /wait DXSETUP.exe
IF %ERRORLEVEL% == 0 goto VCREDIST
else goto VCREDIST

:VCREDIST

cd ..
if %PROCESSOR_ARCHITECTURE%==x86 (
  start /wait vcredist_x86.exe
) else (
  start /wait vcredist_x64.exe
)
IF %ERRORLEVEL% == 0 goto UE3Redist
else goto UE3Redist

:UE3Redist
start /wait UE3Redist.exe
IF %ERRORLEVEL% == 0 goto QUIT
else goto QUIT

:QUIT
exit