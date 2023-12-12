using DevExpress.XtraTreeList.Columns;
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
    public partial class TreeListTest : Form
    {
        private List<WeekInfo> _firstMonthWeek = new List<WeekInfo>();
        DateTime? minDate = new DateTime(2022, 3, 10);
        DateTime? maxDate = new DateTime(2024, 03, 10);

        public TreeListTest()
        {
            InitializeComponent();
            
            InitTreeView();

            this.Load += TreeListTest_Load;
        }

        private void TreeListTest_Load(object sender, EventArgs e)
        {
            DataTable gridData = InitData();

            SetInitGrid(GridHaederInfo());
            
            treelistMain.BeginUnboundLoad();




            treelistMain.DataSource = gridData;
            SetInitGridOption();
            AppenColumns(gridData);
            SetForamttingRule();

            SetColumnError(gridData);

            AppenWeekColumns(gridData);

            SetWeekInDataTable(gridData);
            //SetLevelCheckInDataTable(gridData);

            //SetIdAndParentId(gridData);

            treelistMain.EndUnboundLoad();
            treelistMain.ExpandAll();
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
                treeDatas.Add(new TreeItem() { JobTitle = $"job{i}", Id = $"{i}", ParentId = level1parent });
                level2parent = $"{i}";
                for(int j = 1; j <=10; j++)
                {
                    treeDatas.Add(new TreeItem() { JobTitle = $"job{i}-{j}", Id = $"{i}-{j}", ParentId = level2parent });
                    level3parent = $"{i}-{j}";
                    for(int k=1; k<=10; k++)
                    {
                        treeDatas.Add(new TreeItem() { JobTitle = $"job{i}-{j}-{k}", Id = $"{i}-{j}-{k}", ParentId = level3parent });
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


        private void SetIdAndParentId(DataTable dataTable)
        {

            DataColumn idCol = new DataColumn("ID", typeof(string));
            DataColumn parentIdCol = new DataColumn("ParentID", typeof(string));
            dataTable.Columns.Add(idCol);
            dataTable.Columns.Add(parentIdCol);

            //parentIdCol.SetOrdinal(0);
            //idCol.SetOrdinal(0);
            //dataTable.Columns["TASK_NM"].SetOrdinal(2);

            foreach (DataRow row in dataTable.Rows)
            {
                string taskCd = row["AutoCalc_UPR_TASK_CD"].ToString();
                row["ID"] = row["TASK_CD"];
                row["ParentID"] = taskCd;
            }
        }

        /// <summary>
        /// grid 옵션 설정
        /// </summary>
        private void SetInitGridOption()
        {
            // 조회 기능 show
            treelistMain.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Search;
            treelistMain.ShowFindPanel();

            // group footer 필요하면 show
            //gridView.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;


            // group panel show
            //treelistMain.OptionsView.ShowGroupPanel = true;

            // 메뉴에 조건부 서식 show
            treelistMain.OptionsMenu.ShowConditionalFormattingItem = true;

            //gridView.ScrollStyle = ScrollStyleFlags.None;
            //gridView.OptionsScrollAnnotations.ShowCustomAnnotations = DevExpress.Utils.DefaultBoolean.True;
            treelistMain.OptionsScrollAnnotations.ShowErrors = DevExpress.Utils.DefaultBoolean.True;
            //gridView.OptionsScrollAnnotations.ShowSelectedRows = DevExpress.Utils.DefaultBoolean.True;

            treelistMain.FixedLineWidth = 3;

            // Disable appearance settings that paint the focused row.
            // 포커스 제거
            treelistMain.OptionsSelection.EnableAppearanceFocusedRow = false;
        }

        #region Grid 컬럼 추가
        /// <summary>
        /// 컬럼 추가
        /// </summary>
        /// <param name="dataTable"></param>
        private void AppenColumns(DataTable dataTable)
        {
            // 왼쪽 고정 컬럼 band 및 컬럼 추가
            TreeListBand fixBand = new TreeListBand();
            fixBand.Name = "AutoCalc_FixBand";
            fixBand.Caption = "고정";
            fixBand.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            CommonBandOption(fixBand);
            treelistMain.Bands.Add(fixBand);

            // 자동 생성되는 컬럼용 band 추가
            TreeListBand autoCalcBand = new TreeListBand();
            autoCalcBand.Caption = "자동 생성 Data(A)";
            CommonBandOption(autoCalcBand);
            treelistMain.Bands.Add(autoCalcBand);

            // 완료여부 표시용 컬럼
            TreeListColumn complitedCol = AppendComplitedColumn();
            treelistMain.Columns.Add(complitedCol);
            fixBand.Columns.Add(complitedCol);

            // 부서 cd로 path 생성
            AppendLevelPathColumn(dataTable, autoCalcBand);

            // 상위 부서 정보 컬럼 생성
            AppendTaskLevelAndUprTaskCdColumn(dataTable, autoCalcBand);

            // 계산된 진행률 컬럼 생성
            AppendTaskProgressColumn(dataTable, autoCalcBand);


            // level 체크용. level 구분하기 어렵다고 하여 고정 band에 level check 용 컬럼 추가함.
            AppendLevelCheckColumn(dataTable, fixBand);
        }

        /// <summary>
        /// band grid col 공통 옵션
        /// </summary>
        /// <param name="gridCol"></param>
        /// <param name="autoFillDown"></param>
        private void CommonColumnOption(TreeListColumn gridCol, bool autoFillDown = true)
        {
            gridCol.OptionsColumn.AllowEdit = false;
            gridCol.OptionsColumn.FixedWidth = true;
        }

        /// <summary>
        /// band 공통 옵션
        /// </summary>
        /// <param name="band"></param>
        private void CommonBandOption(TreeListBand band)
        {
            band.AppearanceHeader.Options.UseTextOptions = true;
            band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        /// <summary>
        /// 완료 컬럼 추가. 따로 데이터 넣지는 않고 조건부서식을 사용해서 컬럼에 표시용도임
        /// </summary>
        private TreeListColumn AppendComplitedColumn()
        {
            TreeListColumn gridCol = new TreeListColumn();

            gridCol.FieldName = "AutoCalc_IS_COMPLETED";
            gridCol.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            //gridCol.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("completedColumn.ImageOptions.SvgImage")));
            gridCol.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            gridCol.MaxWidth = 40;
            gridCol.MinWidth = 40;
            gridCol.Name = "AutoCalc_IS_COMPLETED";
            gridCol.Caption = "완료";
            //gridCol.OptionsColumn.AllowFocus = false;
            gridCol.OptionsColumn.AllowMove = false;
            gridCol.OptionsColumn.AllowSize = false;
            gridCol.OptionsColumn.AllowSort = false;
            //gridCol.OptionsFilter.AllowAutoFilter = false;
            //gridCol.OptionsFilter.AllowFilter = false;
            gridCol.ToolTip = "완료";
            gridCol.Visible = true;
            gridCol.VisibleIndex = 0;
            gridCol.Width = 40;

            CommonColumnOption(gridCol);

            return gridCol;
        }

        /// <summary>
        /// level 체크용. level 구분하기 어렵다고 하여 고정 band에 level check 용 컬럼 추가함.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="parentBand"></param>
        private void AppendLevelCheckColumn(DataTable dt, TreeListBand parentBand)
        {
            string pattern = @"^LEVEL[1-9]_CD$";
            IEnumerable<DataColumn> levelColumns = dt.Columns.Cast<DataColumn>().Where(column => System.Text.RegularExpressions.Regex.IsMatch(column.ColumnName, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase));


            for (int index = 1; index <= levelColumns.Count(); index++)
            {
                TreeListColumn gridCol = new TreeListColumn();
                string colName = $"AutoCalc_LEVEL_CHECK_{index}";

                //gridCol.ToolTip = $"현재 업무의 LEVEL 정보입니다.{Environment.NewLine}자동으로 생성되는 정보입니다.";

                gridCol.FieldName = colName;
                //gridCol.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
                //gridCol.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("completedColumn.ImageOptions.SvgImage")));
                //gridCol.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
                //gridCol.MaxWidth = 40;
                gridCol.MinWidth = 40;
                gridCol.Name = colName;
                gridCol.Caption = $"{index}";
                //gridCol.OptionsColumn.AllowFocus = false;
                gridCol.OptionsColumn.AllowEdit = false;
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.AllowSize = false;
                //gridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                //gridCol.OptionsFilter.AllowAutoFilter = false;
                //gridCol.OptionsFilter.AllowFilter = false;
                gridCol.AppearanceCell.Options.UseTextOptions = true;
                gridCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridCol.Visible = true;

                gridCol.Width = 40;

                CommonColumnOption(gridCol);

                treelistMain.Columns.Add(gridCol);
                parentBand.Columns.Add(gridCol);

                dt.Columns.Add(colName, typeof(string));
            }
        }

        /// <summary>
        /// 업무 코드 Full Path 컬럼
        /// </summary>
        /// <param name="dt"></param>
        private void AppendLevelPathColumn(DataTable dt, TreeListBand parentBand)
        {
            TreeListColumn gridCol = new TreeListColumn();

            gridCol.ToolTip = $"현재 업무의 Full Path 정보입니다.{Environment.NewLine}자동으로 생성되는 정보입니다.";

            gridCol.FieldName = "AutoCalc_PATH";
            //gridCol.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            //gridCol.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("completedColumn.ImageOptions.SvgImage")));
            //gridCol.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            //gridCol.MaxWidth = 40;
            gridCol.MinWidth = 40;
            gridCol.Name = "path";
            gridCol.Caption = "Level Path(A)";
            //gridCol.OptionsColumn.AllowFocus = false;
            gridCol.Visible = true;

            gridCol.Tag = new TreeListColAppendData() { InitBaseBand = parentBand };
            gridCol.Width = 120;

            CommonColumnOption(gridCol);


            treelistMain.Columns.Add(gridCol);
            parentBand.Columns.Add(gridCol);

            // 정규식 패턴으로 변경
            // LEVEL로 시작_CD로 끝나는 컬럼 단 사이에 1-9까지만 허용
            string pattern = @"^LEVEL[1-9]_CD$";
            IEnumerable<DataColumn> levelColumns = dt.Columns.Cast<DataColumn>().Where(column => System.Text.RegularExpressions.Regex.IsMatch(column.ColumnName, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase));


            dt.Columns.Add("AutoCalc_PATH", typeof(string));

            // 각 행에 "path" 컬럼 값 설정
            foreach (DataRow row in dt.Rows)
            {
                string pathValue = string.Join("/", levelColumns.Select(column => row[column].ToString()).Where(x => string.IsNullOrEmpty(x) == false));
                if (pathValue.EndsWith("/") == false) pathValue += "/";

                row["AutoCalc_PATH"] = pathValue;
            }
        }

        /// <summary>
        /// 업무 단계, 상위 업무 코드 컬럼
        /// </summary>
        /// <param name="dt"></param>
        private void AppendTaskLevelAndUprTaskCdColumn(DataTable dt, TreeListBand parentBand)
        {
            TreeListColumn gridColTaskLevel = new TreeListColumn();

            gridColTaskLevel.ToolTip = $"현재 업무의 업무 단계를 표시합니다.{Environment.NewLine}자동생성되는 정보입니다.";

            gridColTaskLevel.FieldName = "AutoCalc_TASK_LEVEL";
            gridColTaskLevel.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            gridColTaskLevel.MinWidth = 40;
            gridColTaskLevel.Name = "taskLevel";
            gridColTaskLevel.Caption = "업무 단계(A)";
            //gridColTaskLevel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //gridColTaskLevel.VisibleIndex = 0;
            gridColTaskLevel.Width = 120;
            gridColTaskLevel.Visible = true;
            gridColTaskLevel.Tag = new TreeListColAppendData() { InitBaseBand = parentBand };


            CommonColumnOption(gridColTaskLevel);

            treelistMain.Columns.Add(gridColTaskLevel);
            parentBand.Columns.Add(gridColTaskLevel);

            TreeListColumn gridColUprTaskCd = new TreeListColumn();

            gridColUprTaskCd.ToolTip = $"상위 업무 코드 입니다.{Environment.NewLine}자동으로 생성되는 정보입니다.";

            gridColUprTaskCd.FieldName = "AutoCalc_UPR_TASK_CD";
            gridColUprTaskCd.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            gridColUprTaskCd.MinWidth = 40;
            gridColUprTaskCd.Name = "uprTaskCd";
            gridColUprTaskCd.Caption = "상위 업무 코드(A)";
            //gridColUprTaskCd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            gridColUprTaskCd.Width = 120;
            gridColUprTaskCd.Visible = true;
            gridColUprTaskCd.Tag = new TreeListColAppendData() { InitBaseBand = parentBand };

            CommonColumnOption(gridColUprTaskCd);

            treelistMain.Columns.Add(gridColUprTaskCd);
            parentBand.Columns.Add(gridColUprTaskCd);

            dt.Columns.Add("AutoCalc_UPR_TASK_CD", typeof(string));
            dt.Columns.Add("AutoCalc_TASK_LEVEL", typeof(int));

            // "level"로 시작하는 컬럼명을 조회하고 정렬
            var levelColumns = dt.Columns.Cast<DataColumn>()
                .Where(column => column.ColumnName.StartsWith("LEVEL", StringComparison.OrdinalIgnoreCase))
                .OrderBy(column => column.ColumnName);

            // "AutoCalc_UPR_TASK_CD" 값을 설정
            string AutoCalc_UPR_TASK_CD = string.Empty;
            // 데이터가 있는 level을 찾은경우
            bool findLevel = false;
            foreach (DataRow row in dt.Rows)
            {
                row["AutoCalc_TASK_LEVEL"] = levelColumns.Count(x => string.IsNullOrEmpty(row[x.ColumnName].ToString()) == false);

                findLevel = false;
                foreach (DataColumn col in levelColumns.OrderByDescending(x => x.ColumnName))
                {
                    //if (row[col] == DBNull.Value) break;
                    string checkLevel = row[col].ToString();


                    if (!string.IsNullOrEmpty(checkLevel))
                    {
                        if (findLevel == false)
                        {
                            // 처음으로 찾은 데이터가 있는 level이 LEVEL1_CD인경우 상위 level은 빈값으로 처리
                            if (row[col] == row[levelColumns.First()])
                            {
                                row["AutoCalc_UPR_TASK_CD"] = string.Empty;
                                break;
                            }
                            findLevel = true;
                        }
                        else
                        {
                            // 두번째로 데이터가 있는 level을 찾은경우 상위 level로 설정함
                            row["AutoCalc_UPR_TASK_CD"] = checkLevel;
                            break;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 계산된 진행률 컬럼
        /// </summary>
        /// <param name="dt"></param>
        private void AppendTaskProgressColumn(DataTable dt, TreeListBand parentBand)
        {
            TreeListColumn gridCol = new TreeListColumn();

            gridCol.ToolTip = $"최종 업무단계 이면 진행률을 표시. 하위 업무단계가 있는 경우 하위업무 단계 평균을 구하여 표시합니다.{Environment.NewLine}자동으로 생성되는 정보입니다.";

            gridCol.FieldName = "AutoCalc_PROGRS_RT";
            gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            gridCol.MinWidth = 40;
            gridCol.Name = "AutoCalc_PROGRS_RT";
            gridCol.Caption = "진행률(A)";
            gridCol.Visible = true;
            gridCol.Tag = new TreeListColAppendData() { InitBaseBand = parentBand };
            gridCol.Width = 120;

            //gridCol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //gridCol.DisplayFormat.FormatString = "F2";

            CommonColumnOption(gridCol);

            treelistMain.Columns.Add(gridCol);
            parentBand.Columns.Add(gridCol);

            dt.Columns.Add("AutoCalc_PROGRS_RT", typeof(double));


            // "AutoCalc_PROGRS_RT" 컬럼 값 설정
            // task level 가장 높은거 순서대로 진행률 처리함
            foreach (DataRow row in dt.Rows.Cast<DataRow>().OrderByDescending(r => Convert.ToInt32(r["AutoCalc_TASK_LEVEL"])))
            {
                string path = row["AutoCalc_PATH"].ToString();

                // task path startwith 와 일치 하는게 있으면 하위 level이 있다고 판단함.
                // 현재 row의 level + 1
                // 현재 row 조건에서 제외
                var rowsWithSamePath = dt.Rows.Cast<DataRow>()
                    .Where(r => r["AutoCalc_PATH"].ToString().StartsWith(path) && Convert.ToInt32(r["AutoCalc_TASK_LEVEL"]) == Convert.ToInt32(row["AutoCalc_TASK_LEVEL"]) + 1 && r != row);

                // 하위 level이 없으면 입력된 진행률을 "AutoCalc_PROGRS_RT" 에 넣어줌.
                // 있으면 평균값 구해서 넣음
                if (rowsWithSamePath.Count() == 0)
                {
                    row["AutoCalc_PROGRS_RT"] = row["PROGRS_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PROGRS_RT"]);
                }
                else
                {
                    double avgPROGRS_RT = rowsWithSamePath.Average(r => r["AutoCalc_PROGRS_RT"] == DBNull.Value ? 0 : Convert.ToDouble(r["AutoCalc_PROGRS_RT"]));
                    row["AutoCalc_PROGRS_RT"] = avgPROGRS_RT;
                }
            }
        }

        /// <summary>
        /// week 컬럼 추가
        /// </summary>
        /// <param name="weekinfo"></param>
        /// <param name="isPrearng"></param>
        /// <returns></returns>
        private TreeListColumn AddWeekColumn(WeekInfo weekinfo, bool isPrearng)
        {
            TreeListColumn gridCol = new TreeListColumn(); ;

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
            gridCol.OptionsColumn.AllowSort = false;
            gridCol.OptionsFilter.AllowAutoFilter = false;
            gridCol.OptionsFilter.AllowFilter = false;
            gridCol.AppearanceCell.Options.UseTextOptions = true;
            gridCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridCol.Visible = true;

            if (isPrearng == false) gridCol.RowIndex = 1;

            gridCol.Width = 40;

            CommonColumnOption(gridCol, false);

            return gridCol;

        }

        /// <summary>
        /// week 구분 컬럼 추가
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="isPrearng"></param>
        /// <returns></returns>
        private TreeListColumn AddWeekColumnEmpty(string caption, bool isPrearng)
        {
            TreeListColumn gridCol = new TreeListColumn();

            //gridCol.FieldName = $"Week_Empty";
            gridCol.ImageOptions.Alignment = System.Drawing.StringAlignment.Center;
            gridCol.MinWidth = 40;
            //gridCol.Name = $"Week_Empty";
            gridCol.Caption = caption;
            gridCol.OptionsColumn.AllowEdit = false;
            gridCol.OptionsColumn.AllowMove = false;
            gridCol.OptionsColumn.AllowSize = false;
            gridCol.OptionsColumn.AllowSort = false;
            gridCol.OptionsFilter.AllowAutoFilter = false;
            gridCol.OptionsFilter.AllowFilter = false;

            gridCol.Visible = true;

            if (isPrearng == false) gridCol.RowIndex = 1;

            gridCol.Width = 40;

            return gridCol;
        }
        #endregion

        #region Grid 조건부 서식
        /// <summary>
        /// 기본 조건부 서식 반영
        /// </summary>
        private void SetForamttingRule()
        {
            treelistMain.FormatRules.Clear();

            SetForamttingRuleProgressBar();
            SetForamttingRuleAutoCalcProgressBar();
            SetForamttingRuleComplited();
            SetForamttingRuleFinshDayExceeded();
            SetForamttingRuleFinshDayEmpty();

        }

        /// <summary>
        /// grid 에 조건부 서식 등록
        /// </summary>
        /// <param name="targetCol"></param>
        /// <param name="rule"></param>
        private void AddRule(string targetCol, TreeListFormatRule rule)
        {
            TreeListColumn col = treelistMain.Columns[targetCol];
            if (col != null)
            {
                rule.Column = col;
                treelistMain.FormatRules.Add(rule);
            }
        }

        /// <summary>
        /// 조건부 서식 생성. 진행률 bar로 생성
        /// </summary>
        private void SetForamttingRuleProgressBar()
        {
            string targetCol = "PROGRS_RT";
            TreeListFormatRule rule = new TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar conditionRule = new DevExpress.XtraEditors.FormatConditionRuleDataBar();

            //gridFormatRule1.Column = _;
            //gridFormatRule1.Name = "Format0";
            conditionRule.MaximumType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            conditionRule.MinimumType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            conditionRule.Maximum = 100;
            conditionRule.Minimum = 0;
            conditionRule.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#76923C");
            conditionRule.Appearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#76923C");
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }

        /// <summary>
        /// 조건부 서식. 자동생성된 진행률 bar로 생성
        /// </summary>
        private void SetForamttingRuleAutoCalcProgressBar()
        {
            string targetCol = "AutoCalc_PROGRS_RT";
            TreeListFormatRule rule = new TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleDataBar conditionRule = new DevExpress.XtraEditors.FormatConditionRuleDataBar();

            conditionRule.MaximumType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            conditionRule.MinimumType = DevExpress.XtraEditors.FormatConditionValueType.Number;
            conditionRule.Maximum = 100;
            conditionRule.Minimum = 0;
            conditionRule.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#E36C09");
            conditionRule.Appearance.BorderColor = System.Drawing.ColorTranslator.FromHtml("#E36C09");
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }

        /// <summary>
        /// 조건부 서식 생성. 완료시 완료 컬럼에 표시
        /// </summary>
        private void SetForamttingRuleComplited()
        {
            string targetCol = "AutoCalc_IS_COMPLETED";
            TreeListFormatRule rule = new TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression conditionRule = new DevExpress.XtraEditors.FormatConditionRuleExpression();
            conditionRule.Expression = "[AutoCalc_PROGRS_RT] = 100"; // 진행률 100인경우
            conditionRule.Appearance.BackColor = Color.Green;
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }

        /// <summary>
        /// 조건부 서식 생성. 완료시간 초과한 항목 표시
        /// </summary>
        private void SetForamttingRuleFinshDayExceeded()
        {
            string targetCol = "FINSH_DT";
            TreeListFormatRule rule = new TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression conditionRule = new DevExpress.XtraEditors.FormatConditionRuleExpression();

            // [FINSH_DT] > '2023-09-10' And [AutoCalc_PROGRS_RT] < 100
            // 작업일 초과 및 진행률 100 미만인경우
            conditionRule.Expression = $"[FINSH_DT] < '{DateTime.Now.ToString("yyyy-MM-dd")}' And [AutoCalc_PROGRS_RT] < 100";
            //conditionRule.Appearance.BackColor = Color.Red;
            conditionRule.Appearance.BackColor = System.Drawing.ColorTranslator.FromHtml("#E5B9B7");

            //conditionRule.Appearance.ForeColor = Color.Yellow;
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }

        /// <summary>
        /// 조건부 서식 생성. 완료시간이 비어있는 항목 표시
        /// </summary>
        private void SetForamttingRuleFinshDayEmpty()
        {
            string targetCol = "FINSH_DT";

            TreeListFormatRule rule = new TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression conditionRule = new DevExpress.XtraEditors.FormatConditionRuleExpression();

            // 작업일이 없는경우
            conditionRule.Expression = $"IsNullOrEmpty([{targetCol}])";
            conditionRule.Appearance.BackColor = Color.Orange;
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }

        /// <summary>
        /// 조건부 서식 생성, week 컬럼에 데이터 있으면 색상 표시
        /// </summary>
        /// <param name="targetCol"></param>
        /// <param name="color"></param>
        private void SetForamttingRuleWeekRule(string targetCol, Color color)
        {
            TreeListFormatRule rule = new TreeListFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleExpression conditionRule = new DevExpress.XtraEditors.FormatConditionRuleExpression();

            // Not IsNullOrEmpty([test-8])
            conditionRule.Expression = $"Not IsNullOrEmpty([{targetCol}])";
            conditionRule.Appearance.BackColor = color;
            rule.Rule = conditionRule;

            AddRule(targetCol, rule);
        }
        #endregion

        /// <summary>
        /// findsh_dt에 에러 표시용
        /// row 정합성 체크해서 error 처리되면 FINSH_DT 에러 아이콘이 생성됨
        /// </summary>
        /// <param name="dataTable"></param>
        private void SetColumnError(DataTable dataTable)
        {
            // row 정합성 체크해서 error 처리되면 FINSH_DT 에러 아이콘이 생성됨
            foreach (DataRow row in dataTable.Rows)
            {
                //// [FINSH_DT] < '{DateTime.Now.ToString("yyy-MM-dd")}' And [PROGRS_RT] < 100

                //if (row["FINSH_DT"] == DBNull.Value || row["AutoCalc_PROGRS_RT"] == DBNull.Value)
                //{
                //    row.SetColumnError(dataTable.Columns["FINSH_DT"], "값이 없음.");
                //    continue;
                //}

                string finshDt = Convert.ToString(row["FINSH_DT"]);
                double? rt = Convert.ToDouble(row["AutoCalc_PROGRS_RT"]);


                DateTime pasDt;
                if (DateTime.TryParse(finshDt, out pasDt) && pasDt < DateTime.Now && rt < 100)
                {
                    row.SetColumnError(dataTable.Columns["FINSH_DT"], "만료일자 초과");
                    continue;
                }
            }
        }

        /// <summary>
        /// 일정 시작일, 종료일 기준으로 해당 week에 데이터 넣음
        /// </summary>
        /// <param name="dataTable"></param>
        private void SetWeekInDataTable(DataTable dataTable)
        {
            Action<bool, DataRow, string, string, string> setData = (isPrearng, row, stDateField, edDateField, inputText) =>
            {
                DateTime? stDate = GetDatetime(row[stDateField]);
                DateTime? edDate = GetDatetime(row[edDateField]);

                var info = CreateYearInfos(stDate, edDate);
                if (info == null) return;
                foreach (var yearItem in info)
                {
                    foreach (WeekInfo weekItem in yearItem.Weeks)
                    {
                        row[$"Week_{(isPrearng ? "Prearng" : "Real")}_{yearItem.Year}_{weekItem.WeekNumber}"] = inputText;
                    }
                }
            };

            foreach (DataRow row in dataTable.Rows)
            {
                setData(true, row, "ST_PREARNG_DT", "FINSH_PREARNG_DT", "*");
                setData(false, row, "ST_DT", "FINSH_DT", "V");
            }
        }

        /// <summary>
        /// level 체크용. level 구분하기 어렵다고 하여 고정 band에 level check 용 컬럼 추가함.
        /// 추가한 컬럼에 level check 표시함. 데이터의 마지막 level에만 체크됨
        /// </summary>
        /// <param name="dataTable"></param>
        private void SetLevelCheckInDataTable(DataTable dataTable)
        {
            Action<DataRow, string> setData = (row, inputText) =>
            {
                int level = Convert.ToInt32(row["AutoCalc_TASK_LEVEL"]);

                row[$"AutoCalc_LEVEL_CHECK_{level}"] = inputText;
            };

            // AutoCalc_TASK_LEVEL
            foreach (DataRow row in dataTable.Rows)
            {
                setData(row, "V");
            }
        }

        #region week 컬럼 추가
        /// <summary>
        /// data row에서 지정된 컬럼 날짜로 변환. 날짜 형 아니면 null 전달
        /// </summary>
        Func<object, DateTime?> GetDatetime = (data) =>
        {
            if (data == DBNull.Value) return null;

            DateTime temp;
            if (DateTime.TryParse(data.ToString(), out temp))
            {
                return temp;
            }
            return null;
        };

        /// <summary>
        /// week 컬럼 생성
        /// </summary>
        /// <param name="dataTable"></param>
        private void AppenWeekColumns(DataTable dataTable)
        {
            // 1. 전체 데이터에서 min,max 날짜 정보 조회
            // 2. min, max 날짜 기준으로 year, 월, week 정보 생성
            // 3. 생성된 정보로 week 컬럼 추가

            // 1.
            //////DateTime? minDate = null;
            //////DateTime? maxDate = null;

            //////// 작업 시작일, 종료일 min, max 가져오기
            //////var MIN_ST_PREARNG_DT = dataTable.AsEnumerable().Select(x => GetDatetime(x.Field<object>("ST_PREARNG_DT"))).Min();
            //////var MIN_ST_DT = dataTable.AsEnumerable().Select(x => GetDatetime(x.Field<object>("ST_DT"))).Min();
            //////if (MIN_ST_PREARNG_DT == null && MIN_ST_DT == null) return;

            //////var MAX_FINSH_PREARNG_DT = dataTable.AsEnumerable().Select(x => GetDatetime(x.Field<object>("FINSH_PREARNG_DT"))).Max();
            //////var MAX_FINSH_DT = dataTable.AsEnumerable().Select(x => GetDatetime(x.Field<object>("FINSH_DT"))).Max();
            //////if (MAX_FINSH_PREARNG_DT == null && MAX_FINSH_DT == null) return;

            //////// minDate 에 값 입력
            //////if (MIN_ST_PREARNG_DT != null) minDate = MIN_ST_PREARNG_DT.Value;
            //////if (MIN_ST_DT != null)
            //////{
            //////    if (minDate != null && minDate.Value > MIN_ST_DT.Value) minDate = MIN_ST_DT.Value;
            //////    if (minDate == null) minDate = MIN_ST_DT.Value;
            //////}

            //////// maxDate에 값 입력
            //////if (MAX_FINSH_PREARNG_DT != null) maxDate = MAX_FINSH_PREARNG_DT.Value;
            //////if (MAX_FINSH_DT != null)
            //////{
            //////    if (maxDate != null && maxDate.Value < MAX_FINSH_DT.Value) maxDate = MAX_FINSH_DT.Value;
            //////    if (maxDate == null) maxDate = MAX_FINSH_DT.Value;
            //////}

            //////if (minDate > maxDate) return;

            // 2.
            // CalendarInfo 데이터 작업
            List<YearInfo> infos = CreateYearInfos(minDate, maxDate);


            // WeekNumberBaseOnFirstWeek 값 설정
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



            // 3.
            /* 아래와 같이 band 및 컬럼 구성
             * ----------------------------
             * | 기간                     |
             * ----------------------------
             * |월      |3    |4      |...|
             * ----------------------------
             * |사전계획|1|2|3|4|5|6|7|...|
             * ----------------------------
             * |수행    |1|2|3|4|5|6|7|...|
             * ----------------------------
             */
            TreeListBand weekRoot = new TreeListBand();
            weekRoot.Name = "AutoCalc_WeekRoot";
            weekRoot.Caption = "기간";
            CommonBandOption(weekRoot);

            treelistMain.Bands.Add(weekRoot);

            //AddWeekColumnEmpty(dataTable);
            TreeListBand gubunBand = new TreeListBand();
            gubunBand.Caption = "구분";
            //weekRoot.Children.Add(gubunBand);
            weekRoot.Bands.Add(gubunBand);
            CommonBandOption(gubunBand);

            TreeListBand monthBandInGubun = new TreeListBand();
            monthBandInGubun.Caption = "월";
            monthBandInGubun.AppearanceHeader.BackColor = Color.LightGreen;
            CommonBandOption(monthBandInGubun);
            //gubunBand.Children.Add(monthBandInGubun);
            gubunBand.Bands.Add(monthBandInGubun);

            TreeListColumn gubunCol1 = AddWeekColumnEmpty("사전계획", true);
            gubunCol1.Name = "Week_Prearng";
            treelistMain.Columns.Add(gubunCol1);
            monthBandInGubun.Columns.Add(gubunCol1);

            TreeListColumn gubunCol2 = AddWeekColumnEmpty("수행", false);
            gubunCol2.Name = "Week_Real";
            treelistMain.Columns.Add(gubunCol2);
            monthBandInGubun.Columns.Add(gubunCol2);


            foreach (YearInfo item in infos)
            {
                TreeListBand year = new TreeListBand();
                year.Name = $"AutoCalc_WeekYear_{item.Year}";
                year.Caption = item.Year.ToString();
                CommonBandOption(year);


                //weekRoot.Children.Add(year);
                weekRoot.Bands.Add(year);

                List<TreeListBand> monthInYearBands = new List<TreeListBand>();
                foreach (WeekInfo week in item.Weeks)
                {
                    string monthName = $"AutoCalc_WeekMonth_{week.Year}_{week.Month}";
                    TreeListBand targetMonth = monthInYearBands.Find(x => x.Name == monthName);
                    if (targetMonth == null)
                    {
                        targetMonth = new TreeListBand();
                        monthInYearBands.Add(targetMonth);

                        targetMonth.Name = monthName;
                        targetMonth.Caption = week.Month.ToString();
                        targetMonth.AppearanceHeader.BackColor = Color.LightGreen;
                        CommonBandOption(targetMonth);

                        //year.Children.Add(targetMonth);
                        year.Bands.Add(targetMonth);
                    }

                    TreeListColumn gridCol = AddWeekColumn(week, true);
                    treelistMain.Columns.Add(gridCol);
                    targetMonth.Columns.Add(gridCol);
                    dataTable.Columns.Add(gridCol.FieldName, typeof(string));
                    SetForamttingRuleWeekRule(gridCol.FieldName, Color.Yellow);

                    TreeListColumn gridCol2 = AddWeekColumn(week, false);
                    treelistMain.Columns.Add(gridCol2);
                    targetMonth.Columns.Add(gridCol2);
                    dataTable.Columns.Add(gridCol2.FieldName, typeof(string));
                    SetForamttingRuleWeekRule(gridCol2.FieldName, Color.SkyBlue);
                }
            }
        }

        /// <summary>
        /// 입력된 min max 기준으로 year, week 정보 생성
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
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
                // min max 정보가 같는 년도일경우
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
                        // min 년도와 yearData 년도가 같을때
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
                        // max 년도와 yearData 가 같은경우
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

        /// <summary>
        /// 입력된 날짜 기준으로 해당년도 week number 조회
        /// </summary>
        /// <param name="targetDatetime"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 입력된 년도, week number 기준으로 weekInfo 생성
        /// WeekNumberBaseOnFirstWeek 정보는 나중에 생성함
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekNumber"></param>
        /// <returns></returns>
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

        /// <summary>
        /// week info에 WeekNumberBaseOnFirstWeek 정보 입력
        /// </summary>
        /// <param name="weekDatas"></param>
        /// <param name="startIndex"></param>
        /// <param name="isFirstYear"></param>
        /// <returns></returns>
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
        #endregion

        #region Grid 정보 초기화
        /// <summary>
        /// fix, week 밴드를 제외한 데이터 컬럼용 밴드 구성
        /// </summary>
        /// <param name="headerInfo"></param>
        private void SetInitGrid(DataTable headerInfo)
        {

            IEnumerable<DataRow> rootCol = headerInfo.Rows.Cast<DataRow>()
             .Where(row => Convert.ToString(row["ParentFieldName"]) == "" && Convert.ToString(row["IsBand"]) == "Y");

            // root band 생성
            var rootBand = MakeRootCol(rootCol);
            treelistMain.Bands.AddRange(rootBand.ToArray());

            // band에 데이터 컬럼 추가
            foreach (var root in rootBand)
            {
                MakeSubCol(root, headerInfo);
            }
        }

        /// <summary>
        /// 데이터용 root band 구성
        /// </summary>
        /// <param name="rootCol"></param>
        /// <returns></returns>
        private List<TreeListBand> MakeRootCol(IEnumerable<DataRow> rootCol)
        {
            ////// 1. band로 지정된 항목 root 생성
            ////// 2. band로 지정되지 않은 항목을 넣을 내용 band 생성

            ////List<TreeListBand> retValue = new List<TreeListBand>();
            ////foreach (var row in rootCol)
            ////{
            ////    TreeListBand band = new TreeListBand();
            ////    band.Name = row["docComnNm"].ToString();
            ////    band.Caption = row["docComnTitleNm"].ToString();
            ////    CommonBandOption(band);

            ////    retValue.Add(band);
            ////}

            ////// root 포함되지 않는 나머지 항목을 data root에 넣기
            ////TreeListBand dataRoot = new TreeListBand();
            ////dataRoot.Name = "AutoCalc_DATA_ROOT";
            ////dataRoot.Caption = "내용";
            ////CommonBandOption(dataRoot);
            ////retValue.Add(dataRoot);

            ////return retValue;
            ///
            List<TreeListBand> retValue = new List<TreeListBand>();
            foreach (var row in rootCol)
            {
                TreeListBand band = new TreeListBand();
                band.Name = row["FieldName"].ToString();
                band.Caption = row["Caption"].ToString();

                retValue.Add(band);
            }

            // root 포함되지 않는 나머지 항목을 data root에 넣기
            TreeListBand dataRoot = new TreeListBand();
            dataRoot.Name = "AutoCalc_DATA_ROOT";
            dataRoot.Caption = "내용";
            retValue.Add(dataRoot);

            return retValue;
        }

        /// <summary>
        /// band에 컬럼 추가. 하위 밴드가 있는경우 상위 band에 추가함
        /// </summary>
        /// <param name="parentBand"></param>
        /// <param name="headerInfo"></param>
        private void MakeSubCol(TreeListBand parentBand, DataTable headerInfo)
        {
            //////// header 정보에 상위 컬럼 명이 없고 band가 아닌경우 내용 band에 컬럼 추가
            //////// 상위 band명이 있으면 해당 band에 컬럼 추가

            //////IEnumerable<DataRow> childRows = null;
            //////if (parentBand.Name == "AutoCalc_DATA_ROOT")
            //////{

            //////    childRows = headerInfo.Rows.Cast<DataRow>()
            //////     .Where(row => Convert.ToString(row["uprDocComnNm"]) == "" && row["addHdrYn"].ToString() == "N")
            //////     .OrderBy(row => Convert.ToInt32(row["sortSeq"]));
            //////}
            //////else
            //////{
            //////    childRows = headerInfo.Rows.Cast<DataRow>()
            //////     .Where(row => Convert.ToString(row["uprDocComnNm"]) == parentBand.Name)
            //////     .OrderBy(row => Convert.ToInt32(row["sortSeq"]));
            //////}

            //////foreach (var row in childRows)
            //////{
            //////    if (row["addHdrYn"].ToString() == "Y")
            //////    {
            //////        // 자식이 데이터 band 이면 상위 band에 추가.
            //////        TreeListBand band = new TreeListBand();
            //////        band.Name = row["docComnNm"].ToString();
            //////        band.Caption = row["docComnTitleNm"].ToString();

            //////        //parentBand.Children.Add(band);
            //////        parentBand.Bands.Add(band);

            //////        // 자식 band나 데이터 컬럼이 있는지 확인후 작업 진행
            //////        MakeSubCol(band, headerInfo);
            //////    }
            //////    else
            //////    {
            //////        // 데이터 컬럼이면. grid column 생성후 상위 band에 추가
            //////        TreeListColumn col = new TreeListColumn();
            //////        col.Visible = true;
            //////        col.Caption = row["docComnTitleNm"].ToString();
            //////        //col.RowIndex = ((i - 1) % 3);
            //////        col.FieldName = row["docComnNm"].ToString();
            //////        col.Width = Convert.ToInt32(row["comnLthVal"]);
            //////        col.Tag = new TreeListColAppendData() { InitBaseBand = parentBand };

            //////        CommonColumnOption(col);

            //////        // gridview에도 추가.
            //////        treelistMain.Columns.Add(col);

            //////        parentBand.Columns.Add(col);

            //////    }
            //////}


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
                    TreeListBand band = new TreeListBand();
                    band.Name = row["FieldName"].ToString();
                    band.Caption = row["Caption"].ToString();

                    parentBand.Bands.Add(band);

                    // 자식 band나 데이터 컬럼이 있는지 확인후 작업 진행
                    MakeSubCol(band, headerInfo);
                }
                else
                {
                    // 데이터 컬럼이면. grid column 생성후 상위 band에 추가
                    TreeListColumn col = new TreeListColumn();
                    col.Visible = true;
                    col.Caption = row["Caption"].ToString();
                    //col.RowIndex = ((i - 1) % 3);
                    col.FieldName = row["FieldName"].ToString();
                    
                    col.Tag = new TreeListColAppendData() { InitBaseBand = parentBand };

                    treelistMain.Columns.Add(col);

                    parentBand.Columns.Add(col);

                }
            }

        }
        #endregion

        private void TreeListTest_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = $"{treelistMain.LeftCoord}, {treelistMain.TopVisibleNodeIndex}";
            treelistMain.LeftCoord += 10;
            treelistMain.TopVisibleNodeIndex += 10;
            //treelistMain.
        }

        private void treelistMain_TopVisibleNodeIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = $"{treelistMain.LeftCoord}, {treelistMain.TopVisibleNodeIndex}";
        }
    }

    public class TreeItem
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string JobTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string StateProvinceName { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public int PROGRS_RT { get; set; }

        public string ST_PREARNG_DT { get; set; }
        public string FINSH_PREARNG_DT { get; set; }
        public string ST_DT { get; set; }
        public string FINSH_DT { get; set; }
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
        /// 해당 년도 week number
        /// </summary>
        public int WeekNumber { get; set; }
        /// <summary>
        /// 전체 일정기준으로 처음 시작 week를 1로 기준으로 설정
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

    public class TreeListColAppendData
    {
        public DevExpress.XtraTreeList.Columns.TreeListBand InitBaseBand { get; set; }
    }
}
