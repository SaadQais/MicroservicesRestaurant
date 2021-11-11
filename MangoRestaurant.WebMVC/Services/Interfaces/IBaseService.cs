using MangoRestaurant.WebMVC.Models;

namespace MangoRestaurant.WebMVC.Services.Interfaces
{
    public interface IBaseService : IDisposable
    {
        ResponseDto ResponseModel { get; set; }

        Task<T> SendAsync<T>(ApiRequest request);
    }
}
