using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace DatawoodGH.Local
{
    public class RapidWriter : LocalComponent
    {
        /// <summary>
        /// Initializes a new instance of the RapidWriter class.
        /// </summary>
        public RapidWriter()
          : base("ScanfileCreator", "Scan",
              "Writes RAPID for the scan method")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Directory", "Dir", "Directory where to save the RAPID file", GH_ParamAccess.item);
            pManager.AddTextParameter("Name", "N", "Name of the rapid file", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Run", "R", "When to run", GH_ParamAccess.item, true);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string dir = null;
            string name = null;
            bool run = false;

            if (!DA.GetData("Directory", ref dir)) {
                return;
            }
            if (!DA.GetData("Name", ref name))
            {
                return;
            }
            DA.GetData("Run", ref run);

            if (run) { 
            
            }

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("DE60D7BB-874A-4853-A08D-A152278C39BF"); }
        }
    }
}