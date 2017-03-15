using MVC_Core;

namespace DoMainService
{
    public interface IBaiduMapService : IDependency
    {
        string GetAddressInfo(string xpoint, string ypoint);
    }
}
