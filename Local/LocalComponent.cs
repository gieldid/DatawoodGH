using Grasshopper.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatawoodGH.Local
{
	public abstract class LocalComponent : GH_Component
	{
		public LocalComponent(string name, string nickname, string description)
			: base(name, nickname, description, "Datawood", "Local") 
		{ 
		}
	}
}
