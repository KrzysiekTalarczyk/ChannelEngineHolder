using System;

namespace ChannelEngineHolder.RestApiClient.Exceptions
{
    public class RequestFailException : Exception
    {
        public RequestFailException(string url) : base($"Request failed url {url}")
        {

        }

        public RequestFailException(string url, string message) : base($"Request failed url {url}, Exception Message {message}")
        {

        }
    }
}
