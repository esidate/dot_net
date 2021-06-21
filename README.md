# Dot Net School project

### Test Routes

`curl --user test:test http://localhost:5000/users`

```
curl -X POST -H "Content-Type: application/json" \
 -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate
```

### Project arch

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
