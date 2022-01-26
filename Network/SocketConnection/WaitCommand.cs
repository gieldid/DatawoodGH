using System.Net.Sockets;
using System.Threading.Tasks;

namespace DatawoodGH.Network.SocketConnection
{
    public class WaitCommand : CommandObject
    {
        public WaitCommand() {
            Name = "Wait";
        }
        public override async Task SendOverSocket(Socket client)
        {
            await SendOverSocketCommandBase(client, 1500);
        }
    }
}
