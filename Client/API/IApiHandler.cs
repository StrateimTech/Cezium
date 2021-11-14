
namespace Main.API
{
    public interface IApiHandler
    {
        public string? HandlePacket(string[] data);
    }
}