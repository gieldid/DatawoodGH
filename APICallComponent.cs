using System;
using System.Collections.Generic;
using System.Net;
using CSVModule.Properties;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace CSVModule
{
    public class APICallComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the APICallComponent class.
        /// </summary>
        public APICallComponent()
          : base("API Call", "API",
            "Send get call to an api and retrieve result",
            "Datawood", "Network")
        {
        }

        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("URL", "U", "Url of api to call", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Enabled", "E", "Makes API call when enabled", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "J", "The JSON Output", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string url = null;
            string result = null;
            bool enabled = false;
            if (!DA.GetData(0, ref url)) return;
            if (!DA.GetData(1, ref enabled)) return;

            if (enabled) { 
                using (WebClient wc = new WebClient())
                {
                    result = wc.DownloadString(url);
                }

                DA.SetData(0, result);
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