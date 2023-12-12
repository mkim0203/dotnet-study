
namespace ClassLibrary1
{
    partial class TreeListTest2
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
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.band1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.col1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.band2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.col3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treelistMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // treelistMain
            // 
            this.treelistMain.Bands.AddRange(new DevExpress.XtraTreeList.Columns.TreeListBand[] {
            this.treeListBand3});
            this.treelistMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn7});
            this.treelistMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.treelistMain.Dock = System.Windows.Forms.DockStyle.Left;
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
            this.treelistMain.Size = new System.Drawing.Size(450, 734);
            this.treelistMain.TabIndex = 11;
            this.treelistMain.TreeLevelWidth = 24;
            this.treelistMain.FocusedColumnChanged += new DevExpress.XtraTreeList.FocusedColumnChangedEventHandler(this.treelistMain_FocusedColumnChanged);
            this.treelistMain.SelectionChanged += new System.EventHandler(this.treelistMain_SelectionChanged);
            this.treelistMain.TopVisibleNodeIndexChanged += new System.EventHandler(this.treelistMain_TopVisibleNodeIndexChanged);
            // 
            // treeListBand3
            // 
            this.treeListBand3.Caption = "고정";
            this.treeListBand3.Columns.Add(this.treeListColumn1);
            this.treeListBand3.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            this.treeListBand3.MinWidth = 64;
            this.treeListBand3.Name = "treeListBand3";
            this.treeListBand3.Width = 1107;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Job Title";
            this.treeListColumn1.FieldName = "JobTitle";
            this.treeListColumn1.MinWidth = 44;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 549;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "First Name";
            this.treeListColumn2.FieldName = "FirstName";
            this.treeListColumn2.MinWidth = 27;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            this.treeListColumn2.Width = 557;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Last Name";
            this.treeListColumn3.FieldName = "LastName";
            this.treeListColumn3.MinWidth = 27;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 3;
            this.treeListColumn3.Width = 795;
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
            this.treeListColumn4.Width = 788;
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
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "Origin State";
            this.treeListColumn5.FieldName = "StateProvinceName";
            this.treeListColumn5.MinWidth = 27;
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.Visible = true;
            this.treeListColumn5.VisibleIndex = 2;
            this.treeListColumn5.Width = 560;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.Caption = "Origin City";
            this.treeListColumn6.FieldName = "City";
            this.treeListColumn6.MinWidth = 27;
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.Visible = true;
            this.treeListColumn6.VisibleIndex = 1;
            this.treeListColumn6.Width = 560;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.Caption = "Phone";
            this.treeListColumn7.FieldName = "Phone";
            this.treeListColumn7.MinWidth = 27;
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.Visible = true;
            this.treeListColumn7.VisibleIndex = 4;
            this.treeListColumn7.Width = 788;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1377, 73);
            this.textBox1.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1256, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(450, 73);
            this.gridControl1.MainView = this.gridView;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(927, 734);
            this.gridControl1.TabIndex = 14;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.band1,
            this.band2,
            this.gridBand1});
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7,
            this.bandedGridColumn1});
            this.gridView.GridControl = this.gridControl1;
            this.gridView.Name = "gridView";
            // 
            // band1
            // 
            this.band1.Caption = "band1";
            this.band1.Columns.Add(this.col1);
            this.band1.Columns.Add(this.col4);
            this.band1.MinWidth = 97;
            this.band1.Name = "band1";
            this.band1.Visible = false;
            this.band1.VisibleIndex = -1;
            this.band1.Width = 1408;
            // 
            // col1
            // 
            this.col1.AppearanceCell.BorderColor = System.Drawing.Color.Fuchsia;
            this.col1.AppearanceCell.Options.UseBorderColor = true;
            this.col1.Caption = "col1";
            this.col1.MinWidth = 187;
            this.col1.Name = "col1";
            this.col1.Visible = true;
            this.col1.Width = 704;
            // 
            // col4
            // 
            this.col4.Caption = "col4";
            this.col4.MinWidth = 187;
            this.col4.Name = "col4";
            this.col4.Visible = true;
            this.col4.Width = 704;
            // 
            // band2
            // 
            this.band2.Caption = "band2";
            this.band2.Columns.Add(this.col3);
            this.band2.Columns.Add(this.col7);
            this.band2.MinWidth = 73;
            this.band2.Name = "band2";
            this.band2.Visible = false;
            this.band2.VisibleIndex = -1;
            this.band2.Width = 1408;
            // 
            // col3
            // 
            this.col3.Caption = "col3";
            this.col3.MinWidth = 187;
            this.col3.Name = "col3";
            this.col3.Visible = true;
            this.col3.Width = 704;
            // 
            // col7
            // 
            this.col7.AppearanceCell.Options.UseTextOptions = true;
            this.col7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col7.Caption = "col7";
            this.col7.MinWidth = 187;
            this.col7.Name = "col7";
            this.col7.Visible = true;
            this.col7.Width = 704;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.MinWidth = 13;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 167;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.AutoFillDown = true;
            this.bandedGridColumn1.Caption = "JobTitle";
            this.bandedGridColumn1.FieldName = "JobTitle";
            this.bandedGridColumn1.MinWidth = 44;
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 167;
            // 
            // col2
            // 
            this.col2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.col2.AppearanceCell.BackColor2 = System.Drawing.Color.Yellow;
            this.col2.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.col2.AppearanceCell.Options.UseBackColor = true;
            this.col2.AppearanceCell.Options.UseBorderColor = true;
            this.col2.Caption = "col2";
            this.col2.MinWidth = 187;
            this.col2.Name = "col2";
            this.col2.Visible = true;
            this.col2.Width = 704;
            // 
            // col5
            // 
            this.col5.Caption = "col5";
            this.col5.MinWidth = 187;
            this.col5.Name = "col5";
            this.col5.Visible = true;
            this.col5.Width = 704;
            // 
            // col6
            // 
            this.col6.Caption = "col6";
            this.col6.MinWidth = 187;
            this.col6.Name = "col6";
            this.col6.Visible = true;
            this.col6.Width = 704;
            // 
            // TreeListTest2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 807);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.treelistMain);
            this.Controls.Add(this.textBox1);
            this.Name = "TreeListTest2";
            this.Text = "TreeListTest";
            ((System.ComponentModel.ISupportInitialize)(this.treelistMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gridView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col5;
        private DevExpress.XtraTreeList.Columns.TreeListBand treeListBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand band1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand band2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}