using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        private List<WeekInfo> _firstMonthWeek = new List<WeekInfo>();

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            AddCol();


            DataTable gridData = new DataTable();

            gridControl1.DataSource = gridData;

            SetGridView();

            CreateData(gridData);

            MakeCol2();

            AddWeekSchedule(gridData);
            SetColTempDt();
            gridData.Columns.Add("tempDt", typeof(DateTime));

            gridData.Columns.Add("RowSelect", typeof(bool));

            SetMaskedNum();
            gridData.Columns.Add("MaskedNum", typeof(string));
        }

        private void SetMaskedNum()
        {
            RepositoryItemTextEdit colType = new RepositoryItemTextEdit();

            colType.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            colType.Mask.EditMask = "0000-00-000";
            colType.Mask.UseMaskAsDisplayFormat = true;
            colType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colType.KeyDown += ColType_KeyDown;

            colMaskedNum.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colMaskedNum.ColumnEdit = colType;
            colMaskedNum.ColumnEdit.Name = "MaskedNum";
        }

        private void ColType_KeyDown(object sender, KeyEventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;
            object temp = textEditor.EditValue;
            if (temp != null && string.IsNullOrEmpty(temp.ToString()))
            {
                textEditor.EditValue = DateTime.Now.ToString("yyyyMM") + temp.ToString();
            }
        }

        private void SetColTempDt()
        {
            RepositoryItemDateEdit dateColType = new RepositoryItemDateEdit();

            dateColType.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateColType.Mask.EditMask = "yyyy-MM-dd";
            dateColType.Mask.UseMaskAsDisplayFormat = true;
            dateColType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dateColType.DisplayFormat.FormatString = "d";
            //dateColType.DisplayFormat.FormatType = FormatType.DateTime;
            

            colTempDt.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colTempDt.ColumnEdit = dateColType;
            colTempDt.ColumnEdit.Name = "Date";
        }

        private void SetToolTip()
        {
            
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
            gridView.OptionsSelection.EnableAppearanceFocusedRow = false;

            //gridView.OptionsSelection.MultiSelect = true;
            //gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            //gridView.OptionsSelection.CheckBoxSelectorField = "RowSelect";
            ////gridView.Columns["RowSelect"].Visible = false;
            //gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DefaultBoolean.True;

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
            for (int i = 1; i<=10; i++)
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
                dataTable.Columns.Add($"test-{i}", typeof(int));
            }
            


            Random rm = new Random();
            for(int i = 0; i <= 10; i++)
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

            foreach(var root in rootBand)
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

            foreach(var row in childRows)
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



        #region New column menu
        DevExpress.Utils.Menu.DXMenuCheckItem CreateCheckItem(string caption, GridColumn column, FixedStyle style, DevExpress.Utils.Svg.SvgImage image, bool isBeginGroup = false)
        {
            DevExpress.Utils.Menu.DXMenuCheckItem item = new DevExpress.Utils.Menu.DXMenuCheckItem(caption, column.Fixed == style, null, new EventHandler(OnFixedClick));
            item.ImageOptions.SvgImage = image;
            item.ImageOptions.SvgImageSize = svgImageCollection1.ImageSize;
            item.BeginGroup = isBeginGroup;
            if (column is BandedGridColumn)
            {
                item.Tag = new MenuInfo(column, style);
            }
            else
            {
                item.Tag = new MenuInfo(column, style);
            }
            return item;
        }
        void OnFixedClick(object sender, EventArgs e)
        {
            DevExpress.Utils.Menu.DXMenuItem item = sender as DevExpress.Utils.Menu.DXMenuItem;
            MenuInfo info = item.Tag as MenuInfo;
            if (info == null) return;

            if (info.Column is BandedGridColumn)
            {
                GridBand fixBand = gridView.Bands["AutoCalc_FixBand"];

                if (info.Column is BandedGridColumn && info.Column.Tag is GridColAppendData)
                {
                    BandedGridColumn col = info.Column as BandedGridColumn;
                    GridColAppendData colAppendData = info.Column.Tag as GridColAppendData;
                    if (info.Style == FixedStyle.Left)
                    {
                        col.OwnerBand.Columns.Remove(col);
                        fixBand.Columns.Add(col);
                    }
                    else if (info.Style == FixedStyle.None)
                    {
                        if (colAppendData?.InitBaseGridBand != null)
                        {
                            fixBand.Columns.Remove(col);
                            colAppendData.InitBaseGridBand.Columns.Add(col);

                        }
                    }
                }
            }
            else
            {
                info.Column.Fixed = info.Style;
            }
        }
        class MenuInfo
        {
            public MenuInfo(GridColumn column, FixedStyle style)
            {
                this.Column = column;
                this.Style = style;
            }
            public FixedStyle Style;
            public GridColumn Column;
        }

        

        #endregion

        private void advBandedGridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)
            {
                DevExpress.XtraGrid.Menu.GridViewColumnMenu menu = e.Menu as DevExpress.XtraGrid.Menu.GridViewColumnMenu;
                //menu.Items.Clear();
                if (menu.Column != null &&
                     (menu.Column.Name.StartsWith("Week_Real", StringComparison.OrdinalIgnoreCase) == false
                             && menu.Column.Name.StartsWith("Week_Prearng", StringComparison.OrdinalIgnoreCase) == false
                             && menu.Column.Name.Equals("AutoCalc_IS_COMPLETED", StringComparison.OrdinalIgnoreCase) == false
                             ))
                {
                    menu.Items.Add(CreateCheckItem("Fix 해제", menu.Column, FixedStyle.None, svgImageCollection1[3], true));
                    menu.Items.Add(CreateCheckItem("Left Fix", menu.Column, FixedStyle.Left, svgImageCollection1[4]));
                    //menu.Items.Add(CreateCheckItem("Right Fix", menu.Column, FixedStyle.Right, svgImageCollection1[5]));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FirstGridBand.Columns.Remove(BandedGridColumn);
            //RequiredGridBand.Columns.Add(BandedGridColumn);

             var test = middleband1.Columns;

            BandedGridColumn col = middleband1.Columns["col5"];
            middleband1.Columns.Remove(col);

            GridBand band = gridView.Bands["FixBand"];

            band.Children[0].Columns.Add(col);
        }

        private void advBandedGridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                //e.Appearance.BackColor = Color.FromArgb(200, Color.SkyBlue);
                e.Appearance.BorderColor = Color.Red;
                e.Appearance.BackColor = Color.FromArgb(30, Color.Yellow);

                //e.Appearance.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void advBandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            BandedGridColumn currentColumn = e.Column as BandedGridColumn;
            if (currentColumn != null)
            {
                Rectangle drawBounds = new Rectangle(e.Bounds.X, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).Bounds.Y, e.Bounds.Width, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).Bounds.Height);

                if (currentColumn.Name.StartsWith("Week"))
                {
                    // week 컬럼이 해당 달의 첫번쨰 week인경우 특정 cell 왼쪽에 라인 표시처리
                    string findName = currentColumn.Name.Replace("Week_Prearng_", "").Replace("Week_Real_", "");
                    bool exists = _firstMonthWeek.Exists(x => $"{x.Year}_{x.WeekNumber}" == findName);
                    if (exists)
                    {
                        //Rectangle drawBounds = new Rectangle(e.Bounds.X, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).Bounds.Y, e.Bounds.Width, (e.Cell as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridCellInfo).Bounds.Height);
                        DrawVerticalDividedLine(e.Graphics, drawBounds, 2);
                    }
                }

                if (e.RowHandle == gridView.FocusedRowHandle)
                {
                    e.DefaultDraw();
                    //DrawCellBorder(e, currentColumn.Name);
                    DrawCellBorder(e.Graphics, drawBounds, 2, currentColumn.Name);
                    e.Handled = true;
                }
            }
        }

        void DrawVerticalDividedLine(Graphics graphics, Rectangle r, int penWidth)
        {
            DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridViewInfo bandViewInfo = gridView.GetViewInfo() as DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridViewInfo;
            //Color vertLineColor = bandViewInfo.PaintAppearance.VertLine.ForeColor;

            // 현재 Cell의 x-2좌표에 그려야 조건부 서식이 있어도 보임
            // 조건부 서식이 있는경우 x좌표에 그릴경우 drawline위에 덮어 씌워서 안보임.
            graphics.DrawLine(new Pen(Color.Blue, penWidth),
                               new Point(r.X - 2, r.Y),
                               new Point(r.X - 2, r.Y + r.Height));


        }


        /// <summary>
        /// 선택된 row에 대해 cell 위 아래 line 그려줌. 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="frameBounds"></param>
        /// <param name="penWidth"></param>
        /// <param name="colName"></param>
        private void DrawCellBorder(Graphics graphics, Rectangle frameBounds, int penWidth, string colName)
        {
            if (colName.StartsWith("Week_Real") == false)
                graphics.DrawLine(new Pen(Color.Red, 2), new Point(frameBounds.X, frameBounds.Y + 1), new Point(frameBounds.X + frameBounds.Width, frameBounds.Y + 1));
            if (colName.StartsWith("Week_Prearng") == false)
                graphics.DrawLine(new Pen(Color.Red, 2), new Point(frameBounds.X, frameBounds.Y + frameBounds.Height - 1),
                    new Point(frameBounds.X + frameBounds.Width, frameBounds.Y + frameBounds.Height - 1));
        }

        private void advBandedGridView1_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.RowHandle == view.FocusedRowHandle)
            //{
            //    //e.DefaultDraw();

            //    e.Appearance.BackColor = Color.FromArgb(200, Color.Red);
            //    //e.Handled = true;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "output.xlsx";
            saveDialog.Filter = "Excel 2010 (.xlsx)|*.xlsx";
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Title = "Save an Excel File";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                //var option = new DevExpress.XtraPrinting.XlsxExportOptions();
                //option.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                gridView.ExportToXlsx(saveDialog.FileName);
            }

            //gridView.ExportToXlsx("output.xlsx");
        }

        private void gridView_DragObjectOver(object sender, DevExpress.XtraGrid.Views.Base.DragObjectOverEventArgs e)
        {
            GridColumn column = e.DragObject as GridColumn;
        }

        private void gridView_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            GridColumn column = e.DragObject as GridColumn;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SuperToolTip superTip = new SuperToolTip();
            ToolTipItem item = new ToolTipItem();
            
            item.Text = "Transform data to its most appropriate and readable visual representation. Insert bar charts, pie graphs, line graphs, or financial diagrams.";
            ToolTipItem footer = new ToolTipItem();
            superTip.AllowHtmlText = DefaultBoolean.True;
            footer.Text = "<a href=\"https://www.devexpress.com\">Learn more</a>";
            superTip.Items.AddTitle("Add a Chart");
            superTip.Items.Add(item);
            superTip.Items.AddSeparator();
            superTip.Items.Add(footer);
            btnTest.SuperTip = superTip;
        }

        private SuperToolTip GetSuperTip(GridColumn col)
        {
            SuperToolTip superTip = new SuperToolTip();
            ToolTipItem item = new ToolTipItem();

            item.Text = $"test {col.Name}";
            ToolTipItem footer = new ToolTipItem();
            superTip.AllowHtmlText = DefaultBoolean.True;
            footer.Text = "<a href=\"https://www.devexpress.com\">Learn more</a>";
            superTip.Items.AddTitle("Add a Chart");
            superTip.Items.Add(item);
            superTip.Items.AddSeparator();
            superTip.Items.Add(footer);

            return superTip;
        }

        private void tooltip_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != gridControl1) return;

            ToolTipControlInfo info = null;
            //Get the view at the current mouse position
            GridView view = gridControl1.GetViewAt(e.ControlMousePosition) as GridView;
            if (view == null) return;
            //Get the view's element information that resides at the current position
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
            //Display a hint for row indicator cells
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.Column)
            {
                //An object that uniquely identifies a row indicator cell
                //object o = hi.HitTest.ToString() + hi.RowHandle.ToString();
                //string text = "Row " + hi.RowHandle.ToString();
                //info = new ToolTipControlInfo(o, text);

                tooltip.SetSuperTip(e.SelectedControl, GetSuperTip(hi.Column));
            }
            //Supply tooltip information if applicable, otherwise preserve default tooltip (if any)
            if (info != null)
                e.Info = info;
        }

        private void btnLayoutExport_Click(object sender, EventArgs e)
        {
            string layoutFileName = "LayoutSchedule.xml";
            string appLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string saveDir = System.IO.Path.Combine(appLocal, "AMIS");
            if (Directory.Exists(saveDir) == false) Directory.CreateDirectory(saveDir);
            string savePath = System.IO.Path.Combine(saveDir, layoutFileName);
            gridView.SaveLayoutToXml(savePath, DevExpress.Utils.OptionsLayoutBase.FullLayout);

        }

        private void btnLayoutLoad_Click(object sender, EventArgs e)
        {
            string layoutFileName = "LayoutSchedule.xml";
            string appLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string saveDir = System.IO.Path.Combine(appLocal, "AMIS");
            if (Directory.Exists(saveDir) == false) Directory.CreateDirectory(saveDir);
            string savePath = System.IO.Path.Combine(saveDir, layoutFileName);
            gridView.RestoreLayoutFromXml(savePath, DevExpress.Utils.OptionsLayoutBase.FullLayout);
        }

        private void gridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null || e.Value == DBNull.Value) return;
            if (e.Column.ColumnEditName == "Date")
            {
                DateTime dt;
                if (DateTime.TryParse(e.Value.ToString(), out dt))
                {
                    e.DisplayText = dt.ToString("yyyy-MM-dd");
                }
            } 
            else if(e.Column.ColumnEditName == "MaskedNum")
            {
                Debug.WriteLine(e.Value);
                e.DisplayText = Convert.ToInt32(e.Value).ToString("0000-00-000");
            }
            else
            {
                e.DisplayText = e.Value.ToString();
            }
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

    /// <summary>
    /// week 항목별 정보
    /// </summary>
    public class WeekInfo
    {
        /// <summary>
        /// week 의 해당 년도
        /// </summary>
        public int Year
        {
            get { return WeekStartDate.Year; }
        }
        /// <summary>
        /// week 의 해당 달
        /// </summary>
        public int Month
        {
            get { return WeekStartDate.Month; }
        }
        /// <summary>
        /// week number
        /// </summary>
        public int WeekNumber { get; set; }

        /// <summary>
        /// "WeekNumber" 첫 번째 주를 기준으로 함
        /// </summary>
        public int WeekNumberBaseOnFirstWeek { get; set; }
        /// <summary>
        /// 해당 week 의 시작일
        /// </summary>
        public DateTime WeekStartDate { get; set; }
        /// <summary>
        /// 해당 week의 종료일
        /// </summary>
        public DateTime WeekEndDate { get; set; }
    }

    /// <summary>
    /// week 컬럼 구성시 사용
    /// 스케줄러 전체 기간 기준으로 year, week 정보 생성시 씀
    /// </summary>
    public class YearInfo
    {
        public YearInfo()
        {
            Weeks = new List<WeekInfo>();
        }
        public int Year { get; set; }
        public List<WeekInfo> Weeks { get; set; }

        public override string ToString()
        {
            return $"Year : {Year}{Environment.NewLine}Weeks : {string.Join(",", Weeks)}";
        }
    }
}
