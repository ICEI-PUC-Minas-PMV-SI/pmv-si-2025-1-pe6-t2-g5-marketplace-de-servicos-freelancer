@echo off
:: Verifica se está sendo executado como Administrador
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Solicitando permissao de administrador...
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo ===============================
echo Iniciando instalacao de dependencias
echo ===============================

:: Instala o .NET SDK 9.0 (ajuste se necessário para versão exata, pois winget nem sempre suporta versões específicas)
echo Instalando .NET SDK 9.0...
winget install --id Microsoft.DotNet.SDK.9 --exact --silent

:: Instala o PostgreSQL 17
echo Instalando PostgreSQL...
winget install --id PostgreSQL.PostgreSQL --exact --silent

:: Instala o pgAdmin 4
echo Instalando pgAdmin 4...
winget install --id PostgreSQL.pgAdmin --exact --silent

echo ===============================
echo Instalacao concluida.
echo Verifique os programas instalados.
pause
