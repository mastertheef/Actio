using System;

namespace Actio.Common.Commands
{
    public class CreateUser : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
