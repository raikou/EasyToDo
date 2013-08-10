using System;
using System.Drawing;

namespace DisplayColorUsing
{
	/// <summary>
	///     色の状態識別子
	/// </summary>
	public enum ColorStatus :int
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
	///     色の設定情報を取得する
	/// </summary>
	public class DisplayColor
	{
		/// <summary>
		///     画面に表示中の文字列からColorStatusを返す関数
		/// </summary>
		/// <param name="_colorStatusString">画面に表示中の文字列</param>
		/// <returns>色情報の識別子</returns>
		public static ColorStatus GetColorStatusString( string _colorStatusString ) {
			if ( _colorStatusString == Finish.DisplayName ) return ColorStatus.Finish;
			if ( _colorStatusString == Emergency.DisplayName ) return ColorStatus.Emergency;
			if ( _colorStatusString == Memo1.DisplayName ) return ColorStatus.Memo1;
			if ( _colorStatusString == Memo2.DisplayName ) return ColorStatus.Memo2;
			if ( _colorStatusString == Memo3.DisplayName ) return ColorStatus.Memo3;
			if ( _colorStatusString == Unlimited.DisplayName ) return ColorStatus.Unlimited;
			return _colorStatusString == DefColor.DisplayName ? ColorStatus.DefColor : ColorStatus.Date;
		}

		/// <summary>
		///     保存した ColorStatus の情報からColorStatusを返す関数
		/// </summary>
		/// <param name="_colorStatusInt">データベースに保存した int 型の値</param>
		/// <returns>色情報の識別子</returns>
		public static ColorStatus GetColorStatusInt( int _colorStatusInt ) {
			if ( _colorStatusInt == (int)Finish.ColorFlag ) return ColorStatus.Finish;
			if ( _colorStatusInt == (int)Emergency.ColorFlag ) return ColorStatus.Emergency;
			if ( _colorStatusInt == (int)Memo1.ColorFlag ) return ColorStatus.Memo1;
			if ( _colorStatusInt == (int)Memo2.ColorFlag ) return ColorStatus.Memo2;
			if ( _colorStatusInt == (int)Memo3.ColorFlag ) return ColorStatus.Memo3;
			if ( _colorStatusInt == (int)Unlimited.ColorFlag ) return ColorStatus.Unlimited;
			return _colorStatusInt == (int)Date.ColorFlag ? ColorStatus.Date : ColorStatus.DefColor;
		}

		/// <summary>
		///     設定されている文字色を返す
		/// </summary>
		/// <param name="_colorStatus"> 色情報の識別子 </param>
		/// <returns>色情報の識別子</returns>
		public static Color ForeColor( ColorStatus _colorStatus ) {
			if ( _colorStatus == ColorStatus.Finish ) return Finish.ForeColor;
			if ( _colorStatus == ColorStatus.Emergency ) return Emergency.ForeColor;
			if ( _colorStatus == ColorStatus.Memo1 ) return Memo1.ForeColor;
			if ( _colorStatus == ColorStatus.Memo2 ) return Memo2.ForeColor;
			if ( _colorStatus == ColorStatus.Memo3 ) return Memo3.ForeColor;
			if ( _colorStatus == ColorStatus.Unlimited ) return Unlimited.ForeColor;
			return _colorStatus == ColorStatus.Date ? Date.ForeColor : DefColor.ForeColor;
		}

		/// <summary>
		///     設定されている文字色を返す
		/// </summary>
		/// <param name="_colorStatus"> 色情報の識別子 </param>
		/// <returns>色情報の識別子</returns>
		public static Color BackColor( ColorStatus _colorStatus ) {
			if ( _colorStatus == ColorStatus.Finish ) return Finish.BackColor;
			if ( _colorStatus == ColorStatus.Emergency ) return Emergency.BackColor;
			if ( _colorStatus == ColorStatus.Memo1 ) return Memo1.BackColor;
			if ( _colorStatus == ColorStatus.Memo2 ) return Memo2.BackColor;
			if ( _colorStatus == ColorStatus.Memo3 ) return Memo3.BackColor;
			if ( _colorStatus == ColorStatus.Unlimited ) return Unlimited.BackColor;
			return _colorStatus == ColorStatus.Date ? Date.BackColor : DefColor.BackColor;
		}

