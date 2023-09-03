using Newtonsoft.Json;

namespace eStore.IdentityService.API.Models
{
    public class BaseResponseModel<TResult>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("exception")]
        public Exception Exception { get; set; }
        [JsonProperty("result")]
        public TResult Result { get; set; }
    }
}
