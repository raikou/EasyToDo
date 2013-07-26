using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace EasyToDo
{
	/// <summary>
	/// 
	/// </summary>
	public class OutFileDataSet
	{
		readonly UseXml _uXml = new UseXml();
		Flags _flags = new Flags();

		/// <summary>
		/// ファイル保存メッセージ＆自動実行
		/// </summary>
		/// <param name="fileName"></param>
		private void SaveFile(string fileName)
		{
			MessageBox.Show(fileName.ToString(CultureInfo.InvariantCulture) + "に保存しました。");
			Process.Start(fileName.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// 選択データのタイトルをファイルに出力する。
		/// </summary>
		/// <param name="data">項目データ</param>
		public void AllDataTitle(Datas data)
		{
			SaveFileDialog sfd = new SaveFileDialog();

			_flags = _uXml.Read();

			sfd.FileName = "全データタイトル一覧.txt";
			if (sfd.ShowDialog() == DialogResult.OK)
			{

				StreamWriter ofs = new StreamWriter(sfd.FileName);
				foreach (Data dataDisp in data.data)
				{
//フィルターに引っかかる物は表示しない
					if (_uXml.Check(dataDisp.nowStatus, _flags) != true)
					{
						continue;
					}

					//ofs.WriteLine("{0}\t:{1}", data.data[i].getStatus().ToString(), data.data[i].name.ToString());
					ofs.WriteLine("{1}（期限：{0}）", dataDisp.GetStatusTextForDisp().ToString(CultureInfo.InvariantCulture), dataDisp.name.ToString(CultureInfo.InvariantCulture));
				}
				ofs.Close();

				//MessageBox.Show(sfd.FileName.ToString()+"に保存しました。");
				SaveFile(sfd.FileName.ToString(CultureInfo.InvariantCulture));

			}

		}

		/// <summary>
		/// 選択データのタイトル及び内容を出力する。
		/// タイトルのみは AllDataTitle を利用する。
		/// </summary>
		/// <param name="data">項目データ</param>
		public void AllDatas(Datas data)
		{
			SaveFileDialog sfd = new SaveFileDialog();

			_flags = _uXml.Read();


			sfd.FileName = "全データ一覧.txt";
			if (sfd.ShowDialog() == DialogResult.OK)
			{

				StreamWriter ofs = new StreamWriter(sfd.FileName);
				foreach (Data dataDisp in data.data)
				{
//フィルターに引っかかる物は表示しない
					if (_uXml.Check(dataDisp.nowStatus, _flags) != true)
					{
						continue;
					}


					//ofs.WriteLine("{0}\t:{1}", data.data[i].getStatus().ToString(), data.data[i].name.ToString());
					ofs.Write("【{0}】【{1}】", dataDisp.GetStatusTextForDisp().ToString(CultureInfo.InvariantCulture), dataDisp.name.ToString(CultureInfo.InvariantCulture));
					ofs.Write(Environment.NewLine);
					ofs.Write("（作成日：{0}）", dataDisp.createDate.ToString(CultureInfo.InvariantCulture));
					ofs.Write("（期限：{0}）", dataDisp.limitDate.ToString(CultureInfo.InvariantCulture));
					ofs.Write("（終了日：{0}）", dataDisp.exitDate.ToString(CultureInfo.InvariantCulture));
					ofs.Write(Environment.NewLine);
					ofs.Write("【メモ】" + Environment.NewLine);

					ofs.Write("\t{0}", dataDisp.memo.Replace(Environment.NewLine, Environment.NewLine + "\t").ToString(CultureInfo.InvariantCulture));
					ofs.Write(Environment.NewLine);
					ofs.Write(Environment.NewLine);
				}
				ofs.Close();
				//MessageBox.Show(sfd.FileName.ToString() + "に保存しました。");
				SaveFile(sfd.FileName.ToString(CultureInfo.InvariantCulture));
			}
		}
	}
}
