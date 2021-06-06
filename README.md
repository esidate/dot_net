Run server with `dotnet run`.

Test Routes:  
`curl --user test:test http://localhost:5000/users`
`curl -X POST -H "Content-Type: application/json" -d '{"username" :"test", "password" : "test"}' http://localhost:5000/users/authenticate`
