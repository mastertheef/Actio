using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Identity.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _bus;
        private readonly IUserService _userService;

        public CreateUserHandler(IBusClient bus, IUserService userService)
        {
            _bus = bus;
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
                await _bus.PublishAsync(new UserCreated(command.Email, command.Name));
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(
                     ex.Code,
                     ex.Message,
                     command.Email));
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(
                     "error",
                     ex.Message,
                     command.Email));
            }
        }
    }
}
