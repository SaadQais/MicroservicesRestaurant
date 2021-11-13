using MangoRestaurant.WebMVC.Models;
using MangoRestaurant.WebMVC.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static MangoRestaurant.WebMVC.SD;

namespace MangoRestaurant.WebMVC.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClient;

        public ResponseDto ResponseModel { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            ResponseModel = new ResponseDto();
        }

        public async Task<T> SendAsync<T>(ApiRequest request)
        {
            try
            {
                var client = _httpClient.CreateClient("MangoAPI");
                var message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);

                client.DefaultRequestHeaders.Clear();

                if (request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                        Encoding.UTF8, "application/json");
                }

                if(!string.IsNullOrEmpty(request.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);
                }

                message.Method = request.Method switch
                {
                    ApiMethod.POST => HttpMethod.Post,
                    ApiMethod.PUT => HttpMethod.Put,
                    ApiMethod.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                var response = await client.SendAsync(message);
                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<T>(content);

                return responseDto;
            }
            catch (Exception ex)
            {
                var responseDto = new ResponseDto()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };

                var response = JsonConvert.SerializeObject(responseDto);

                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
