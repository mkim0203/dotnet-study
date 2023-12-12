using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraRichEdit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public partial class Form2 : Form
    {
        string _base64Data = "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";

        private string DocTempl = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
   <head>
      <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
      <title>
      </title>
      <style type=""text/css"">
         .cs5F80D9B9{{text-align:center;text-indent:0pt;margin:0pt 0pt 8pt 0pt;line-height:1.079167}}
         .csDF4544F5{{color:#000000;background-color:transparent;font-family:나눔고딕;font-size:20pt;font-weight:bold;font-style:normal;}}
         .cs2D2816FE{{}}
         .cs95E54B96{{width:14%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1.5pt windowtext solid;border-right:1pt windowtext solid;border-bottom:1pt windowtext solid;border-left:none}}
         .cs2E86D3A6{{text-align:center;text-indent:0pt;margin:0pt 0pt 0pt 0pt}}
         .cs7C6DA362{{color:#000000;background-color:transparent;font-family:나눔고딕;font-size:10pt;font-weight:bold;font-style:normal;}}
         .csAD56A1DF{{width:56%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1.5pt windowtext solid;border-right:1pt windowtext solid;border-bottom:1pt windowtext solid;border-left:1pt windowtext solid}}
         .cs95E872D0{{text-align:left;text-indent:0pt;margin:0pt 0pt 0pt 0pt}}
         .csC035B3DE{{color:#000000;background-color:transparent;font-family:나눔고딕;font-size:10pt;font-weight:normal;font-style:normal;}}
         .cs84424F58{{width:11%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1.5pt windowtext solid;border-right:1pt windowtext solid;border-bottom:1pt windowtext solid;border-left:1pt windowtext solid}}
         .csC808E302{{width:18%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1.5pt windowtext solid;border-right:none;border-bottom:1pt windowtext solid;border-left:1pt windowtext solid}}
         .cs59E853D8{{width:14%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1pt windowtext solid;border-right:1pt windowtext solid;border-bottom:1.5pt windowtext solid;border-left:none}}
         .csB659E1D3{{width:85%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1pt windowtext solid;border-right:none;border-bottom:1.5pt windowtext solid;border-left:1pt windowtext solid}}
         .cs8ADB2719{{text-align:justify;text-indent:0pt;margin:0pt 0pt 8pt 0pt;line-height:1.079167}}
         .csC39937F2{{width:85%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1.5pt windowtext solid;border-right:none;border-bottom:1pt windowtext solid;border-left:1pt windowtext solid}}
      </style>
   </head>
   <body>
      <p class=""cs5F80D9B9""><span class=""csDF4544F5"">회의록</span></p>
      <table class=""cs2D2816FE"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""border-collapse:collapse;"">
         <tr style=""height:22.7pt;"">
            <td class=""cs95E54B96"" width=""14%"">
               <p class=""cs2E86D3A6""><span class=""cs7C6DA362"">회의 일시</span></p>
            </td>
            <td class=""csAD56A1DF"" width=""56%"">
               <p class=""cs95E872D0""><span class=""csC035B3DE"">{0}</span></p>
            </td>
            <td class=""cs84424F58"" width=""11%"">
               <p class=""cs2E86D3A6""><span class=""csC035B3DE"">작성자</span></p>
            </td>
            <td class=""csC808E302"" width=""18%"">
               <p class=""cs2E86D3A6""><span class=""csC035B3DE"">{1}</span></p>
            </td>
         </tr>
         <tr style=""height:48.15pt;"">
            <td class=""cs59E853D8"" width=""14%"">
               <p class=""cs2E86D3A6""><span class=""cs7C6DA362"">참석자</span></p>
            </td>
            <td class=""csB659E1D3"" colspan=""3"" width=""85%"">
               <p class=""cs95E872D0""><span class=""csC035B3DE"">{2}</span></p>
            </td>
         </tr>
      </table>
      <p class=""cs8ADB2719""><span class=""csC035B3DE"">&nbsp;</span></p>
      <table class=""cs2D2816FE"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""border-collapse:collapse;"">
         <tr style=""height:22.7pt;"">
            <td class=""cs95E54B96"" width=""14%"">
               <p class=""cs2E86D3A6""><span class=""cs7C6DA362"">회의 안건</span></p>
            </td>
            <td class=""csC39937F2"" width=""85%"">
               <p class=""cs95E872D0""><span class=""csC035B3DE"">{3}</span></p>
            </td>
         </tr>
      </table>
      <p class=""cs8ADB2719""><span class=""csC035B3DE"">&nbsp;</span></p>
      <p class=""cs8ADB2719""><span class=""csC035B3DE"">&nbsp;</span></p>
   </body>
</html>
 ";


		public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = richEditControl.HtmlText;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestProject.ReportTempl");


            using (Stream stream = new MemoryStream(Properties.Resources.ReportTempl))
            {
                if(stream != null) richEditControl.LoadDocument(stream);
            }


            //using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ReportTempl"))
            //{
            //    richEditControl.LoadDocument(stream);
            //}

            //richEditControl.LoadDocument(".\\Datas\\template.docx");
            //richEditControl.LoadDocument(".\\Datas\\ReportTempl.docx");
            //richEditControl.HtmlText = string.Format(DocTempl, "2023-09-26", "mhkim", "<p attr=\"item1\"><span>개발팀 : A, B </span><br/><span> B부서 : C, D</span></p>", "회의록 안건 테스트", "");
            //richEditControl.HtmlText = string.Format(DocTempl, "2023-09-26");

            //richEditControl.load.LoadDefaultBarAndDockingLocalization(UserLookAndFeel.Default, "en-US");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var row = new DocItem() { WriteDay = DateTime.Now.ToString("yyyy-MM-dd"), WriteTime = "22:10", ReportTitle = "title" };
            row.ListWriter.Add(new MeetingUserInfo("dev", "개발팀", "mhkim", "김OO"));
            row.ListJoinUser.Add(new MeetingUserInfo("dev", "개발팀", "user1", "사용자1"));
            row.ListJoinUser.Add(new MeetingUserInfo("dev", "개발팀", "user2", "사용자2"));
            row.ListJoinUser.Add(new MeetingUserInfo("dev", "개발팀", "oper1", "업무1"));

            List<DocItem> datas = new List<DocItem>() { row };
            

            //var viewItem = new DocItem() { MakeDay = DateTime.Now.ToString("yyyy-MM-dd"), Writer = "mhkim", JoinUsers = "test adlfa", ReportTitle = "title" };
            richEditControl.Options.MailMerge.ViewMergedData = true;
            richEditControl.Options.MailMerge.DataSource = datas;

            DevExpress.XtraRichEdit.API.Native.Document document = richEditControl.Document;
            //for (int i = 0; i < document.Fields.Count; i++)
            //{
            //    // Access a field code.
            //    string fieldCode = document.GetText(document.Fields[i].CodeRange);
            //    DocumentPosition position = document.Fields[i].CodeRange.End;

            //    switch(fieldCode)
            //    {
            //        case " MERGEFIELD  MakeDay ":
            //            document.InsertText(position, DateTime.Now.ToString("yyyy-MM-dd"));
            //            break;
            //        case " MERGEFIELD  Writer ":
            //            document.InsertText(position, "mhkim");
            //            break;
            //        case " MERGEFIELD  JoinUsers ":
            //            document.InsertText(position, "참여자");
            //            break;
            //        case " MERGEFIELD  ReportTitle ":
            //            document.InsertText(position, "title");
            //            break;

            //    }


            //    //// Check whether a field code is "DATE".
            //    //if (fieldCode == "DATE")
            //    //{
            //    //    // Set the document position to the end of the field code range.
            //    //    DocumentPosition position = document.Fields[i].CodeRange.End;
            //    //    // Specify a date and time format for the field. 
            //    //    //document.InsertText(position, @" \@ ""M/d/yyyy HH:mm:ss""");
            //    //}
            //}

            document.Fields.Update();

            ///

            //DevExpress.XtraRichEdit.API.Native.Document document = richEditControl.Document;
            //document.CustomProperties["MakeDay"] = "1231231";
            //document.CustomProperties["Writer"] = "mkim";
            //document.CustomProperties["JoinUsers"] = "dfldl";
            //document.CustomProperties["ReportTitle"] = "titledfasdfas";

            //document.Fields.Update();

        }



        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string wordMl = richEditControl.WordMLText;

            //richEditControl.SaveDocumentAs();

            MemoryStream stream = new MemoryStream();

            richEditControl.SaveDocument(stream, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);


            byte[] datas = stream.ToArray();

            string base64 = Convert.ToBase64String(datas);

            _base64Data = base64;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_base64Data.Length > 0)
            {
                byte[] datas = Convert.FromBase64String(_base64Data);

                Stream stream = new MemoryStream(datas);

                if (stream != null) richEditControl.LoadDocument(stream);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //richEditControl.ReadOnly = true;
            richEditControl.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RibbonControl ribbonControl = richEditControl.CreateRibbon(RichEditToolbarType.Home | RichEditToolbarType.Insert | RichEditToolbarType.Table);
            pnlMenu.Controls.Add(ribbonControl);
            
            //AppendCustomRibbonItems(ribbonControl);
        }


        private DevExpress.XtraBars.BarStaticItem pagesBarItem;
        private DevExpress.XtraBars.BarEditItem zoomBarEditItem;
        private DevExpress.XtraBars.BarButtonItem documentStatisticsBarButtonItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        void AppendCustomRibbonItems(RibbonControl ribbonControl1)
        {
            this.pagesBarItem = new DevExpress.XtraBars.BarStaticItem();
            this.zoomBarEditItem = new DevExpress.XtraBars.BarEditItem();
            this.documentStatisticsBarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();


            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
                this.pagesBarItem,
                this.zoomBarEditItem,
                this.documentStatisticsBarButtonItem,
            });

            ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                this.repositoryItemZoomTrackBar1,
            });

            // 
            // pagesBarItem
            // 
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.pagesBarItem.Id = 246;
            this.pagesBarItem.Name = "pagesBarItem";
            toolTipItem1.Text = "Page number in document.";
            superToolTip1.Items.Add(toolTipItem1);
            this.pagesBarItem.SuperTip = superToolTip1;

            // 
            // zoomBarEditItem
            // 
            this.zoomBarEditItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.zoomBarEditItem.Caption = "100%";
            this.zoomBarEditItem.Edit = this.repositoryItemZoomTrackBar1;
            this.zoomBarEditItem.EditValue = 100;
            this.zoomBarEditItem.EditWidth = 150;
            this.zoomBarEditItem.Id = 245;
            this.zoomBarEditItem.Name = "zoomBarEditItem";
            this.zoomBarEditItem.EditValueChanged += new System.EventHandler(this.zoomBarEditItem_EditValueChanged);


            // 
            // repositoryItemZoomTrackBar1
            // 
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();

            this.repositoryItemZoomTrackBar1.AllowUseMiddleValue = true;
            this.repositoryItemZoomTrackBar1.LargeChange = 50;
            this.repositoryItemZoomTrackBar1.Maximum = 500;
            this.repositoryItemZoomTrackBar1.Middle = 100;
            this.repositoryItemZoomTrackBar1.Minimum = 10;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.SmallChange = 10;
            this.repositoryItemZoomTrackBar1.SnapToMiddle = 2;

            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();

            //this.ribbonStatusBar1.ItemLinks.Add(this.pagesBarItem);
            //this.ribbonStatusBar1.ItemLinks.Add(this.zoomBarEditItem);
            //this.ribbonStatusBar1.ItemLinks.Add(this.documentStatisticsBarButtonItem);

            //this.ribbonStatusBar1.Ribbon = ribbonControl1;

            // 
            // documentStatisticsBarButtonItem
            // 
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.documentStatisticsBarButtonItem.Id = 247;
            this.documentStatisticsBarButtonItem.Name = "documentStatisticsBarButtonItem";
            toolTipItem2.Text = "Number of words in document. Click to open the Document Statistics dialog box.";
            superToolTip2.Items.Add(toolTipItem2);
            this.documentStatisticsBarButtonItem.SuperTip = superToolTip2;
            this.documentStatisticsBarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.documentStatistics_ItemClick);
        }

        private void documentStatistics_ItemClick(object sender, ItemClickEventArgs e)
        {
            //using (DocumentStatisticsForm form = new DocumentStatisticsForm(RichEdit.Document, IncludeTextBoxes))
            //{
            //    form.LookAndFeel.ParentLookAndFeel = LookAndFeel;
            //    form.ShowDialog();
            //    IncludeTextBoxes = form.IncludeTextboxes;
            //}
        }

        void zoomBarEditItem_EditValueChanged(object sender, System.EventArgs e)
        {
            //if (this._isZoomChanging)
            //    return;
            //int value = Convert.ToInt32(zoomBarEditItem.EditValue);
            //this._isZoomChanging = true;
            //try
            //{
            //    RichEdit.ActiveView.ZoomFactor = value / 100f;
            //    zoomBarEditItem.Caption = String.Format("{0}%", value);
            //}
            //finally
            //{
            //    this._isZoomChanging = false;
            //}
        }
    }


    public class DocItem
    {
        public DocItem()
        {
            ListWriter = new List<MeetingUserInfo>();
            ListJoinUser = new List<MeetingUserInfo>();
        }

        public string WriteDay { get; set; }
        public string WriteTime { get; set; }
        public List<MeetingUserInfo> ListWriter { get; }
        public string JsonWriter { get { return JsonConvert.SerializeObject(ListWriter); } }
        public string Writer
        {
            get
            {
                return View(ListWriter);
            }
        }
        public List<MeetingUserInfo> ListJoinUser { get; }
        public string JosnJoinUser { get { return JsonConvert.SerializeObject(ListJoinUser); } } 
        public string JoinUsers
        {
            get
            {
                return View(ListJoinUser);
            }
        }
        public string ReportTitle { get; set; }
        public string ReportDocumentBase64 { get; set; }

        public string View(List<MeetingUserInfo>  datas)
        {
            List<string> temp = new List<string>();
            IEnumerable<IGrouping<string, MeetingUserInfo>> groupbyData = datas.GroupBy(x => x.DeptName);
            foreach(var group in groupbyData)
            {
                //Console.WriteLine(group.Key);
                //foreach (var userInfo in group)
                //{
                //    Console.WriteLine($"UserId: {userInfo.UserId}");
                //}
                // string.Join(" ", group);
                temp.Add($"{group.Key} : {string.Join(" ", group.Select(x => x.UserId))}");
            }
            return string.Join(Environment.NewLine, temp);
        }
    }

    public class MeetingUserInfo
    {
        public MeetingUserInfo(string deptCd, string deptNm, string userId, string userNm)
        {
            this.DeptCd = deptCd;
            this.DeptName = deptNm;
            this.UserId = userId;
            this.UserName = userNm;
        }

        public string UserId { get; set; }
        public string DeptName { get; set; }
        public string UserName { get; set; }
        public string DeptCd { get; set; }
    }
}
