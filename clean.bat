@echo off
setlocal enabledelayedexpansion

REM Directorio raíz de la solución .NET
set "ROOT_DIR=%cd%"

REM Buscar y eliminar todas las carpetas bin y obj
for /d /r "%ROOT_DIR%" %%d in (bin obj) do (
  if exist "%%d" (
    echo Eliminando %%d
    rmdir /s /q "%%d"
  )
)

echo Limpieza completada.