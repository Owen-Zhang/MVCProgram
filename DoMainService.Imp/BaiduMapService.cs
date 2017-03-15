using DataService;

namespace DoMainService.Imp
{
    public class BaiduMapService : IBaiduMapService
    {
        private IMapDataService mapDataService;

        public BaiduMapService(IMapDataService mapDataService)
        {
            this.mapDataService = mapDataService;
        }

        public string GetAddressInfo(string xpoint, string ypoint)
        {
            return mapDataService.GetAddressInfo(xpoint, ypoint);
        }
    }
}