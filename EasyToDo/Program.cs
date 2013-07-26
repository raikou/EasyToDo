using System;
using System.Windows.Forms;

namespace EasyToDo
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}

	/// <summary>
	/// 利用する色情報を持つ
	/// </summary>
	public class decorateColor
	{
		
	}
}
