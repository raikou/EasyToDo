using System;
using System.Drawing;

namespace DecorateDisplayColor
{
	/// <summary>
	/// 利用する色のフラグ名
	/// </summary>
	public enum ColorFlag
	{
		Finish = 0,
		Emergency,
		Memo1,
		Date,
		Memo2,
		Unlimited,
		Memo3,

		Other
	};

    public  class DecorateColor
    {
		public Color ForeColor(ColorFlag colorFlag)
		{
			switch (colorFlag)
			{
				case ColorFlag.Finish:
					return Finish.ForeColor;
				case ColorFlag.Emergency:
					return Emergency.ForeColor;
				case ColorFlag.Memo1:
					return Memo1.ForeColor;
				case ColorFlag.Memo2:
					return Memo2.ForeColor;
				case ColorFlag.Memo3:
					return Memo3.ForeColor;
				case ColorFlag.Unlimited:
					return Unlimited.ForeColor;
			}
			return Other.ForeColor;
		}
		public Color ForeColor(DateTime date)
		{
			return Date.ForeColor(date);
		}
		public Color BackColor(ColorFlag colorFlag)
		{
			switch (colorFlag)
			{
				case ColorFlag.Finish:
					return Finish.BackColor;
				case ColorFlag.Emergency:
					return Emergency.BackColor;
				case ColorFlag.Memo1:
					return Memo1.BackColor;
				case ColorFlag.Memo2:
					return Memo2.BackColor;
				case ColorFlag.Memo3:
					return Memo3.BackColor;
				case ColorFlag.Unlimited:
					return Unlimited.BackColor;
			}
			return Other.BackColor;
		}
		public Color BackColor(DateTime date)
		{
			return Date.BackColor(date);
		}
	    public class Finish
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Finish; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor
			{
				get { return Color.Gray; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.White; }
				set { throw new NotImplementedException(); }
			}
		}
		public class Emergency
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Emergency; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor
			{
				get { return Color.White; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.Red; }
				set { throw new NotImplementedException(); }
			}
		}
		public class Memo1
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Memo1; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.Aquamarine; }
				set { throw new NotImplementedException(); }
			}
		}
		public class Date
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Date; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor(DateTime date)
			{
				if (DateTime.Today.Date >= date)
				{
					return Color.White;
				}
				else if (DateTime.Today.AddDays(3).Date >= date)
				{
					return Color.Black;
				}
				else
				{
					return Color.Black;
				}
			}
			public static Color BackColor(DateTime date)
			{
				if (DateTime.Today.Date >= date)
				{
					return Color.Red;
				}
				else if (DateTime.Today.AddDays(3).Date >= date)
				{
					return Color.Orange;
				}
				else
				{
					return Color.White;
				}
			}
		}
		public class Memo2
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Memo2; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.GreenYellow; }
				set { throw new NotImplementedException(); }
			}
		}
		public class Unlimited
		{
			public static Color ForeColor
			{
				get { return Color.Blue; }
				set { throw new NotImplementedException(); }
			}
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Unlimited; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.White; }
				set { throw new NotImplementedException(); }
			}
		}
		public class Memo3
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Memo3; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.LightGreen; }
				set { throw new NotImplementedException(); }
			}
		}
		public class Other
		{
			public static ColorFlag ColorFlag
			{
				get { return ColorFlag.Other; }
				set { throw new NotImplementedException(); }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
				set { throw new NotImplementedException(); }
			}
			public static Color BackColor
			{
				get { return Color.White; }
				set { throw new NotImplementedException(); }
			}
		}
    }



}

