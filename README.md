# Dot Net School project

### Project arch

```
.
├── appsettings.Development.json
├── appsettings.Development.json.example
├── appsettings.json
├── bin
│   └── Debug
│       └── net5.0
│           ├── appsettings.Development.json
│           ├── appsettings.json
│           ├── dot_net
│           ├── dot_net.deps.json
│           ├── dot_net.dll
│           ├── dot_net.pdb
│           ├── dot_net.runtimeconfig.dev.json
│           ├── dot_net.runtimeconfig.json
│           ├── Humanizer.dll
│           ├── Microsoft.EntityFrameworkCore.Abstractions.dll
│           ├── Microsoft.EntityFrameworkCore.Design.dll
│           ├── Microsoft.EntityFrameworkCore.dll
│           ├── Microsoft.EntityFrameworkCore.Relational.dll
│           ├── Microsoft.Extensions.DependencyInjection.dll
│           ├── MySqlConnector.dll
│           ├── Pomelo.EntityFrameworkCore.MySql.dll
│           ├── ref
│           │   └── dot_net.dll
│           └── System.Diagnostics.DiagnosticSource.dll
├── Controllers
│   ├── CandidatureController.cs
│   └── UsersController.cs
├── Data
│   └── DataContext.cs
├── dot_net.csproj
├── Entities
│   ├── Candidature.cs
│   ├── Role.cs
│   └── User.cs
├── Helpers
│   ├── BasicAuthenticationHandler.cs
│   └── ExtensionMethods.cs
├── Insomnia_2021-06-21
├── Migrations
│   ├── 20210606122325_InitialCreate.cs
│   ├── 20210606122325_InitialCreate.Designer.cs
│   ├── 20210621102334_addCandidatureTable.cs
│   ├── 20210621102334_addCandidatureTable.Designer.cs
│   └── DataContextModelSnapshot.cs
├── Models
│   ├── AuthenticateModel.cs
│   ├── CandidatrueModel.cs
│   ├── ErrorViewModel.cs
│   └── JustificativeModel.cs
├── obj
│   ├── Debug
│   │   └── net5.0
│   │       ├── apphost
│   │       ├── dot_net.AssemblyInfo.cs
│   │       ├── dot_net.AssemblyInfoInputs.cache
│   │       ├── dot_net.assets.cache
│   │       ├── dot_net.csproj.AssemblyReference.cache
│   │       ├── dot_net.csproj.CopyComplete
│   │       ├── dot_net.csproj.CoreCompileInputs.cache
│   │       ├── dot_net.csproj.FileListAbsolute.txt
│   │       ├── dot_net.dll
│   │       ├── dot_net.GeneratedMSBuildEditorConfig.editorconfig
│   │       ├── dot_net.genruntimeconfig.cache
│   │       ├── dot_net.MvcApplicationPartsAssemblyInfo.cache
│   │       ├── dot_net.pdb
│   │       ├── dot_net.RazorTargetAssemblyInfo.cache
│   │       ├── project.razor.json
│   │       ├── ref
│   │       │   └── dot_net.dll
│   │       └── staticwebassets
│   │           ├── dot_net.StaticWebAssets.Manifest.cache
│   │           └── dot_net.StaticWebAssets.xml
│   ├── dot_net.csproj.EntityFrameworkCore.targets
│   ├── dot_net.csproj.nuget.dgspec.json
│   ├── dot_net.csproj.nuget.g.props
│   ├── dot_net.csproj.nuget.g.targets
│   ├── project.assets.json
│   └── project.nuget.cache
├── Program.cs
├── Properties
│   └── launchSettings.json
├── README.md
├── Requests
│   └── CreateUserRequest.cs
├── Services
│   ├── CandidatureService.cs
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

Authentication

```
curl -X POST -H "Content-Type: application/json" \
 -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate
```

Get all users

```
curl -X GET http://localhost:5000/users
```

Get all evaluators

```
curl -X GET http://localhost:5000/users/evaluators
```

Get evaluator by ID

```
curl -X GET http://localhost:5000/users/1
```

Add evaluator

```
curl -X POST -H "Content-Type: application/json" \
 -d '{ "Username" : "bruh", "FirstName" : "ayoo", "LastName" : "bruh", "test": "hey" }' http://localhost:5000/users/add
```
