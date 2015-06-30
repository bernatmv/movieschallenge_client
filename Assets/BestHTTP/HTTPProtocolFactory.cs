using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BestHTTP
{
    public enum SupportedProtocols
    {
        Unknown,
        HTTP,
        WebSocket,
        ServerSentEvents
    }

    internal static class HTTPProtocolFactory
    {
        public static HTTPResponse Get(SupportedProtocols protocol, HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
        {
            switch (protocol)
            {
                case SupportedProtocols.WebSocket: return new WebSocket.WebSocketResponse(request, stream, isStreamed, isFromCache);
                case SupportedProtocols.ServerSentEvents: return new ServerSentEvents.EventSourceResponse(request, stream, isStreamed, isFromCache);
                default: return new HTTPResponse(request, stream, isStreamed, isFromCache);
            }
        }

        public static SupportedProtocols GetProtocolFromUri(Uri uri)
        {
            string scheme = uri.Scheme.ToLowerInvariant();
            switch (scheme)
            {
                case "ws":
                case "wss":
                    return SupportedProtocols.WebSocket;
                default:
                    return SupportedProtocols.HTTP;
            }
        }

        public static bool IsSecureProtocol(Uri uri)
        {
            string scheme = uri.Scheme.ToLowerInvariant();
            switch (scheme)
            {
                // http
                case "https":
                // WebSocket
                case "wss":
                    return true;
            }

            return false;
        }
    }
}