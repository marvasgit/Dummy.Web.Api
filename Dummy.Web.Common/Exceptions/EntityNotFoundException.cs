namespace Dummy.Web.Common.Exceptions
{
    using System;

    public class EntityNotFoundException<TPublicIdentity> : Exception
    {
        public TPublicIdentity Id { get; private set; }

        public EntityNotFoundException(TPublicIdentity id) : this()
        {
            Id = id;
        }

        public EntityNotFoundException() { }
    }

    public abstract class EntityNotFoundException : EntityNotFoundException<int>
    {
        public EntityNotFoundException(int id) : base(id) { }

        public EntityNotFoundException() : base() { }

    }
}