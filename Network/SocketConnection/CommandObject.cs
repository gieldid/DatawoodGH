using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DatawoodGH.Network.SocketConnection
{
    public abstract class CommandObject
    {
        public string Name { get; set; }

        public abstract void SendOverSocket(Socket client);

        protected void SendOverSocketCommandBase(Socket client) {
            byte[] payload = Encoding.UTF8.GetBytes(Name);
            client.Send(payload);
            System.Threading.Thread.Sleep(500);
        }
    }
}
