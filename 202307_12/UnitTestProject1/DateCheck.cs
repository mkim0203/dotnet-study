using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class DateCheck
    {
        [TestMethod]
        public void 주차_체크()
        {
            DateTime now = DateTime.Now;

            int? result = GetWeekOfYear(now);
            //DayOfWeek result2 = GetWeekOfYear(now);

            Console.WriteLine($"{now.DayOfWeek},");

            Console.WriteLine(result);


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

        [TestMethod]
        public void 주차_기준_장표뽑기()
        {
            DateTime? minDate = new DateTime(2022, 03, 21);
            DateTime? maxDate = new DateTime(2026, 11, 1);
            //DateTime? maxDate = new DateTime(2022, 11, 5);


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
                    Console.WriteLine("날짜 정보가 잘못됨");
                    return;
                }

                YearInfo target = infos.First(x => x.Year == minDate.Value.Year);
                if (target != null)
                {
                    target.Weeks.AddRange(Enumerable.Range(minWeekOfYear.Value, maxWeekOfYear.Value - minWeekOfYear.Value + 1));
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
                            Console.WriteLine("날짜 정보가 잘못됨");
                            return;
                        }
                        int? end = GetLastWeekOfYear(yearData.Year);

                        yearData.Weeks.AddRange(Enumerable.Range(weekOfYear.Value, end.Value - weekOfYear.Value + 1));
                    }
                    else if (yearData.Year == maxDate.Value.Year)
                    {
                        int? weekOfYear = GetWeekOfYear(maxDate);
                        if (weekOfYear == null)
                        {
                            Console.WriteLine("날짜 정보가 잘못됨");
                            return;
                        }
                        yearData.Weeks.AddRange(Enumerable.Range(1, weekOfYear.Value));
                    }
                    else
                    {
                        int? end = GetLastWeekOfYear(yearData.Year);
                        yearData.Weeks.AddRange(Enumerable.Range(1, end.Value));
                    }
                }
            }

            var temp = infos.Select(x => x.Weeks);

            foreach(var item in infos)
            {
                Console.WriteLine(item.ToString());
            }
            
        }



        [TestMethod]
        public void 주차_1주일날짜_확인()
        {
            int year = 2024; // 원하는 연도
            int weekNumber = 20; // 조회하려는 주차

            DateTime jan1st = new DateTime(year, 1, 1);
            DateTime firstDayOfDesiredWeek = jan1st.AddDays((weekNumber - 1) * 7);

            int? result = GetWeekOfYear(firstDayOfDesiredWeek);
            if(result != null && weekNumber == result.Value)
            {
                DayOfWeek dayOfWeek = firstDayOfDesiredWeek.DayOfWeek;
                DateTime weekSunday = firstDayOfDesiredWeek.AddDays(((int)dayOfWeek) * -1);
                DateTime weekSaturday = weekSunday.AddDays(((int)DayOfWeek.Saturday));
            }

            DateTime lastDayOfDesiredWeek = firstDayOfDesiredWeek.AddDays(6);

        }

        [TestMethod]
        public void 주차_1년_시작종료_조회()
        {
            int year = 2023; // 원하는 연도
            

            DateTime jan1st = new DateTime(year, 1, 1);
            DateTime yearLastDate = jan1st.AddYears(1).AddDays(-1);
            int? lastWeekOfYear = GetWeekOfYear(yearLastDate);

            if(lastWeekOfYear != null)
            {
                List<WeekInfo> weekinfos = new List<WeekInfo>();
                for (int weekNumber = 1; weekNumber <= lastWeekOfYear; weekNumber++)
                {
                    WeekInfo additem = new WeekInfo();
                    weekinfos.Add(GetWeekInfo(year, weekNumber));
                }

                SetWeekNumberBaseOnFirstWeek(weekinfos, 0);

                weekinfos.ForEach(x => Console.WriteLine(x.ToString()));
            }
            
        }

        private WeekInfo GetWeekInfo( int year, int weekNumber)
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

        private int SetWeekNumberBaseOnFirstWeek(List<WeekInfo> weekDatas, int startIndex)
        {
            foreach(WeekInfo item in weekDatas.OrderBy(x => x.Year).ThenBy(x => x.WeekNumber))
            {
                if (item.WeekStartDate.DayOfWeek == DayOfWeek.Sunday)
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

    }

    public class WeekInfo
    {
        public int Year
        {
            get { return WeekStartDate.Year; }
        }
        public int Month
        {
            get { return WeekStartDate.Month; }
        }
        public int WeekNumber { get; set; }
        public int WeekNumberBaseOnFirstWeek { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }

        public override string ToString()
        {
            return $"[{WeekNumber}] [{WeekNumberBaseOnFirstWeek}] {WeekStartDate.ToString("yyyy-MM-dd")} : {WeekStartDate.DayOfWeek}, {WeekEndDate.ToString("yyyy-MM-dd")} : {WeekEndDate.DayOfWeek} ";
        }
    }



    public class YearInfo
    {
        public YearInfo()
        {
            Weeks = new List<int>();
        }
        public int Year { get; set; }
        public List<int> Weeks { get; set; }

        public override string ToString()
        {
            return $"Year : {Year}{Environment.NewLine}Weeks : {string.Join(",", Weeks)}";
        }
    }

    public class WeekInfo2
    {
        /// <summary>
        /// week 의 해당 년도
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// week 의 해당 달
        /// </summary>
        public int Month { get; set; }
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
}
