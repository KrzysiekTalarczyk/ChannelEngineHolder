using System.Collections.Generic;

namespace ChannelEngineHolder.RestApiClient.Models
{
   public class ChannelEngineContent<T>
    {
        public IEnumerable<T> Content { get; set; }
    }
}
