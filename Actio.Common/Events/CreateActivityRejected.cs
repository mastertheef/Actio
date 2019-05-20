using System;

namespace Actio.Common.Events
{
    public class CreateActivityRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }
        public Guid Id { get; }

        protected CreateActivityRejected() { }

        public CreateActivityRejected(
            Guid id,
            string code,
            string reason)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}
