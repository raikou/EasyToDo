using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Windows;
using System.Xml;

//master + dev//master + testnamespace EasyToDo
//test

namespace EasyToDo
{
	public enum COLOR_FLAG{
		FINISH=0,
		EMERGENCY,
		MEMO1,
		DATE,
		MEMO2,
		UNLIMITED,
		MEMO3,

		OTHER
	};

	public partial class Form1 : Form
	{

		Datas data = new Datas();
		Flags flags = new Flags();
		useXML uXml = new useXML();

		/// <summary>
		/// 初期化
		/// </summary>
		public Form1()
		{
			InitializeComponent();
			flags = uXml.read();
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point( flags.Location_x, flags.Location_y );

			string title;
			title = System.Environment.CurrentDirectory;
			System.IO.DirectoryInfo di = new DirectoryInfo( title );
			string version="";
			if( flags.check ){
				System.Diagnostics.FileVersionInfo ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
				version = "　　v" + ver.FileVersion;
			}
			this.Text = "EasyToDo in " + di.Name + version;
		}

		/// <summary>
		/// 再描画関数
		/// </summary>
		private void reDraw()
		{
			try{
			listView1.Items.Clear();
			string[] str = new string[3];

			for( int i =0; i < data.data.Count; i++)
			{
				//フィルターに引っかかる物は表示しない
				if( uXml.check( data.data[i].now_status, flags ) != true ){
					continue;
				}

				str[0] = data.data[i].getStatusTextForDisp();
				str[1] = data.data[i].name;
				str[2] = data.data[i].memo;
				listView1.Items.Add( new ListViewItem(str) );

				int item_num = listView1.Items.Count-1;
				//int item_num = data.same_data(listView1.Items[i].SubItems[1].Text, listView1.Items[i].SubItems[0].Text);

				//色設定
				switch ( data.data[ i ].now_status )
				{
					case COLOR_FLAG.FINISH:
						//return "無期限";
						listView1.Items[item_num].BackColor = Color.White;
						listView1.Items[item_num].ForeColor = Color.Gray;
						break;
					case COLOR_FLAG.DATE:
						//日付カラー
						DateTime t, tt;
						t = DateTime.Today;
						tt = this.data.data[ i ].limitDate;
						
						if( DateTime.Today.Date >= this.data.data[ i ].limitDate.Date ){
							listView1.Items[item_num].BackColor = Color.Red;
							listView1.Items[item_num].ForeColor = Color.White;
						}
						else if (DateTime.Today.AddDays(3).Date >= this.data.data[i].limitDate.Date)
						{
							listView1.Items[item_num].BackColor = Color.Orange;
							listView1.Items[item_num].ForeColor = Color.Black;
						}
						else
						{
							listView1.Items[item_num].BackColor = Color.White;
							listView1.Items[item_num].ForeColor = Color.Black;
						}
						break;
					case COLOR_FLAG.UNLIMITED:
						//return "無期限";
						listView1.Items[item_num].BackColor = Color.White;
						listView1.Items[item_num].ForeColor = Color.Blue;
						break;
					case COLOR_FLAG.MEMO1:
						//return "メモ１";
						listView1.Items[item_num].BackColor = Color.Aquamarine;
						listView1.Items[item_num].ForeColor = Color.Black;
						break;
					case COLOR_FLAG.MEMO2:
						//return "メモ２";
						listView1.Items[item_num].BackColor = Color.GreenYellow;
						listView1.Items[item_num].ForeColor = Color.Black;
						break;
					case COLOR_FLAG.MEMO3:
						//return "メモ３";
						listView1.Items[item_num].BackColor = Color.LightGreen;
						listView1.Items[item_num].ForeColor = Color.Black;
						break;
					case COLOR_FLAG.EMERGENCY:
						//return "緊急";
						listView1.Items[item_num].BackColor = Color.White;
						listView1.Items[item_num].ForeColor = Color.Red;
						break;
				}
			}
			}catch( Exception e ){
				MessageBox.Show(e.Message);
			}
			
			//緊急度の高い順にソート（now_statusの番号の若い順）
			//DataComparer comp = new DataComparer();
			//data.data.Sort( comp );

			data.MySort();
		}

		/// <summary>
		/// データ読み込み部・・・のはず。
		/// 再描画を行っている
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e">エラーデータ</param>
		private void Form1_Load(object sender, EventArgs e)
		{
			//データの読み込み
			data.read_data();
			data.MySort();

			//画面への描画
			this.reDraw();
		}

