using Actio.Common.Auth;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncriptor _encriptor;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IEncriptor encriptor, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encriptor = encriptor;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ActioException("invalid_credentials", "Invalid credentials");
            }

            if (!user.ValidatePassword(password, _encriptor))
            {
                throw new ActioException("invalid_credentials", "Invalid credentials");
            }

            return _jwtHandler.Create(user.Id);
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ActioException("email_in_use", $"Given email '{email}' is in use");
            }
            user = new User(email, name);
            user.SetPassword(password, _encriptor);
            await _userRepository.AddAsync(user);
        }
    }
}
