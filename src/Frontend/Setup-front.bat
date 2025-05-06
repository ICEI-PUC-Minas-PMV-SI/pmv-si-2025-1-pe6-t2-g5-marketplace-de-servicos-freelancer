@echo off
:: Verifica se está sendo executado como Administrador
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Solicitando permissao de administrador...
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo ===============================
echo Instalando dependencias do Frontend
echo ===============================

:: Instala o Node.js LTS (altere o ID se quiser uma versão exata)
echo Instalando Node.js...
winget install --id OpenJS.NodeJS.LTS --exact --silent

:: Atualiza o npm (por garantia)
echo Atualizando npm...
npm install -g npm

:: Instala o TypeScript globalmente
echo Instalando TypeScript...
npm install -g typescript

:: Instala o Tailwind CSS globalmente
echo Instalando Tailwind CSS...
npm install -g tailwindcss

echo ===============================
echo Instalacao concluida.
pause
