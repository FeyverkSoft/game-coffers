﻿using System.Threading;
using System.Threading.Tasks;

namespace Core.Rabbita
{
    public interface IEventHandler { }

    public interface IEventHandler<in T> : IEventHandler where T : IEvent
    {
        public Task Handle(T message, CancellationToken cancellationToken);
    }
}
