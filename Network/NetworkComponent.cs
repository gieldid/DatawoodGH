using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;

namespace DatawoodGH.Network
{
	abstract public class NetworkComponent : GH_Component
	{
		public NetworkComponent(string name, string nickname, string description)
			: base(name, nickname, description, "Datawood", "Network") 
		{ 
		}
	}

	abstract public class TaskCapableNetworkComponent<T> : GH_TaskCapableComponent<T>
	{
		public TaskCapableNetworkComponent(string name, string nickname, string description)
			: base(name, nickname, description, "Datawood", "Network")
		{
		}
	}
}