		/// <summary>
		/// 項目ダブルクリック時の動作
		/// change()を呼び出す。
		/// </summary>
		/// <param name="sender">しるか！</param>
		/// <param name="e"></param>
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			this.change();
		}

		/// <summary>
		/// 表示内容の変更を行う。
		/// </summary>
		private void change()
		{
			int num = listView1.SelectedItems[0].Index;
			if( num < data.data.Count ){

				num = data.same_data(listView1.Items[num].SubItems[1].Text, listView1.Items[num].SubItems[0].Text);

				//生成
				Form2 form2 = new Form2(data.data[num].name, data.data[num].memo, data.data[num].getStatusTextForDisp());
				form2.ShowDialog( this );

			
				if( form2.status ){
					//データの更新
					data.data[num].setStatus( form2.date );
					data.data[num].name = form2.name;
					data.data[num].memo = form2.memo;
			
					//画面へ描画
					data.MySort();
					this.reDraw();
					data.write_data();
				}
				//殺す
				form2.Dispose();
			}
		}

		/// <summary>
		/// 新規作成
		/// </summary>
		public void create()
		{
			Form2 form2 = new Form2("", "", DateTime.Today.ToString());
			form2.ShowDialog( this );

			if( form2.status ){
				//データの更新
				Data tD = new Data();
				//data.add( form2.name, form2.memo, data.data[0].status( form2.date ) );
				data.add(form2.name, form2.memo, tD.status(form2.date));
				int num = data.data.Count - 1;
				if( data.data[ num ].now_status == COLOR_FLAG.DATE ) data.data[ num ].setStatus( form2.date );

				//画面へ描画
				data.MySort();
				this.reDraw();
				data.write_data();
			}
			//殺す
			form2.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="num"></param>
		public void delete( int num)
		{
			if( num < data.data.Count ){
				data.data.Remove( data.data[ data.same_data( listView1.Items[num].SubItems[1].Text, listView1.Items[num].SubItems[0].Text ) ] );
				//画面へ描画
				data.MySort();
				this.reDraw();
				data.write_data();
			}
		}

		private void newMenuItem_Click(object sender, EventArgs e)
		{
			this.create();
		}

		private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.create();
		}