		/// <summary>
		///     設定されている背景色を返す
		/// </summary>
		/// <param name="colorStatus"> 色情報の識別子 </param>
		/// <returns>色情報の識別子</returns>
		public static string GetDispName( ColorStatus colorStatus ) {
			if ( colorStatus == ColorStatus.Finish ) return Finish.DisplayName;
			if ( colorStatus == ColorStatus.Emergency ) return Emergency.DisplayName;
			if ( colorStatus == ColorStatus.Memo1 ) return Memo1.DisplayName;
			if ( colorStatus == ColorStatus.Memo2 ) return Memo2.DisplayName;
			if ( colorStatus == ColorStatus.Memo3 ) return Memo3.DisplayName;
			if ( colorStatus == ColorStatus.Unlimited ) return Unlimited.DisplayName;
			if ( colorStatus == ColorStatus.Date ) return Date.DisplayName;
			return DefColor.DisplayName;
		}

		/// <summary>
		///     //TODO:こいつだけ特殊
		/// </summary>
		public class Date
		{
			private static readonly TimeSpan MinusThreeDay = new TimeSpan( 3, 0, 0, 0 );
			private static readonly TimeSpan MinusOneDay = new TimeSpan( 1, 0, 0, 0 );

			public static string DisplayName {
				get { return "日付"; }
			}

			#region 基本色
			public static Color ForeColor
			{
				get { return Color.Black; }
			}
			public static Color BackColor
			{
				get { return Color.White; }
			}
			#endregion

			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Date; }
			}

			#region 本日 or 期限が過ぎている場合の色
			public static Color ForeColorLimit
			{
				get { return Color.Red; }
			}

			public static Color BackColorLimit {
				get { return Color.White; }
			}
			#endregion

			#region 期限が近日の色
			public static Color ForeColorNearLimit {
				get { return Color.Salmon; }
			}

			public static Color BackColorNearLimit {
				get { return Color.White; }
			}
			#endregion

			public static Color GetForeColor( DateTime assesmentDate ) {
				DateTime today = DateTime.Now;
				if ( assesmentDate > today - MinusThreeDay ) return ForeColor;
				return assesmentDate > today - MinusOneDay ? ForeColorNearLimit : ForeColorLimit;
			}

			public static Color GetBackColor( DateTime assesmentDate ) {
				DateTime today = DateTime.Now;
				if ( assesmentDate > today - MinusThreeDay ) return BackColor;
				return assesmentDate > today - MinusOneDay ? BackColorNearLimit : BackColorLimit;
			}
		}

		public abstract class DefColor
		{
			protected static string displayName = "その他";
			protected static Color foreColor = Color.Black;
			protected static Color backColor = Color.White;
			protected static ColorStatus colorFlag = ColorStatus.DefColor;

			public static string DisplayName {
				get { return displayName; }
			}

			public static Color ForeColor {
				get { return foreColor; }
			}

			public static Color BackColor {
				get { return backColor; }
			}

			public static ColorStatus ColorFlag {
				get { return colorFlag; }
			}

			public override string ToString() { return DisplayName; }
		}

		public class Emergency
		{
			public static string DisplayName
			{
				get { return "緊急"; }
			}
			public static Color ForeColor
			{
				get { return Color.White; }
			}
			public static Color BackColor
			{
				get { return Color.Red; }
			}
			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Emergency; }
			}
		}

		public class Finish
		{
			public static string DisplayName
			{
				get { return "完了"; }
			}
			public static Color ForeColor
			{
				get { return Color.Gray; }
			}
			public static Color BackColor
			{
				get { return Color.White; }
			}
			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Finish; }
			}
		}

		public class Memo1
		{
			public static string DisplayName
			{
				get { return "メモ１"; }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
			}
			public static Color BackColor
			{
				get { return Color.Aquamarine; }
			}
			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Memo1; }
			}
		}

		public class Memo2
		{
			public static string DisplayName
			{
				get { return "メモ２"; }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
			}
			public static Color BackColor
			{
				get { return Color.GreenYellow; }
			}
			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Memo2; }
			}
		}

		public class Memo3
		{
			public static string DisplayName
			{
				get { return "メモ３"; }
			}
			public static Color ForeColor
			{
				get { return Color.Black; }
			}
			public static Color BackColor
			{
				get { return Color.LightGreen; }
			}
			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Memo3; }
			}
		}

		public class Unlimited
		{
			public static string DisplayName
			{
				get { return "無期限"; }
			}
			public static Color ForeColor
			{
				get { return Color.Blue; }
			}
			public static Color BackColor
			{
				get { return Color.White; }
			}
			public static ColorStatus ColorFlag
			{
				get { return ColorStatus.Unlimited; }
			}
		}
	}
}