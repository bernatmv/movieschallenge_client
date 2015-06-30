using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestHTTP
{
    /// <summary>
    /// Possible logical states of a HTTTPRequest object.
    /// </summary>
    public enum HTTPRequestStates
    {
        /// <summary>
        /// Initial status of a request. No callback will be called with this status.
        /// </summary>
        Initial,

        /// <summary>
        /// Waiting in a queue to be processed. No callback will be called with this status.
        /// </summary>
        Queued,

        /// <summary>
        /// Processing of the request started. In this state the client will send the request, and parse the response. No callback will be called with this status.
        /// </summary>
        Processing,

        /// <summary>
        /// The request finished without problem. Parsing the response done, the result can be used. The user defined callback will be called with a valid response object. The request’s Exception property will be null.
        /// </summary>
        Finished,

        /// <summary>
        /// The request finished with an unexpected error. The user defined callback will be called with a null response object. The request's Exception property may contain more info about the error, but it can be null.
        /// </summary>
        Error,

        /// <summary>
        /// The request aborted by the client(HTTPRequest’s Abort() function). The user defined callback will be called with a null response. The request’s Exception property will be null.
        /// </summary>
        Aborted,

        /// <summary>
        /// Connecting to the server timed out. The user defined callback will be called with a null response. The request’s Exception property will be null.
        /// </summary>
        ConnectionTimedOut,

        /// <summary>
        /// The request didn't finished in the given time. The user defined callback will be called with a null response. The request’s Exception property will be null.
        /// </summary>
        TimedOut
    }
}