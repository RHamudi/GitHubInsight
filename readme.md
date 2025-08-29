# 📘 GithubInsight

## 🔹 Project Overview

**GithubInsight** é uma **.NET 8 Web API** (arquitetura em camadas) que integra com a **API pública do GitHub** para buscar dados de usuários, processar estatísticas de repositórios em memória e **persistir apenas o resumo** em **SQL Server**.

**Principais funcionalidades**

- Buscar informações de usuários do GitHub.
- Processar estatísticas: total de repositórios, soma de estrelas, ranking de linguagens, top 5 repositórios por estrelas, repositórios criados por ano.
- Persistência em SQL Server (Database-First com EF Core).
- **Autenticação JWT** e **Swagger** para documentação.

**Camadas**

- **API**: Controllers, pipeline ASP.NET, Auth/Swagger.
- **Application**: Regras e contratos (services, DTOs).
- **Infrastructure**: EF Core (DbContext via scaffold), implementações de serviços externos e repositórios.
- **Domain**: Entidades e regras puras (se aplicável).

---

## 🔹 Tech Stack

- **.NET 8 Web API**
- **SQL Server** (Database First + EF Core)
- **Entity Framework Core** (Scaffold)
- **JWT Authentication**
- **BCrypt** para hashing de senhas
- **Swagger**

---

## Documentação da API Utilizada

- Este projeto utiliza a API pública do GitHub:
  🔗 https://docs.github.com/en/rest

## Endpoints principais

- POST /api/GithubInsight/AuthUser/{username} → autenticação e geração de token JWT.
- GET /api/GithubInsight/sync-user/{username} → busca informações de usuário no GitHub e Salva no Banco.
- GET /api/GithubInsight/stats/{username} → Obtém estatísticas de um usuário do GitHub
- GET /api/GithubInsight/TopRepos/{username} → Obtem os 5 repositórios mais populares de um usuário
- GET /api/GithubInsight/Languages/{username} → Obtém as linguagems mais utilizadas do usuario do Github.

## 🔹 Setup Instructions

### 1) Clonar o repositório

```bash
git clone https://github.com/seuusuario/GithubInsight.git
cd GithubInsight
```

## Banco De dados (SQL Server)

### 2 Criar Banco De dados

- Script Sql em db/GithubInsight.sql

```bash
    CREATE DATABASE GithubInsight;
```

## Configuração de conexão (API)

- em src/GithubInsight.API crie o arquivo appsettings.json

```
    {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=GithubInsight;Trusted_Connection=True;TrustServerCertificate=True;"
    },
    "Jwt": {
        "Key": "sua_chave_super_secreta",
        "Issuer": "GithubInsightAPI",
        "Audience": "GithubInsightClient",
        "ExpireMinutes": 60
        }
    }
```

## 4) Restaurar, compilar e executar

```
    dotnet restore
    dotnet build
    dotnet run --project src/GithubInsight.API
```

## 🔹 License

- Este projeto foi desenvolvido exclusivamente para fins de avaliação técnica.
