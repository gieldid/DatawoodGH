using DatawoodGH.Properties;
using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DatawoodGH.Network.SocketConnection
{
    public class SocketClient : TaskCapableNetworkComponent<SolveResults>
    {
        //Timeout for connecting to socket server in ms
        public const int TimeOut = 5000;
        /// <summary>
        /// Initializes a new instance of the WebSocketComponent class.
        /// </summary>
        public SocketClient()
          : base("Socket Client", "Socket",
              "Makes a socket client and connects to given server.")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Ip", "IP", "IP to make a web socket connection to", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Port","P","Port for the socket connection",GH_ParamAccess.item);
            pManager.AddTextParameter("Mod file", "m", "Mod file to read", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Run", "R", "When to run", GH_ParamAccess.item, true);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Finished","f","finished",GH_ParamAccess.item);
        }
       

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            if (InPreSolve)
            {
                string ip = null;
                int port = 0;
                bool run = true;
                string path = null;

                if (!DA.GetData("Ip", ref ip))
                {
                    return;
                }

                if (!DA.GetData("Port", ref port))
                {
                    return;
                }

                if (!DA.GetData("Mod file", ref path))
                {
                    return;
                }
                DA.GetData("Run", ref run);
                if (!run) {
                    return;
                }
                Task<SolveResults> task = Task.Run(() => ComputeSockets(path, ip, port), CancelToken);
                TaskList.Add(task);
                return;
            }

            if(!GetSolveResults(DA, out SolveResults result)) {

                string ip = null;
                int port = 0;
                bool run = true;
                string path = null;

                if (!DA.GetData("Ip", ref ip))
                {
                    return;
                }

                if (!DA.GetData("Port", ref port))
                {
                    return;
                }

                if (!DA.GetData("Mod file", ref path))
                {
                    return;
                }
                DA.GetData("Run", ref run);
                if (!run)
                {
                    return;
                }

                result = ComputeSockets(path, ip, port);
            }


            if (result != null) {
                DA.SetData("Finished", result.Value);
            } 
        }

        private SolveResults ComputeSockets(string path, string ip, int port) {
            SolveResults result = new SolveResults
            {
                Value = false
            };
            ModFileObject mod = new ModFileObject(path);
            try
            {
                Socket client = SocketConnection(ip, port);
                Task task = SendCommands(client, mod.Commands);
                task.Wait();
                CloseConnection(client);
                result.Value = true;

            }
            catch (Exception ex)
            {
                result.Value = false;
                Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Shuts down and closes the socket
        /// </summary>
        /// <param name="client">The socket the close down</param>
		private void CloseConnection(Socket client)
		{
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        /// <summary>
        /// Makes a socket connection with a server and syncs with it
        /// </summary>
        /// <param name="ip">Server to connect to</param>
        /// <param name="port">port of the socket connection</param>
        /// <returns>The socket object</returns>
		private Socket SocketConnection(string ip, int port) {        
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
            Socket client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            client.SendTimeout = TimeOut;
            client.ReceiveTimeout = TimeOut;
            Task result = client.ConnectAsync(remoteEP);
            int index = Task.WaitAny(new[] { result }, TimeOut);
            if (!client.Connected) { 
                client.Close();
                throw new Exception("Can't connect to server");
            }

            Message = "Socket connected to "+ client.RemoteEndPoint.ToString();
     
            byte[] payload = Encoding.UTF8.GetBytes("listening?");
            client.Send(payload);

            byte[] bytes = new byte[1024];
            int bytesRec = client.Receive(bytes);
            string answer = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Message = "Received the following: " + answer;
            if (answer != "Yes") {
                CloseConnection(client);
            }
            return client;
        }

        /// <summary>
        /// The targets to send to the server
        /// </summary>
        /// <param name="client"></param>
        /// <param name="targets"></param>
        private async Task SendCommands(Socket client, List<CommandObject> commands) {
            //Sets the timeout back to infinite(0),
            //this is done because the robot might be moving for 10+ seconds and the receive might timeoute because of it.
            client.SendTimeout = 0;
            client.ReceiveTimeout = 0;
            for (int i = 0; i < commands.Count; i++)
			{
                await commands[i].SendOverSocket(client);

                //Reply from server
                byte[] bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                string answer = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                if (answer != "Ready") { 
                    return; 
                }

                byte[] end_payload;
                if (i == commands.Count -1)
                {
                    end_payload = Encoding.UTF8.GetBytes("No more targets");
                }
                else {
                    end_payload = Encoding.UTF8.GetBytes("Sending next target");
                }
                client.Send(end_payload);
                await Task.Delay(CommandObject.WaitTime);
            }

		}

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.datawoodsocket.ToBitmap();

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("50843146-2808-4E45-8501-6716BDACB6EC"); }
        }
    }
}