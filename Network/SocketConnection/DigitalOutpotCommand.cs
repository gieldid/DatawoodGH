using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
            var dioNum = splitLine[2].Trim(new char[]{';'});
            DioNum = dioNum;
        }
        public override async Task SendOverSocket(Socket client)
        {
            await SendOverSocketCommandBase(client);
            byte[] payload = Encoding.UTF8.GetBytes(ValveName);
            client.Send(payload);
            Task.Delay(500).Wait();

            payload = Encoding.UTF8.GetBytes(DioNum);
            client.Send(payload);
            Task.Delay(500).Wait();
        }
    }
}
