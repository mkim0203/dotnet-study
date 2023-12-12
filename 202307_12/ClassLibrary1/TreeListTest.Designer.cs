
namespace ClassLibrary1
{
    partial class TreeListTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treelistMain = new DevExpress.XtraTreeList.TreeList();
            this.treeListBand3 = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListBand1 = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.treeListBand2 = new DevExpress.XtraTreeList.Columns.TreeListBand();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.treelistMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // treelistMain
            // 
            this.treelistMain.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.treeListBand3,
            this.treeListBand1,
            this.treeListBand2});
            this.treelistMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn7});
            this.treelistMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.treelistMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treelistMain.FixedLineWidth = 3;
            this.treelistMain.Location = new System.Drawing.Point(0, 73);
            this.treelistMain.Margin = new System.Windows.Forms.Padding(4);
            this.treelistMain.MinWidth = 27;
            this.treelistMain.Name = "treelistMain";
            this.treelistMain.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            this.treelistMain.OptionsSelection.KeepSelectedOnClick = false;
            this.treelistMain.OptionsSelection.MultiSelect = true;
            this.treelistMain.OptionsSelection.MultiSelectMode = DevExpress.XtraTreeList.TreeListMultiSelectMode.CellSelect;
            this.treelistMain.OptionsSelection.UseIndicatorForSelection = true;
            this.treelistMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.treelistMain.Size = new System.Drawing.Size(1337, 763);
            this.treelistMain.TabIndex = 11;
            this.treelistMain.TreeLevelWidth = 24;
            this.treelistMain.TopVisibleNodeIndexChanged += new System.EventHandler(this.treelistMain_TopVisibleNodeIndexChanged);
            // 
            // treeListBand3
            // 
            this.treeListBand3.Caption = "고정";
            this.treeListBand3.Columns.Add(this.treeListColumn1);
            this.treeListBand3.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.treeListBand3.MinWidth = 48;
            this.treeListBand3.Name = "treeListBand3";
            this.treeListBand3.Width = 830;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Job Title";
            this.treeListColumn1.FieldName = "JobTitle";
            this.treeListColumn1.MinWidth = 44;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 412;
            // 
            // treeListBand1
            // 
            this.treeListBand1.Caption = "Band";
            this.treeListBand1.Columns.Add(this.treeListColumn2);
            this.treeListBand1.Columns.Add(this.treeListColumn4);
            this.treeListBand1.MinWidth = 64;
            this.treeListBand1.Name = "treeListBand1";
            this.treeListBand1.Width = 591;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "First Name";
            this.treeListColumn2.FieldName = "FirstName";
            this.treeListColumn2.MinWidth = 27;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 418;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Birth Date";
            this.treeListColumn4.ColumnEdit = this.repositoryItemDateEdit1;
            this.treeListColumn4.FieldName = "BirthDate";
            this.treeListColumn4.MinWidth = 27;
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 2;
            this.treeListColumn4.Width = 591;
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // treeListBand2
            // 
            this.treeListBand2.Caption = "Etc";
            this.treeListBand2.Columns.Add(this.treeListColumn6);
            this.treeListBand2.Columns.Add(this.treeListColumn5);
            this.treeListBand2.Columns.Add(this.treeListColumn3);
            this.treeListBand2.Columns.Add(this.treeListColumn7);
            this.treeListBand2.MinWidth = 64;
            this.treeListBand2.Name = "treeListBand2";
            this.treeListBand2.Width = 2027;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Origin City";
            this.treeListColumn6.FieldName = "City";
            this.treeListColumn6.MinWidth = 27;
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 3;
            this.treeListColumn6.Width = 420;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Origin State";
            this.treeListColumn5.FieldName = "StateProvinceName";
            this.treeListColumn5.MinWidth = 27;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 4;
            this.treeListColumn5.Width = 420;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Last Name";
            this.treeListColumn3.FieldName = "LastName";
            this.treeListColumn3.MinWidth = 27;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 5;
            this.treeListColumn3.Width = 596;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Phone";
            this.treeListColumn7.FieldName = "Phone";
            this.treeListColumn7.MinWidth = 27;
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 6;
            this.treeListColumn7.Width = 591;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1337, 73);
            this.textBox1.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(666, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TreeListTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 836);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treelistMain);
            this.Controls.Add(this.textBox1);
            this.Name = "TreeListTest";
            this.Text = "TreeListTest";
            ((System.ComponentModel.ISupportInitialize)(this.treelistMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private DevExpress.XtraTreeList.TreeList treelistMain;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand3;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand1;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}