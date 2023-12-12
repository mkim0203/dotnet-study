
namespace TestProject
{
    partial class LookupTest
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupTest));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridLookUpEdit1 = new DevExpress.XtraEditors.GridLookUpEdit();
            this.aGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEngName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.txtIsoCode = new System.Windows.Forms.TextBox();
            this.txtIsoName = new System.Windows.Forms.TextBox();
            this.lookupJob = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colJobCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJobName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtJobCode = new System.Windows.Forms.TextBox();
            this.txtJobName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupJob.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLookUpEdit1
            // 
            this.gridLookUpEdit1.EditValue = "";
            this.gridLookUpEdit1.EnterMoveNextControl = true;
            this.gridLookUpEdit1.Location = new System.Drawing.Point(112, 97);
            this.gridLookUpEdit1.Name = "gridLookUpEdit1";
            this.gridLookUpEdit1.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.gridLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gridLookUpEdit1.Properties.Appearance.Options.UseBorderColor = true;
            this.gridLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.gridLookUpEdit1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(178)))), ((int)(((byte)(177)))));
            this.gridLookUpEdit1.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.gridLookUpEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            serializableAppearanceObject2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(178)))), ((int)(((byte)(177)))));
            serializableAppearanceObject2.Options.UseBorderColor = true;
            this.gridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.gridLookUpEdit1.Properties.PopupView = this.aGridLookUpEdit1View;
            this.gridLookUpEdit1.Size = new System.Drawing.Size(448, 26);
            this.gridLookUpEdit1.TabIndex = 0;
            this.gridLookUpEdit1.EditValueChanged += new System.EventHandler(this.gridLookUpEdit1_EditValueChanged);
            this.gridLookUpEdit1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridLookUpEdit1_KeyUp);
            // 
            // aGridLookUpEdit1View
            // 
            this.aGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colName,
            this.colEngName});
            this.aGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.aGridLookUpEdit1View.Name = "aGridLookUpEdit1View";
            this.aGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.aGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colCode
            // 
            this.colCode.Caption = "코드";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 50;
            // 
            // colName
            // 
            this.colName.Caption = "이름";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 100;
            // 
            // colEngName
            // 
            this.colEngName.Caption = "영문 이름";
            this.colEngName.FieldName = "EngName";
            this.colEngName.Name = "colEngName";
            this.colEngName.Visible = true;
            this.colEngName.VisibleIndex = 2;
            this.colEngName.Width = 200;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(642, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 26);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtIsoCode
            // 
            this.txtIsoCode.Location = new System.Drawing.Point(112, 168);
            this.txtIsoCode.Name = "txtIsoCode";
            this.txtIsoCode.Size = new System.Drawing.Size(150, 25);
            this.txtIsoCode.TabIndex = 2;
            // 
            // txtIsoName
            // 
            this.txtIsoName.Location = new System.Drawing.Point(294, 168);
            this.txtIsoName.Name = "txtIsoName";
            this.txtIsoName.Size = new System.Drawing.Size(150, 25);
            this.txtIsoName.TabIndex = 2;
            // 
            // lookupJob
            // 
            this.lookupJob.EditValue = "";
            this.lookupJob.EnterMoveNextControl = true;
            this.lookupJob.Location = new System.Drawing.Point(112, 258);
            this.lookupJob.Name = "lookupJob";
            this.lookupJob.Properties.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.lookupJob.Properties.Appearance.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lookupJob.Properties.Appearance.Options.UseBorderColor = true;
            this.lookupJob.Properties.Appearance.Options.UseFont = true;
            this.lookupJob.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(178)))), ((int)(((byte)(177)))));
            this.lookupJob.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.lookupJob.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            serializableAppearanceObject6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(178)))), ((int)(((byte)(177)))));
            serializableAppearanceObject6.Options.UseBorderColor = true;
            this.lookupJob.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.lookupJob.Properties.PopupView = this.gridView1;
            this.lookupJob.Size = new System.Drawing.Size(448, 26);
            this.lookupJob.TabIndex = 0;
            this.lookupJob.EditValueChanged += new System.EventHandler(this.lookupJob_EditValueChanged);
            this.lookupJob.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lookupJob_KeyUp);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colJobCode,
            this.colJobName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colJobCode
            // 
            this.colJobCode.Caption = "코드";
            this.colJobCode.FieldName = "Code";
            this.colJobCode.Name = "colJobCode";
            this.colJobCode.Visible = true;
            this.colJobCode.VisibleIndex = 0;
            this.colJobCode.Width = 50;
            // 
            // colJobName
            // 
            this.colJobName.Caption = "이름";
            this.colJobName.FieldName = "Name";
            this.colJobName.Name = "colJobName";
            this.colJobName.Visible = true;
            this.colJobName.VisibleIndex = 1;
            this.colJobName.Width = 100;
            // 
            // txtJobCode
            // 
            this.txtJobCode.Location = new System.Drawing.Point(112, 310);
            this.txtJobCode.Name = "txtJobCode";
            this.txtJobCode.Size = new System.Drawing.Size(150, 25);
            this.txtJobCode.TabIndex = 2;
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(294, 310);
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.Size = new System.Drawing.Size(150, 25);
            this.txtJobName.TabIndex = 2;
            // 
            // LookupTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtJobName);
            this.Controls.Add(this.txtIsoName);
            this.Controls.Add(this.txtJobCode);
            this.Controls.Add(this.txtIsoCode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lookupJob);
            this.Controls.Add(this.gridLookUpEdit1);
            this.Name = "LookupTest";
            this.Text = "TreeListLookupTest";
            this.Load += new System.EventHandler(this.TreeListLookupTest_Load);
            this.Shown += new System.EventHandler(this.LookupTest_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookupJob.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView aGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.Columns.GridColumn colEngName;
        private System.Windows.Forms.TextBox txtIsoCode;
        private System.Windows.Forms.TextBox txtIsoName;
        private DevExpress.XtraEditors.GridLookUpEdit lookupJob;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colJobCode;
        private DevExpress.XtraGrid.Columns.GridColumn colJobName;
        private System.Windows.Forms.TextBox txtJobCode;
        private System.Windows.Forms.TextBox txtJobName;
    }
}