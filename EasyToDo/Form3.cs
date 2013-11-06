using System;
using System.Windows.Forms;


namespace EasyToDo
{

	/// <summary>
	/// 
	/// </summary>
	public partial class Form3 : Form
	{
		readonly Flags m_flags ;
		readonly SettingConfig m_uXml = new SettingConfig();

		/// <summary>
		/// 
		/// </summary>
		public Form3()
		{
			InitializeComponent();
			m_flags = m_uXml.Read();
			this.checkBox1.Checked = m_flags.finish;
			this.checkBox2.Checked = m_flags.date;
			this.checkBox3.Checked = m_flags.unLimited;
			this.checkBox4.Checked = m_flags.memo1;
			this.checkBox5.Checked = m_flags.memo2;
			this.checkBox6.Checked = m_flags.memo3;
			this.checkBox7.Checked = m_flags.emergency;
			this.checkBox8.Checked = m_flags.check;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			m_flags.finish	= this.checkBox1.Checked;
			m_flags.date		= this.checkBox2.Checked;
			m_flags.unLimited = this.checkBox3.Checked;
			m_flags.memo1		= this.checkBox4.Checked;
			m_flags.memo2		= this.checkBox5.Checked;
			m_flags.memo3		= this.checkBox6.Checked;
			m_flags.emergency = this.checkBox7.Checked;
			m_flags.check		= this.checkBox8.Checked;
			m_uXml.Write( m_flags );
			this.Close();
		}



	}
}
