using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("LEVEL1_CD", typeof(string));
            dataTable.Columns.Add("LEVEL2_CD", typeof(string));
            dataTable.Columns.Add("LEVEL3_CD", typeof(string));
            dataTable.Columns.Add("LEVEL4_CD", typeof(string));
            dataTable.Columns.Add("FCPR_RT", typeof(int));
            dataTable.Columns.Add("AutoCalc_FCPR_RT", typeof(double));

            var addRow1 = dataTable.NewRow();
            addRow1["LEVEL1_CD"] = "st1";
            addRow1["LEVEL2_CD"] = "st1-1";
            addRow1["FCPR_RT"] = 20;

            dataTable.Rows.Add(addRow1);

            var addRow2 = dataTable.NewRow();
            addRow2["LEVEL1_CD"] = "st1";
            dataTable.Rows.Add(addRow2);

            var addRow3 = dataTable.NewRow();
            addRow3["LEVEL1_CD"] = "st1";
            addRow3["LEVEL2_CD"] = "st1-2";
            //addRow3["FCPR_RT"] = 40;
            dataTable.Rows.Add(addRow3);

            var addRow4 = dataTable.NewRow();
            addRow4["LEVEL1_CD"] = "st1";
            addRow4["LEVEL2_CD"] = "st1-2";
            addRow4["LEVEL3_CD"] = "st1-2-1";
            addRow4["FCPR_RT"] = 50;
            dataTable.Rows.Add(addRow4);

            var addRow5 = dataTable.NewRow();
            addRow5["LEVEL1_CD"] = "st1";
            addRow5["LEVEL2_CD"] = "st1-2";
            addRow5["LEVEL3_CD"] = "st1-2-2";
            addRow5["FCPR_RT"] = 80;
            dataTable.Rows.Add(addRow5);

            // "level"로 시작하는 컬럼명을 조회하고 정렬
            IOrderedEnumerable<DataColumn> levelColumns = dataTable.Columns.Cast<DataColumn>()
                .Where(column => column.ColumnName.StartsWith("LEVEL", StringComparison.OrdinalIgnoreCase))
                .OrderBy(column => column.ColumnName);

            // "AutoCalc_PATH" 컬럼 추가
            dataTable.Columns.Add("AutoCalc_PATH", typeof(string));


            // 각 행에 "AutoCalc_PATH" 컬럼 값 설정
            foreach (DataRow row in dataTable.Rows)
            {
                string pathValue = string.Join("/", levelColumns.Select(column => row[column].ToString()).Where(x => string.IsNullOrEmpty(x) == false));
                if (pathValue.EndsWith("/") == false) pathValue += "/";

                row["AutoCalc_PATH"] = pathValue;
            }


            // 결과 출력
            Console.WriteLine("Level 컬럼들을 오름차순으로 정렬:");
            foreach (var column in levelColumns)
            {
                Console.WriteLine(column.ColumnName);
            }


            Console.WriteLine("\n새로운 'path' 컬럼:");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["AutoCalc_PATH"]);
            }


            // 결과 출력
            Console.WriteLine("DataTable 내용:");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"LEVEL1_CD: {row["LEVEL1_CD"]}, LEVEL2_CD: {row["LEVEL2_CD"]}, FCPR_RT: {row["FCPR_RT"]}, AutoCalc_FCPR_RT: {row["AutoCalc_FCPR_RT"]}");
            }

            SetAutoCalc_UPR_TASK_CD(dataTable, levelColumns);


            // "AutoCalc_FCPR_RT" 컬럼 값 설정
            foreach (DataRow row in dataTable.Rows.Cast<DataRow>().OrderByDescending(r => Convert.ToInt32(r["AutoCalc_TASK_LEVEL"])))
            {
                string path = row["AutoCalc_PATH"].ToString();


                var rowsWithSamePath = dataTable.Rows.Cast<DataRow>()
                    .Where(r => r["AutoCalc_PATH"].ToString().StartsWith(path) && Convert.ToInt32(r["AutoCalc_TASK_LEVEL"]) == Convert.ToInt32(row["AutoCalc_TASK_LEVEL"]) + 1 && r != row);

                if(rowsWithSamePath.Count() == 0)
                {
                    row["AutoCalc_FCPR_RT"] = row["FCPR_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FCPR_RT"]);
                } else
                {
                    double avgFCPR_RT = rowsWithSamePath.Average(r => r["AutoCalc_FCPR_RT"] == DBNull.Value ? 0 : Convert.ToDouble(r["AutoCalc_FCPR_RT"]));
                    row["AutoCalc_FCPR_RT"] = avgFCPR_RT;
                }


                //int FCPR_RT = row["FCPR_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FCPR_RT"]);

                //Console.WriteLine($"{row["AutoCalc_PATH"]} => ");
                //foreach (DataRow targerRow in rowsWithSamePath)
                //{
                //    Console.WriteLine($"LEVEL1_CD: {targerRow["LEVEL1_CD"]}, LEVEL2_CD: {targerRow["LEVEL2_CD"]}, FCPR_RT: {targerRow["FCPR_RT"]}");
                //}

                //if (rowsWithSamePath.Count() > 0)
                //{
                //    double avgFCPR_RT = rowsWithSamePath.Average(r => r["FCPR_RT"] == DBNull.Value ? 0 : Convert.ToDouble(r["FCPR_RT"]));
                //    row["AutoCalc_FCPR_RT"] = avgFCPR_RT;
                //}
                //else
                //{
                //    row["AutoCalc_FCPR_RT"] = FCPR_RT;
                //}
            }

            Console.WriteLine("완료");

        }

        private void SetAutoCalc_UPR_TASK_CD(DataTable dataTable, IOrderedEnumerable<DataColumn> levelColumns)
        {

            // "AutoCalc_UPR_TASK_CD" 컬럼 추가
            dataTable.Columns.Add("AutoCalc_UPR_TASK_CD", typeof(string));
            dataTable.Columns.Add("AutoCalc_TASK_LEVEL", typeof(int));

            // "AutoCalc_UPR_TASK_CD" 값을 설정
            string AutoCalc_UPR_TASK_CD = string.Empty;
            // 데이터가 있는 level을 찾은경우
            bool findLevel = false;
            foreach (DataRow row in dataTable.Rows)
            {
                row["AutoCalc_TASK_LEVEL"] = levelColumns.Count(x => string.IsNullOrEmpty(row[x.ColumnName].ToString()) == false);

                findLevel = false;
                foreach (DataColumn col in levelColumns.OrderByDescending(x => x.ColumnName))
                {
                    //if (row[col] == DBNull.Value) break;
                    string checkLevel = row[col].ToString();
                  
                    
                    if (!string.IsNullOrEmpty(checkLevel) )
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

            // 결과 출력
            Console.WriteLine("DataTable 내용:");
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"AutoCalc_UPR_TASK_CD: {row["AutoCalc_UPR_TASK_CD"]}, work level : {row["AutoCalc_TASK_LEVEL"]}");
            }
        }

        [TestMethod]
        public void 부서_FullPath_생성()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ORG_CD", typeof(string));
            dataTable.Columns.Add("ORG_UPR_CD", typeof(string));
            dataTable.Columns.Add("ORG_NM", typeof(string));
            dataTable.Columns.Add("SORT_SEQ", typeof(int));
            dataTable.Columns.Add("ORG_CD_FULL_PATH", typeof(string));

            var root = dataTable.NewRow();
            root["ORG_CD"] = "5000";
            root["ORG_UPR_CD"] = "0100";
            root["ORG_NM"] = "ROOT";
            root["SORT_SEQ"] = 1;
            dataTable.Rows.Add(root);

            var root2 = dataTable.NewRow();
            root2["ORG_CD"] = "5001";
            root2["ORG_UPR_CD"] = "0100";
            root2["ORG_NM"] = "ROOT2";
            root2["SORT_SEQ"] = 1;
            dataTable.Rows.Add(root2);

            var sub1 = dataTable.NewRow();
            sub1["ORG_CD"] = "SUB1";
            sub1["ORG_UPR_CD"] = "5000";
            sub1["ORG_NM"] = "SUB1";
            sub1["SORT_SEQ"] = 2;
            dataTable.Rows.Add(sub1);

            var sub1_1 = dataTable.NewRow();
            sub1_1["ORG_CD"] = "SUB1_1";
            sub1_1["ORG_UPR_CD"] = "SUB1";
            sub1_1["ORG_NM"] = "SUB1_1";
            sub1_1["SORT_SEQ"] = 3;
            dataTable.Rows.Add(sub1_1);

            var roots = dataTable.AsEnumerable().Where(row => row.Field<int>("SORT_SEQ") == 1);

            foreach (var rootRow in roots)
            {
                // 재귀 함수 호출
                UpdateOrgCdFullPath(dataTable, rootRow, $"/{rootRow["ORG_UPR_CD"]}/");
            }

            // 결과 출력
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"ORG_CD: {row["ORG_CD"]}, ORG_CD_FULL_PATH: {row["ORG_CD_FULL_PATH"]}");
            }
        }

        [TestMethod]
        public void Is체크및형변환()
        {

            object temp = new List<string>() { "AA", "bb" };
            if (temp is IList<string> list) {
                Console.WriteLine(string.Join(",", list));
            }
        }

        [TestMethod]
        public void 멀티리턴테스트()
        {
            var workResult = Work(1, "A");
            (_, string result) = Work(1, "A");
            _ = Work(2, "b");
            Console.WriteLine(result);
            
            
        }

        private (int, string) Work(int a, string b)
        {
            return (a, b);
        }

        static void UpdateOrgCdFullPath(DataTable dataTable, DataRow parentRow, string parentPath)
        {
            string orgCd = parentRow["ORG_CD"].ToString();
            string orgUpCd = parentRow["ORG_UPR_CD"].ToString();

            // 현재 행의 ORG_CD_FULL_PATH 업데이트
            parentRow["ORG_CD_FULL_PATH"] = parentPath + orgCd + "/";

            // ORG_UPR_CD와 일치하는 자식 행을 찾아 재귀 호출
            foreach (DataRow childRow in dataTable.Rows)
            {
                if (childRow["ORG_UPR_CD"].ToString() == orgCd)
                {
                    UpdateOrgCdFullPath(dataTable, childRow, parentPath + orgCd + "/");
                }
            }
        }



    }
}
