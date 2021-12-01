using CsvHelper;
using CsvHelper.Configuration;
using CSVModule.Properties;
using Grasshopper.Kernel;
using System;
using System.Globalization;
using System.IO;

namespace csvModule
{
	public class CSVModuleComponent : GH_Component
	{

		private static readonly string CSV_NAME = "Datawood.csv";

		/// <summary>
		/// Each implementation of GH_Component must provide a public 
		/// constructor without any arguments.
		/// Category represents the Tab in which the component will appear, 
		/// Subcategory the panel. If you use non-existing tab or panel names, 
		/// new tabs/panels will automatically be created.
		/// </summary>
		public CSVModuleComponent()
		  : base("Datawood to csv", "csv",
			"Adds datawood to csv",
			"Datawood", "Local")
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

			//This is the Time parameter, if it's not set it will use DateTime.Now as default.
			Params.Input[8].Optional = true;
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
			string path = "";
			int index = 0;
			int width = 0;
			int length = 0;
			int height = 0;
			string type = "";
			int typeCeraintry =0;
			string color = "";
			DateTime time = new DateTime();
			double density = 0;
			int weight = 0;
			double moistureContent = 0;
			string picturePath = "";
			string delimiter = "";

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
			if (!DA.GetData(6, ref typeCeraintry))
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
			if (!DA.GetData(11, ref moistureContent))
			{
				return;
			}
			if (!DA.GetData(12, ref picturePath))
			{
				return;
			}
			if (!DA.GetData(13, ref delimiter))
			{
				return;
			}

			string[] paths = {path,  CSV_NAME};
			string fullPath = Path.Combine(paths);

			DataWood datawood = new DataWood { 
				Index = index,
				Width = width,
				Length = length,
				Height = height,
				Type = type,
				TypeCertainty = typeCeraintry,
				Color = color, 
				Indexed = time,
				Density = density,
				Weight = weight,
				MoistureContent = moistureContent,
				PicturePath = picturePath				
			};

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
					csv.WriteRecord(datawood);
					csv.NextRecord();
				}
			}
			else
			{
				using (var writer = new StreamWriter(fullPath))
				using (var csv = new CsvWriter(writer, config))
				{
					csv.WriteHeader<DataWood>();
					csv.NextRecord();
					csv.WriteRecord(datawood);
					csv.NextRecord();
				}
			
			}

			DA.SetData(0, fullPath);
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