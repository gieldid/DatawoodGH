using CsvHelper;
using CsvHelper.Configuration;
using DatawoodGH.Local;
using DatawoodGH.Properties;
using Grasshopper.Kernel;
using System;
using System.Globalization;
using System.IO;

namespace csvModule
{
	public class CsvCreator : LocalComponent
	{

		private static readonly string CSV_NAME = "Datawood.csv";

		/// <summary>
		/// Each implementation of GH_Component must provide a public 
		/// constructor without any arguments.
		/// Category represents the Tab in which the component will appear, 
		/// Subcategory the panel. If you use non-existing tab or panel names, 
		/// new tabs/panels will automatically be created.
		/// </summary>
		public CsvCreator()
		  : base("Datawood to csv", "csv",
			"Adds datawood to csv")
		{
		}

		/// <summary>
		/// Registers all the input parameters for this component.
		/// </summary>
		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddTextParameter("Directory", "Dir", "Directory of csv file", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Index", "I", "Part Index", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Width", "Wi", "Width of object in mm", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Length", "L", "Length of object in mm", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Height", "H", "Height of object in mm", GH_ParamAccess.item);
			pManager.AddTextParameter("Type", "T", "Wood type", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Type certainty", "TC", "Percaintage type certainty", GH_ParamAccess.item);	
			pManager.AddTextParameter("Color", "C", "Color values of object", GH_ParamAccess.item);
			pManager.AddTimeParameter("Time", "T", "Time Indexed", GH_ParamAccess.item);
			pManager.AddNumberParameter("Density", "DSY", "Density of object", GH_ParamAccess.item);
			pManager.AddIntegerParameter("Weight", "We", "Weight of object", GH_ParamAccess.item);
			pManager.AddNumberParameter("Moisture content", "MC", "Moisture content of object", GH_ParamAccess.item);
			pManager.AddTextParameter("Picture Path", "PP", "Path to the picture of object", GH_ParamAccess.item);
			pManager.AddTextParameter("Delimter", "D", "The delimter seperating values in the csv", GH_ParamAccess.item, ";");
			pManager.AddBooleanParameter("Run", "R", "To run application", GH_ParamAccess.item, true);
			pManager.AddTextParameter("Name", "N", "csv file name", GH_ParamAccess.item, CSV_NAME);
			pManager.AddTextParameter("Location", "L", "Location where the object is physically stored", GH_ParamAccess.item);
			//This is the Time parameter, if it's not set it will use DateTime.Now as default.
			Params.Input[8].Optional = true;
			//Moisture content
			Params.Input[11].Optional = true;
			
		}

		/// <summary>
		/// Registers all the output parameters for this component.
		/// </summary>
		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.AddTextParameter("Result", "R", "CSV File", GH_ParamAccess.item);
		}

		/// <summary>
		/// This is the method that actually does the work.
		/// </summary>
		/// <param name="DA">The DA object can be used to retrieve data from input parameters and 
		/// to store data in output parameters.</param>
		protected override void SolveInstance(IGH_DataAccess DA)
		{
			string path = null;
			int index = 0;
			int width = 0;
			int length = 0;
			int height = 0;
			string type = null;
			int typeCertainty =0;
			string color = null;
			DateTime time = new DateTime();
			double density = 0;
			int weight = 0;
			double? moistureContent = null;
			string picturePath = null;
			string delimiter = null;
			bool run = true;
			string name = null;
			string location = null;
			
			if (!DA.GetData(0, ref path)) {
				return;
			}
			if (!DA.GetData(1, ref index))
			{
				return;
			}
			if (!DA.GetData(2, ref width))
			{
				return;
			}
			if (!DA.GetData(3, ref length))
			{
				return;
			}
			if (!DA.GetData(4, ref height))
			{
				return;
			}
			if (!DA.GetData(5, ref type))
			{
				return;
			}
			if (!DA.GetData(6, ref typeCertainty))
			{
				return;
			}
			if (!DA.GetData(7, ref color))
			{
				return;
			}
			if (!DA.GetData(8, ref time))
			{
				time = DateTime.Now;
			}
			if (!DA.GetData(9, ref density))
			{
				return;
			}
			if (!DA.GetData(10, ref weight))
			{
				return;
			}

			if (!DA.GetData(11, ref moistureContent)) {
				moistureContent = null;
			}
			
			if (!DA.GetData(12, ref picturePath))
			{
				return;
			}
			if (!DA.GetData(13, ref delimiter))
			{
				return;
			}
			DA.GetData(14, ref run);
			DA.GetData(15, ref name);
			if (!DA.GetData(16, ref location)) {
				return;
			}

			if (run)
			{
				DataWoodObject datawood = new DataWoodObject
				{
					Index = index,
					Width = width,
					Length = length,
					Height = height,
					Type = type,
					TypeCertainty = typeCertainty,
					Color = color,
					Indexed = time,
					Density = density,
					Weight = weight,
					MoistureContent = moistureContent,
					PicturePath = picturePath,
					Location = location
				};
				var result = WriteTOCSV(datawood, path, name, delimiter);
				DA.SetData(0, result);
			}
			else {
				return;
			}
		}


		/// <summary>
		/// Creates a new csv file or appends to it
		/// </summary>
		/// <param name="datawood">The object that should be written to the csv</param>
		/// <param name="path">dir path</param>
		/// <param name="name">file name</param>
		/// <param name="delimiter">Delimter used in the csv</param>
		/// <returns></returns>
		private string WriteTOCSV(DataWoodObject datawood, string path, string name, string delimiter) {
			string[] paths = { path, name };
			string fullPath = Path.Combine(paths);

			var config = new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				Delimiter = delimiter
			};

			if (File.Exists(fullPath))
			{
				using (var stream = File.Open(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
				using (var writer = new StreamWriter(stream))
				using (var csv = new CsvWriter(writer, config))
				{
					csv.Context.RegisterClassMap<DataWoodMap>();
					csv.WriteRecord(datawood);
					csv.NextRecord();
				}
			}
			else
			{
				using (var writer = new StreamWriter(fullPath))
				using (var csv = new CsvWriter(writer, config))
				{
					//csv.Context.RegisterClassMap<DataWoodMap>();
					csv.WriteHeader<DataWoodObject>();
					csv.NextRecord();
					csv.WriteRecord(datawood);
					csv.NextRecord();
				}

			}

			return fullPath;
		}


		/// <summary>
		/// Provides an Icon for every component that will be visible in the User Interface.
		/// Icons need to be 24x24 pixels.
		/// You can add image files to your project resources and access them like this:
		/// return Resources.IconForThisComponent;
		/// </summary>
		protected override System.Drawing.Bitmap Icon => Resources.datwoodcsv.ToBitmap();

		/// <summary>
		/// Each component must have a unique Guid to identify it. 
		/// It is vital this Guid doesn't change otherwise old ghx files 
		/// that use the old ID will partially fail during loading.
		/// </summary>
		public override Guid ComponentGuid => new Guid("7500576F-7A59-4F48-819D-9E8C7828D7C3");
	}
}