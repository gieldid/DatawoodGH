using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using DatawoodGH.Network;
using DatawoodGH.Properties;
using Grasshopper.Kernel;
using Renci.SshNet;
using Rhino.Geometry;

namespace CSVModule
{
    public class FtpTransfer : NetworkComponent
    {
        /// <summary>
        /// Initializes a new instance of the FTPComponent class.
        /// </summary>
        public FtpTransfer()
          : base("FTPComponent", "FTP",
              "FTP component to transfer files over")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //FTP Details
            pManager.AddTextParameter("Host", "H", "IP of ftp server", GH_ParamAccess.item);
            pManager.AddTextParameter("User", "U", "User to login to", GH_ParamAccess.item);
            pManager.AddTextParameter("Password", "Pwd", "Password for the user", GH_ParamAccess.item);

            //File details
            pManager.AddTextParameter("Path server", "PS", "Path of file on server", GH_ParamAccess.item);
            pManager.AddTextParameter("Path client", "PC", "Path to transfer file to on client", GH_ParamAccess.item);

            pManager.AddBooleanParameter("Enabled", "E", "Transfers file when enabled", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "File path", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string host = null;
            string user = null;
            string password = null;
            string pathServer = null;
            string pathClient = null;
            bool enabled = false;

            if (!DA.GetData(0, ref host))
            {
                return;
            }
            if (!DA.GetData(1, ref user))
            {
                return;
            }
            if (!DA.GetData(2, ref password))
            {
                return;
            }
            if (!DA.GetData(3, ref pathServer))
            {
                return;
            }
            if (!DA.GetData(4, ref pathClient))
            {
                return;
            }
            DA.GetData(5, ref enabled);

            if (enabled)
            {

                string path = DownloadFile(host, user, password, pathServer, pathClient);

                DA.SetData(0, path);
            }
        }

        public string DownloadFile(string host, string user, string password, string pathserver, string pathclient) {
            int port = 22;

            using (var sftp = new SftpClient (host, port, user, password))
            {

                sftp.Connect();

                using (FileStream file = File.OpenWrite(pathclient))
                {
                    sftp.DownloadFile(pathserver, file);
                }
                sftp.Disconnect();
            }
            return pathclient;
        }



        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Resources.datawoodftp.ToBitmap();

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("898438BC-71F0-4141-A747-237E349976EC"); }
        }
    }
}