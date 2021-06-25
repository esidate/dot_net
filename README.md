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
Add environment variables `export ADMIN_USERNAME=admin ADMIN_FIRSTNAME=fname ADMIN_LASTNAME=lname ADMIN_PASSWORD=admin`, and hit `/users/seed` to seed admin in database.

### Test Routes

Authentication

```
curl -X POST -H "Content-Type: application/json" \
 -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate
```

Get all users

```
curl -X GET -u admin:admin http://localhost:5000/users
```

Get all evaluators

```
curl -X GET -u admin:admin http://localhost:5000/users/evaluators
```

Get evaluator by ID

```
curl -X GET -u admin:admin http://localhost:5000/users/1
```

Add evaluator

```
curl -X POST -u admin:admin -H "Content-Type: application/json" \
 -d '{ "Username" : "bruh", "FirstName" : "ayoo", "LastName" : "bruh", "test": "hey" }' http://localhost:5000/users/add
```

Modify account

```
curl --request POST \
  --url http://localhost:5000/users/modify  --header 'Content-Type: application/json' \
  --data '{"username" : "currentUsername", "newUsername" : "newUsername", "newPassword" : "newPassword"
}'
```

Toggle evaluator Block

```
curl -X GET http://localhost:5000/users/evaluators/block/{id}
```

Add candidature

```
curl -X POST -H "Content-Type: application/json" \
 -d '{ "candidature" : "{\"test\":\"test\",\"test\":\"test\"}" }' http://localhost:5000/candidature/new
```

Get candidature by id

```
curl http://localhost:5000/candidature/{id}
```

Get candidature by token

```
curl http://localhost:5000/candidature/submitted?token=TOKEN
```

Get treated candidatures

```
curl http://localhost:5000/candidature/treated
```

Get untreated candidatures

```
curl http://localhost:5000/candidature/treated
```

Update candidature

```
curl -X POST -H "Content-Type: application/json" \
 -d '{ "id" : "1" , "note" : "28.5" }' http://localhost:5000/candidature/update
```

Upload justificative

```
curl -X POST http://localhost:5000/candidature/justificative \
 -H 'Content-Type: multipart/form-data; boundary=---011000010111000001101001' --form justificative=@/PATH_TO_FILE
```

Delete justificative

```
curl -X DELETE http://localhost:5000/candidature/justificative \
    -H 'Content-Type: application/json' -d '{"fileName": "FILE_NAME"}'
```

### Deployment

> Note : this is not complete

Export db (temporary solution for a problem), `sudo mysqldump -u root -p dot_net > mysql-dump/db-dump.sql`
