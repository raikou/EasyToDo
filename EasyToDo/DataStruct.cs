using System;
using System.Data;
using System.IO;
using DisplayColorUsing;

namespace EasyToDo
{
	/// <summary>
	///     データの格納、保存、読込を行う
	/// </summary>
	internal class DataStruct
	{
		/// <summary>
		///     保存データ
		/// </summary>
		private static ListDataSet _listDataSet = new ListDataSet();

		public DataView TableView
		{
			get
			{
				DataView temp = _listDataSet.Tables[ "ListData" ].DefaultView;
				temp.Sort = "status";
				temp.RowFilter = "status <> " + (int)DisplayColor.DefColor.ColorFlag;

				Flags flags = new SettingConfig().Read();
				if( flags.finish == false ) temp.RowFilter += " and status <> " + (int)DisplayColor.Finish.ColorFlag;
				if( !flags.emergency ) temp.RowFilter += " and status <> " + (int)DisplayColor.Emergency.ColorFlag;
				if( !flags.date ) temp.RowFilter += " and status <> " + (int)DisplayColor.Date.ColorFlag;
				if( !flags.memo1 ) temp.RowFilter += " and status <> " + (int)DisplayColor.Memo1.ColorFlag;
				if( !flags.memo2 ) temp.RowFilter += " and status <> " + (int)DisplayColor.Memo2.ColorFlag;
				if( !flags.memo3 ) temp.RowFilter += " and status <> " + (int)DisplayColor.Memo3.ColorFlag;
				if( !flags.unLimited ) temp.RowFilter += " and status <> " + (int)DisplayColor.Unlimited.ColorFlag;
				return temp;
			}
		}

		/// <summary>
		///     保存するファイル名
		///     ※拡張子は不要
		/// </summary>
		public string FileName
		{
			get
			{
				Flags flags = new SettingConfig().Read();
				return flags.dataFileName;
			}
			set
			{
				SettingConfig settingConfig = new SettingConfig();
				Flags flags = settingConfig.Read();
				flags.dataFileName = value;
				settingConfig.Write(flags);
			}
		}

		/// <summary>
		///     データの追加
		/// </summary>
		/// <param name="name">やること</param>
		/// <param name="memo">メモ</param>
		/// <param name="status">設定するColorStatusを入れる</param>
		/// <param name="dateTime">期限の曜日を入れる</param>
		public void Add(string name, string memo, ColorStatus status, DateTime dateTime)
		{
			DataRow row = _listDataSet.Tables[ "ListData" ].NewRow();
			row[ "name" ] = name;
			row[ "memo" ] = memo;
			row[ "status" ] = Convert.ToInt32(status);
			row[ "limitDate" ] = dateTime;
			row[ "createDate" ] = DateTime.Now;
			_listDataSet.Tables[ "ListData" ].Rows.Add(row);
		}

		public void Delete(string _name, string _memo, ColorStatus _status, DateTime _dateTime)
		{
			for( int i = 0; i < _listDataSet.Tables.Count; i++ ) if( _listDataSet.Tables[ "ListData" ].Rows[ i ][ "name" ].ToString() == _name ) if( _listDataSet.Tables[ "ListData" ].Rows[ i ][ "memo" ].ToString() == _memo ) if( _listDataSet.Tables[ "ListData" ].Rows[ i ][ "status" ].ToString() == _status.ToString() ) _listDataSet.Tables[ "ListData" ].Rows.RemoveAt(i);
			ListDataSet.ListDataRow row = (ListDataSet.ListDataRow)_listDataSet.Tables[ "ListData" ].NewRow();
			row.name = _name;
			row.memo = _memo;
			row.status = Convert.ToInt32(_status);
			row.limitDate = _dateTime;
			row.createDate = DateTime.Now;
			_listDataSet.Tables[ "ListData" ].Rows.Add(row);
		}

		/// <summary>
		///     データの書き出し
		/// </summary>
		public void write_data()
		{
			_listDataSet.WriteXml(this.FileName);
		}

		/// <summary>
		///     データの読み込み
		/// </summary>
		public void read_data()
		{
			if( File.Exists(this.FileName) ) _listDataSet.ReadXml(this.FileName);
			else {
				_listDataSet = new ListDataSet();
				_listDataSet.WriteXml(this.FileName);
			}
		}
	}
}