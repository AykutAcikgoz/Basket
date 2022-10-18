using System;
using Newtonsoft.Json;

namespace Basket.Service.Payload
{
    public class SuccessPayload<T> : ResponsePayload
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        public SuccessPayload() : base(true)
        {
        }
    }
}

