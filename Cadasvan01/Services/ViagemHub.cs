using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Cadasvan01.Services
{
    public class ViagemHub : Hub
    {
        public async Task EnviarNotificacao(string message)
        {
            await Clients.All.SendAsync("ReceberNotificacao", message);
        }
    }
}
