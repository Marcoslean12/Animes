# CineAnimeWeb

Aplicacao web academica em ASP.NET Core MVC para cadastro e consulta de filmes e animes. O sistema usa Entity Framework Core com SQL Server, persistencia real em banco de dados, CRUDs funcionais e consultas LINQ exibidas no navegador.

## Tecnologias utilizadas

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB ou SQL Server Express
- Razor Views
- LINQ
- Bootstrap
- Migrations do Entity Framework Core

## Estrutura das entidades

- `Obra`: representa um filme ou anime, com titulo, tipo, ano, duracao, sinopse, diretor/estudio, generos e avaliacoes.
- `DiretorEstudio`: representa o diretor de um filme ou o estudio de um anime.
- `Genero`: representa categorias como Acao, Drama, Fantasia, Shounen e Suspense.
- `ObraGenero`: classe associativa do relacionamento N:N entre `Obra` e `Genero`.
- `Avaliacao`: representa uma avaliacao feita por um usuario para uma obra.

## Relacionamentos

- Relacionamento 1:N: um `DiretorEstudio` possui varias `Obras`, e cada `Obra` pertence a um `DiretorEstudio`.
- Relacionamento 1:N: uma `Obra` possui varias `Avaliacoes`.
- Relacionamento N:N: uma `Obra` possui varios `Generos`, e um `Genero` pode estar em varias `Obras`, usando a classe associativa `ObraGenero`.

## Connection string

A connection string fica em `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CineAnimeWebDb;Trusted_Connection=True;MultipleActiveResultSets=true;Connect Timeout=5"
  }
}
```

Para usar SQL Server Express, altere o servidor. Exemplo:

```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=CineAnimeWebDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

## Como rodar o projeto

Na pasta do projeto, execute:

```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Observacao: a migration inicial `InitialCreate` ja foi incluida na pasta `Migrations`. Se nao quiser gerar outra migration, use apenas:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Ao iniciar, a aplicacao tambem chama `Database.Migrate()` e executa o seed inicial se o banco ainda estiver vazio.

Se o LocalDB apresentar erro ao iniciar, verifique a instalacao do SQL Server LocalDB ou use SQL Server Express e atualize a connection string para `.\\SQLEXPRESS`.

## Dados iniciais

O seed cria exemplos de:

- Obras: O Senhor dos Aneis, Interestelar, Naruto, Attack on Titan, Death Note e Jujutsu Kaisen.
- Generos: Acao, Aventura, Drama, Ficcao Cientifica, Fantasia, Shounen e Suspense.
- Diretores/Estudios: Christopher Nolan, Peter Jackson, Studio Pierrot, MAPPA e Madhouse.
- Avaliacoes iniciais para demonstrar medias e consultas.

## Consultas LINQ obrigatorias

As consultas ficam em `ConsultasController` e aparecem no navegador em `/Consultas`.

1. Obras com diretor/estudio: lista dados de `Obra` junto com `DiretorEstudio`, usando relacionamento entre duas classes.
2. Agrupamento por tipo: agrupa obras por `Filme` e `Anime`, usando `GroupBy`, `Count` e `Average`.
3. Where principal e Having: filtra obras com `AnoLancamento >= 2010`, agrupa por diretor/estudio e mostra apenas grupos com mais de uma obra usando `Where` apos o `GroupBy`.

## Checklist atendido

- Aplicacao ASP.NET Core MVC com Razor Views.
- Entity Framework Core configurado com SQL Server.
- Persistencia real em banco de dados.
- Migration inicial criada.
- Models relacionados com 1:N e N:N.
- Classe associativa `ObraGenero`.
- CRUDs para obras, diretores/estudios, generos e avaliacoes.
- Relacionamento de uma obra com um ou mais generos.
- Visualizacao das avaliacoes na pagina de detalhes da obra.
- Pagina `/Consultas` com as tres consultas LINQ obrigatorias.
- Seed de dados iniciais.
