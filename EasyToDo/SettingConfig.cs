using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace EasyToDo
{
	/// <summary>
	/// XMLの読み込み、書き出し、関連するフラグの設定をしてる。
	/// </summary>
	public class SettingConfig
	{
		/// <summary>
		/// XMLの設定を保存するファイル名
		/// </summary>
		public readonly string fileName = "Config.xml";

		/// <summary>
		/// XML読み込み
		/// </summary>
		/// <returns></returns>
		public Flags Read()
		{
			Flags f;
			try {
				XmlSerializer seri = new XmlSerializer(typeof( Flags ));
				FileStream fs = new FileStream(this.fileName, FileMode.Open);
				f = (Flags)seri.Deserialize(fs);
				fs.Close();
				if( !File.Exists(f.dataFileName) ) {
					f.dataFileName = Directory.GetCurrentDirectory();
					f.dataFileName += "\\dataSave.xml";
					this.Write(f);
					MessageBox.Show("保存したファイルが見つかりませんでした。\n実行フォルダに作成します。");
				}
			} catch( Exception ) {
				f = new Flags { dataFileName = Directory.GetCurrentDirectory() + "\\dataSave.xml" };
				this.Write(f);
				return this.Read();
			}
			return f;
		}

		/// <summary>
		/// XML書き出し
		/// </summary>
		/// <param name="_flags"></param>
		public void Write(Flags _flags)
		{
			XmlSerializer seri = new XmlSerializer(typeof( Flags ));
			FileStream fs = new FileStream(this.fileName, FileMode.Create);
			seri.Serialize(fs, _flags);
			fs.Close();
		}
	}

	/// <summary>
	/// 情報格納場所
	/// </summary>
	public class Flags
	{
		/// <summary>
		/// 各のフラグ状態の管理
		///		（利用は主に、出力するしない）
		/// </summary>
		public bool check = true;

		/// <summary>
		/// データを保存するファイル名（PASSを含む）
		/// </summary>
		public string dataFileName;

		/// <summary>
		/// 各のフラグ状態の管理
		///		（利用は主に、出力するしない）
		/// </summary>
		public bool date = true;

		/// <summary>
		/// 各のフラグ状態の管理
		///		（利用は主に、出力するしない）
		/// </summary>
		public bool emergency = true;

		/// <summary>
		/// 各のフラグ状態の管理
		///		（利用は主に、出力するしない）
		/// </summary>
		public bool finish = false;

		/// <summary>
		/// 表示位置
		/// </summary>
		public int locationX = 100, locationY = 100;

		/// <summary>
		/// 各のフラグ状態の管理
		///		（利用は主に、出力するしない）
		/// </summary>
		public bool memo1 = true,
			memo2 = true,
			memo3 = true;

		/// <summary>
		/// 各のフラグ状態の管理
		///		（利用は主に、出力するしない）
		/// </summary>
		public bool unLimited = true;
	}
}