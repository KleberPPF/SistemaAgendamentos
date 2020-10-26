# SistemaAgendamentos

Sistema desenvolvido para controlar o fluxo de mercadorias.  

=======Back-end=======  
Web API desenvolvida em .NET Core 3.1  
EntityFrameworkCore para mapeamento objeto - banco de dados  
A conexão com o banco de dados está no arquivo appsettings.json e foi utilizado o banco SQL Server  
Para criação do banco de dados e tabelas, executar os comandos dentro de ../back-end:  
  dotnet ef migrations add InitialCreate (caso der erro, utilizar outro nome ao invés de "InitialCreate")  
  dotnet ef database update  
  
    
=======Front-end=======  
Desenvolvido em React  
Para instalar as dependências, executar em ../front-end:  
npm install  
Para rodar a aplicação, executar dentro da mesma pasta:  
npm start  
  
  
  
  <img align="left" width="225" height="500" src="https://github.com/KleberPPF/SistemaAgendamentos/front-end/public/print0.PNG">
  <img align="left" width="225" height="500" src="https://github.com/KleberPPF/SistemaAgendamentos/front-end/public/print.PNG">
