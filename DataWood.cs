using System;

namespace csvModule
{
	public class DataWood
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
		public double MoistureContent { get; set; }
		public string PicturePath { get; set; }
	}
}
