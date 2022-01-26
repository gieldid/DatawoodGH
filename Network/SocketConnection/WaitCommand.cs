using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DatawoodGH.Network.SocketConnection
{
    public class WaitCommand : CommandObject
    {

        public string WaitValue { get; set; }

        public WaitCommand(string line, Dictionary<string,string> waitTimes) {
            Name = "Wait";
            ReadWaitValue(line, waitTimes);
        }

        private void ReadWaitValue(string line, Dictionary<string,string> waitTimes) {
            ///TODO: read the wait name and get the value from the dictionary.
            ///WaitTime Wait000; is an example how it looks in rapid, we want Wait000 to be replaced with dictionary value.

        }

        public override async Task SendOverSocket(Socket client)
        {
            await SendOverSocketCommandBase(client, 1500);
        }
    }
}
