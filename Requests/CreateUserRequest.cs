namespace dot_net.Requests {
    public class CreateUserRequest
    {
        public CreateUserRequest ()
        {
        }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Role {get; set;}
    }
}