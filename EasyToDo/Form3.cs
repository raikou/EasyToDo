using System;
using System.Windows.Forms;


namespace EasyToDo
{

	/// <summary>
	/// 
	/// </summary>
	public partial class Form3 : Form
	{
		readonly Flags _flags ;
		readonly UseXml _uXml = new UseXml();

		/// <summary>
		/// 
		/// </summary>
		public Form3()
		{
			InitializeComponent();
			_flags = _uXml.Read();
			this.checkBox1.Checked = _flags.finish;
			this.checkBox2.Checked = _flags.date;
			this.checkBox3.Checked = _flags.unLimited;
			this.checkBox4.Checked = _flags.memo1;
			this.checkBox5.Checked = _flags.memo2;
			this.checkBox6.Checked = _flags.memo3;
			this.checkBox7.Checked = _flags.emergency;
			this.checkBox8.Checked = _flags.check;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_flags.finish	= this.checkBox1.Checked;
			_flags.date		= this.checkBox2.Checked;
			_flags.unLimited = this.checkBox3.Checked;
			_flags.memo1		= this.checkBox4.Checked;
			_flags.memo2		= this.checkBox5.Checked;
			_flags.memo3		= this.checkBox6.Checked;
			_flags.emergency = this.checkBox7.Checked;
			_flags.check		= this.checkBox8.Checked;
			_uXml.Write( _flags );
			this.Close();
		}



	}
}
