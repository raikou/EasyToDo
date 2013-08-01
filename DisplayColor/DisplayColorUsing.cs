using System;
using System.Drawing;

namespace DisplayColorUsing
{
	/// <summary>
	/// 色の状態識別子
	/// </summary>
	public enum ColorStatus
	{
		Finish = 0,
		Emergency,
		Memo1,
		Date,
		Memo2,
		Unlimited,
		Memo3,

		DefColor
	};
	
	/// <summary>
	/// 色の設定情報を取得する
	/// </summary>
	public class DisplayColor
	{
		public static ColorStatus GetColorStatus(string colorStatusString) {
			if (colorStatusString == Finish.DisplayName) return ColorStatus.Finish;
			if (colorStatusString == Emergency.DisplayName) return ColorStatus.Emergency;
			if (colorStatusString == Memo1.DisplayName) return ColorStatus.Memo1;
			if (colorStatusString == Memo2.DisplayName) return ColorStatus.Memo2;
			if (colorStatusString == Memo3.DisplayName) return ColorStatus.Memo3;
			if (colorStatusString == Unlimited.DisplayName) return ColorStatus.Unlimited;
			if (colorStatusString == Date.DisplayName) return ColorStatus.Date;
			return ColorStatus.DefColor;
		}

		public static Color ForeColor(ColorStatus colorStatus) {
			if (colorStatus == ColorStatus.Finish) return Finish.ForeColor;
			if (colorStatus == ColorStatus.Emergency) return Emergency.ForeColor;
			if (colorStatus == ColorStatus.Memo1) return Memo1.ForeColor;
			if (colorStatus == ColorStatus.Memo2) return Memo2.ForeColor;
			if (colorStatus == ColorStatus.Memo3) return Memo3.ForeColor;
			if (colorStatus == ColorStatus.Unlimited) return Unlimited.ForeColor;
			if (colorStatus == ColorStatus.Date) return Date.ForeColor;
			return DefColor.ForeColor;
		}

		public static string GetDispName(ColorStatus colorStatus) {
			if (colorStatus == ColorStatus.Finish) return Finish.DisplayName;
			if (colorStatus == ColorStatus.Emergency) return Emergency.DisplayName;
			if (colorStatus == ColorStatus.Memo1) return Memo1.DisplayName;
			if (colorStatus == ColorStatus.Memo2) return Memo2.DisplayName;
			if (colorStatus == ColorStatus.Memo3) return Memo3.DisplayName;
			if (colorStatus == ColorStatus.Unlimited) return Unlimited.DisplayName;
			if (colorStatus == ColorStatus.Date) return Date.DisplayName;
			return DefColor.DisplayName;
		}

		public class Finish : DefColor
		{
			private Finish() {
				displayName = "完了";
				foreColor = Color.Gray;
				backColor = Color.White;
				colorFlag = ColorStatus.Finish;
			}
		}

		public class Emergency : DefColor
		{
			private Emergency() {
				displayName = "緊急";
				foreColor = Color.White;
				backColor = Color.Red;
				colorFlag = ColorStatus.Emergency;
			}
		}

		public class Memo1 : DefColor
		{
			private Memo1() {
				displayName = "メモ１";
				foreColor = Color.Black;
				backColor = Color.Aquamarine;
				colorFlag = ColorStatus.Memo1;
			}
		}

		/// <summary>
		/// //TODO:こいつだけ特殊
		/// </summary>
		public class Date : DefColor
		{
			static readonly TimeSpan MinusThreeDay = new TimeSpan(3, 0, 0, 0);
			static readonly TimeSpan MinusOneDay = new TimeSpan(1, 0, 0, 0);

			/// <summary>
			/// デフォルト状態が帰る
			/// </summary>
			private Date() {
				displayName = "Finish";
				foreColor = Color.Gray;
				backColor = Color.White;
				colorFlag = ColorStatus.Date;
			}

			public static Color ForeColorLimit {
				get { return Color.Red; }
			}

			public static Color BackColorLimit {
				get { return Color.White; }
			}

			public static Color ForeColorNearLimit
			{
				get { return Color.Salmon; }
			}

			public static Color BackColorNearLimit
			{
				get { return Color.White; }
			}

			public static Color getForeColor(DateTime assesmentDate) {
				DateTime today = System.DateTime.Now;
				if (assesmentDate > today - MinusThreeDay) {
					return foreColor;
				} else if (assesmentDate > today - MinusOneDay) {
					return ForeColorNearLimit;
				}
				return ForeColorLimit;
			}

			public static Color getBackColor(DateTime assesmentDate)
			{
				DateTime today = System.DateTime.Now;
				if (assesmentDate > today - MinusThreeDay)
				{
					return foreColor;
				}
				else if (assesmentDate > today - MinusOneDay)
				{
					return ForeColorNearLimit;
				}
				return ForeColorLimit;
			}
		}

		public class Memo2 : DefColor
		{
			private Memo2() {
				displayName = "メモ２";
				foreColor = Color.Black;
				backColor = Color.GreenYellow;
				colorFlag = ColorStatus.Memo2;
			}
		}

		public class Unlimited : DefColor
		{
			private Unlimited() {
				displayName = "無期限";
				foreColor = Color.Blue;
				backColor = Color.White;
				colorFlag = ColorStatus.Unlimited;
			}
		}

		public class Memo3 : DefColor
		{
			private Memo3() {
				displayName = "メモ３";
				foreColor = Color.Black;
				backColor = Color.LightGreen;
				colorFlag = ColorStatus.Memo3;
				colorFlag = ColorStatus.Memo3;
			}
		}

		public abstract class DefColor
		{
			protected static string displayName = "その他";
			protected static Color foreColor = Color.Black;
			protected static Color backColor = Color.White;
			protected static ColorStatus colorFlag = ColorStatus.DefColor;
			public static string DisplayName { get { return displayName; } }
			public static Color ForeColor { get { return foreColor; } }
			public static Color BackColor { get { return backColor; } }
			public static ColorStatus ColorFlag { get { return colorFlag; } }
			public override string ToString() { return DisplayName; }
		}
	}
}