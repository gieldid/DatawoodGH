using System.Net.Sockets;

namespace DatawoodGH.Network.SocketConnection
{
    public class DigitalOutpotCommand : CommandObject {
        public string ValveName { get; set; }
        public string DioNum { get; set; }
        public DigitalOutpotCommand(string line) {
            Name = "DO";
            ReadValveName(line);
            ReadDioNum(line);
        }

        private void ReadValveName(string line) {
            var splitLine = line.Split(',');
            ValveName = splitLine[1];
        }

        private void ReadDioNum(string line) {
            var splitLine = line.Split(',');
            DioNum = splitLine[2];
        }
        public override void SendOverSocket(Socket client)
        {
            SendOverSocketCommandBase(client);
        }
    }
}
