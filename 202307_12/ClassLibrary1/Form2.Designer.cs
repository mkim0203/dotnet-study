
using DevExpress.XtraRichEdit;

namespace ClassLibrary1
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richEditControl = new DevExpress.XtraRichEdit.RichEditControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.fileRibbonPage1 = new DevExpress.XtraRichEdit.UI.FileRibbonPage();
            this.commonRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.CommonRibbonPageGroup();
            this.infoRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.InfoRibbonPageGroup();
            this.homeRibbonPage1 = new DevExpress.XtraRichEdit.UI.HomeRibbonPage();
            this.clipboardRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup();
            this.fontRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
            this.paragraphRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup();
            this.stylesRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.StylesRibbonPageGroup();
            this.editingRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1480, 72);
            this.panel1.TabIndex = 0;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(635, 13);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(113, 39);
            this.button6.TabIndex = 6;
            this.button6.Text = "메뉴추가";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(754, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(153, 32);
            this.button5.TabIndex = 5;
            this.button5.Text = "readonly";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(913, 17);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 39);
            this.button4.TabIndex = 4;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1060, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 41);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Location = new System.Drawing.Point(1192, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 72);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1336, 72);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(1336, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 72);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richEditControl
            // 
            this.richEditControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl.Location = new System.Drawing.Point(0, 253);
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.Size = new System.Drawing.Size(1480, 769);
            this.richEditControl.TabIndex = 1;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // fileRibbonPage1
            // 
            this.fileRibbonPage1.Name = "fileRibbonPage1";
            // 
            // commonRibbonPageGroup1
            // 
            this.commonRibbonPageGroup1.Name = "commonRibbonPageGroup1";
            this.commonRibbonPageGroup1.Text = "";
            // 
            // infoRibbonPageGroup1
            // 
            this.infoRibbonPageGroup1.Name = "infoRibbonPageGroup1";
            this.infoRibbonPageGroup1.Text = "";
            // 
            // homeRibbonPage1
            // 
            this.homeRibbonPage1.Name = "homeRibbonPage1";
            // 
            // clipboardRibbonPageGroup1
            // 
            this.clipboardRibbonPageGroup1.Name = "clipboardRibbonPageGroup1";
            this.clipboardRibbonPageGroup1.Text = "";
            // 
            // fontRibbonPageGroup1
            // 
            this.fontRibbonPageGroup1.Name = "fontRibbonPageGroup1";
            this.fontRibbonPageGroup1.Text = "";
            // 
            // paragraphRibbonPageGroup1
            // 
            this.paragraphRibbonPageGroup1.Name = "paragraphRibbonPageGroup1";
            this.paragraphRibbonPageGroup1.Text = "";
            // 
            // stylesRibbonPageGroup1
            // 
            this.stylesRibbonPageGroup1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("stylesRibbonPageGroup1.ImageOptions.Image")));
            this.stylesRibbonPageGroup1.Name = "stylesRibbonPageGroup1";
            this.stylesRibbonPageGroup1.Text = "";
            // 
            // editingRibbonPageGroup1
            // 
            this.editingRibbonPageGroup1.Name = "editingRibbonPageGroup1";
            this.editingRibbonPageGroup1.Text = "";
            // 
            // pnlMenu
            // 
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMenu.Location = new System.Drawing.Point(0, 72);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(1480, 181);
            this.pnlMenu.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1480, 1022);
            this.Controls.Add(this.richEditControl);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion



        private System.Windows.Forms.Panel panel1;
        private RichEditControl richEditControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private DevExpress.XtraRichEdit.UI.FileRibbonPage fileRibbonPage1;
        private DevExpress.XtraRichEdit.UI.CommonRibbonPageGroup commonRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.InfoRibbonPageGroup infoRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.HomeRibbonPage homeRibbonPage1;
        private DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup clipboardRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup paragraphRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.StylesRibbonPageGroup stylesRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup editingRibbonPageGroup1;
        private System.Windows.Forms.Panel pnlMenu;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private System.Windows.Forms.Button button6;
    }
}