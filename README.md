
# Stock Chat App

Is a simple browser-based chat application that allows several users to talk in a chatroom and get stock quotes
from an API using a specific command.

It's composed of two main projects:
- Razor pages (.NET Core 6), for the chat application
- .NET bot, with it testing project. To communicate with the API for the stock values

The bot is decoupled from the chat application thanks to RabbitMQ message broker.



## Features

- User registration and login, using ASP NET core identity
- Chat room, powered by SignalR
- Ask for stock quote using the command /stock=Stock_code


## Project Overview
This image shows the structure and interaction between the components of the stock chat application.

![Stock Chat App (1)](https://user-images.githubusercontent.com/7692606/160728749-b3fe3224-fb49-4ab6-a790-b5d9af971ea6.jpg)


## Installation

1. Clone the repo or download it
2. Make sure you have the dependencies installed
- Chat App project
  - Microsoft.AspNetCore.Identity.entityFrameworkCore
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
  - RabbitMQ.Client
- StockBot project
  - RabbitMQ.Client
3. Run the migrations to create the database to store the users.
**Note:** Make sure the appsettings.json has the correct connection string before running the migrations.

4. Install RabbitMQ server (to complete this part, every installation should be done with administrative account)
**Note:** the project was built to work with RabbitMQ on localhost.
* Download and install [erlang](https://www.erlang.org/downloads)

* Download de server [installer for windows](https://www.rabbitmq.com/install-windows.html)
* After installing the RabbitMQ service will start (double check is recommended)

For more details go to [RabbitMQ](https://www.rabbitmq.com/install-windows.html) official web site

5. Run the bot project
6. Run the Chat App project


## Knowledgebase

 - [Razor pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio)
 - [.NET Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio)
 - [Signal R](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-6.0&tabs=visual-studio)
 - [.NET Bot Framework](https://docs.microsoft.com/es-es/azure/bot-service/bot-service-quickstart-create-bot?view=azure-bot-service-4.0&tabs=csharp%2Cvs)
 - [RabbitMQ](https://www.rabbitmq.com/)
 - [Xunit](https://xunit.net/)


    

