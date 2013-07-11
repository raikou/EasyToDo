using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EasyToDo
{
	public partial class OutFileDataSet
	{
		useXML uXml = new useXML();
		Flags flags = new Flags();

		/// <summary>
		/// ファイル保存メッセージ＆自動実行
		/// </summary>
		/// <param name="file_name"></param>
		private void SaveFile( string file_name )
		{
			MessageBox.Show(file_name.ToString() + "に保存しました。");
			Process.Start( file_name.ToString() );


		}

		/// <summary>
		/// 選択データのタイトルをファイルに出力する。
		/// </summary>
		/// <param name="data">項目データ</param>
		public void AllDataTitle( Datas data )
		{
			SaveFileDialog sfd = new SaveFileDialog();

			flags = uXml.read();

			sfd.FileName = "全データタイトル一覧.txt";
			if( sfd.ShowDialog() == DialogResult.OK ){

				StreamWriter ofs = new StreamWriter( sfd.FileName );
				for( int i=0; i<data.data.Count; i++){

					//フィルターに引っかかる物は表示しない
					if (uXml.check(data.data[i].now_status, flags) != true)
					{
						continue;
					}

					//ofs.WriteLine("{0}\t:{1}", data.data[i].getStatus().ToString(), data.data[i].name.ToString());
					ofs.WriteLine("{1}（期限：{0}）", data.data[i].getStatusTextForDisp().ToString(), data.data[i].name.ToString());
				}
				ofs.Close();
				
				//MessageBox.Show(sfd.FileName.ToString()+"に保存しました。");
				SaveFile(sfd.FileName.ToString());

			}

		}

		/// <summary>
		/// 選択データのタイトル及び内容を出力する。
		/// タイトルのみは AllDataTitle を利用する。
		/// </summary>
		/// <param name="data">項目データ</param>
		public void AllDatas( Datas data )
		{
			SaveFileDialog sfd = new SaveFileDialog();

			flags = uXml.read();


			sfd.FileName = "全データ一覧.txt";
			if (sfd.ShowDialog() == DialogResult.OK)
			{

				StreamWriter ofs = new StreamWriter(sfd.FileName);
				for (int i = 0; i < data.data.Count; i++)
				{
					//フィルターに引っかかる物は表示しない
					if (uXml.check(data.data[i].now_status, flags) != true)
					{
						continue;
					}

					//ofs.WriteLine("{0}\t:{1}", data.data[i].getStatus().ToString(), data.data[i].name.ToString());
					ofs.Write("【{0}】【{1}】", data.data[i].getStatusTextForDisp().ToString(), data.data[i].name.ToString());
					ofs.Write(Environment.NewLine);
					ofs.Write("（作成日：{0}）", data.data[i].createDate.ToString());
					ofs.Write("（期限：{0}）", data.data[i].limitDate.ToString());
					ofs.Write("（終了日：{0}）", data.data[i].exitDate.ToString());
					ofs.Write(Environment.NewLine);
					ofs.Write("【メモ】" + Environment.NewLine);

					ofs.Write("\t{0}", data.data[i].memo.Replace(Environment.NewLine, Environment.NewLine + "\t").ToString());
					ofs.Write(Environment.NewLine);
					ofs.Write(Environment.NewLine);
				}
				ofs.Close();
				//MessageBox.Show(sfd.FileName.ToString() + "に保存しました。");
				SaveFile(sfd.FileName.ToString());

			}
		}
	}
}
