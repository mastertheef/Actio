using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Name { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public User(string email, string name)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ActioException("empty_email", "User email cannot be empty");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ActioException("empty_name", "User name cannot be empty");
            }

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        protected User()
        {
        }

        public void SetPassword(string password, IEncriptor encriptor)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ActioException("empty_password", "Password cannot be empty");
            }

            Salt = encriptor.GetSalt(password);
            Password = encriptor.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncriptor encriptor)
        {
            return Password.Equals(encriptor.GetHash(password, Salt));
        }
    }
}