		private void 変更ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.change();
		}

		private void 削除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int num = listView1.SelectedItems[0].Index;
			DialogResult re = MessageBox.Show(this, "削除しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
			if( re == System.Windows.Forms.DialogResult.Yes ){
				this.delete( num );
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try{
			int num = listView1.SelectedItems[0].Index;
				if( num < data.data.Count ){

					num = data.same_data(listView1.Items[num].SubItems[1].Text, listView1.Items[num].SubItems[0].Text) ;

					//変更
					data.data[num].setStatus("完了");
					data.data[num].exitDate = DateTime.Today;
			
					//画面へ描画
					data.MySort();
					this.reDraw();
					data.write_data();
				}

			}catch(Exception ex){
				MessageBox.Show( ex.Message );
			}
		}

		private void フィルタ設定ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form3 f = new Form3();
			f.ShowDialog( this );

			f.Close();

			flags = uXml.read();
			this.reDraw();
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			try{
				//↓のように設定していけば設定が保存される 場所：Form1.Designer.cs
				global::EasyToDo.Properties.Settings.Default.ColumnIndex_0 = this.columnHeader1.DisplayIndex;
				global::EasyToDo.Properties.Settings.Default.ColumnWidth_0 = this.columnHeader1.Width;
				global::EasyToDo.Properties.Settings.Default.ColumnIndex_1 = this.columnHeader2.DisplayIndex;
				global::EasyToDo.Properties.Settings.Default.ColumnWidth_1 = this.columnHeader2.Width;
				global::EasyToDo.Properties.Settings.Default.ColumnIndex_3 = this.columnHeader3.DisplayIndex;
				global::EasyToDo.Properties.Settings.Default.ColumnWidht_3 = this.columnHeader3.Width;

				//Locationの保存
				flags.Location_x = this.Location.X;
				flags.Location_y = this.Location.Y;

				uXml.write( flags );

				//最後に保存
				Properties.Settings.Default.Save();
			}catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void 全データToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OutFileDataSet outData = new OutFileDataSet();
			outData.AllDataTitle( data );
		}

		private void バージョン情報ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.FileVersionInfo ver =
				System.Diagnostics.FileVersionInfo.GetVersionInfo(	System.Reflection.Assembly.GetExecutingAssembly().Location);
			MessageBox.Show("バージョン："+ver.FileVersion.ToString(),"バージョン情報");
		}

		private void Form1_Activated(object sender, EventArgs e)
		{
			this.reDraw();
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			try{
				int num = listView1.SelectedItems[0].Index;
				if( num < data.data.Count ){
					this.変更ToolStripMenuItem.Enabled = true;
					this.toolStripMenuItem1.Enabled = true;
					this.削除ToolStripMenuItem.Enabled = true;

				}
			}catch{
				this.変更ToolStripMenuItem.Enabled = false;
				this.toolStripMenuItem1.Enabled = false;
				this.削除ToolStripMenuItem.Enabled = false;
			}
		}

		private void 全データ内容込ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OutFileDataSet outData = new OutFileDataSet();
			outData.AllDatas( data );
		}
	}


	public class Data
	{
		//構造情報
		public COLOR_FLAG now_status;
		public DateTime limitDate,createDate, exitDate;
		public string name, memo;


		/// <summary>
		/// now_statusに対応する文字列を返す
		/// -1：完了
		/// ０：緊急
		/// １：日付
		/// ２：無期限
		/// ３：メモ１
		/// ４：メモ２
		/// ５：メモ３
		/// </summary>
		/// <returns>期限の文字列</returns>
		public string getStatusTextForDisp()
		{
			switch (now_status)
			{
				case COLOR_FLAG.FINISH:
					return "完了";
				case COLOR_FLAG.DATE:
					//　月/日　を返すように変更
					string str="";
					str += this.limitDate.Month + "/" + this.limitDate.Day;
					return str;
					//return this.limitDate.ToShortDateString();
				case COLOR_FLAG.UNLIMITED:
					return "無期限";
				case COLOR_FLAG.MEMO1:
					return "メモ１";
				case COLOR_FLAG.MEMO2:
					return "メモ２";
				case COLOR_FLAG.MEMO3:
					return "メモ３";
				case COLOR_FLAG.EMERGENCY:
					return "緊急";
				default:
					MessageBox.Show(" getStatus においてエラーが発生しました。 ");
					return "";
			}
		}

		/// <summary>
		/// 文字列を入力すると自動的にステータスを設定する
		/// このデータはgetStatus()で読み出せる
		/// </summary>
		/// <param name="status_string">日付または定めた文字列</param>
		public void setStatus(string status_string)
		{
			now_status = this.status( status_string );
			if (now_status == COLOR_FLAG.DATE)
			{
				limitDate = DateTime.Parse(status_string);
			}
		}

		public COLOR_FLAG status(string status_string)
		{
			COLOR_FLAG now;
			if (status_string == "緊急")
			{
				now = COLOR_FLAG.EMERGENCY;
			}
			else if (status_string == "メモ１")
			{
				now = COLOR_FLAG.MEMO1;
			}
			else if (status_string == "メモ２")
			{
				now = COLOR_FLAG.MEMO2;
			}
			else if (status_string == "メモ３")
			{
				now = COLOR_FLAG.MEMO3;
			}
			else if (status_string == "無期限")
			{
				now = COLOR_FLAG.UNLIMITED;
			}
			else if (status_string == "完了")
			{
				now = COLOR_FLAG.FINISH;
			}
			else
			{
				//例外データが来たときの処理を核必要あり：正規表現
				now = COLOR_FLAG.DATE;
			}
			return now;
		}
	}
	public class Datas
	{
		//データ
		public System.Collections.Generic.List<Data> data = new List<Data>();

		//保存ファイル名
		string file_name = "data.csv";

		//改行コード
		public const string enter_str = "<br>";

		/// <summary>
		/// データの追加
		/// ※
		/// </summary>
		/// <param name="name">やること</param>
		/// <param name="memo">メモ</param>
		/// <param name="now_status">期限の状態</param>
		public void add(string name, string memo, COLOR_FLAG now_status)
		{
			Data temp = new Data();
			temp.createDate = DateTime.Now;
			temp.limitDate = DateTime.Now;
			temp.exitDate = DateTime.Now;
			temp.name = name;
			temp.memo = memo;
			temp.now_status = now_status;

			this.data.Add(temp);
			this.MySort();
		}


		/// <summary>
		/// データの書き出し
		/// </summary>
		public void write_data()
		{
			StreamWriter sw = new StreamWriter(file_name, false, System.Text.Encoding.GetEncoding("shift_jis"));
			string str;
			for (int num = 0; num < data.Count; num++)
			{
				str = data[num].createDate + ","
						+ data[num].limitDate + ","
						+ data[num].exitDate + ","
						+ data[num].now_status + ","
						+ data[num].name + ","
						+ data[num].memo.Replace(Environment.NewLine, Datas.enter_str);
				sw.WriteLine(str);
			}

			sw.Close();
		}

		/// <summary>
		/// データの読み込み
		/// </summary>
		public void read_data()
		{
			string str;
			string[] sstr;


			if( File.Exists( file_name ) ){
			}else{
				FileStream fs = new FileStream( file_name, FileMode.Create );
				fs.Close();
			}
			StreamReader sr = new StreamReader(file_name, System.Text.Encoding.GetEncoding("shift_jis"));
			
			while (sr.Peek() > -1)
			{
				str = sr.ReadLine();
				sstr = str.Split(',');
				Data temp = new Data();
				temp.createDate = DateTime.Parse(sstr[0]);
				temp.limitDate = DateTime.Parse(sstr[1]);
				temp.exitDate = DateTime.Parse(sstr[2]);
				if( COLOR_FLAG.DATE.ToString() == sstr[3] ){
					temp.now_status = COLOR_FLAG.DATE;
				}
				else if (COLOR_FLAG.EMERGENCY.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.EMERGENCY;
				}
				else if (COLOR_FLAG.FINISH.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.FINISH;
				}
				else if (COLOR_FLAG.MEMO1.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.MEMO1;
				}
				else if (COLOR_FLAG.MEMO2.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.MEMO2;
				}
				else if (COLOR_FLAG.MEMO3.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.MEMO3;
				}
				else if (COLOR_FLAG.UNLIMITED.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.UNLIMITED;
				}
				else if (COLOR_FLAG.OTHER.ToString() == sstr[3])
				{
					temp.now_status = COLOR_FLAG.OTHER;
				}
				temp.name = sstr[4];
				temp.memo = sstr[5];
				for(int i=6; i < sstr.Length; i++ ){
					temp.memo += ",";
					temp.memo += sstr[ i ];
				}

				temp.memo = temp.memo.Replace("<br>", Environment.NewLine );

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
		public int same_data(string tname, string tdate)
		{
			for (int i = 0; i < data.Count; i++)
			{
				if (tdate == data[i].getStatusTextForDisp())
				{
					if ( tname == data[i].name)
					{
						//data.data.Remove( data.data[num] );
						//画面へ描画
						return i;
					}
				}
			}
			return -1;
		}

		public void MySort()
		{
			bool flag;
			Data temp;
			while(true){
				flag = true;

				for( int i=0; i<data.Count - 1; i++){
					if( (int)data[i].now_status > (int)data[i+1].now_status ){
						flag = false;
						temp = data[i];
						data[i] = data[i+1];
						data[i+1] = temp;
					}else if( (int)data[i].now_status == (int)data[i+1].now_status ){
						if (data[i].limitDate > data[i + 1].limitDate)
						{
							flag = false;
							temp = data[i];
							data[i] = data[i + 1];
							data[i + 1] = temp;
						}
					}
				}

				if(flag) break;
			}
		}
	}

	public class Flags
	{
		public bool finish = false, date = true, unLimited = true, memo1 = true, memo2 = true, memo3 = true, emergency = true, check = true;
		public int Location_x=100, Location_y=100;
	}

	public class useXML
	{
		public const string fileName = "Config.xml";
		
		public Flags read()
		{
			Flags f;

			try{
				System.Xml.Serialization.XmlSerializer seri = new System.Xml.Serialization.XmlSerializer(typeof(Flags));
				FileStream fs = new FileStream(fileName, FileMode.Open);
				f = (Flags) seri.Deserialize(fs);
				fs.Close();
			}catch( Exception e){
				f = new Flags();
				this.write(f);
				return this.read();
			}
			return f;
		}

		public void write( Flags f)
		{
			System.Xml.Serialization.XmlSerializer seri = new System.Xml.Serialization.XmlSerializer( typeof( Flags ) );
			FileStream fs = new FileStream( fileName, FileMode.Create );
			seri.Serialize( fs, f );
			fs.Close();
		}

		public bool check( COLOR_FLAG num, Flags flag )
		{
			switch (num)
			{
				case COLOR_FLAG.FINISH:
					if( flag.finish ) return true;
					break;
				case COLOR_FLAG.EMERGENCY:
					if( flag.emergency ) return true;
					break;
				case COLOR_FLAG.DATE:
					if( flag.date ) return true;
					break;
				case COLOR_FLAG.UNLIMITED:
					if( flag.unLimited ) return true;
					break;
				case COLOR_FLAG.MEMO1:
					if( flag.memo1 ) return true;
					break;
				case COLOR_FLAG.MEMO2:
					if( flag.memo2 ) return true;
					break;
				case COLOR_FLAG.MEMO3:
					if( flag.memo3 ) return true;
					break;
			}
		

			return false;
		}

	}
}
