using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Cadasvan01.Hubs
{
    public class LocationHub : Hub
    {
        public async Task SendLocation(string motoristaId, double latitude, double longitude)
        {
            await Clients.All.SendAsync("ReceiveLocation", motoristaId, latitude, longitude);
        }
    }
}
