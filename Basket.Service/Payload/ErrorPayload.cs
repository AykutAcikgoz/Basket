using System;
using Newtonsoft.Json;

namespace Basket.Service.Payload
{
    public class ErrorPayload : ResponsePayload
    {
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
        [JsonProperty("exception")]
        public Exception Exception { get; set; }

        public ErrorPayload() : base(false) { }
    }
}

