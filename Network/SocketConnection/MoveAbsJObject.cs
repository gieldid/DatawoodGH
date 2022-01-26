using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DatawoodGH.Utility;

namespace DatawoodGH.Network.SocketConnection
{
    public class MoveAbsJObject : MoveObject
    {
        public string RobJoint { get; set; }
        public MoveAbsJObject(string line, Dictionary<string, string> speeds, Dictionary<string, string> zones) : base(line, speeds, zones)
        {
            Name = "MoveAbsJ";
            ReadRobJoint(line);
        }

        private void ReadRobJoint(string line) {
            int openBracket = Utils.GetNthIndex(line, '[', 2);
            int closeBracket = line.IndexOf(']') + 1;

            RobJoint = line.Substring(openBracket, closeBracket - openBracket);
        }

        public override async Task SendOverSocket(Socket client){
            await SendOverSocketMoveBase(client);
            byte[] payload = Encoding.UTF8.GetBytes(RobJoint);
            client.Send(payload);
            await Task.Delay(WaitTime);
        }
        
    }
}
