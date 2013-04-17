using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;


namespace EasyToDo
{

	public partial class Form3 : Form
	{
		Flags f ;
		useXML uXml = new useXML();

		public Form3()
		{
			InitializeComponent();
			f = uXml.read();
			this.checkBox1.Checked = f.finish;
			this.checkBox2.Checked = f.date;
			this.checkBox3.Checked = f.unLimited;
			this.checkBox4.Checked = f.memo1;
			this.checkBox5.Checked = f.memo2;
			this.checkBox6.Checked = f.memo3;
			this.checkBox7.Checked = f.emergency;
			this.checkBox8.Checked = f.check;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			f.finish	= this.checkBox1.Checked;
			f.date		= this.checkBox2.Checked;
			f.unLimited = this.checkBox3.Checked;
			f.memo1		= this.checkBox4.Checked;
			f.memo2		= this.checkBox5.Checked;
			f.memo3		= this.checkBox6.Checked;
			f.emergency = this.checkBox7.Checked;
			f.check		= this.checkBox8.Checked;
			uXml.write( f );
			this.Close();
		}



	}
}
