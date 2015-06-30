﻿using System;

using BestHTTP.SignalR;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Hubs
{
    /// <summary>
    /// Interface to be able to hide internally used functions and properties.
    /// </summary>
    public interface IHub
    {
        Connection Connection { get; set; }

        void Call(ClientMessage msg);
        bool HasSentMessageId(UInt64 id);
        void Close();
        void OnMethod(MethodCallMessage msg);
        void OnMessage(IServerMessage msg);
    }
}
