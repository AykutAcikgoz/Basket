using System;
using Newtonsoft.Json;

namespace Basket.Service.Payload
{
    public class ResponsePayload
    {
        [JsonProperty("httpStatusCode")]
        public int HttpStatusCode { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        public ResponsePayload(bool isSuccess, int httpStatusCode = 200) {
            this.IsSuccess = isSuccess;
            this.HttpStatusCode = httpStatusCode;
        }
    }
}

