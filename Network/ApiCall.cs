using System;
using System.Collections.Generic;
using System.Net;
using DatawoodGH.Network;
using DatawoodGH.Properties;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace DatawoodGH.Network
{
    public class ApiCall : NetworkComponent
    {
        /// <summary>
        /// Initializes a new instance of the APICallComponent class.
        /// </summary>
        public ApiCall()
          : base("API Call", "API",
            "Send get call to an api and retrieve result"
            )
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("URL", "U", "Url of api to call", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Run", "R", "Runs component", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Post", "P", "Is a post call", GH_ParamAccess.item, false);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "J", "The JSON Output", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Finished", "F", "Finished it's call", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string url = null;
            string result = null;
            bool run = false;
            bool post = false;
            DA.SetData("Finished", false);
            if (!DA.GetData("URL", ref url)) return;
            if (!DA.GetData("Run", ref run)) return;
            DA.GetData("Post", ref post);

            if (run) {
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        if (post) {
                            //http://127.0.0.1:5000/start_scan/multiangle/single/preset1/C:\\Users\\test\\folder\\newfolder
                            result = wc.UploadString(url, "POST", string.Empty);
                        }
                        else {
                            result = wc.DownloadString(url); 
                        }
                    }

                    DA.SetData("JSON", result);
                    DA.SetData("Finished", true);
                }
                catch (WebException webex) {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, webex.Message);
                }
            }
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.datawoodapi.ToBitmap();

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("770D14BD-F236-4AED-8955-2002379C18F3"); }
        }
    }
}