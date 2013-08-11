using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using DisplayColorUsing;
using EasyToDo.Properties;

namespace EasyToDo
{
	/// <summary>
	///     メイン画面
	/// </summary>
	public partial class Form1 :Form
	{
		private readonly DataStruct m_dataStruct = new DataStruct();
		private readonly SettingConfig m_settingConfig = new SettingConfig();
		private Flags m_flags = new Flags();

		/// <summary>
		///     初期化
		/// </summary>
		public Form1() {
			InitializeComponent();
			m_flags = m_settingConfig.Read();
			StartPosition = FormStartPosition.Manual;
			Location = new Point( m_flags.locationX, m_flags.locationY );
			string title = Environment.CurrentDirectory;
			var di = new DirectoryInfo( title );
			string version = "";
			if ( m_flags.check ) {
				FileVersionInfo ver = FileVersionInfo.GetVersionInfo( Assembly.GetExecutingAssembly().Location );
				version = "　　v" + ver.FileVersion;
			}
			Text = "EasyToDo in " + di.Name + version;
		}

		/// <summary>
		///     再描画関数
		/// </summary>
		private void ReDraw() {
			try {
				listView1.Items.Clear();
				var str = new string[3];
				DataView dataView = m_dataStruct.TableView;
				for ( int i = 0; i < dataView.Count; i++ ) {
					string test = dataView[ i ][ "status" ].ToString();
					ColorStatus colorStatus = DisplayColor.GetColorStatusInt( int.Parse( test ) );
					if ( colorStatus == ColorStatus.Date ) str[ 0 ] = ( (DateTime)dataView[ i ][ "limitDate" ] ).ToString( "MM/dd" );
					else str[ 0 ] = DisplayColor.GetDispName( colorStatus );
					str[ 1 ] = dataView[ i ][ "name" ].ToString();
					str[ 2 ] = dataView[ i ][ "memo" ].ToString();

					//listViewに追加
					listView1.Items.Add(new ListViewItem(str));

					//色設定
					if ( colorStatus == ColorStatus.Date ) {
						listView1.Items[ i ].BackColor = DisplayColor.Date.GetForeColor( (DateTime)dataView[ i ][ "limitDate" ] );
						listView1.Items[ i ].ForeColor = DisplayColor.Date.GetBackColor( (DateTime)dataView[ i ][ "limitDate" ] );
					}
					else {
						listView1.Items[ i ].BackColor = DisplayColor.BackColor( colorStatus );
						listView1.Items[ i ].ForeColor = DisplayColor.ForeColor( colorStatus );
					}
				}
			}
			catch ( Exception e ) {
				MessageBox.Show( e.Message );
			}
		}

		/// <summary>
		///     フォームのロード時の処理
		///     処理：
		///     データ読み込み
		///     再描画
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e">エラーデータ</param>
		private void Form1_Load( object sender, EventArgs e ) {
			//データの読み込み
			m_dataStruct.read_data();
			//画面への描画
			ReDraw();
		}

		/// <summary>
		///     項目ダブルクリック時の動作
		///     change()を呼び出す。
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e"></param>
		private void listView1_DoubleClick( object sender, EventArgs e ) {
			Change();
		}

		/// <summary>
		///     表示内容の変更を行う。
		/// </summary>
		private void Change() {
			DataView dataView = m_dataStruct.TableView;
			int num = listView1.SelectedItems[ 0 ].Index;
			if ( num < dataView.Count ) {
				Form2 form2 = new Form2( dataView[ num ][ "name" ].ToString(), dataView[ num ][ "memo" ].ToString(),
					DisplayColor.GetColorStatusInt( int.Parse( dataView[ num ][ "status" ].ToString() ) ),
					(DateTime)dataView[ num ][ "limitDate" ] );
				form2.ShowDialog(this);

				if ( form2.status ) {
					//データ更新
					dataView[ num ][ "name" ] = form2.name;
					dataView[ num ][ "memo" ] = form2.memo;
					dataView[ num ][ "limitDate" ] = form2.date;
					dataView[ num ][ "status" ] = form2.colorStatus;
				}
				int dataVewNum = 0;
				for ( ; dataVewNum < dataView.Count; ) {
					if ( listView1.Items[ num ].SubItems[ 1 ].Text == (string)dataView[ dataVewNum ][ "name" ] ) if ( listView1.Items[ num ].SubItems[ 2 ].Text == (string)dataView[ dataVewNum ][ "memo" ] ) break;
					dataVewNum++;
				}
				//殺す
				form2.Dispose();
			}
			this.ReDraw();
		}

		/// <summary>
		///     新規作成
		/// </summary>
		public void Create() {
			Form2 form2 = new Form2( "", "", DisplayColor.Date.ColorFlag, DateTime.Today );
			form2.ShowDialog( this );
			if ( form2.status ) {
				m_dataStruct.Add( form2.name, form2.memo, form2.colorStatus, form2.date );
				ReDraw();
				m_dataStruct.write_data();
			}
			//殺す
			form2.Dispose();
		}

		/// <summary>
		///     削除
		/// </summary>
		/// <param name="_num"></param>
		public void Delete( int _num ) {
			DataView dataView = m_dataStruct.TableView;
			dataView.Delete( _num );
		}

