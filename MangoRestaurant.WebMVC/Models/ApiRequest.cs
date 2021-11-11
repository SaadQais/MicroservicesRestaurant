using static MangoRestaurant.WebMVC.SD;

namespace MangoRestaurant.WebMVC.Models
{
    public class ApiRequest
    {
        public ApiMethod Method { get; set; } = ApiMethod.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
