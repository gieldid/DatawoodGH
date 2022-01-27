﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DatawoodGH.Network.SocketConnection
{
    public abstract class CommandObject
    {
        public const int WaitTime = 200;
        public string Name { get; set; }

        public abstract Task SendOverSocket(Socket client);

        protected async Task SendOverSocketCommandBase(Socket client, int waitTime = WaitTime) {
            byte[] payload = Encoding.UTF8.GetBytes(Name);
            client.Send(payload);
            await Task.Delay(waitTime);
            //System.Threading.Thread.Sleep(waitTime);
        }

        
    }
}