		private void newMenuItem_Click( object sender, EventArgs e ) { Create(); }
		private void NewCreateToolStripMenuItem_Click( object sender, EventArgs e ) { Create(); }
		private void ChangeToolStripMenuItem_Click( object sender, EventArgs e ) { Change(); }

		private void DeleateToolStripMenuItem_Click( object sender, EventArgs e ) {
			int num = listView1.SelectedItems[ 0 ].Index;
			DialogResult re = MessageBox.Show( this, "削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
			if ( re == DialogResult.Yes ) Delete( num );
		}

		private void toolStripMenuItem1_Click( object sender, EventArgs e ) {
			try {
				DataView dataView = m_dataStruct.TableView;
				int num = listView1.SelectedItems[ 0 ].Index;
				if ( num < m_dataStruct.TableView.Count ) {
					dataView[ num ][ "status" ] = Convert.ToInt16( DisplayColor.DefColor.ColorFlag );
					dataView[ num ][ "exitDate" ] = DateTime.Now;
					ReDraw();
					m_dataStruct.write_data();
				}
			}
			catch ( Exception ex ) {
				MessageBox.Show( ex.Message );
			}
		}

		private void FilterSetteingToolStripMenuItem_Click( object sender, EventArgs e ) {
			var f = new Form3();
			f.ShowDialog( this );
			f.Close();
			m_flags = m_settingConfig.Read();
			ReDraw();
		}

		private void Form1_FormClosed( object sender, FormClosedEventArgs e ) {
			try {
				//↓のように設定していけば設定が保存される 場所：Form1.Designer.cs
				Settings.Default.ColumnIndex_0 = columnHeader1.DisplayIndex;
				Settings.Default.ColumnWidth_0 = columnHeader1.Width;
				Settings.Default.ColumnIndex_1 = columnHeader2.DisplayIndex;
				Settings.Default.ColumnWidth_1 = columnHeader2.Width;
				Settings.Default.ColumnIndex_3 = columnHeader3.DisplayIndex;
				Settings.Default.ColumnWidht_3 = columnHeader3.Width;
				//Locationの保存
				m_flags.locationX = Location.X;
				m_flags.locationY = Location.Y;
				m_settingConfig.Write( m_flags );
				//最後に保存
				Settings.Default.Save();
			}
			catch ( Exception ex ) {
				MessageBox.Show( ex.Message );
			}
		}

		private void AllDataToolStripMenuItem_Click( object sender, EventArgs e ) {
			var outData = new OutFileDataSet();
			outData.AllDataTitle();
		}

		private void ToolStripMenuItem_Click_Ver_Info( object sender, EventArgs e ) {
			FileVersionInfo ver = FileVersionInfo.GetVersionInfo( Assembly.GetExecutingAssembly().Location );
			MessageBox.Show( "バージョン：" + ver.FileVersion.ToString( CultureInfo.InvariantCulture ), "バージョン情報" );
		}

		private void Form1_Activated( object sender, EventArgs e ) { ReDraw(); }

		private void contextMenuStrip1_Opening( object sender, CancelEventArgs e ) {
			try {
				int num = listView1.SelectedItems[ 0 ].Index;
				if ( num < m_dataStruct.TableView.Count ) {
					changeToolStripMenuItem.Enabled = true;
					toolStripMenuItem1.Enabled = true;
					DeleteToolStripMenuItem.Enabled = true;
				}
			}
			catch {
				changeToolStripMenuItem.Enabled = false;
				toolStripMenuItem1.Enabled = false;
				DeleteToolStripMenuItem.Enabled = false;
			}
		}

		private void ToolStripMenuItem_Click_DataRead( object sender, EventArgs e ) {
			var outData = new OutFileDataSet();
			outData.AllDatas();
		}
	}

	/// <summary>
	///     情報格納場所
	/// </summary>
	/// <summary>
	///     条件（）
	/// </summary>
	public class Flags
	{
		/// <summary>
		/// </summary>
		public bool finish = false, date = true, unLimited = true, memo1 = true, memo2 = true, memo3 = true, emergency = true,
			check = true;

		public int locationX = 100, locationY = 100;
	}

	/// <summary>
	///     XMLの読み込みとか
	/// </summary>
	public class SettingConfig
	{
		public const string FILE_NAME = "Config.xml";

		/// <summary>
		///     XML読み込み
		/// </summary>
		/// <returns></returns>
		public Flags Read() {
			Flags f;
			try {
				var seri = new XmlSerializer( typeof ( Flags ) );
				var fs = new FileStream( FILE_NAME, FileMode.Open );
				f = (Flags)seri.Deserialize( fs );
				fs.Close();
			}
			catch ( Exception ) {
				f = new Flags();
				Write( f );
				return Read();
			}
			return f;
		}

		/// <summary>
		/// </summary>
		/// <param name="f"></param>
		public void Write( Flags f ) {
			var seri = new XmlSerializer( typeof ( Flags ) );
			var fs = new FileStream( FILE_NAME, FileMode.Create );
			seri.Serialize( fs, f );
			fs.Close();
		}

		/// <summary>
		/// </summary>
		/// <param name="num"></param>
		/// <param name="flag"></param>
		/// <returns></returns>
		public bool Check( ColorStatus num, Flags flag ) {
			return num != ColorStatus.DefColor;
		}
	}
}