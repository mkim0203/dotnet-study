using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class StringCheck
    {
        [TestMethod]
        public void LEVEL_CD_정규식체크()
        {
            string[] Levels = new string[] { "LEVEL_ABC_CD", "LEVEL1_CD", "LEVEL_CD", "LEVEL2_CD", "LEVEL10_CD", "level9_cd" };
            //string inputString = "LEVEL_ABC_CD"; // 여기에 검사하려는 문자열을 넣으세요.

            // 정규식 패턴
            string pattern = @"^LEVEL[1-9]_CD$";



            foreach (string inputString in Levels)
            {
                // 정규식 체크
                bool isMatch = Regex.IsMatch(inputString, pattern);
                if (isMatch)
                {
                    Console.WriteLine($"{inputString} 문자열이 패턴과 일치합니다.");
                }
                else
                {
                    Console.WriteLine($"{inputString} 문자열이 패턴과 일치하지 않습니다.");
                }
            }

            IEnumerable<string> datas = Levels.Where(inputString => Regex.IsMatch(inputString, pattern, RegexOptions.IgnoreCase));
            datas.ToList().ForEach(x => Console.WriteLine(x));

        }

        [TestMethod]
        public void StringFormatTest()
        {
            string format = @"{0}";
            Console.WriteLine(string.Format(format, "a"));


            var item = new FormatItem() { StartDate = DateTime.Now, Writer = "테스트a", MeetingUsers = "참석자b", Title = "title" };

            string format2 = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
	<head>
		<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><title>
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
			.cs85368FFC{{width:100%;padding:0pt 5.4pt 0pt 5.4pt;border-top:1pt windowtext solid;border-right:none;border-bottom:1.5pt windowtext solid;border-left:none}}
			.cs80D9435B{{text-align:justify;text-indent:0pt;margin:0pt 0pt 0pt 0pt}}
			.csE381FCF8{{color:#000000;background-color:#D8D8D8;font-family:나눔고딕;font-size:10pt;font-weight:normal;font-style:normal;}}
		</style>
	</head>
	<body>
		<p class=""cs5F80D9B9""><span class=""csDF4544F5"">회의록</span></p><table class=""cs2D2816FE"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""border-collapse:collapse;"">
			<tr style=""height:22.7pt;"">
				<td class=""cs95E54B96"" width=""14%""><p class=""cs2E86D3A6""><span class=""cs7C6DA362"">회의 일시</span></p></td><td class=""csAD56A1DF"" width=""56%""><p class=""cs95E872D0""><span class=""csC035B3DE"">{0}</span></p></td><td class=""cs84424F58"" width=""11%""><p class=""cs2E86D3A6""><span class=""csC035B3DE"">작성자</span></p></td><td class=""csC808E302"" width=""18%""><p class=""cs2E86D3A6""><span class=""csC035B3DE"">{1}</span></p></td></tr>
			<tr style=""height:48.15pt;"">
				<td class=""cs59E853D8"" width=""14%""><p class=""cs2E86D3A6""><span class=""cs7C6DA362"">참석자</span></p></td><td class=""csB659E1D3"" colspan=""3"" width=""85%""><p class=""cs95E872D0""><span class=""csC035B3DE"">{2}</span></p></td></tr>
		</table>
		<p class=""cs8ADB2719""><span class=""csC035B3DE"">&nbsp;</span></p><table class=""cs2D2816FE"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""border-collapse:collapse;"">
			<tr style=""height:22.7pt;"">
				<td class=""cs95E54B96"" width=""14%""><p class=""cs2E86D3A6""><span class=""cs7C6DA362"">회의 안건</span></p></td><td class=""csC39937F2"" width=""85%""><p class=""cs95E872D0""><span class=""csC035B3DE"">{3}</span></p></td></tr>
			<tr style=""height:504.55pt;"">
				<td class=""cs85368FFC"" colspan=""2"" valign=""top"" width=""100%""><p class=""cs80D9435B""><span class=""csE381FCF8"">&nbsp;</span></p><p class=""cs80D9435B""><span class=""csC035B3DE""></span></p></td></tr>
		</table>
		<p class=""cs8ADB2719""><span class=""csC035B3DE"">&nbsp;</span></p></body>
</html> ";

            Console.WriteLine(string.Format(format2, DateTime.Now.ToString("yyyy-MM-dd"), "테스트a", "참석자b", "title"));
            
        }

        [TestMethod]
        public void GetAssemblyName()
        {
            var assem = System.Reflection.Assembly.GetExecutingAssembly();
            string fullName = assem.FullName;
            var name = assem.GetName();

            var thisName = typeof(StringCheck).Assembly.GetName().Name;
            //.GetName()

            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

    }
    public class FormatItem
    {
        public DateTime StartDate { get; set; }
        public string Writer { get; set; }
        public string MeetingUsers { get; set; }
        public string Title { get; set; }
    }
}
