using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using DisplayColorUsing;

namespace EasyToDo
{
	/// <summary>
	/// ファイル出力を制御する
	/// </summary>
	public class OutFileDataSet
	{
		private readonly DataStruct m_dataStruct = new DataStruct();

		/// <summary>
		///     ファイル保存メッセージ＆自動実行
		/// </summary>
		/// <param name="fileName">保存ファイル名</param>
		private void SaveFile(string fileName)
		{
			MessageBox.Show(fileName.ToString(CultureInfo.InvariantCulture) + "に保存しました。");
			Process.Start(fileName.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		///     選択データのタイトルをファイルに出力する。
		/// </summary>
		public void AllDataTitle()
		{
			SaveFileDialog sfd = new SaveFileDialog { FileName = "全データタイトル一覧.txt" };

			if( sfd.ShowDialog() == DialogResult.OK ) {
				ColorStatus colorStatus;
				StreamWriter ofs = new StreamWriter(sfd.FileName);
				foreach( DataRowView dataRowView in this.m_dataStruct.TableView ) {
					colorStatus = DisplayColor.GetColorStatusInt((int)dataRowView[ "status" ]);
					if( colorStatus == DisplayColor.Date.ColorFlag ) ofs.WriteLine("{0}（期限：{1}）", dataRowView[ "name" ], dataRowView[ "limitDate" ]);
					else ofs.WriteLine("{0}（期限：{1}）", dataRowView[ "name" ], DisplayColor.GetDispName(colorStatus));
				}
				ofs.Close();

				this.SaveFile(sfd.FileName.ToString(CultureInfo.InvariantCulture));
			}
		}

		/// <summary>
		///     選択データのタイトル及び内容を出力する。
		///     タイトルのみは AllDataTitle を利用する。
		/// </summary>
		public void AllDatas()
		{
			SaveFileDialog sfd = new SaveFileDialog { FileName = "全データタイトル一覧.txt" };

			if( sfd.ShowDialog() == DialogResult.OK ) {
				ColorStatus colorStatus;
				StreamWriter ofs = new StreamWriter(sfd.FileName);

				foreach( DataRowView dataRowView in this.m_dataStruct.TableView ) {
					colorStatus = DisplayColor.GetColorStatusInt((int)dataRowView[ "status" ]);
					ofs.Write("【{0}】【{1}】", DisplayColor.GetDispName(colorStatus), dataRowView[ "name" ]);
					ofs.Write(Environment.NewLine);
					ofs.Write("（作成日：{0}）", dataRowView[ "createDate" ]);
					if( colorStatus == DisplayColor.Date.ColorFlag ) ofs.Write("（期限：{0}）", dataRowView[ "limitDate" ]);
					if( colorStatus == DisplayColor.Finish.ColorFlag ) ofs.Write("（終了日：{0}）", dataRowView[ "exitDate" ]);
					ofs.Write(Environment.NewLine);
					ofs.Write("【メモ】" + Environment.NewLine);

					ofs.Write("\t{0}",
						( (string)dataRowView[ "memo" ] ).Replace(Environment.NewLine, Environment.NewLine + "\t")
							.ToString(CultureInfo.InvariantCulture));
					ofs.Write(Environment.NewLine);
					ofs.Write(Environment.NewLine);
				}
				ofs.Close();
				this.SaveFile(sfd.FileName.ToString(CultureInfo.InvariantCulture));
			}
		}
	}
}