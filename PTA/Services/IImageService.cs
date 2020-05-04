using PTA.Models;
using System.Threading.Tasks;

namespace PTA.Services
{
    public interface IImageService
    {
        Task<Image> GetRandomImageAsync();
        Task SaveImageAsync(string filename, string text);
        Task SaveImagesAsync(string filename);
    }
}