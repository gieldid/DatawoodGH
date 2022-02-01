using System;
using System.Collections.Generic;
using System.IO;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace DatawoodGH.Local
{
    public class PcdConverter : LocalComponent
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public PcdConverter()
          : base("Point cloud converter", "Pcd",
              "Converts point cloud to txt and removes lines")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Directory", "Dir", "Directory of pcd file", GH_ParamAccess.item);
            pManager.AddIntegerParameter("NStep", "N", "Reads nth line of file", GH_ParamAccess.item, 4);
            pManager.AddBooleanParameter("Run", "R", "When to run", GH_ParamAccess.item, true);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Result", "R", "Txt file", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string dir = null;
            bool run = false;
            int nStep = 0;
            if (!DA.GetData("Directory", ref dir)) {
                return;
            }
            DA.GetData("Run", ref run);
            DA.GetData("NStep", ref nStep);

            if (run) { 
                var pcdPath = FindPcdFile(dir);
                var txtPath = ChangeToTxt(pcdPath);
                RemoveLines(txtPath, nStep);
                DA.SetData("Result", txtPath);
            }
            
        }


        private string ChangeToTxt(string path) {
            var txtPath = Path.ChangeExtension(path, ".txt");
            File.Move(path, txtPath);
            return txtPath;
        }
        private string FindPcdFile(string dir) {
            string[] files = Directory.GetFiles(dir, "*.pcd");
            return files[0];
        }

        private void RemoveLines(string path , int nStep) {
            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(path))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i > 10 &&( i % nStep == 0))
                    {
                        sw.WriteLine(line);
                    }
                    i++;
                }
            }

            File.Delete(path);
            File.Move(tempFile, path);
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
            get { return new Guid("A9A2AD45-B0EE-47D4-BBB1-4F273C438065"); }
        }
    }
}