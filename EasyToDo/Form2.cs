using System;
using System.Windows.Forms;

namespace EasyToDo
{
	/// <summary>
	/// ToDoの詳細を書き込むダイアログ
	/// </summary>
	public partial class Form2 : Form
	{
#pragma warning disable 1591
		public string date, name, memo;
		public string check=""; // 期限を一時的に保存する物
		public bool status = false; //true:OK false:キャンセル

		public Form2()
		{
			InitializeComponent();
		}
		public Form2( string name, string memo, string date )
		{
			InitializeComponent();
			this.name = name;
			this.memo = memo;
			this.date = date;

		}
#pragma warning restore 1591

		private void Form2_Load(object sender, EventArgs e)
		{
			try{
				this.textBox1.Text = this.name;
				this.textBox2.Text = this.memo;
				if( this.date == "緊急")
				{
					this.radioButton2.Checked = true;
				}
				else if (this.date == "完了")
				{
					//完了の場合は表示を無期限に自動的にする
					this.radioButton3.Checked = true;
				}
				else if (this.date == "無期限")
				{
					this.radioButton3.Checked = true;
				}
				else if (this.date == "メモ１")
				{
					this.radioButton4.Checked = true;
				}
				else if (this.date == "メモ２")
				{
					this.radioButton5.Checked = true;
				}
				else if (this.date == "メモ３")
				{
					this.radioButton6.Checked = true;
				}
				else
				{
					this.radioButton1.Checked = true;
					this.dateTimePicker1.Text = this.date;
				}
			}catch(Exception ex ){
				MessageBox.Show( ex.Message );
			}
		}

		private void OK_Button_Click(object sender, EventArgs e)
		{
			this.Save();
		}
		private void Save()
		{
			this.status = true;
			this.name = this.textBox1.Text;
			this.memo = this.textBox2.Text;
			this.date = this.check;
			this.Close();
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.check = this.dateTimePicker1.Text;
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			this.check = this.dateTimePicker1.Text;
			radioButton1.Checked = true;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.check = "緊急";
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			this.check = "無期限";
		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{
			this.check = "メモ１";
		}

		private void radioButton5_CheckedChanged(object sender, EventArgs e)
		{
			this.check = "メモ２";
		}

		private void radioButton6_CheckedChanged(object sender, EventArgs e)
		{
			this.check = "メモ３";
		}

		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			//this.save();
			this.Close();
		}

		private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.Save();
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if( e.KeyChar == (char)Keys.Return ){
				this.Save();
			}
		}

		private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void Form2_Shown(object sender, EventArgs e)
		{
			this.textBox1.Focus();
		}


	}
}
