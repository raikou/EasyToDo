namespace EasyToDo
{
	partial class Form1
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.FileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.名前を付けて保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.他の保存ファイル読込ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DataOutPutEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.FilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DataOutPutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AllDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AllDataInfoInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listDataSet = new EasyToDo.ListDataSet();
			this.listDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.menuStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.listDataSet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.listDataBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
			this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileFToolStripMenuItem,
            this.DataOutPutEToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(284, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// FileFToolStripMenuItem
			// 
			this.FileFToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.FileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.名前を付けて保存ToolStripMenuItem,
            this.他の保存ファイル読込ToolStripMenuItem,
            this.toolStripSeparator3,
            this.testToolStripMenuItem});
			this.FileFToolStripMenuItem.Name = "FileFToolStripMenuItem";
			this.FileFToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
			this.FileFToolStripMenuItem.Text = "ファイル(&F)";
			// 
			// 名前を付けて保存ToolStripMenuItem
			// 
			this.名前を付けて保存ToolStripMenuItem.Name = "名前を付けて保存ToolStripMenuItem";
			this.名前を付けて保存ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.名前を付けて保存ToolStripMenuItem.Text = "名前を付けて保存";
			this.名前を付けて保存ToolStripMenuItem.Click += new System.EventHandler(this.AddNameSaveToolStripMenuItem_Click);
			// 
			// 他の保存ファイル読込ToolStripMenuItem
			// 
			this.他の保存ファイル読込ToolStripMenuItem.Name = "他の保存ファイル読込ToolStripMenuItem";
			this.他の保存ファイル読込ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.他の保存ファイル読込ToolStripMenuItem.Text = "ファイル読込";
			this.他の保存ファイル読込ToolStripMenuItem.Click += new System.EventHandler(this.DataLoadToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
			// 
			// testToolStripMenuItem
			// 
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.testToolStripMenuItem.ShowShortcutKeys = false;
			this.testToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.testToolStripMenuItem.Text = "項目の追加(&N)";
			this.testToolStripMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
			// 
			// DataOutPutEToolStripMenuItem
			// 
			this.DataOutPutEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterToolStripMenuItem,
            this.DataOutPutToolStripMenuItem});
			this.DataOutPutEToolStripMenuItem.Name = "DataOutPutEToolStripMenuItem";
			this.DataOutPutEToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
			this.DataOutPutEToolStripMenuItem.Text = "オプション";
			// 
			// FilterToolStripMenuItem
			// 
			this.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem";
			this.FilterToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.FilterToolStripMenuItem.Text = "フィルタ設定";
			this.FilterToolStripMenuItem.Click += new System.EventHandler(this.FilterSetteingToolStripMenuItem_Click);
			// 
			// DataOutPutToolStripMenuItem
			// 
			this.DataOutPutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AllDataToolStripMenuItem,
            this.AllDataInfoInToolStripMenuItem});
			this.DataOutPutToolStripMenuItem.Name = "DataOutPutToolStripMenuItem";
			this.DataOutPutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.DataOutPutToolStripMenuItem.Text = "データ出力";
			// 
			// AllDataToolStripMenuItem
			// 
			this.AllDataToolStripMenuItem.Name = "AllDataToolStripMenuItem";
			this.AllDataToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.AllDataToolStripMenuItem.Text = "表示データ（項目のみ）";
			this.AllDataToolStripMenuItem.Click += new System.EventHandler(this.AllDataToolStripMenuItem_Click);
			// 
			// AllDataInfoInToolStripMenuItem
			// 
			this.AllDataInfoInToolStripMenuItem.Name = "AllDataInfoInToolStripMenuItem";
			this.AllDataInfoInToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
			this.AllDataInfoInToolStripMenuItem.Text = "表示データ（内容込）";
			this.AllDataInfoInToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click_DataRead);
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listView1.AllowColumnReorder = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.listView1.ContextMenuStrip = this.contextMenuStrip1;
			this.listView1.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::EasyToDo.Properties.Settings.Default, "ListLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			this.listView1.LabelWrap = false;
			this.listView1.Location = global::EasyToDo.Properties.Settings.Default.ListLocation;
			this.listView1.Margin = new System.Windows.Forms.Padding(0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(284, 238);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.DisplayIndex = global::EasyToDo.Properties.Settings.Default.ColumnIndex_0;
			this.columnHeader1.Text = "期限";
			this.columnHeader1.Width = global::EasyToDo.Properties.Settings.Default.ColumnWidth_0;
			// 
			// columnHeader2
			// 
			this.columnHeader2.DisplayIndex = global::EasyToDo.Properties.Settings.Default.ColumnIndex_1;
			this.columnHeader2.Text = "やること";
			this.columnHeader2.Width = global::EasyToDo.Properties.Settings.Default.ColumnWidth_1;
			// 
			// columnHeader3
			// 
			this.columnHeader3.DisplayIndex = global::EasyToDo.Properties.Settings.Default.ColumnIndex_3;
			this.columnHeader3.Text = "メモ";
			this.columnHeader3.Width = global::EasyToDo.Properties.Settings.Default.ColumnWidht_3;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.NewToolStripMenuItem,
            this.changeToolStripMenuItem,
            this.toolStripSeparator2,
            this.DeleteToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(173, 104);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Enabled = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.toolStripMenuItem1.Text = "完了";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
			// 
			// NewToolStripMenuItem
			// 
			this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
			this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.NewToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.NewToolStripMenuItem.Text = "新規作成";
			this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewCreateToolStripMenuItem_Click);
			// 
			// changeToolStripMenuItem
			// 
			this.changeToolStripMenuItem.Enabled = false;
			this.changeToolStripMenuItem.Name = "changeToolStripMenuItem";
			this.changeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.changeToolStripMenuItem.Text = "変更";
			this.changeToolStripMenuItem.Click += new System.EventHandler(this.ChangeToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
			// 
			// DeleteToolStripMenuItem
			// 
			this.DeleteToolStripMenuItem.Enabled = false;
			this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
			this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.DeleteToolStripMenuItem.Text = "削除";
			this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleateToolStripMenuItem_Click);
			// 
			// listDataSet
			// 
			this.listDataSet.DataSetName = "ListDataSet";
			this.listDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// listDataBindingSource
			// 
			this.listDataBindingSource.DataMember = "ListData";
			this.listDataBindingSource.DataSource = this.listDataSet;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = global::EasyToDo.Properties.Settings.Default.MyClientSize;
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.menuStrip1);
			this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::EasyToDo.Properties.Settings.Default, "MyClientSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(100, 100);
			this.Name = "Form1";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "EasyToDo";
			this.TopMost = true;
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.listDataSet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.listDataBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem FileFToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DataOutPutEToolStripMenuItem;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem FilterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DataOutPutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AllDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AllDataInfoInToolStripMenuItem;
		private ListDataSet listDataSet;
		private System.Windows.Forms.BindingSource listDataBindingSource;
		private System.Windows.Forms.ToolStripMenuItem 他の保存ファイル読込ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem 名前を付けて保存ToolStripMenuItem;

	}
}

