using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Value", typeof(double));

            DataTable dataTable_2 = new DataTable();
            dataTable_2.Columns.Add("Value", typeof(double));
            dataTable_2.Columns.Add("Name", typeof(string));

            var dr = dataTable.NewRow();
            dr["Value"] = DBNull.Value;
            dataTable.Rows.Add(dr);

            DataTable dt2 = dataTable_2.Clone();
            dt2.ImportRow(dr);


            Console.WriteLine(dt2.Rows[0][0]);
        }

        [TestMethod]
        public void Test2()
        {
            Dictionary<string, Dictionary<string, int?>> datas = new Dictionary<string, Dictionary<string, int?>>();
            var subDic1 = new Dictionary<string, int?>();
            subDic1.Add("B1", 1);
            subDic1.Add("B2", null);
            datas.Add("A", subDic1);



        }

        [TestMethod]
        public void Test3()
        {
            // 예제 데이터 생성
            var datas = new Dictionary<string, Dictionary<string, int?>>
            {
                {
                    "Key1",
                    new Dictionary<string, int?>
                    {
                        { "Value1", 10 },
                        { "Value2", null },
                        { "Value3", 20 }
                    }
                },
                {
                    "Key2",
                    new Dictionary<string, int?>
                    {
                        { "Value4", null },
                        { "Value5", 30 },
                        { "Value6", null }
                    }
                }
            };

            // int? 값이 null인 항목을 가져오기
            var nullItems = datas
                .SelectMany(outer => outer.Value
                    .Where(inner => inner.Value == null)
                    .Select(inner => new
                    {
                        OuterKey = outer.Key,
                        InnerKey = inner.Key
                    }));

            foreach (var item in nullItems)
            {
                Console.WriteLine($"Outer Key: {item.OuterKey}, Inner Key: {item.InnerKey}");
            }

        }

        [TestMethod]
        public void Test4()
        {
            // 예제 데이터 생성
            var datas = new Dictionary<string, Dictionary<string, int?>>
            {
                {
                    "Key1",
                    new Dictionary<string, int?>
                    {
                        { "Value1", 10 },
                        { "Value2", null },
                        { "Value3", 20 }
                    }
                },
                {
                    "Key2",
                    new Dictionary<string, int?>
                    {
                        { "Value4", null },
                        { "Value5", 30 },
                        { "Value6", null }
                    }
                }
            };

            // int? 값이 null인 항목을 삭제하기
            var keysToDelete = datas
                .SelectMany(outer => outer.Value
                    .Where(inner => inner.Value == null)
                    .Select(inner => new { OuterKey = outer.Key, InnerKey = inner.Key }))
                .ToList();

            foreach (var key in keysToDelete)
            {
                datas[key.OuterKey].Remove(key.InnerKey);
            }

            // 결과 출력
            Console.WriteLine("Updated Dictionary:");
            foreach (var outerKvp in datas)
            {
                Console.WriteLine($"Outer Key: {outerKvp.Key}");
                foreach (var innerKvp in outerKvp.Value)
                {
                    Console.WriteLine($"  Inner Key: {innerKvp.Key}, Inner Value: {innerKvp.Value}");
                }
            }
        }


        [TestMethod]
        public void IEnumerableTest()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };

            Console.WriteLine("1");
            IEnumerable<int> greaterThanTwo = numbers.Where(number => {
                Console.WriteLine("TEST");
                return number > 2;
                }); // The query is not executed yet!

            Console.WriteLine("2");
            numbers.Add(6);

            Console.WriteLine("3");
            Console.WriteLine(string.Join(",", greaterThanTwo));
            //List<int> result = greaterThanTwo.ToList(); // Enumerates now.

            //Console.WriteLine(string.Join(",", result)); // 3,4,5,6
        }

        [TestMethod]
        public void IListTest()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };

            Console.WriteLine("1");
            IList<int> greaterThanTwo = numbers.Where(number => {
                Console.WriteLine("TEST");
                return number > 2;
            }).ToList();

            Console.WriteLine("2");
            numbers.Add(6);

            Console.WriteLine("3");
            Console.WriteLine(string.Join(",", greaterThanTwo));
            //List<int> result = greaterThanTwo.ToList(); 

            //Console.WriteLine(string.Join(",", result));
        }
    }
}
