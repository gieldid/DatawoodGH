using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
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
            int openW = line.LastIndexOf('W');
            int close = line.IndexOf(';');
            var waitName = line.Substring(openW, close - openW);
            WaitValue = waitTimes[waitName];
        }

        public override async Task SendOverSocket(Socket client)
        {
            //String to float
            var waitValueFloat = float.Parse(WaitValue);
            //Waitvalue to int
            int waitTimeInt = ConvertSecToMs(waitValueFloat) + WaitTime;  
            await SendOverSocketCommandBase(client);
            byte[] payload = Encoding.UTF8.GetBytes(WaitValue);
            client.Send(payload);
            await Task.Delay(waitTimeInt);
        }


        private int ConvertSecToMs(float waitValue) {
            int waitTimeMS = (int)(waitValue * 1000);
            return waitTimeMS;
        }
    }
}
