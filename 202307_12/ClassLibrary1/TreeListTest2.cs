using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
//using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.StyleFormatConditions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    public partial class TreeListTest2 : Form
    {
        private List<WeekInfo> _firstMonthWeek = new List<WeekInfo>();
        DateTime? minDate = new DateTime(2022, 3, 10);
        DateTime? maxDate = new DateTime(2024, 03, 10);

        public TreeListTest2()
        {
            InitializeComponent();
            
            InitTreeView();

            this.Load += TreeListTest_Load;
        }

        private void TreeListTest_Load(object sender, EventArgs e)
        {
            DataTable gridData = InitData();

            treelistMain.BeginUnboundLoad();

            treelistMain.DataSource = gridData;


            treelistMain.EndUnboundLoad();
            treelistMain.ExpandAll();

            //treelistMain.Appearance.FocusedCell.BackColor = Color.Yellow;
            treelistMain.Appearance.SelectedRow.BackColor = Color.Yellow;
            treelistMain.Appearance.FocusedRow.BackColor = Color.Yellow;
            
            treelistMain.OptionsSelection.EnableAppearanceFocusedCell = false;


            AddCol();
            gridControl1.DataSource = gridData;

            SetGridView();
            //CreateData(gridData);
            MakeCol2();
            AddWeekSchedule(gridData);
        }

        private DataTable GridHaederInfo()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FieldName", typeof(string));
            dataTable.Columns.Add("Caption", typeof(string));
            dataTable.Columns.Add("Width", typeof(string));
            dataTable.Columns.Add("ParentFieldName", typeof(string));
            dataTable.Columns.Add("IsBand", typeof(string));
            dataTable.Columns.Add("Sort", typeof(int));

            dataTable.Rows.Add("N1_LVL_TASK", "1레벨", "150", "", "Y", "1");
            dataTable.Rows.Add("LEVEL2_CD", "level2 코드", "30", "", "Y", "4");
            dataTable.Rows.Add("LEVEL_TOP1", "TOP", "30", "", "Y", "7");
            dataTable.Rows.Add("LEVEL1_CD", "시스템코드(1)", "150", "N1_LVL_TASK", "N", "2");
            dataTable.Rows.Add("LEVEL1_NM", "시스템명(1)", "150", "N1_LVL_TASK", "N", "3");
            dataTable.Rows.Add("LEVEL3_CD", "level3 코드", "30", "LEVEL2_CD", "N", "5");
            dataTable.Rows.Add("LEVEL4_CD", "level4 코드", "30", "LEVEL2_CD", "N", "6");
            dataTable.Rows.Add("SUB_1", "SUB1", "30", "LEVEL_TOP1", "Y", "8");
            dataTable.Rows.Add("SUB_2", "SUB2", "30", "LEVEL_TOP1", "Y", "9");
            dataTable.Rows.Add("SUB_3", "SUB3", "30", "LEVEL_TOP1", "N", "15");
            dataTable.Rows.Add("BOT_1", "하단1", "30", "SUB_1", "N", "10");
            dataTable.Rows.Add("BOT_2", "하단2", "30", "SUB_1", "N", "11");

            dataTable.Rows.Add("DATA1", "내용1", "30", "", "N", "12");
            dataTable.Rows.Add("DATA2", "내용2", "30", "", "N", "13");
            dataTable.Rows.Add("DATA3", "내용3", "30", "", "N", "14");

            return dataTable;
        }

        private DataTable InitData()
        {
            List<TreeItem> treeDatas = new List<TreeItem>();
            string level1parent = "0";
            string level2parent = string.Empty;
            string level3parent = string.Empty;

            for (int i = 1; i <= 10; i++)
            {
                treeDatas.Add(new TreeItem() { JobTitle = $"job{i:00}", Id = $"{i:00}", ParentId = level1parent });
                level2parent = $"{i:00}";
                for(int j = 1; j <=10; j++)
                {
                    treeDatas.Add(new TreeItem() { JobTitle = $"job{i:00}-{j:00}", Id = $"{i:00}-{j:00}", ParentId = level2parent });
                    level3parent = $"{i:00}-{j:00}";
                    for(int k=1; k<=10; k++)
                    {
                        treeDatas.Add(new TreeItem() { JobTitle = $"job{i:00}-{j:00}-{k:00}", Id = $"{i:00}-{j:00}-{k:00}", ParentId = level3parent });
                    }
                }
            }

            treeDatas.Add(new TreeItem() { JobTitle = "jobC-1", Id = "C1", ParentId = "C0" });

            treeDatas.Add(new TreeItem() { JobTitle = "jobD-9", Id = "D9", ParentId = "D8" });

            return ToDataTable(treeDatas);
        }

        private void InitTreeView()
        {
            treelistMain.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            treelistMain.ShowFindPanel();

            // 메뉴에 조건부 서식 show
            treelistMain.OptionsMenu.ShowConditionalFormattingItem = true;

            treelistMain.ExpandAll();
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


        private void SetGridView()
        {
            // 조회 기능 show
            gridView.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            gridView.ShowFindPanel();


            // group panel show
            gridView.OptionsView.ShowGroupPanel = true;
            // 메뉴에 조건부 서식 show
            gridView.OptionsMenu.ShowConditionalFormattingItem = true;

            //gridView.ScrollStyle = ScrollStyleFlags.None;
            //gridView.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True;
            gridView.OptionsScrollAnnotations.ShowErrors = DevExpress.Utils.DefaultBoolean.True;
            //gridView.OptionsScrollAnnotations.ShowSelectedRows = DevExpress.Utils.DefaultBoolean.True;


            //gridView.Columns["TASK_NM"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            gridView.FixedLineWidth = 3;

            // Disable appearance settings that paint the focused row.
            // 포커스 제거
            //gridView.OptionsSelection.EnableAppearanceFocusedRow = false;
        }



        private void AddCol()
        {
            GridBand fixBand = new GridBand();
            fixBand.Caption = "고정";
            fixBand.Name = "AutoCalc_FixBand";
            fixBand.Fixed = FixedStyle.Left;
            //AutoCalc_FixBand
            gridView.Bands.Add(fixBand);

            // 최상위 grid에 band 컬럼 추가.
            // GridBand, BandedGridColumn 
            // 밴드에 col 추가
            /*   
             this.band2.Columns.Add(this.col3);
             */
            // band는 하위 band가 추가될수 있음. 
            // gridview에 band, col 
            /*
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.band1,
            this.band2});
            this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7});
            */


            //BandedGridColumn col = new BandedGridColumn();
            GridBand top = new GridBand();
            top.Caption = "top";
            top.Name = "bandTop";

            GridBand band = new GridBand();

            band.Caption = "test";
            band.Name = "testBand";
            band.RowCount = 2;

            top.Children.Add(band);

            //col.Caption = "data1";
            //col.Visible = true;

            var cols = MakeCol("test");
            foreach (var col in cols)
            {
                col.Tag = new GridColAppendData() { InitBaseGridBand = band };

                band.Columns.Add(col);
            }

            // 계단식 구조일경우 최상위 band만 gridview에 등록
            //advBandedGridView1.Bands.Add(band);
            gridView.Bands.Add(top);
            gridView.Columns.AddRange(cols.ToArray());
        }

        private List<BandedGridColumn> MakeCol(string captionFix)
        {
            List<BandedGridColumn> retValue = new List<BandedGridColumn>();
            for (int i = 1; i <= 10; i++)
            {
                BandedGridColumn col = new BandedGridColumn();
                col.Visible = true;
                col.Caption = $"{captionFix}-{i}";
                //col.RowIndex = ((i -1) % 3);
                col.FieldName = $"{captionFix}-{i}";
                col.AutoFillDown = true;

                retValue.Add(col);
            }

            return retValue;
        }


        private void CreateData(DataTable dataTable)
        {
            for (int i = 1; i <= 10; i++)
            {
                dataTable.Columns.Add($"test-{i}", typeof(string));
            }



            Random rm = new Random();
            for (int i = 0; i <= 10; i++)
            {
                var row = dataTable.NewRow();
                for (int colIndex = 1; colIndex <= 10; colIndex++)
                {
                    row[$"test-{colIndex}"] = rm.Next(100);
                }
                dataTable.Rows.Add(row);
            }

        }

        private void MakeCol2()
        {
            DataTable headerInfo = GridHaederInfo();

            //IOrderedEnumerable<DataColumn> levelColumns = headerInfo.Columns.Cast<DataColumn>()
            //  .Where(column => column.ColumnName.StartsWith("LEVEL", StringComparison.OrdinalIgnoreCase))
            //  .OrderBy(column => column.ColumnName);

            IEnumerable<DataRow> rootCol = headerInfo.Rows.Cast<DataRow>()
             .Where(row => Convert.ToString(row["ParentFieldName"]) == "" && Convert.ToString(row["IsBand"]) == "Y");

            var rootBand = MakeRootCol(rootCol);
            gridView.Bands.AddRange(rootBand.ToArray());

            foreach (var root in rootBand)
            {
                MakeSubCol(root, headerInfo);
            }



        }

        private List<GridBand> MakeRootCol(IEnumerable<DataRow> rootCol)
        {
            List<GridBand> retValue = new List<GridBand>();
            foreach (var row in rootCol)
            {
                GridBand band = new GridBand();
                band.Name = row["FieldName"].ToString();
                band.Caption = row["Caption"].ToString();

                retValue.Add(band);
            }

            // root 포함되지 않는 나머지 항목을 data root에 넣기
            GridBand dataRoot = new GridBand();
            dataRoot.Name = "AutoCalc_DATA_ROOT";
            dataRoot.Caption = "내용";
            retValue.Add(dataRoot);

            return retValue;
        }

        private void MakeSubCol(GridBand parentBand, DataTable headerInfo)
        {
            IEnumerable<DataRow> childRows = null;
            if (parentBand.Name == "AutoCalc_DATA_ROOT")
            {
                childRows = headerInfo.Rows.Cast<DataRow>()
                 .Where(row => Convert.ToString(row["ParentFieldName"]) == "" && row["IsBand"].ToString() == "N")
                 .OrderBy(row => Convert.ToInt32(row["Sort"]));
            }
            else
            {
                childRows = headerInfo.Rows.Cast<DataRow>()
                 .Where(row => Convert.ToString(row["ParentFieldName"]) == parentBand.Name)
                 .OrderBy(row => Convert.ToInt32(row["Sort"]));
            }

            foreach (var row in childRows)
            {
                if (row["IsBand"].ToString() == "Y")
                {
                    // 자식이 데이터 band 이면 상위 band에 추가.
                    GridBand band = new GridBand();
                    band.Name = row["FieldName"].ToString();
                    band.Caption = row["Caption"].ToString();

                    parentBand.Children.Add(band);

                    // 자식 band나 데이터 컬럼이 있는지 확인후 작업 진행
                    MakeSubCol(band, headerInfo);
                }
                else
                {
                    // 데이터 컬럼이면. grid column 생성후 상위 band에 추가
                    BandedGridColumn col = new BandedGridColumn();
                    col.Visible = true;
                    col.Caption = row["Caption"].ToString();
                    //col.RowIndex = ((i - 1) % 3);
                    col.FieldName = row["FieldName"].ToString();
                    col.AutoFillDown = true;
                    col.Tag = new GridColAppendData() { InitBaseGridBand = parentBand };

                    // gridview에도 추가.
                    gridView.Columns.Add(col);

                    parentBand.Columns.Add(col);

                }
            }
        }


        private void SetForamttingRuleWeekRule(string targetCol, Color color)
        {
            DevExpress.XtraGrid.GridFormatRule rule = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression conditionRule = new DevExpress.XtraEditors.FormatConditionRuleExpression();

            // Not IsNullOrEmpty([test-8])
            conditionRule.Expression = $"Not IsNullOrEmpty([{targetCol}])";
            conditionRule.Appearance.BackColor = color;
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }
        private void AddRule(string targetCol, DevExpress.XtraGrid.GridFormatRule rule)
        {
            DevExpress.XtraGrid.Columns.GridColumn col = gridView.Columns[targetCol];
            if (col != null)
            {
                rule.Column = col;
                gridView.FormatRules.Add(rule);
            }
        }

        private void AddWeekSchedule(DataTable dataTable)
        {
            //Func<object, DateTime?> GetDatetime = (data) =>
            //{
            //    if (data == DBNull.Value) return null;

            //    DateTime temp;
            //    if (DateTime.TryParse(data.ToString(), out temp))
            //    {
            //        return temp;
            //    }
            //    return null;
            //};

            //DateTime? minDate = new DateTime(2022, 03, 01);
            DateTime? minDate = new DateTime(2022, 3, 10);
            DateTime? maxDate = new DateTime(2024, 03, 10);

            // CalendarInfo 데이터 작업
            List<YearInfo> infos = CreateYearInfos(minDate, maxDate);

            var temp = infos.OrderBy(x => x.Year).Select(x => x.Weeks);
            int startIndex = 0;
            foreach (var item in temp)
            {
                if (temp.First() == item)
                {
                    startIndex = SetWeekNumberBaseOnFirstWeek(item, startIndex, true);
                }
                else startIndex = SetWeekNumberBaseOnFirstWeek(item, startIndex);
            }

            // 각 월별로 WeekNumber가 가장 작은 항목 선택
            foreach (var item in temp)
            {
                var minWeeksByMonth = item
                    .GroupBy(w => new { w.Year, w.Month })
                    .Select(group => group.OrderBy(w => w.WeekNumber).First());

                _firstMonthWeek.AddRange(minWeeksByMonth);
            }



            GridBand weekRoot = new GridBand();
            weekRoot.Name = "AutoCalc_WeekRoot";
            weekRoot.Caption = "기간";
            CommonGridBandOption(weekRoot);

            gridView.Bands.Add(weekRoot);

            //AddWeekColumnEmpty(dataTable);
            GridBand gubunBand = new GridBand();
            gubunBand.Caption = "구분";
            weekRoot.Children.Add(gubunBand);
            CommonGridBandOption(gubunBand);

            GridBand monthBandInGubun = new GridBand();
            monthBandInGubun.Caption = "월";
            monthBandInGubun.AppearanceHeader.BackColor = Color.LightGreen;
            CommonGridBandOption(monthBandInGubun);
            gubunBand.Children.Add(monthBandInGubun);

            BandedGridColumn gubunCol1 = AddWeekColumnEmpty("사전계획", true);
            gubunCol1.Name = "Week_Prearng";
            gridView.Columns.Add(gubunCol1);
            monthBandInGubun.Columns.Add(gubunCol1);

            BandedGridColumn gubunCol2 = AddWeekColumnEmpty("수행", false);
            gubunCol2.Name = "Week_Real";
            gridView.Columns.Add(gubunCol2);
            monthBandInGubun.Columns.Add(gubunCol2);



            foreach (YearInfo item in infos)
            {
                GridBand year = new GridBand();
                year.Name = $"AutoCalc_WeekYear_{item.Year}";
                year.Caption = item.Year.ToString();
                CommonGridBandOption(year);


                weekRoot.Children.Add(year);

                List<GridBand> monthInYearBands = new List<GridBand>();
                foreach (WeekInfo week in item.Weeks)
                {
                    string monthName = $"AutoCalc_WeekMonth_{week.Year}_{week.Month}";
                    GridBand targetMonth = monthInYearBands.Find(x => x.Name == monthName);
                    if (targetMonth == null)
                    {
                        targetMonth = new GridBand();
                        monthInYearBands.Add(targetMonth);

                        targetMonth.Name = monthName;
                        targetMonth.Caption = week.Month.ToString();
                        targetMonth.AppearanceHeader.BackColor = Color.LightGreen;
                        CommonGridBandOption(targetMonth);

                        year.Children.Add(targetMonth);
                    }

                    BandedGridColumn gridCol = AddWeekColumn(week, true);
                    gridView.Columns.Add(gridCol);
                    targetMonth.Columns.Add(gridCol);
                    dataTable.Columns.Add(gridCol.FieldName, typeof(string));
                    SetForamttingRuleWeekRule(gridCol.FieldName, Color.Yellow);

                    BandedGridColumn gridCol2 = AddWeekColumn(week, false);
                    gridView.Columns.Add(gridCol2);
                    targetMonth.Columns.Add(gridCol2);
                    dataTable.Columns.Add(gridCol2.FieldName, typeof(string));
                    SetForamttingRuleWeekRule(gridCol2.FieldName, Color.SkyBlue);
                }
            }
        }


        private void CommonBandedGridColumnOption(BandedGridColumn gridCol, bool autoFillDown = true)
        {
            gridCol.OptionsColumn.AllowEdit = true;
            gridCol.OptionsColumn.FixedWidth = true;
            gridCol.AutoFillDown = autoFillDown;
        }

        private void CommonGridBandOption(GridBand band)
        {
            band.AppearanceHeader.Options.UseTextOptions = true;
            band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }


        private List<YearInfo> CreateYearInfos(DateTime? minDate, DateTime? maxDate)
        {
            if (minDate == null || maxDate == null) return null;

            Func<int, int?> GetLastWeekOfYear = (year) =>
            {
                DateTime lastDay = new DateTime(year + 1, 1, 1).AddDays(-1);
                return GetWeekOfYear(lastDay);
            };

            // CalendarInfo 데이터 작업
            List<YearInfo> infos = new List<YearInfo>();
            for (int i = minDate.Value.Year; i <= maxDate.Value.Year; i++)
            {
                infos.Add(new YearInfo() { Year = i });
            }

            if (minDate.Value.Year == maxDate.Value.Year)
            {
                int? minWeekOfYear = GetWeekOfYear(minDate);
                int? maxWeekOfYear = GetWeekOfYear(maxDate);
                if (minWeekOfYear == null || maxWeekOfYear == null)
                {
                    return null;
                }

                YearInfo target = infos.First(x => x.Year == minDate.Value.Year);
                if (target != null)
                {
                    //target.Weeks.AddRange(Enumerable.Range(minWeekOfYear.Value, maxWeekOfYear.Value - minWeekOfYear.Value + 1));
                    var addDatas = Enumerable.Range(minWeekOfYear.Value, maxWeekOfYear.Value - minWeekOfYear.Value + 1)
                                        .ToList()
                                        .Select(week => GetWeekInfo(target.Year, week));
                    target.Weeks.AddRange(addDatas);
                }
            }
            else
            {
                foreach (YearInfo yearData in infos)
                {

                    if (yearData.Year == minDate.Value.Year)
                    {
                        int? weekOfYear = GetWeekOfYear(minDate);
                        if (weekOfYear == null)
                        {
                            return null;
                        }
                        int? end = GetLastWeekOfYear(yearData.Year);

                        //yearData.Weeks.AddRange(Enumerable.Range(weekOfYear.Value, end.Value - weekOfYear.Value + 1));
                        var addDatas = Enumerable.Range(weekOfYear.Value, end.Value - weekOfYear.Value + 1)
                                        .ToList()
                                        .Select(week => GetWeekInfo(yearData.Year, week));
                        yearData.Weeks.AddRange(addDatas);
                    }
                    else if (yearData.Year == maxDate.Value.Year)
                    {
                        int? weekOfYear = GetWeekOfYear(maxDate);
                        if (weekOfYear == null)
                        {
                            return null;
                        }
                        //yearData.Weeks.AddRange(Enumerable.Range(1, weekOfYear.Value));
                        var addDatas = Enumerable.Range(1, weekOfYear.Value)
                                        .ToList()
                                        .Select(week => GetWeekInfo(yearData.Year, week));
                        yearData.Weeks.AddRange(addDatas);
                    }
                    else
                    {
                        int? end = GetLastWeekOfYear(yearData.Year);
                        //yearData.Weeks.AddRange(Enumerable.Range(1, end.Value));
                        var addDatas = Enumerable.Range(1, end.Value)
                                        .ToList()
                                        .Select(week => GetWeekInfo(yearData.Year, week));
                        yearData.Weeks.AddRange(addDatas);
                    }
                }
            }

            return infos;
        }


        private WeekInfo GetWeekInfo(int year, int weekNumber)
        {
            DateTime jan1st = new DateTime(year, 1, 1);
            DateTime yearLastDate = jan1st.AddYears(1).AddDays(-1);

            WeekInfo additem = new WeekInfo();
            additem.WeekNumber = weekNumber;
            if (weekNumber == 1)
            {
                additem.WeekStartDate = jan1st;
                additem.WeekEndDate = jan1st.AddDays((int)DayOfWeek.Saturday).AddDays(((int)jan1st.DayOfWeek * -1));
            }
            else
            {
                // 입력된 weekNumber 기준으로 해당 주차 특정날짜 가져옴
                DateTime temp = jan1st.AddDays((weekNumber - 1) * 7);
                int? tempResult = GetWeekOfYear(temp);
                if (tempResult != null && weekNumber == tempResult.Value)
                {
                    // 특정 날짜를 기준으로 해당 주차 일요일, 토요일 날짜 조회
                    DayOfWeek dayOfWeek = temp.DayOfWeek;
                    DateTime weekSunday = temp.AddDays(((int)dayOfWeek) * -1);
                    additem.WeekStartDate = weekSunday;
                    DateTime weekSaturday = weekSunday.AddDays(((int)DayOfWeek.Saturday));
                    if (weekSaturday > yearLastDate)
                        additem.WeekEndDate = yearLastDate;
                    else
                        additem.WeekEndDate = weekSaturday;
                }
            }

            return additem;
        }


        private int SetWeekNumberBaseOnFirstWeek(List<WeekInfo> weekDatas, int startIndex, bool isFirstYear = false)
        {
            var items = weekDatas.OrderBy(x => x.Year).ThenBy(x => x.WeekNumber);
            foreach (WeekInfo item in items)
            {
                if (item == items.First() && isFirstYear && item.WeekStartDate.Month == 1)
                {
                    item.WeekNumberBaseOnFirstWeek = ++startIndex;
                }
                else if (item.WeekStartDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    item.WeekNumberBaseOnFirstWeek = ++startIndex;
                }
                else
                {
                    item.WeekNumberBaseOnFirstWeek = startIndex;
                }
            }

            return startIndex;
        }


        private int? GetWeekOfYear(DateTime? targetDatetime)
        {
            if (targetDatetime == null) return null;
            // 1900년도 들어오면 null로 처리함
            if (targetDatetime.Value.Year == 1900) return null;

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ko-KR");
            System.Globalization.Calendar calendar = cultureInfo.Calendar;

            System.Globalization.CalendarWeekRule myCWR = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            return calendar.GetWeekOfYear(targetDatetime.Value, myCWR, myFirstDOW);

        }

        private BandedGridColumn AddWeekColumn(WeekInfo weekinfo, bool isPrearng)
        {
            BandedGridColumn gridCol = new BandedGridColumn(); ;

            string fieldName = $"Week_{(isPrearng ? "Prearng" : "Real")}_{weekinfo.Year}_{weekinfo.WeekNumber}";

            gridCol.ToolTip = $"{weekinfo.WeekNumberBaseOnFirstWeek}{Environment.NewLine}{weekinfo.WeekStartDate.ToString("MM-dd")} ~ {weekinfo.WeekEndDate.ToString("MM-dd")}";

            gridCol.FieldName = fieldName;
            gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            gridCol.MinWidth = 40;
            gridCol.Name = fieldName;
            //gridCol.Caption = $"{weekinfo.WeekNumber}";
            gridCol.Caption = $"{weekinfo.WeekNumberBaseOnFirstWeek}";
            gridCol.OptionsColumn.AllowEdit = false;
            gridCol.OptionsColumn.AllowMove = false;
            gridCol.OptionsColumn.AllowSize = false;
            gridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridCol.OptionsFilter.AllowAutoFilter = false;
            gridCol.OptionsFilter.AllowFilter = false;
            gridCol.AppearanceCell.Options.UseTextOptions = true;
            gridCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridCol.Visible = true;

            if (isPrearng == false) gridCol.RowIndex = 1;

            gridCol.Width = 35;

            CommonBandedGridColumnOption(gridCol, false);

            return gridCol;

        }

        private BandedGridColumn AddWeekColumnEmpty(string caption, bool isPrearng)
        {
            BandedGridColumn gridCol = new BandedGridColumn();

            //gridCol.FieldName = $"Week_Empty";
            gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            gridCol.MinWidth = 40;
            //gridCol.Name = $"Week_Empty";
            gridCol.Caption = caption;
            gridCol.OptionsColumn.AllowEdit = false;
            gridCol.OptionsColumn.AllowMove = false;
            gridCol.OptionsColumn.AllowSize = false;
            gridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridCol.OptionsFilter.AllowAutoFilter = false;
            gridCol.OptionsFilter.AllowFilter = false;

            gridCol.Visible = true;

            if (isPrearng == false) gridCol.RowIndex = 1;

            gridCol.Width = 40;

            return gridCol;
        }



        private void TreeListTest_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = $"{treelistMain.LeftCoord}, {treelistMain.TopVisibleNodeIndex}, {gridView.LeftCoord}. {gridView.TopRowIndex}";
        }

        private void treelistMain_TopVisibleNodeIndexChanged(object sender, EventArgs e)
        {
            //gridView.TopRowIndex = treelistMain.TopVisibleNodeIndex;
            textBox1.Text = $"{treelistMain.LeftCoord}, {treelistMain.TopVisibleNodeIndex}, {gridView.LeftCoord}. {gridView.TopRowIndex}";
        }

        private void treelistMain_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            textBox1.Text = "treelistMain_FocusedColumnChanged";
        }

        private void treelistMain_SelectionChanged(object sender, EventArgs e)
        {
            textBox1.Text = "treelistMain_SelectionChanged";

            var data = treelistMain.GetFocusedRow();

          
        }
    }

    /// <summary>
    /// grid 컬럼에 필요한 추가 데이터 입력. Tag에 저장해서 사용
    /// </summary>
    public class GridColAppendData
    {
        /// <summary>
        /// 초기 설정시 설정한 band 정보
        /// </summary>
        public GridBand InitBaseGridBand { get; set; }
    }
}
