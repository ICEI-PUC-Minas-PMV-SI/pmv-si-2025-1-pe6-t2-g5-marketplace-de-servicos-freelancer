# Instruções de configuração do Backend

# -Instalação
# Opção 1: Instalação manual

## Programas

1. Instale o .NET, PostgreSQL e pgAdmin.

- [.NET SDK 9.0 - v 9.0.203] (https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-9.0.203-windows-x64-installer)
- [PostgreSQL 17.4.1] (https://sbp.enterprisedb.com/getfile.jsp?fileid=1259505)
- [pgAdmin v9.3] (https://ftp.postgresql.org/pub/pgadmin/pgadmin4/v9.3/windows/pgadmin4-9.3-x64.exe)

# Opção 2: Instalação automática

- Execute o arquivo `Setup-back.bat` nesta pasta. Este arquivo irá instalar o .NET SDK, PostgreSQL e pgAdmin automaticamente.

# -Execução
## Passo a passo

1. Na pasta `src/Backend/MarketplaceFreelance/Infraestructure`, execute o comando:
   ```bash
    dotnet ef database update
   ```
2. Na pasta `src/Backend/MarketplaceFreelance/API`, execute os comandos:
   ```bash
    dotnet clean
   ```
   ```bash
    dotnet restore
   ```
   ```bash
    dotnet watch run
   ```
3. Acesse a URL https://localhost:7292/swagger/index.html para verificar se a API está funcionando corretamente.