using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
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
		public Form1()
		{
			this.InitializeComponent();
			this.m_flags = this.m_settingConfig.Read();
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(this.m_flags.locationX, this.m_flags.locationY);
			string title = Environment.CurrentDirectory;
			DirectoryInfo di = new DirectoryInfo(title);
			string version = "";
			if( this.m_flags.check ) {
				FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
				version = "　　v" + ver.FileVersion;
			}
			this.Text = "EasyToDo in " + di.Name + version;
		}

		/// <summary>
		///     再描画関数
		/// </summary>
		private void ReDraw()
		{
			try {
				this.listView1.Items.Clear();
				string[] str = new string[3];
				DataView dataView = this.m_dataStruct.TableView;
				for( int i = 0; i < dataView.Count; i++ ) {
					string test = dataView[ i ][ "status" ].ToString();
					ColorStatus colorStatus = DisplayColor.GetColorStatusInt(int.Parse(test));
					if( colorStatus == ColorStatus.Date ) str[ 0 ] = ( (DateTime)dataView[ i ][ "limitDate" ] ).ToString("MM/dd");
					else str[ 0 ] = DisplayColor.GetDispName(colorStatus);
					str[ 1 ] = dataView[ i ][ "name" ].ToString();
					str[ 2 ] = dataView[ i ][ "memo" ].ToString();

					//listViewに追加
					this.listView1.Items.Add(new ListViewItem(str));

					//色設定
					if( colorStatus == ColorStatus.Date ) {
						this.listView1.Items[ i ].BackColor = DisplayColor.Date.GetForeColor((DateTime)dataView[ i ][ "limitDate" ]);
						this.listView1.Items[ i ].ForeColor = DisplayColor.Date.GetBackColor((DateTime)dataView[ i ][ "limitDate" ]);
					} else {
						this.listView1.Items[ i ].BackColor = DisplayColor.BackColor(colorStatus);
						this.listView1.Items[ i ].ForeColor = DisplayColor.ForeColor(colorStatus);
					}
				}
			} catch( Exception e ) {
				MessageBox.Show(e.Message);
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
		private void Form1_Load(object sender, EventArgs e)
		{
			//データの読み込み
			this.m_dataStruct.read_data();
			//画面への描画
			this.ReDraw();
		}

		/// <summary>
		///     項目ダブルクリック時の動作
		///     change()を呼び出す。
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e"></param>
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			this.Change();
		}

		/// <summary>
		///     表示内容の変更を行う。
		/// </summary>
		private void Change()
		{
			DataView dataView = this.m_dataStruct.TableView;
			int num = this.listView1.SelectedItems[ 0 ].Index;
			if( num < dataView.Count ) {
				Form2 form2 = new Form2(dataView[ num ][ "name" ].ToString(),
					dataView[ num ][ "memo" ].ToString(),
					DisplayColor.GetColorStatusInt(int.Parse(dataView[ num ][ "status" ].ToString())),
					(DateTime)dataView[ num ][ "limitDate" ]);
				form2.ShowDialog(this);

				if( form2.status ) {
					//データ更新
					dataView[ num ][ "name" ] = form2.name;
					dataView[ num ][ "memo" ] = form2.memo;
					dataView[ num ][ "limitDate" ] = form2.date;
					dataView[ num ][ "status" ] = form2.colorStatus;
				}
				int dataVewNum = 0;
				for( ; dataVewNum < dataView.Count; ) {
					if( this.listView1.Items[ num ].SubItems[ 1 ].Text == (string)dataView[ dataVewNum ][ "name" ] ) if( this.listView1.Items[ num ].SubItems[ 2 ].Text == (string)dataView[ dataVewNum ][ "memo" ] ) break;
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
		public void Create()
		{
			Form2 form2 = new Form2("", "", DisplayColor.Date.ColorFlag, DateTime.Today);
			form2.ShowDialog(this);
			if( form2.status ) {
				this.m_dataStruct.Add(form2.name, form2.memo, form2.colorStatus, form2.date);
				this.ReDraw();
				this.m_dataStruct.write_data();
			}
			//殺す
			form2.Dispose();
		}

		/// <summary>
		///     削除
		/// </summary>
		/// <param name="_num"></param>
		public void Delete(int _num)
		{
			DataView dataView = this.m_dataStruct.TableView;
			dataView.Delete(_num);
		}

		private void newMenuItem_Click(object sender, EventArgs e) { this.Create(); }

		private void NewCreateToolStripMenuItem_Click(object sender, EventArgs e) { this.Create(); }

		private void ChangeToolStripMenuItem_Click(object sender, EventArgs e) { this.Change(); }

		private void DeleateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int num = this.listView1.SelectedItems[ 0 ].Index;
			DialogResult re = MessageBox.Show(this, "削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if( re == DialogResult.Yes ) this.Delete(num);
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try {
				DataView dataView = this.m_dataStruct.TableView;
				int num = this.listView1.SelectedItems[ 0 ].Index;
				if( num < this.m_dataStruct.TableView.Count ) {
					dataView[ num ][ "status" ] = (int)( DisplayColor.Finish.ColorFlag );
					dataView[ num ][ "exitDate" ] = DateTime.Now;
					this.m_dataStruct.write_data();
					this.ReDraw();
				}
			} catch( Exception ex ) {
				MessageBox.Show(ex.Message);
			}
		}

		private void FilterSetteingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form3 f = new Form3();
			f.ShowDialog(this);
			f.Close();
			this.m_flags = this.m_settingConfig.Read();
			this.ReDraw();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			try {
				//↓のように設定していけば設定が保存される 場所：Form1.Designer.cs
				Settings.Default.ColumnIndex_0 = this.columnHeader1.DisplayIndex;
				Settings.Default.ColumnWidth_0 = this.columnHeader1.Width;
				Settings.Default.ColumnIndex_1 = this.columnHeader2.DisplayIndex;
				Settings.Default.ColumnWidth_1 = this.columnHeader2.Width;
				Settings.Default.ColumnIndex_3 = this.columnHeader3.DisplayIndex;
				Settings.Default.ColumnWidht_3 = this.columnHeader3.Width;
				//Locationの保存
				this.m_flags.locationX = this.Location.X;
				this.m_flags.locationY = this.Location.Y;
				this.m_settingConfig.Write(this.m_flags);
				//最後に保存
				Settings.Default.Save();
			} catch( Exception ex ) {
				MessageBox.Show(ex.Message);
			}
		}

		private void AllDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OutFileDataSet outData = new OutFileDataSet();
			outData.AllDataTitle();
		}

		private void ToolStripMenuItem_Click_Ver_Info(object sender, EventArgs e)
		{
			FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			MessageBox.Show("バージョン：" + ver.FileVersion.ToString(CultureInfo.InvariantCulture), "バージョン情報");
		}

		private void Form1_Activated(object sender, EventArgs e) { this.ReDraw(); }

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			try {
				int num = this.listView1.SelectedItems[ 0 ].Index;
				if( num < this.m_dataStruct.TableView.Count ) {
					this.changeToolStripMenuItem.Enabled = true;
					this.toolStripMenuItem1.Enabled = true;
					this.DeleteToolStripMenuItem.Enabled = true;
				}
			} catch {
				this.changeToolStripMenuItem.Enabled = false;
				this.toolStripMenuItem1.Enabled = false;
				this.DeleteToolStripMenuItem.Enabled = false;
			}
		}

		private void ToolStripMenuItem_Click_DataRead(object sender, EventArgs e)
		{
			OutFileDataSet outData = new OutFileDataSet();
			outData.AllDatas();
		}

		private void DataLoadToolStripMenuItem_Click(object sender, EventArgs e) { }

		private void AddNameSaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SettingConfig settingConfig = new SettingConfig();
			Flags flags = settingConfig.Read();
		}
	}
}
