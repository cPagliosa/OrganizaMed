# Organiza Med

[![Stack](https://skillicons.dev/icons?i=dotnet,cs,postman,nodejs,typescript,angular&perline=8)](https://skillicons.dev)

## Projeto

Desenvolvido durante o curso Full-Stack da [Academia do Programador](https://www.academiadoprogramador.net) 2024

---
## Descrição

Sistema de Cadastro de Medicos e Atividades desenvolvido utilizando ASP.NET WebAPI e Angular.

---
## Funcionalidades

1. O cadastro do **Medico** consiste de:
	- nome
	- crm
	- email
	- telefone

2. O cadastro do **Ativide** consiste de:
	- título
	- incio
	- termino
	- tipo  
	- medicos

---
## Link do site hospedado na azure

[Link para o Organiza Med: ](https://polite-pebble-079d1eb0f.4.azurestaticapps.net)

---
## Requisitos para Execução do Projeto Completo

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto back-end.
- Node.js v20+
- Angular v18 

---
## Executando o Back-End 

Vá para a pasta do projeto da WebAPI:

```bash
cd server/NoteKeeper.WebApi
```

Execute o projeto:

```bash
dotnet run
```

A API poderá ser acessada no endereço `https://localhost:5000/api`.

A documentação **OpenAPI** também estará disponível em: `https://localhost:5000/swagger`.

---
## Executando o Front-End 

Vá para a pasta do projeto Angular:

```bash
cd client
```

Instale as dependências:

```bash
npm install
```

Execute o projeto:

```bash
npm start
```

A aplicação Angular estárá disponível no endereço `http://localhost:4200`.
