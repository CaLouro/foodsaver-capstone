using Newtonsoft.Json;

namespace FoodSaverWebApp.Models
{
    public class LoginError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }

    public class RegisterError
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
