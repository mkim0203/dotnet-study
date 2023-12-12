


namespace TestProject
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.band1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.col1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.band2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.col3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col7 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.topband1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.middleband1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colTempDt = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMaskedNum = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRowSelect = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col6 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.col5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.tooltip = new DevExpress.Utils.ToolTipController(this.components);
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLayoutLoad = new System.Windows.Forms.Button();
            this.btnLayoutExport = new System.Windows.Forms.Button();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
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
            this.gridControl1.Size = new System.Drawing.Size(874, 505);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ToolTipController = this.tooltip;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.band1,
            this.band2,
            this.topband1});
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7,
            this.colTempDt,
            this.colRowSelect,
            this.colMaskedNum});
            this.gridView.DetailHeight = 280;
            this.gridView.GridControl = this.gridControl1;
            this.gridView.Name = "gridView";
            this.gridView.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.gridView_DragObjectDrop);
            this.gridView.DragObjectOver += new DevExpress.XtraGrid.Views.Base.DragObjectOverEventHandler(this.gridView_DragObjectOver);
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.advBandedGridView1_CustomDrawCell);
            this.gridView.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.advBandedGridView1_CustomDrawGroupRow);
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.advBandedGridView1_RowCellStyle);
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.advBandedGridView1_PopupMenuShowing);
            this.gridView.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView_CustomColumnDisplayText);
            // 
            // band1
            // 
            this.band1.Caption = "band1";
            this.band1.Columns.Add(this.col1);
            this.band1.Columns.Add(this.col4);
            this.band1.MinWidth = 76;
            this.band1.Name = "band1";
            this.band1.Visible = false;
            this.band1.VisibleIndex = -1;
            this.band1.Width = 1100;
            // 
            // col1
            // 
            this.col1.AppearanceCell.BorderColor = System.Drawing.Color.Fuchsia;
            this.col1.AppearanceCell.Options.UseBorderColor = true;
            this.col1.Caption = "col1";
            this.col1.MinWidth = 146;
            this.col1.Name = "col1";
            this.col1.Visible = true;
            this.col1.Width = 549;
            // 
            // col4
            // 
            this.col4.Caption = "col4";
            this.col4.MinWidth = 146;
            this.col4.Name = "col4";
            this.col4.Visible = true;
            this.col4.Width = 549;
            // 
            // band2
            // 
            this.band2.Caption = "band2";
            this.band2.Columns.Add(this.col3);
            this.band2.Columns.Add(this.col7);
            this.band2.MinWidth = 57;
            this.band2.Name = "band2";
            this.band2.Visible = false;
            this.band2.VisibleIndex = -1;
            this.band2.Width = 1100;
            // 
            // col3
            // 
            this.col3.Caption = "col3";
            this.col3.MinWidth = 146;
            this.col3.Name = "col3";
            this.col3.Visible = true;
            this.col3.Width = 549;
            // 
            // col7
            // 
            this.col7.AppearanceCell.Options.UseTextOptions = true;
            this.col7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col7.Caption = "col7";
            this.col7.MinWidth = 146;
            this.col7.Name = "col7";
            this.col7.Visible = true;
            this.col7.Width = 549;
            // 
            // topband1
            // 
            this.topband1.Caption = "top";
            this.topband1.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.middleband1});
            this.topband1.MinWidth = 23;
            this.topband1.Name = "topband1";
            this.topband1.VisibleIndex = 0;
            this.topband1.Width = 1940;
            // 
            // middleband1
            // 
            this.middleband1.Caption = "중간";
            this.middleband1.Columns.Add(this.colTempDt);
            this.middleband1.Columns.Add(this.colMaskedNum);
            this.middleband1.Columns.Add(this.colRowSelect);
            this.middleband1.Columns.Add(this.col2);
            this.middleband1.Columns.Add(this.col6);
            this.middleband1.Columns.Add(this.col5);
            this.middleband1.Name = "middleband1";
            this.middleband1.VisibleIndex = 0;
            this.middleband1.Width = 1940;
            // 
            // colTempDt
            // 
            this.colTempDt.Caption = "bandedGridColumn1";
            this.colTempDt.FieldName = "tempDt";
            this.colTempDt.MinWidth = 31;
            this.colTempDt.Name = "colTempDt";
            this.colTempDt.Visible = true;
            this.colTempDt.Width = 105;
            // 
            // colMaskedNum
            // 
            this.colMaskedNum.AutoFillDown = true;
            this.colMaskedNum.Caption = "숫자테스트";
            this.colMaskedNum.DisplayFormat.FormatString = "0000-00-000";
            this.colMaskedNum.FieldName = "MaskedNum";
            this.colMaskedNum.MinWidth = 23;
            this.colMaskedNum.Name = "colMaskedNum";
            this.colMaskedNum.Visible = true;
            this.colMaskedNum.Width = 87;
            // 
            // colRowSelect
            // 
            this.colRowSelect.Caption = "Row select";
            this.colRowSelect.FieldName = "RowSelect";
            this.colRowSelect.MinWidth = 27;
            this.colRowSelect.Name = "colRowSelect";
            this.colRowSelect.Visible = true;
            this.colRowSelect.Width = 101;
            // 
            // col2
            // 
            this.col2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.col2.AppearanceCell.BackColor2 = System.Drawing.Color.Yellow;
            this.col2.AppearanceCell.BorderColor = System.Drawing.Color.Red;
            this.col2.AppearanceCell.Options.UseBackColor = true;
            this.col2.AppearanceCell.Options.UseBorderColor = true;
            this.col2.Caption = "col2";
            this.col2.MinWidth = 146;
            this.col2.Name = "col2";
            this.col2.Visible = true;
            this.col2.Width = 549;
            // 
            // col6
            // 
            this.col6.Caption = "col6";
            this.col6.MinWidth = 146;
            this.col6.Name = "col6";
            this.col6.Visible = true;
            this.col6.Width = 549;
            // 
            // col5
            // 
            this.col5.Caption = "col5";
            this.col5.MinWidth = 146;
            this.col5.Name = "col5";
            this.col5.Visible = true;
            this.col5.Width = 549;
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
            // tooltip
            // 
            this.tooltip.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tooltip.Appearance.Options.UseBackColor = true;
            this.tooltip.AppearanceTitle.BackColor = System.Drawing.Color.Red;
            this.tooltip.AppearanceTitle.Options.UseBackColor = true;
            this.tooltip.Rounded = true;
            this.tooltip.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
            this.tooltip.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.tooltip_GetActiveObjectInfo);
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("ContactName", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.ContactName"))));
            this.svgImageCollection1.Add("Home", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.Home"))));
            this.svgImageCollection1.Add("Company", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.Company"))));
            this.svgImageCollection1.Add("NonFixed", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.NonFixed"))));
            this.svgImageCollection1.Add("FixLeft", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.FixLeft"))));
            this.svgImageCollection1.Add("FixRight", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.FixRight"))));
            this.svgImageCollection1.Add("City", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.City"))));
            this.svgImageCollection1.Add("Country", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.Country"))));
            this.svgImageCollection1.Add("Fax", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.Fax"))));
            this.svgImageCollection1.Add("PostalCode", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.PostalCode"))));
            this.svgImageCollection1.Add("Region", ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageCollection1.Region"))));
            this.svgImageCollection1.Add("glyph_phone", "image://svgimages/outlook inspired/glyph_phone.svg");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLayoutLoad);
            this.panel1.Controls.Add(this.btnLayoutExport);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(874, 42);
            this.panel1.TabIndex = 1;
            // 
            // btnLayoutLoad
            // 
            this.btnLayoutLoad.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLayoutLoad.Location = new System.Drawing.Point(524, 4);
            this.btnLayoutLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLayoutLoad.Name = "btnLayoutLoad";
            this.btnLayoutLoad.Size = new System.Drawing.Size(66, 34);
            this.btnLayoutLoad.TabIndex = 4;
            this.btnLayoutLoad.Text = "Layout Load";
            this.btnLayoutLoad.UseVisualStyleBackColor = true;
            this.btnLayoutLoad.Click += new System.EventHandler(this.btnLayoutLoad_Click);
            // 
            // btnLayoutExport
            // 
            this.btnLayoutExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLayoutExport.Location = new System.Drawing.Point(590, 4);
            this.btnLayoutExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLayoutExport.Name = "btnLayoutExport";
            this.btnLayoutExport.Size = new System.Drawing.Size(66, 34);
            this.btnLayoutExport.TabIndex = 3;
            this.btnLayoutExport.Text = "Layout Export";
            this.btnLayoutExport.UseVisualStyleBackColor = true;
            this.btnLayoutExport.Click += new System.EventHandler(this.btnLayoutExport_Click);
            // 
            // btnTest
            // 
            this.btnTest.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTest.Location = new System.Drawing.Point(656, 4);
            this.btnTest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(82, 34);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "테스트";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Location = new System.Drawing.Point(738, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "Export";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(804, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSplitContainer1.Grid = this.gridControl1;
            this.gridSplitContainer1.Horizontal = true;
            this.gridSplitContainer1.Location = new System.Drawing.Point(0, 42);
            this.gridSplitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Panel1.Controls.Add(this.gridControl1);
            this.gridSplitContainer1.Size = new System.Drawing.Size(874, 505);
            this.gridSplitContainer1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 547);
            this.Controls.Add(this.gridSplitContainer1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView gridView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn col6;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private DevExpress.Utils.ToolTipController tooltip;
        private System.Windows.Forms.Button button2;
        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;

        private DevExpress.XtraEditors.SimpleButton btnTest;
        private System.Windows.Forms.Button btnLayoutExport;
        private System.Windows.Forms.Button btnLayoutLoad;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colTempDt;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRowSelect;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand band1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand band2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand topband1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand middleband1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMaskedNum;
    }
}

