﻿using System;
using System.Windows.Forms;
using DisplayColorUsing;

namespace EasyToDo
{
	/// <summary>
	/// ToDoの詳細を書き込むダイアログ
	/// </summary>
	public partial class Form2 :Form
	{
		/// <summary>
		/// 色情報
		/// </summary>
		public ColorStatus colorStatus;

		/// <summary>
		/// 時間
		/// </summary>
		public DateTime date;

		/// <summary>
		/// メモ情報
		/// </summary>
		public string memo;

		/// <summary>
		/// 名称
		/// </summary>
		public string name;

		/// <summary>
		/// 押したボタンの判定
		/// </summary>
		public bool status = false; //true:OK false:キャンセル

		/// <summary>
		/// 予定の詳細を決める画面
		/// </summary>
		/// <param name="_name">表示する内容を記述</param>
		/// <param name="_memo">表示するメモの内容を記述</param>
		/// <param name="_colorStatus">設定する色情報を設定</param>
		/// <param name="_date">日付を設定</param>
		public Form2(string _name, string _memo, ColorStatus _colorStatus, DateTime _date)
		{
			if( _name == null ) throw new ArgumentNullException("_name");
			this.InitializeComponent();
			this.name = _name;
			this.memo = _memo;
			this.colorStatus = _colorStatus;
			this.date = _date;
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			try {
				this.textBox1.Text = this.name;
				this.textBox2.Text = this.memo;
				if( this.colorStatus == DisplayColor.Emergency.ColorFlag ) this.radioButton2.Checked = true;
				else if( this.colorStatus == DisplayColor.Finish.ColorFlag ) {
					//完了の場合は表示を無期限に自動的にする
					this.radioButton3.Checked = true;
				} else if( this.colorStatus == DisplayColor.Unlimited.ColorFlag ) this.radioButton3.Checked = true;
				else if( this.colorStatus == DisplayColor.Memo1.ColorFlag ) this.radioButton4.Checked = true;
				else if( this.colorStatus == DisplayColor.Memo2.ColorFlag ) this.radioButton5.Checked = true;
				else if( this.colorStatus == DisplayColor.Memo3.ColorFlag ) this.radioButton6.Checked = true;
				else {
					this.radioButton1.Checked = true;
					this.dateTimePicker1.Value = this.date;
				}
			} catch( Exception ex ) {
				MessageBox.Show(ex.Message);
			}
		}

		private void OK_Button_Click(object sender, EventArgs e) { this.Save(); }

		private void Save()
		{
			this.status = true;
			this.name = this.textBox1.Text;
			this.memo = this.textBox2.Text;
			this.date = this.dateTimePicker1.Value;
			this.Close();
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e) { this.date = this.dateTimePicker1.Value; }

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			this.radioButton1.Checked = true;
			this.colorStatus = DisplayColor.Date.ColorFlag;
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			this.colorStatus = DisplayColor.Emergency.ColorFlag;
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			this.colorStatus = DisplayColor.Unlimited.ColorFlag;
		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{
			this.colorStatus = DisplayColor.Memo1.ColorFlag;
		}

		private void radioButton5_CheckedChanged(object sender, EventArgs e)
		{
			this.colorStatus = DisplayColor.Memo2.ColorFlag;
		}

		private void radioButton6_CheckedChanged(object sender, EventArgs e)
		{
			this.colorStatus = DisplayColor.Memo3.ColorFlag;
		}

		private void Cancel_Button_Click(object sender, EventArgs e) { this.Close(); }

		private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e) { this.Save(); }

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if( e.KeyChar == (char)Keys.Return ) this.Save();
		}

		private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) { }

		private void textBox1_KeyDown(object sender, KeyEventArgs e) { }

		private void Form2_Shown(object sender, EventArgs e) { this.textBox1.Focus(); }
	}
}