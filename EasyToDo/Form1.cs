using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using DisplayColorUsing;

namespace EasyToDo
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class Form1 : Form
	{
		private readonly Datas _data = new Datas();
		private Flags _flags = new Flags();
		private readonly UseXml _uXml = new UseXml();

		/// <summary>
		/// 初期化
		/// </summary>
		public Form1() {
			InitializeComponent();
			_flags = _uXml.Read();
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(_flags.locationX, _flags.locationY);

			string title = Environment.CurrentDirectory;
			DirectoryInfo di = new DirectoryInfo(title);
			string version = "";
			if (_flags.check) {
				FileVersionInfo ver = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
				version = "　　v" + ver.FileVersion;
			}
			this.Text = "EasyToDo in " + di.Name + version;
		}

		/// <summary>
		/// 再描画関数
		/// </summary>
		private void ReDraw() {
			Color c = DisplayColor.Finish.ForeColor;
			try {
				listView1.Items.Clear();
				string[] str = new string[3];

				for (int i = 0; i < _data.data.Count; i++) {
					//フィルターに引っかかる物は表示しない
					if (_uXml.Check(_data.data[i].nowStatus, _flags) != true) continue;

					str[0] = _data.data[i].GetStatusTextForDisp();
					str[1] = _data.data[i].name;
					str[2] = _data.data[i].memo;
					listView1.Items.Add(new ListViewItem(str));

					int itemNum = listView1.Items.Count - 1;
					//int item_num = data.same_data(listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[0].Text);

					//色設定
					if (_data.data[i].nowStatus == DisplayColor.Date.ColorFlag) {
						listView1.Items[itemNum].BackColor = DisplayColor.Date.getForeColor(this._data.data[i].limitDate.Date);
						listView1.Items[itemNum].ForeColor = DisplayColor.Date.getBackColor(this._data.data[i].limitDate.Date);
					} else {
						listView1.Items[itemNum].BackColor = DisplayColor.ForeColor(this._data.data[i].nowStatus);
						listView1.Items[itemNum].ForeColor = DisplayColor.ForeColor(this._data.data[i].nowStatus);
					}
				}
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}

			//緊急度の高い順にソート（now_statusの番号の若い順）
			//DataComparer comp = new DataComparer();
			//data.data.Sort( comp );

			_data.MySort();
		}

		/// <summary>
		/// データ読み込み部・・・のはず。
		/// 再描画を行っている
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e">エラーデータ</param>
		private void Form1_Load(object sender, EventArgs e) {
			//データの読み込み
			_data.read_data();
			_data.MySort();

			//画面への描画
			this.ReDraw();
		}

		/// <summary>
		/// 項目ダブルクリック時の動作
		/// change()を呼び出す。
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e"></param>
		private void listView1_DoubleClick(object sender, EventArgs e) {
			this.Change();
		}

		/// <summary>
		/// 表示内容の変更を行う。
		/// </summary>
		private void Change() {
			int num = listView1.SelectedItems[0].Index;
			if (num < _data.data.Count) {
				num = _data.same_data(listView1.Items[num].SubItems[1].Text, listView1.Items[num].SubItems[0].Text);

				//生成
				Form2 form2 = new Form2(_data.data[num].name, _data.data[num].memo, _data.data[num].GetStatusTextForDisp());
				form2.ShowDialog(this);

				if (form2.status) {
					//データの更新
					_data.data[num].SetStatus(form2.date);
					_data.data[num].name = form2.name;
					_data.data[num].memo = form2.memo;

					//画面へ描画
					_data.MySort();
					this.ReDraw();
					_data.write_data();
				}
				//殺す
				form2.Dispose();
			}
		}

		//TODO:何故か新規作成されない
		/// <summary>
		/// 新規作成
		/// </summary>
		public void Create() {
			Form2 form2 = new Form2("", "", DateTime.Today.ToString(CultureInfo.InvariantCulture));
			form2.ShowDialog(this);

			if (form2.status) {
				//データの更新
				Data tD = new Data();
				//data.add( form2.name, form2.memo, data.data[0].status( form2.date ) );
				_data.Add(form2.name, form2.memo, tD.Status(form2.date));
				int num = _data.data.Count - 1;

				//日付情報設定
				_data.data[num].nowStatus = DisplayColor.Date.ColorFlag;
				_data.data[num].SetStatus(form2.date);

				//画面へ描画
				_data.MySort();
				this.ReDraw();
				_data.write_data();
			}
			//殺す
			form2.Dispose();
		}

		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="num"></param>
		public void Delete(int num) {
			if (num < _data.data.Count) {
				_data.data.Remove(
					_data.data[_data.same_data(listView1.Items[num].SubItems[1].Text, listView1.Items[num].SubItems[0].Text)]);
				//画面へ描画
				_data.MySort();
				this.ReDraw();
				_data.write_data();
			}
		}

		private void newMenuItem_Click(object sender, EventArgs e) { this.Create(); }

		private void NewCreateToolStripMenuItem_Click(object sender, EventArgs e) { this.Create(); }

		private void ChangeToolStripMenuItem_Click(object sender, EventArgs e) { this.Change(); }

		private void DeleateToolStripMenuItem_Click(object sender, EventArgs e) {
			int num = listView1.SelectedItems[0].Index;
			DialogResult re = MessageBox.Show(this, "削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (re == DialogResult.Yes) this.Delete(num);
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e) {
			try {
				int num = listView1.SelectedItems[0].Index;
				if (num < _data.data.Count) {
					num = _data.same_data(listView1.Items[num].SubItems[1].Text, listView1.Items[num].SubItems[0].Text);

					//変更
					_data.data[num].SetStatus("完了");
					_data.data[num].exitDate = DateTime.Today;

					//画面へ描画
					_data.MySort();
					this.ReDraw();
					_data.write_data();
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void フィルタ設定ToolStripMenuItem_Click(object sender, EventArgs e) {
			Form3 f = new Form3();
			f.ShowDialog(this);

			f.Close();

			_flags = _uXml.Read();
			this.ReDraw();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
			try {
				//↓のように設定していけば設定が保存される 場所：Form1.Designer.cs
				Properties.Settings.Default.ColumnIndex_0 = this.columnHeader1.DisplayIndex;
				Properties.Settings.Default.ColumnWidth_0 = this.columnHeader1.Width;
				Properties.Settings.Default.ColumnIndex_1 = this.columnHeader2.DisplayIndex;
				Properties.Settings.Default.ColumnWidth_1 = this.columnHeader2.Width;
				Properties.Settings.Default.ColumnIndex_3 = this.columnHeader3.DisplayIndex;
				Properties.Settings.Default.ColumnWidht_3 = this.columnHeader3.Width;

				//Locationの保存
				_flags.locationX = this.Location.X;
				_flags.locationY = this.Location.Y;

				_uXml.Write(_flags);

				//最後に保存
				Properties.Settings.Default.Save();
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void 全データToolStripMenuItem_Click(object sender, EventArgs e) {
			OutFileDataSet outData = new OutFileDataSet();
			outData.AllDataTitle(_data);
		}

		private void ToolStripMenuItem_Click_Ver_Info(object sender, EventArgs e) {
			System.Diagnostics.FileVersionInfo ver =
				System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
			MessageBox.Show("バージョン：" + ver.FileVersion.ToString(CultureInfo.InvariantCulture), "バージョン情報");
		}

		private void Form1_Activated(object sender, EventArgs e) { this.ReDraw(); }

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
			try {
				int num = listView1.SelectedItems[0].Index;
				if (num < _data.data.Count) {
					this.変更ToolStripMenuItem.Enabled = true;
					this.toolStripMenuItem1.Enabled = true;
					this.削除ToolStripMenuItem.Enabled = true;
				}
			} catch {
				this.変更ToolStripMenuItem.Enabled = false;
				this.toolStripMenuItem1.Enabled = false;
				this.削除ToolStripMenuItem.Enabled = false;
			}
		}

		private void ToolStripMenuItem_Click_DataRead(object sender, EventArgs e) {
			OutFileDataSet outData = new OutFileDataSet();
			outData.AllDatas(_data);
		}
	}

	/// <summary>
	/// 情報格納場所
	/// </summary>
	public class Data
	{
		/// <summary>
		/// 色のフラグ保持
		/// </summary>
		public ColorStatus nowStatus;
		/// <summary>
		/// データの「制限日時」「作成日時」「完了日時」
		/// </summary>
		public DateTime limitDate, createDate, exitDate;
		/// <summary>
		/// リストに表示される名称
		/// </summary>
		public string name;

		/// <summary>
		/// メモに表示される内容
		/// </summary>
		public string memo;

		/// <summary>
		/// now_statusに対応する文字列を返す
		/// </summary>
		/// <returns>期限の文字列</returns>
		public string GetStatusTextForDisp() {
			return DisplayColor.GetDispName( nowStatus );
		}

		/// <summary>
		/// 文字列を入力すると自動的にステータスを設定する
		/// このデータはgetStatus()で読み出せる
		/// </summary>
		/// <param name="statusString">日付または定めた文字列</param>
		public void SetStatus(string statusString) {
			nowStatus = this.Status(statusString);
			if (nowStatus == DisplayColor.Date.ColorFlag) limitDate = DateTime.Parse(statusString);
		}

		/// <summary>
		/// 引数のステータスを判別する
		/// </summary>
		/// <param name="statusString"></param>
		/// <returns></returns>
		public ColorStatus Status(string statusString) {
			return DisplayColor.GetColorStatus( statusString );
		}
	}

	/// <summary>
	/// 全情報を持つクラス。
	/// </summary>
	public class Datas
	{
		/// <summary>
		/// データ
		/// </summary>
		public List<Data> data = new List<Data>();

		//保存ファイル名
		private const string FileName = "data.csv";

		/// <summary>
		/// 改行コード
		/// </summary>
		public const string ENTER_STR = "<br>";

		/// <summary>
		/// データの追加
		/// ※
		/// </summary>
		/// <param name="name">やること</param>
		/// <param name="memo">メモ</param>
		/// <param name="nowStatus">期限の状態</param>
		public void Add(string name, string memo, ColorStatus nowStatus) {
			Data temp = new Data {
				createDate = DateTime.Now,
				limitDate = DateTime.Now,
				exitDate = DateTime.Now,
				name = name,
				memo = memo,
				nowStatus = nowStatus
			};

			this.data.Add(temp);
			this.MySort();
		}

		/// <summary>
		/// データの書き出し
		/// </summary>
		public void write_data() {
			StreamWriter sw = new StreamWriter(FileName, false, System.Text.Encoding.GetEncoding("shift_jis"));
			foreach (Data dataInfo in data) {
				string str = dataInfo.createDate + ","
				             + dataInfo.limitDate + ","
				             + dataInfo.exitDate + ","
				             + dataInfo.nowStatus + ","
				             + dataInfo.name + ","
				             + dataInfo.memo.Replace(Environment.NewLine, ENTER_STR);
				sw.WriteLine(str);
			}

			sw.Close();
		}

		/// <summary>
		/// データの読み込み
		/// </summary>
		public void read_data() {
			if (File.Exists(FileName)) {} else {
				FileStream fs = new FileStream(FileName, FileMode.Create);
				fs.Close();
			}
			StreamReader sr = new StreamReader(FileName, System.Text.Encoding.GetEncoding("shift_jis"));

			while (sr.Peek() > -1) {
				string str = sr.ReadLine();
				Debug.Assert(str != null, "str != null");
				string[] sstr = str.Split(',');
				Data temp = new Data {
					createDate = DateTime.Parse(sstr[0]),
					limitDate = DateTime.Parse(sstr[1]),
					exitDate = DateTime.Parse(sstr[2])
				};
				temp.nowStatus = DisplayColor.GetColorStatus(sstr[3]);
				temp.name = sstr[4];
				temp.memo = sstr[5];
				for (int i = 6; i < sstr.Length; i++) {
					temp.memo += ",";
					temp.memo += sstr[i];
				}

				temp.memo = temp.memo.Replace("<br>", Environment.NewLine);

				this.data.Add(temp);
			}
			sr.Close();
		}

		/// <summary>
		/// 同じ内容のデータ番号を返す。
		/// 一致するデータがない場合-1を返す。
		/// </summary>
		/// <param name="tname">listView1.Items[num].SubItems[1].Text</param>
		/// <param name="tdate">listView1.Items[num].SubItems[0].Text</param>
		public int same_data(string tname, string tdate) {
			for (int i = 0; i < data.Count; i++) {
				if (tdate == data[i].GetStatusTextForDisp()) {
					if (tname == data[i].name) {
						//data.data.Remove( data.data[num] );
						//画面へ描画
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>
		/// このクラスにおける独自ソート
		/// </summary>
		public void MySort() {
			while (true) {
				bool flag = true;

				for (int i = 0; i < data.Count - 1; i++) {
					Data temp;
					if ((int) data[i].nowStatus > (int) data[i + 1].nowStatus) {
						flag = false;
						temp = data[i];
						data[i] = data[i + 1];
						data[i + 1] = temp;
					} else if ((int) data[i].nowStatus == (int) data[i + 1].nowStatus) {
						if (data[i].limitDate > data[i + 1].limitDate) {
							flag = false;
							temp = data[i];
							data[i] = data[i + 1];
							data[i + 1] = temp;
						}
					}
				}

				if (flag) break;
			}
		}
	}

	/// <summary>
	/// 条件（）
	/// </summary>
	public class Flags
	{
		/// <summary>
		/// 
		/// </summary>
		public bool finish = false, date = true, unLimited = true, memo1 = true, memo2 = true, memo3 = true, emergency = true,
			check = true;
#pragma warning disable 1591
		public int locationX = 100, locationY = 100;
#pragma warning restore 1591
	}

	/// <summary>
	/// XMLの読み込みとか
	/// </summary>
	public class UseXml
	{
#pragma warning disable 1591
		public const string FILE_NAME = "Config.xml";
#pragma warning restore 1591

		/// <summary>
		/// XML読み込み
		/// </summary>
		/// <returns></returns>
		public Flags Read() {
			Flags f;

			try {
				System.Xml.Serialization.XmlSerializer seri = new System.Xml.Serialization.XmlSerializer(typeof (Flags));
				FileStream fs = new FileStream(FILE_NAME, FileMode.Open);
				f = (Flags) seri.Deserialize(fs);
				fs.Close();
			} catch (Exception) {
				f = new Flags();
				this.Write(f);
				return this.Read();
			}
			return f;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f"></param>
		public void Write(Flags f) {
			System.Xml.Serialization.XmlSerializer seri = new System.Xml.Serialization.XmlSerializer(typeof (Flags));
			FileStream fs = new FileStream(FILE_NAME, FileMode.Create);
			seri.Serialize(fs, f);
			fs.Close();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="num"></param>
		/// <param name="flag"></param>
		/// <returns></returns>
		public bool Check(ColorStatus num, Flags flag) {
			if (num == ColorStatus.DefColor) return false;
			return true;
		}
	}
}
