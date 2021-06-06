Run server with `dotnet run`.

Test Routes:  
`curl --user test:test http://localhost:5000/users`
`curl -X POST -H "Content-Type: application/json" -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate`

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
