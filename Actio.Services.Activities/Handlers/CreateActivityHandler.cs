using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
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
        private readonly IActivityService _activityService;

        public CreateActivityHandler(IBusClient bus, IActivityService activityService)
        {
            _bus = bus;
            _activityService = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            Console.WriteLine($"Creating activity: {command.Name}");
            try
            {
                await _activityService.AddAsync(
                    command.Id,
                    command.UserId,
                    command.Category,
                    command.Name,
                    command.Description,
                    command.CreatedAt);

                await _bus.PublishAsync(new ActivityCreated(
                    command.Id,
                    command.UserId,
                    command.Category,
                    command.Name,
                    command.Description,
                    command.CreatedAt));
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(
                    command.Id,
                    ex.Code,
                    ex.Message));
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(
                    command.Id,
                    "error",
                    ex.Message));
            }
           
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
