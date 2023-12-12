
namespace TestProject
{
    partial class Form3
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTempDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRowSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemDateEdit1});
            this.gridControl1.Size = new System.Drawing.Size(800, 450);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.EditFormat.FormatString = "yyyy-MM-dd";
            this.repositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7,
            this.colTempDt,
            this.colRowSelect});
            this.gridView.DetailHeight = 280;
            this.gridView.GridControl = this.gridControl1;
            this.gridView.Name = "gridView";
            // 
            // col1
            // 
            this.col1.AppearanceCell.BorderColor = System.Drawing.Color.Fuchsia;
            this.col1.AppearanceCell.Options.UseBorderColor = true;
            this.col1.Caption = "col1";
            this.col1.FieldName = "col1";
            this.col1.MinWidth = 146;
            this.col1.Name = "col1";
            this.col1.Visible = true;
            this.col1.VisibleIndex = 0;
            this.col1.Width = 549;
            // 
            // col2
            // 
            this.col2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.col2.AppearanceCell.BackColor2 = System.Drawing.Color.Yellow;
            this.col2.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.col2.AppearanceCell.Options.UseBackColor = true;
            this.col2.AppearanceCell.Options.UseBorderColor = true;
            this.col2.Caption = "col2";
            this.col2.FieldName = "col2";
            this.col2.MinWidth = 146;
            this.col2.Name = "col2";
            this.col2.Visible = true;
            this.col2.VisibleIndex = 1;
            this.col2.Width = 549;
            // 
            // col3
            // 
            this.col3.Caption = "col3";
            this.col3.FieldName = "col3";
            this.col3.MinWidth = 146;
            this.col3.Name = "col3";
            this.col3.Visible = true;
            this.col3.VisibleIndex = 2;
            this.col3.Width = 549;
            // 
            // col4
            // 
            this.col4.Caption = "col4";
            this.col4.FieldName = "col4";
            this.col4.MinWidth = 146;
            this.col4.Name = "col4";
            this.col4.Visible = true;
            this.col4.VisibleIndex = 3;
            this.col4.Width = 549;
            // 
            // col5
            // 
            this.col5.Caption = "col5";
            this.col5.FieldName = "col5";
            this.col5.MinWidth = 146;
            this.col5.Name = "col5";
            this.col5.Visible = true;
            this.col5.VisibleIndex = 4;
            this.col5.Width = 549;
            // 
            // col6
            // 
            this.col6.Caption = "col6";
            this.col6.FieldName = "col6";
            this.col6.MinWidth = 146;
            this.col6.Name = "col6";
            this.col6.Visible = true;
            this.col6.VisibleIndex = 5;
            this.col6.Width = 549;
            // 
            // col7
            // 
            this.col7.AppearanceCell.Options.UseTextOptions = true;
            this.col7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col7.Caption = "col7";
            this.col7.FieldName = "col7";
            this.col7.MinWidth = 146;
            this.col7.Name = "col7";
            this.col7.Visible = true;
            this.col7.VisibleIndex = 6;
            this.col7.Width = 549;
            // 
            // colTempDt
            // 
            this.colTempDt.Caption = "bandedGridColumn1";
            this.colTempDt.FieldName = "tempDt";
            this.colTempDt.MinWidth = 31;
            this.colTempDt.Name = "colTempDt";
            this.colTempDt.Visible = true;
            this.colTempDt.VisibleIndex = 7;
            this.colTempDt.Width = 105;
            // 
            // colRowSelect
            // 
            this.colRowSelect.Caption = "Row select";
            this.colRowSelect.FieldName = "RowSelect";
            this.colRowSelect.MinWidth = 27;
            this.colRowSelect.Name = "colRowSelect";
            this.colRowSelect.Visible = true;
            this.colRowSelect.VisibleIndex = 8;
            this.colRowSelect.Width = 101;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(331, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridControl1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Shown += new System.EventHandler(this.Form3_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn col1;
        private DevExpress.XtraGrid.Columns.GridColumn col2;
        private DevExpress.XtraGrid.Columns.GridColumn col3;
        private DevExpress.XtraGrid.Columns.GridColumn col4;
        private DevExpress.XtraGrid.Columns.GridColumn col5;
        private DevExpress.XtraGrid.Columns.GridColumn col6;
        private DevExpress.XtraGrid.Columns.GridColumn col7;
        private DevExpress.XtraGrid.Columns.GridColumn colTempDt;
        private DevExpress.XtraGrid.Columns.GridColumn colRowSelect;
        private System.Windows.Forms.Button button1;
    }
}