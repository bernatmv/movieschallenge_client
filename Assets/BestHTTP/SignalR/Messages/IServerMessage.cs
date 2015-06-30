using System;
using System.Collections.Generic;

namespace BestHTTP.SignalR.Messages
{
    public interface IServerMessage
    {
        MessageTypes Type { get; }
        void Parse(object data);
    }

    public interface IHubMessage
    {
        UInt64 InvocationId { get; }
    }
}
