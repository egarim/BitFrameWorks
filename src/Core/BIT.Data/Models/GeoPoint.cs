using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Models
{
	public class GeoPoint
	{
		public double x;
		public double y;

		public GeoPoint(double x, double y)
		{
			this.x = x;
			this.y = y;
		}
	};
}
