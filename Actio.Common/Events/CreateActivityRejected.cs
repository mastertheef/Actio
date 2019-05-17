using System;

namespace Actio.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
        public Guid UserId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; }

        protected CreateActivityRejected() { }

        public CreateActivityRejected(
            Guid id,
            Guid userId,
            string name,
            string descrition,
            DateTime createdAt,
            string reason,
            string code)
        {
            Id = id;
            UserId = userId;
            Name = name;
            CreatedAt = createdAt;
            Reason = reason;
            Code = code;
        }
    }
}
