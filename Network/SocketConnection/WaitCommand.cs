using System.Net.Sockets;

namespace DatawoodGH.Network.SocketConnection
{
    public class WaitCommand : CommandObject
    {
        public WaitCommand() {
            Name = "Wait";
        }
        public override void SendOverSocket(Socket client)
        {
            SendOverSocketCommandBase(client, 1500);
        }
    }
}
