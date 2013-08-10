using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetLinkToXML
{
	public class DataSetLinkToXML
	{
		/// <summary>
		/// 保存するファイル名を設定する
		///		ファイル名は「 .xml 」が不要
		/// </summary>
		public string SaveFileName {
			get { return _saveFileName+".xml"; }
			set { _saveFileName = value; }
		}
		private string _saveFileName ;

		/// <summary>
		/// データを保存する型構造
		/// </summary>
		public class DataStruct
		{
			 
		}

		public bool SaveDataSetToXML(DataSet saveDataSet) 
		{
			StreamWriter xmlSW = new StreamWriter( SaveFileName );
			saveDataSet.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
			xmlSW.Close(); 
			return true;
		}

		public bool LoadXMLToDataSet(DataSet loadDataSet)
		{
			StreamReader xmlSR = new StreamReader( SaveFileName );
			loadDataSet.ReadXml(xmlSR, XmlReadMode.Auto);
			xmlSR.Close();
			return true;
		}
		
	}
}
