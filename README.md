# Dot Net School project

### Project arch

```
.
├── appsettings.Development.json.example
├── appsettings.json
├── bin
│   └── Debug
│       └── net5.0
│           └── ref
├── Controllers
│   ├── CandidatureController.cs
│   └── UsersController.cs
├── Data
│   └── DataContext.cs
├── dot_net.csproj
├── Entities
│   ├── Role.cs
│   └── User.cs
├── Helpers
│   ├── BasicAuthenticationHandler.cs
│   └── ExtensionMethods.cs
├── Migrations
│   ├── 20210606122325_InitialCreate.cs
│   ├── 20210606122325_InitialCreate.Designer.cs
│   └── DataContextModelSnapshot.cs
├── Models
│   ├── AuthenticateModel.cs
│   ├── CandidatrueModel.cs
│   ├── ErrorViewModel.cs
│   └── JustificativeModel.cs
├── obj
│   └── Debug
│       └── net5.0
│           ├── dot_net.AssemblyInfo.cs
│           ├── dot_net.AssemblyInfoInputs.cache
│           ├── dot_net.csproj.AssemblyReference.cache
│           ├── dot_net.GeneratedMSBuildEditorConfig.editorconfig
│           ├── project.razor.json
│           └── ref
├── Program.cs
├── Properties
│   └── launchSettings.json
├── README.md
├── Requests
│   └── CreateUserRequest.cs
├── Services
│   └── UserService.cs
├── Startup.cs
└── wwwroot
    └── favicon.ico
```

### Database configuration

Run the following commands in MySQL to create a DB, a user and grant permissions:

```sql
CREATE DATABASE dot_net;
CREATE USER 'dot_net'@'localhost' IDENTIFIED BY 'Dot_net1234.';
GRANT ALL PRIVILEGES ON dot_net . * TO 'dot_net'@'localhost';
```

Copy `appsettings.Development.json.example` and name the copy `appsettings.Development.json`.  
Fill `appsettings.Development.json` with the appropriate env vars.  
Don't touch `appsettings.json`...  
Run `dotnet tool install dotnet-ef` to install EF.  
Run migrations with `dotnet ef database update`.  
Run server with `dotnet run --environment "Development"`.

### Test Routes

`curl --user test:test http://localhost:5000/users`

```
curl -X POST -H "Content-Type: application/json" \
 -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate
```
