using System.Threading.Tasks;

namespace PTA.Services
{
    public interface IGameService
    {
        public Task PositionOfLetter();
        public Task GetImageAsync();
        public Task VerifyPosition(Position position);
        public Task FillInLetter(Type type);
        public Task VerifyInput(string text);
    }
}