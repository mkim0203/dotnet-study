using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public async Task 스케줄_Data_생성Async()
        {
            // 2천개 이상 스케줄 생성
            int maxItem = 2000;
            List<ScheduleItem> schedules = new List<ScheduleItem>();
            int rootIndex = 1;
            Random rm = new Random();

            DateTime stDate = new DateTime(2023, 1, 1);
            DateTime edDate = new DateTime(2024, 1, 1).AddDays(-1);

            do
            {
                await Task.Delay(10);
                int maxLevel = rm.Next(2, 4);
                string root = $"task-{rootIndex}";
                ScheduleItem addRoot = new ScheduleItem() { Level1Cd = root, TaskCd = root, TaskNm = $"작업 {root}", PerStartDate = stDate, PerEndDate = edDate, StartDate = stDate, EndDate = edDate  };
                schedules.Add(addRoot);

                int level2Cnt = rm.Next(5, 20);
                int lv2Workingday = 365 / level2Cnt;

                for(int lv2 = 1; lv2 <= level2Cnt; lv2++)
                {
                    string level2Cd = $"{root}-{lv2}";

                    ScheduleItem addLv2Item = new ScheduleItem();
                    schedules.Add(addLv2Item);
                    addLv2Item.Level1Cd = root;
                    addLv2Item.Level2Cd = level2Cd;
                    addLv2Item.TaskCd = level2Cd;
                    addLv2Item.TaskNm = $"작업 {level2Cd}";
                    if (lv2Workingday > 0)
                    {
                        addLv2Item.StartDate = addRoot.StartDate.Value.AddDays((lv2 - 1) * lv2Workingday);
                        addLv2Item.EndDate = addRoot.StartDate.Value.AddDays((lv2 - 1) * lv2Workingday + lv2Workingday);
                    }

                    if (maxLevel == 2 || await HavSubAsync() == false)
                    {
                    }
                    else
                    {
                        int level3Cnt = rm.Next(5, 20);
                        int lv3Workingday = lv2Workingday / level3Cnt;

                        for (int lv3 = 1; lv3 <= level3Cnt; lv3++)
                        {
                            string level3Cd = $"{root}-{lv2}-{lv3}";

                            ScheduleItem addLv3Item = new ScheduleItem() { Level1Cd = addLv2Item.Level1Cd, Level2Cd = addLv2Item.Level2Cd };
                            schedules.Add(addLv3Item);
                            addLv3Item.Level3Cd = level3Cd;
                            addLv3Item.TaskCd = level3Cd;
                            addLv3Item.TaskNm = $"작업 {level3Cd}";
                            if (lv3Workingday > 0)
                            {
                                addLv3Item.StartDate = addLv2Item.StartDate.Value.AddDays((lv3 - 1) * lv3Workingday);
                                addLv3Item.EndDate = addLv2Item.StartDate.Value.AddDays((lv3 - 1) * lv3Workingday + lv3Workingday);
                            }

                            if (maxLevel == 3 || await HavSubAsync() == false)
                            {
                                
                            }
                            else
                            {
                                int level4Cnt = rm.Next(5, 20);
                                int lv4Workingday = lv3Workingday / level4Cnt;

                                ScheduleItem addLv4Item = new ScheduleItem() { Level1Cd = addLv3Item.Level1Cd, Level2Cd = addLv3Item.Level2Cd, Level3Cd = addLv3Item.Level3Cd };
                                schedules.Add(addLv4Item);

                                for (int lv4 = 1; lv4 <= level4Cnt; lv4++)
                                {
                                    string level4Cd = $"{root}-{lv2}-{lv3}-{lv4}";
                                    addLv4Item.Level4Cd = level4Cd;
                                    addLv4Item.TaskCd = level4Cd;
                                    addLv4Item.TaskNm = $"작업 {level4Cd}";
                                    if (lv4Workingday > 0)
                                    {
                                        addLv4Item.StartDate = addLv3Item.StartDate.Value.AddDays((lv4 - 1) * lv4Workingday);
                                        addLv4Item.EndDate = addLv3Item.StartDate.Value.AddDays((lv4 - 1) * lv4Workingday + lv4Workingday);
                                    }
                                }
                            }
                        }
                    }
                }


                rootIndex++;
            } while (schedules.Count < maxItem);

            schedules.AsParallel().ForAll(row => {
                row.ItemLevel =
                (string.IsNullOrWhiteSpace(row.Level1Cd) ? 0 : 1)
                + (string.IsNullOrWhiteSpace(row.Level2Cd) ? 0 : 1)
                + (string.IsNullOrWhiteSpace(row.Level3Cd) ? 0 : 1)
                + (string.IsNullOrWhiteSpace(row.Level4Cd) ? 0 : 1);
            });

            foreach (var item in schedules)
            {
                Console.WriteLine($"{item.ItemLevel}\t{item}");
            }
        }

        [TestMethod]
        public async Task DataTable_테스트()
        {
            // 2천개 이상 스케줄 생성
            int maxItem = 2000;
            List<ScheduleItem> schedules = new List<ScheduleItem>();
            int rootIndex = 1;

            DateTime stDate = new DateTime(2023, 1, 1);
            DateTime edDate = new DateTime(2024, 1, 1).AddDays(-1);

            do
            {
                await Task.Delay(10);
                int maxLevel = StaticRandom.Rand(2, 4);
                string root = $"task-{rootIndex}";
                ScheduleItem addRoot = new ScheduleItem() { Level1Cd = root, TaskCd = root, TaskNm = $"작업 {root}", PerStartDate = stDate, PerEndDate = edDate, StartDate = stDate, EndDate = edDate };
                schedules.Add(addRoot);

                int level2Cnt = StaticRandom.Rand(5, 20);
                int lv2Workingday = 365 / level2Cnt;

                Parallel.For(1, level2Cnt, async lv2 =>
                {
                    string level2Cd = $"{root}-{lv2}";

                    ScheduleItem addLv2Item = new ScheduleItem();
                    schedules.Add(addLv2Item);
                    addLv2Item.Level1Cd = root;
                    addLv2Item.Level2Cd = level2Cd;
                    addLv2Item.TaskCd = level2Cd;
                    addLv2Item.TaskNm = $"작업 {level2Cd}";
                    if (lv2Workingday > 0)
                    {
                        addLv2Item.StartDate = addRoot.StartDate.Value.AddDays((lv2 - 1) * lv2Workingday);
                        addLv2Item.EndDate = addRoot.StartDate.Value.AddDays((lv2 - 1) * lv2Workingday + lv2Workingday);
                    }

                    if (maxLevel == 2 || await HavSubAsync() == false)
                    {
                    }
                    else
                    {
                        int level3Cnt = StaticRandom.Rand(5, 20);
                        int lv3Workingday = lv2Workingday / level3Cnt;

                        Parallel.For(1, level3Cnt, async lv3 =>
                       {
                           string level3Cd = $"{root}-{lv2}-{lv3}";

                           ScheduleItem addLv3Item = new ScheduleItem() { Level1Cd = addLv2Item.Level1Cd, Level2Cd = addLv2Item.Level2Cd };
                           schedules.Add(addLv3Item);
                           addLv3Item.Level3Cd = level3Cd;
                           addLv3Item.TaskCd = level3Cd;
                           addLv3Item.TaskNm = $"작업 {level3Cd}";
                           if (lv3Workingday > 0)
                           {
                               addLv3Item.StartDate = addLv2Item.StartDate.Value.AddDays((lv3 - 1) * lv3Workingday);
                               addLv3Item.EndDate = addLv2Item.StartDate.Value.AddDays((lv3 - 1) * lv3Workingday + lv3Workingday);
                           }

                           if (maxLevel == 3 || await HavSubAsync() == false)
                           {

                           }
                           else
                           {
                               int level4Cnt = StaticRandom.Rand(5, 20);
                               int lv4Workingday = lv3Workingday / level4Cnt;

                               ScheduleItem addLv4Item = new ScheduleItem() { Level1Cd = addLv3Item.Level1Cd, Level2Cd = addLv3Item.Level2Cd, Level3Cd = addLv3Item.Level3Cd };
                               schedules.Add(addLv4Item);

                               Parallel.For(1, level4Cnt, lv4 =>
                               {
                                   string level4Cd = $"{root}-{lv2}-{lv3}-{lv4}";
                                   addLv4Item.Level4Cd = level4Cd;
                                   addLv4Item.TaskCd = level4Cd;
                                   addLv4Item.TaskNm = $"작업 {level4Cd}";
                                   if (lv4Workingday > 0)
                                   {
                                       addLv4Item.StartDate = addLv3Item.StartDate.Value.AddDays((lv4 - 1) * lv4Workingday);
                                       addLv4Item.EndDate = addLv3Item.StartDate.Value.AddDays((lv4 - 1) * lv4Workingday + lv4Workingday);
                                   }
                               });
                           }
                       });
                    }
                });


                rootIndex++;
            } while (schedules.Count < maxItem);

            schedules.AsParallel().ForAll(row =>
            {
                if (row != null)
                {
                    row.ItemLevel =
                    (string.IsNullOrWhiteSpace(row.Level1Cd) ? 0 : 1)
                    + (string.IsNullOrWhiteSpace(row.Level2Cd) ? 0 : 1)
                    + (string.IsNullOrWhiteSpace(row.Level3Cd) ? 0 : 1)
                    + (string.IsNullOrWhiteSpace(row.Level4Cd) ? 0 : 1);
                }
            });

            foreach (var item in schedules)
            {
                if(item is null) Console.WriteLine("[null]");
                else 
                Console.WriteLine($"{item.ItemLevel}\t{item}");
            }

            

            
        }

        private async Task<bool> HavSubAsync()
        {
            Random rm = new Random();
            await Task.Delay(10);
            return (rm.Next(10) % 2 == 0);
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

        [TestMethod]
        public void GuidTest()
        {
            Parallel.For(0, 100, (index) => {
                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.ffff")}] {index,3} => {Guid.NewGuid()} ");
            });
        }


        [TestMethod]
        public async Task 스케줄_Data_생성2Async()
        {
            // 2천개 이상 스케줄 생성
            int maxItem = 2000;
            List<ScheduleItem> schedules = new List<ScheduleItem>();
            int rootIndex = 1;

            DateTime stDate = new DateTime(2023, 1, 1);
            DateTime edDate = new DateTime(2024, 1, 1).AddDays(-1);

            do
            {
                await Task.Delay(10);
                int maxLevel = StaticRandom.Rand(2, 5);
                string root = $"task-{rootIndex}";
                ScheduleItem addRoot = new ScheduleItem() { Level1Cd = root, TaskCd = root, TaskNm = $"작업 {root}", PerStartDate = stDate, PerEndDate = edDate, StartDate = stDate, EndDate = edDate };
                schedules.Add(addRoot);

                int level2Cnt = StaticRandom.Rand(5, 20);
                int lv2Workingday = 365 / level2Cnt;

                for (int lv2 = 1; lv2 <= level2Cnt; lv2++)
                {
                    string level2Cd = $"{root}-{lv2}";

                    ScheduleItem addLv2Item = new ScheduleItem();
                    schedules.Add(addLv2Item);
                    addLv2Item.Level1Cd = root;
                    addLv2Item.Level2Cd = level2Cd;
                    addLv2Item.TaskCd = level2Cd;
                    addLv2Item.TaskNm = $"작업 {level2Cd}";
                    if (lv2Workingday > 0)
                    {
                        addLv2Item.StartDate = addRoot.StartDate.Value.AddDays((lv2 - 1) * lv2Workingday);
                        addLv2Item.EndDate = addRoot.StartDate.Value.AddDays((lv2 - 1) * lv2Workingday + lv2Workingday);
                    }

                    if (maxLevel == 2 || await HavSubAsync() == false)
                    {
                    }
                    else
                    {
                        int level3Cnt = StaticRandom.Rand(5, 20);
                        int lv3Workingday = lv2Workingday / level3Cnt;

                        for (int lv3 = 1; lv3 <= level3Cnt; lv3++)
                        {
                            string level3Cd = $"{root}-{lv2}-{lv3}";

                            ScheduleItem addLv3Item = new ScheduleItem() { Level1Cd = addLv2Item.Level1Cd, Level2Cd = addLv2Item.Level2Cd };
                            schedules.Add(addLv3Item);
                            addLv3Item.Level3Cd = level3Cd;
                            addLv3Item.TaskCd = level3Cd;
                            addLv3Item.TaskNm = $"작업 {level3Cd}";
                            if (lv3Workingday > 0)
                            {
                                addLv3Item.StartDate = addLv2Item.StartDate.Value.AddDays((lv3 - 1) * lv3Workingday);
                                addLv3Item.EndDate = addLv2Item.StartDate.Value.AddDays((lv3 - 1) * lv3Workingday + lv3Workingday);
                            }

                            if (maxLevel == 3 || await HavSubAsync() == false)
                            {

                            }
                            else
                            {
                                int level4Cnt = StaticRandom.Rand(5, 20);
                                int lv4Workingday = lv3Workingday / level4Cnt;

                                ScheduleItem addLv4Item = new ScheduleItem() { Level1Cd = addLv3Item.Level1Cd, Level2Cd = addLv3Item.Level2Cd, Level3Cd = addLv3Item.Level3Cd };
                                schedules.Add(addLv4Item);

                                for (int lv4 = 1; lv4 <= level4Cnt; lv4++)
                                {
                                    string level4Cd = $"{root}-{lv2}-{lv3}-{lv4}";
                                    addLv4Item.Level4Cd = level4Cd;
                                    addLv4Item.TaskCd = level4Cd;
                                    addLv4Item.TaskNm = $"작업 {level4Cd}";
                                    if (lv4Workingday > 0)
                                    {
                                        addLv4Item.StartDate = addLv3Item.StartDate.Value.AddDays((lv4 - 1) * lv4Workingday);
                                        addLv4Item.EndDate = addLv3Item.StartDate.Value.AddDays((lv4 - 1) * lv4Workingday + lv4Workingday);
                                    }
                                }
                            }
                        }
                    }
                }


                rootIndex++;
            } while (schedules.Count < maxItem);

            schedules.AsParallel().ForAll(row => {
                row.ItemLevel =
                (string.IsNullOrWhiteSpace(row.Level1Cd) ? 0 : 1)
                + (string.IsNullOrWhiteSpace(row.Level2Cd) ? 0 : 1)
                + (string.IsNullOrWhiteSpace(row.Level3Cd) ? 0 : 1)
                + (string.IsNullOrWhiteSpace(row.Level4Cd) ? 0 : 1);
            });

            foreach (var item in schedules)
            {
                Console.WriteLine($"{item.ItemLevel}\t{item}");
            }
        }

    }

    public class ScheduleItem
    {
        public string Level1Cd { get; set; }
        public string Level2Cd { get; set; }
        public string Level3Cd { get; set; }
        public string Level4Cd { get; set; }

        public string TaskCd { get; set; }
        public string TaskNm { get; set; }
        public DateTime? PerStartDate { get; set; }
        public DateTime? PerEndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TaskProgress
        {
            get
            {
                if (HaveValueAsync())
                {
                    //Random rm = new Random();
                    //return rm.Next(0, 100);
                    return StaticRandom.Rand(0, 100);
                }
                else { return null; }
            }
        }

        public int ItemLevel { get; set; }

        private bool HaveValueAsync()
        {
            //Random rm = new Random();
            //System.Threading.Thread.Sleep(10);
            //return (rm.Next(10) % 2 == 0);

            return (StaticRandom.Rand(1, 10) % 2 == 0);
        }

        public override string ToString()
        {
            return $"{Level1Cd}\t{Level2Cd}\t{Level3Cd}\t{Level4Cd}\t{TaskCd}\t{TaskNm}\t{(PerStartDate.HasValue ? PerStartDate.Value.ToString("yyyy-MM-dd") : string.Empty)}\t{(PerEndDate.HasValue ? PerEndDate.Value.ToString("yyyy-MM-dd") : string.Empty)}\t{(StartDate.HasValue ? StartDate.Value.ToString("yyyy-MM-dd") : string.Empty)}\t{(EndDate.HasValue ? EndDate.Value.ToString("yyyy-MM-dd") : string.Empty)}\t{TaskProgress}";
        }
    }
}
