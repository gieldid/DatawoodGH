using System;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace DatawoodGH
{
	public class DataWoodObject
	{	
		public int Index { get; set; }
		public int Width { get; set; }
		public int Length { get; set; }
		public int Height { get; set; }
		public string Type { get; set; }
		public int TypeCertainty { get; set; }
		public string Color { get; set; }
		public DateTime Indexed { get; set; }
		public double Density { get; set; }
		public int Weight { get; set; }
		public double? MoistureContent { get; set; }
		public string PicturePath { get; set; }
		public string Location { get; set; }


	}

	public class DataWoodMap : ClassMap<DataWoodObject> {
		public DataWoodMap() {
			AutoMap(CultureInfo.InvariantCulture);
		}
	}
}
