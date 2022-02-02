using System;
using System.Collections.Generic;
using System.IO;
using DatawoodGH.Properties;
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
            pManager.AddIntegerParameter("Z-Treshold", "Z", "All the z values below this treshold get removed", GH_ParamAccess.item);
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
            int zTreshold = 0;
            if (!DA.GetData("Directory", ref dir)) {
                return;
            }
            if (!DA.GetData("Z-Treshold", ref zTreshold)) {
                return;
            }

            DA.GetData("Run", ref run);
            DA.GetData("NStep", ref nStep);

            if (run) { 
                var pcdPaths = FindPcdFiles(dir);

                switch (pcdPaths.Length) { 
                    case 0:
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "No pcd found in directory!");
                        return;
                    case 1:
                        break;
                    default:
                        AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Multiple pcd files found, took the first one is this the correct one?");
                        break;

                }
                var txtPath = ChangeToTxt(pcdPaths[0]);
                RemoveLines(txtPath, nStep, zTreshold);
                DA.SetData("Result", txtPath);
            }
            
        }



        private string ChangeToTxt(string path) {
            var txtPath = Path.ChangeExtension(path, ".txt");
            File.Move(path, txtPath);
            return txtPath;
        }

        private string[] FindPcdFiles(string dir) {
            string[] files = Directory.GetFiles(dir, "*.pcd");
            return files; 
        }

        /// <summary>
        /// Checks the line for the z values and checks if it's below the treshold.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="treshHold"></param>
        /// <returns>A boolean that tells to keep the line</returns>
        private bool KeepZValueCheck(string line, int treshHold) {
            //-249.57 0.00 292.28

            bool keepLine = true;

            var startChar = line.LastIndexOf(' ');
            var zValueStr = line.Substring(startChar + 1);
            var zValue = float.Parse(zValueStr);

            if (zValue < treshHold) {
                keepLine = false;
            }

            return keepLine;

        } 


        /// <summary>
        /// Only keeps the lines based on nStep. nStep = 4 means every 4th line stays.
        /// </summary>
        /// <param name="path">Path to the file that needs to be changed</param>
        /// <param name="nStep"></param>
        private void RemoveLines(string path , int nStep, int zTreshhold) {
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
                        if (KeepZValueCheck(line, zTreshhold)) {
                            sw.WriteLine(line);
                        }
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
        protected override System.Drawing.Bitmap Icon => Resources.datawoodpcd.ToBitmap();

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("A9A2AD45-B0EE-47D4-BBB1-4F273C438065"); }
        }
    }
}