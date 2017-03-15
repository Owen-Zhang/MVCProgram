using MVC_Core;

namespace DataService
{
    public interface IMapDataService : IDependency
    {
        string GetAddressInfo(string xpoint, string ypoint);
    }
}
