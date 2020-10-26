# SistemaAgendamentos

Sistema desenvolvido para controlar o fluxo de mercadorias.  

=======Back-end=======
Web API desenvolvida em .NET Core 3.1  
EntityFrameworkCore para mapeamento objeto - banco de dados  
A conexão com o banco de dados está no arquivo appsettings.json e foi utilizado o banco SQL Server  
Para criação do banco de dados e tabelas, executar os comandos dentro de ../back-end:  
  dotnet ef migrations add InitialCreate (caso der erro, utilizar outro nome ao invés de "InitialCreate")  
  dotnet ef database update  
