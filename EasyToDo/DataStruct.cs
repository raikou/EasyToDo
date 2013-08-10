using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DisplayColorUsing;

namespace EasyToDo
{
	/// <summary>
	///     データの格納、保存、読込を行う
	/// </summary>
	internal class DataStruct
	{
		private string m_fileSaveDir;
		public DataStruct() {
			m_fileSaveDir = Directory.GetCurrentDirectory();
			m_fileSaveDir += "\\\\";
		}
		/// <summary>
		///     保存データ
		/// </summary>
		private static ListDataSet m_listDataSet = new ListDataSet();

		private string m_mpMFileName;

		public DataView TableView {
			get {
				DataView temp = m_listDataSet.Tables[ "ListData" ].DefaultView;
				temp.Sort = "status";
				return temp;
			}
		}

		/// <summary>
		///     保存するファイル名
		///     ※拡張子は不要
		/// </summary>
		public string FileName {
			get { return m_mpMFileName + ".xml"; }
			set { m_mpMFileName = value; }
		}

		/// <summary>
		///     データの追加
		/// </summary>
		/// <param name="_name">やること</param>
		/// <param name="_memo">メモ</param>
		/// <param name="_status">設定するColorStatusを入れる</param>
		/// <param name="_dateTime">期限の曜日を入れる</param>
		public void Add( string _name, string _memo, ColorStatus _status, DateTime _dateTime ) {
			var row = m_listDataSet.Tables[ "ListData" ].NewRow();
			row["name"] = _name;
			row["memo"] = _memo;
			row["status"] = Convert.ToInt32( _status );
			row["limitDate"] = _dateTime;
			row["createDate"] = DateTime.Now;
			m_listDataSet.Tables[ "ListData" ].Rows.Add( row );
		}

		public void Delete( string _name, string _memo, ColorStatus _status, DateTime _dateTime ) {
			for ( int i = 0; i < m_listDataSet.Tables.Count; i++ ) {
				if ( m_listDataSet.Tables["ListData"].Rows[i]["name"].ToString() == _name ) {
					if (m_listDataSet.Tables["ListData"].Rows[i]["memo"].ToString() == _memo)
					{
						if (m_listDataSet.Tables["ListData"].Rows[i]["status"].ToString() == _status.ToString())
						{
							m_listDataSet.Tables["ListData"].Rows.RemoveAt( i );
						}
					}
				}
			}
			var row = (ListDataSet.ListDataRow)m_listDataSet.Tables[ "ListData" ].NewRow();
			row.name = _name;
			row.memo = _memo;
			row.status = Convert.ToInt32(_status);
			row.limitDate = _dateTime;
			row.createDate = DateTime.Now;
			m_listDataSet.Tables[ "ListData" ].Rows.Add( row );
		}

		/// <summary>
		///     データの書き出し
		/// </summary>
		public void write_data() { m_listDataSet.WriteXml(m_fileSaveDir+ FileName ); }

		/// <summary>
		///     データの読み込み
		/// </summary>
		public void read_data() {
			FileName = "Defoult";
			if ( File.Exists(m_fileSaveDir+ FileName ) ) m_listDataSet.ReadXml(m_fileSaveDir+ FileName );
			else {
				m_listDataSet = new ListDataSet();
				m_listDataSet.WriteXml(m_fileSaveDir+ FileName );
			}
		}
	}
}