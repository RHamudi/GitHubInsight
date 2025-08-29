-- Criar o banco de dados GithubInsight
CREATE DATABASE GithubInsight;

-- Usar o banco de dados GithubInsight
USE GithubInsight;

-- Criar a tabela User
CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Login NVARCHAR(100) NOT NULL,
    Name NVARCHAR(200),
    Location NVARCHAR(200),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
);

-- Criar a tabela Stats
CREATE TABLE Stats (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL UNIQUE,
    TotalRepos INT NOT NULL,
    Stars INT NOT NULL,
    MostUsedLanguage NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE CASCADE
);

-- Criar a tabela LanguageStats
CREATE TABLE LanguageStats (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StatsId INT NOT NULL,
    Language NVARCHAR(100) NOT NULL,
    Count INT NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (StatsId) REFERENCES Stats(Id) ON DELETE CASCADE
);

-- Criar a tabela ReposCreatedPerYear
CREATE TABLE ReposCreatedPerYear (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StatsId INT NOT NULL,
    Year INT NOT NULL,
    Count INT NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (StatsId) REFERENCES Stats(Id) ON DELETE CASCADE
);

-- Criar a tabela TopRepos
CREATE TABLE TopRepos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StatsId INT NOT NULL,
    RepoName NVARCHAR(200) NOT NULL,
    Stars INT NOT NULL,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (StatsId) REFERENCES Stats(Id) ON DELETE CASCADE
);

-- Criar índices para melhor performance
CREATE INDEX IX_User_Login ON [User](Login);
CREATE INDEX IX_Stats_UserId ON Stats(UserId);
CREATE INDEX IX_LanguageStats_StatsId ON LanguageStats(StatsId);
CREATE INDEX IX_ReposCreatedPerYear_StatsId ON ReposCreatedPerYear(StatsId);
CREATE INDEX IX_TopRepos_StatsId ON TopRepos(StatsId);

-- Verificar se as tabelas foram criadas corretamente
SELECT name FROM sys.tables;