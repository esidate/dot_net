Run server with `dotnet run`.

Test Routes:  
`curl --user test:test http://localhost:5000/users`

```
curl -X POST -H "Content-Type: application/json" \
 -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate
```

Project arch:

```
├── Controllers
│   └── UsersController.cs
├── Entities
│   └── User.cs
├── Helpers
│   └── BasicAuthenticationHandler.cs
├── Models
│   ├── AuthenticateModel.cs
│   └── ErrorViewModel.cs
├── Properties
│   └── launchSettings.json
├── Services
│   └── UserService.cs
├── Views
├── wwwroot
│   └── favicon.ico
├── appsettings.Development.json
├── appsettings.json
├── dot_net.csproj
├── Program.cs
├── README.md
└── Startup.cs
```

Database Configuration:

-Create a MySql database

-Add database informations to `appsetting.json` exampl in `appsetting.copy.json`

-in terminal, type `dotnet ef` to check if EF Core Command Line Tools is installed, if it's not, install it with the folllowing command :  
`dotnet tool install --global dotnet-ef`, replace `global` with `local` if you wish to install it locally.

-Make migrations by executing the command `dotnet ef database update`.
