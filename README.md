<p align="center">
  <img src="http://img.shields.io/badge/Licença-MIT-green"/>
  <img src="https://img.shields.io/badge/Linguagem-CSharp-purple"/>
  <img src="http://img.shields.io/badge/.NET-8-blue"/>
   <img src="http://img.shields.io/badge/Status-Em Desenvolvimento-green "/>
</p>

<h1 align="center">Manager API REST</h1>
<p align="center"><i>Uma API de gerenciamento de usuarios, criada com boas práticas de desenvolvimento e arquitetura!.</i></p>

Para finalizar o nosso README podemos adicionar estatísticas sobre o repositório como Linguagem mais utilizada, Número de linguagens presentes, qualidade do código e muitas outras através da ferramenta oferecida pela Codacy. Não abordaremos aqui como cadastrar seu repositório e ter acesso a estas estatísticas já que no site deles já tem uma documentação completa sobre isto. Veja como fica:


##  Sobre o Projeto
This is a repository used as a base to show the operation of Issues, Discussions, Wiki and other GitHub resources in addition to code versioning..

The project inserted into this repository is a pre-existing template and is used as a basis for displaying statistical data about it.

### Tecnologias
<p display="inline-block">
  <img width="48" src="https://www.freeiconspng.com/uploads/c-logo-icon-18.png" alt="csharp-logo"/>
  <img width="48" src="https://github.com/igormorantos/Manager-API-REST/assets/94862012/155c0471-b80a-4b18-abe2-ecc614e3e81a" alt="Sql-Server-logo"/>
</p>

## Rodando o projeto
- Rodar o `dotnet run` dentro do projeto `Manager.API`
- Acessar a Api através do link `https://localhost:5001/swagger/index.html`
- Lembrar de colocar as suas proprias credenciais de banco de dados dentro do `ManagerContext.cs` e em `appsetting.json`

## Funcionalidades

:heavy_check_mark: Login 

:heavy_check_mark: Criação de Usuario 

:heavy_check_mark: Busca de usuarios por email, nome e Id

:heavy_check_mark: Busca de usuarios por trechos de email e nome


![2](https://github.com/igormorantos/Manager-API-REST/assets/94862012/612b9e32-c271-4d6d-9199-cd19346b55ae)


## Como rodar os testes

No projeto `Manager.Tests`
```
dotnet test
```

## Casos de Uso

Você consegue o token de autenticação, com o login `javaeruim` e senhas `phptambem` 

### Auth: 

|login|password
| -------- |--------
|javaruim|phptambem 



