using Actio.Common.Commands;
using Actio.Common.Events;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;

        public CreateActivityHandler(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity: {command.Name}");
            await _bus.PublishAsync(new ActivityCreated(
                command.Id,
                command.UserId,
                command.Category,
                command.Name,
                command.Description,
                command.CreatedAt));
        }
    }
}
