using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class ScopeTest
    {

        private List<LegalU11OnlineSendScopeItem> _onlineSendScope { get; set; }
        /// <summary>
        /// 자동신고 신고범위 항목
        /// </summary>
        private List<LegalU11OnlineSendScopeItem> OnlineSendScope
        {
            get
            {
                if (_onlineSendScope == null || _onlineSendScope.Count == 0)
                {
                    _onlineSendScope = new List<LegalU11OnlineSendScopeItem>()
                    {
                        // 1급
                        new LegalU11OnlineSendScopeItem("NA0001", "O", "O", "X", "에볼라바이러스병"),
                        new LegalU11OnlineSendScopeItem("NA0002", "O", "O", "X", "마버그열"),
                        new LegalU11OnlineSendScopeItem("NA0003", "O", "O", "X", "라싸열"),
                        new LegalU11OnlineSendScopeItem("NA0004", "O", "O", "X", "크리미안콩고출혈열"),
                        new LegalU11OnlineSendScopeItem("NA0005", "O", "O", "X", "남아메리카출혈열"),
                        new LegalU11OnlineSendScopeItem("NA0006", "O", "O", "X", "리프트밸리열"),
                        new LegalU11OnlineSendScopeItem("NA0007", "O", "O", "X", "두창"),
                        new LegalU11OnlineSendScopeItem("NA0008", "O", "O", "X", "페스트"),
                        new LegalU11OnlineSendScopeItem("NA0009", "O", "O", "X", "탄저"),
                        new LegalU11OnlineSendScopeItem("NA0010", "O", "O", "X", "보툴리눔독소증"),
                        new LegalU11OnlineSendScopeItem("NA0011", "O", "O", "X", "야토병"),
                        new LegalU11OnlineSendScopeItem("NA0012", "O", "O", "X", "신종감염병증후군"),
                        new LegalU11OnlineSendScopeItem("NA0013", "O", "O", "X", "중증급성호흡기증후군(SARS)"),
                        new LegalU11OnlineSendScopeItem("NA0014", "O", "O", "O", "중동호흡기증후군(MERS)"),
                        new LegalU11OnlineSendScopeItem("NA0015", "O", "O", "X", "동물인플루엔자 인체감염증"),
                        new LegalU11OnlineSendScopeItem("NA0016", "O", "O", "X", "신종인플루엔자"),
                        new LegalU11OnlineSendScopeItem("NA0017", "O", "O", "X", "디프테리아"),
                        
                        // 2급
                        new LegalU11OnlineSendScopeItem("NB0002", "O", "O", "X", "수두"),
                        new LegalU11OnlineSendScopeItem("NB0003", "O", "O", "X", "홍역"),
                        new LegalU11OnlineSendScopeItem("NB0004", "O", "O", "O", "콜레라"),
                        new LegalU11OnlineSendScopeItem("NB0005", "O", "O", "O", "장티푸스"),
                        new LegalU11OnlineSendScopeItem("NB0006", "O", "O", "O", "파라티푸스"),
                        new LegalU11OnlineSendScopeItem("NB0007", "O", "O", "O", "세균성이질"),
                        new LegalU11OnlineSendScopeItem("NB0008", "O", "O", "O", "장출혈성대장균감염증"),
                        new LegalU11OnlineSendScopeItem("NB0009", "O", "O", "O", "A형간염"),
                        new LegalU11OnlineSendScopeItem("NB0010", "O", "O", "X", "백일해"),
                        new LegalU11OnlineSendScopeItem("NB0011", "O", "O", "X", "유행성이하선염"),
                        new LegalU11OnlineSendScopeItem("NB1201", "O", "O", "X", "풍진(선천성)"),
                        new LegalU11OnlineSendScopeItem("NB1202", "O", "O", "X", "풍진(후천성)"),
                        new LegalU11OnlineSendScopeItem("NB0013", "O", "O", "X", "폴리오"),
                        new LegalU11OnlineSendScopeItem("NB0014", "O", "O", "X", "수막구균 감염증"),
                        new LegalU11OnlineSendScopeItem("NB0015", "O", "O", "X", "b형헤모필루스인플루엔자"),
                        new LegalU11OnlineSendScopeItem("NB0016", "O", "O", "X", "폐렴구균 감염증"),
                        new LegalU11OnlineSendScopeItem("NB0017", "O", "X", "X", "한센병"),
                        new LegalU11OnlineSendScopeItem("NB0018", "O", "O", "X", "성홍열"),
                        new LegalU11OnlineSendScopeItem("NB0019", "O", "X", "O", "반코마이신내성황색포도알균(VRSA) 감염증"),
                        new LegalU11OnlineSendScopeItem("NB0020", "O", "X", "O", "카바페넴내성장내세균속균종(CRE) 감염증"),
                        new LegalU11OnlineSendScopeItem("NB0021", "O", "X", "O", "E형간염"),
                        new LegalU11OnlineSendScopeItem("NB0023", "O", "O", "X", "엠폭스"),

                        //3급
                        new LegalU11OnlineSendScopeItem("NC0001", "O", "X", "X", "파상풍"),
                        new LegalU11OnlineSendScopeItem("NC0002", "O", "X", "X", "B형간염"),
                        new LegalU11OnlineSendScopeItem("NC0003", "O", "O", "X", "일본뇌염"),
                        new LegalU11OnlineSendScopeItem("NC0004", "O", "X", "O", "C형간염"),
                        new LegalU11OnlineSendScopeItem("NC0005", "O", "O", "O", "말라리아"),
                        new LegalU11OnlineSendScopeItem("NC0006", "O", "O", "X", "레지오넬라증"),
                        new LegalU11OnlineSendScopeItem("NC0007", "O", "O", "X", "비브리오패혈증"),
                        new LegalU11OnlineSendScopeItem("NC0008", "O", "O", "X", "발진티푸스"),
                        new LegalU11OnlineSendScopeItem("NC0009", "O", "O", "X", "발진열"),
                        new LegalU11OnlineSendScopeItem("NC0010", "O", "O", "X", "쯔쯔가무시증"),
                        new LegalU11OnlineSendScopeItem("NC0011", "O", "O", "X", "렙토스피라증"),
                        new LegalU11OnlineSendScopeItem("NC0012", "O", "O", "X", "브루셀라증"),
                        new LegalU11OnlineSendScopeItem("NC0013", "O", "O", "X", "공수병"),
                        new LegalU11OnlineSendScopeItem("NC0014", "O", "O", "X", "신증후군출혈열"),
                        new LegalU11OnlineSendScopeItem("NC0016", "O", "O", "X", "크로이츠펠트-야콥병(CJD) 및변종크로이츠펠트-야콥병(vCJD)"),
                        new LegalU11OnlineSendScopeItem("NC0017", "O", "X", "O", "황열"),
                        new LegalU11OnlineSendScopeItem("NC0018", "O", "X", "O", "뎅기열"),
                        new LegalU11OnlineSendScopeItem("NC0019", "O", "O", "X", "큐열"),
                        new LegalU11OnlineSendScopeItem("NC0020", "O", "O", "O", "웨스트나일열"),
                        new LegalU11OnlineSendScopeItem("NC0021", "O", "O", "X", "라임병"),
                        new LegalU11OnlineSendScopeItem("NC0022", "O", "X", "X", "진드기매개뇌염"),
                        new LegalU11OnlineSendScopeItem("NC0023", "O", "O", "X", "유비저"),
                        new LegalU11OnlineSendScopeItem("NC0024", "O", "X", "O", "치쿤구니야열"),
                        new LegalU11OnlineSendScopeItem("NC0025", "O", "O", "X", "중증열성혈소판감소증후군(SFTS)"),
                        new LegalU11OnlineSendScopeItem("NC0026", "O", "O", "O", "지카바이러스 감염증"),
                        new LegalU11OnlineSendScopeItem("NC2701", "O", "X", "X", "매독(1기)"),
                        new LegalU11OnlineSendScopeItem("NC2702", "O", "X", "X", "매독(2기)"),
                        new LegalU11OnlineSendScopeItem("NC2703", "O", "X", "X", "매독(3기)"),
                        new LegalU11OnlineSendScopeItem("NC2704", "O", "X", "X", "매독(선천성)"),
                        new LegalU11OnlineSendScopeItem("NC2705", "X", "X", "O", "매독(잠복)"),

                    };
                }
                return _onlineSendScope;
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            string icdCode = "NA0001";
            List<string> clsfs = new List<string>() { "01", "02", "03" };

            foreach (string clsfCode in clsfs)
            {
                LegalU11OnlineSendScopeItem targetIcdInfo = OnlineSendScope.Find(x => x.IcdCode == icdCode);
                if (targetIcdInfo == null)
                {
                    // 감염병 코드가 없을때. 전자신고 못하게 처리
                    Console.WriteLine("NOT FOUND");
                    //return null;
                }
                else
                {
                    bool needSend = false;
                    needSend = targetIcdInfo.Scope01 && clsfCode == "01";
                    if (needSend == false) needSend = targetIcdInfo.Scope02 && clsfCode == "02";
                    if (needSend == false) needSend = targetIcdInfo.Scope03 && clsfCode == "03";

                    Console.WriteLine($"{icdCode} {clsfCode} => {needSend}");
                }
            }
        }
    }

    /// <summary>
    /// 전자신고 신고 대상 감염병 관리 item
    /// </summary>
    public class LegalU11OnlineSendScopeItem
    {
        /// <summary>
        /// 감염병 코드
        /// </summary>
        public string IcdCode { get; }
        /// <summary>
        /// 검염병 이름
        /// </summary>
        public string IcdName { get; }
        /// <summary>
        /// 감염병환자등 분류 환자 신고대상 여부
        /// </summary>
        public bool Scope01 { get; }
        /// <summary>
        /// 감염병환자등 분류 의사환자 신고대상 여부
        /// </summary>
        public bool Scope02 { get; }
        /// <summary>
        /// 감염병환자등 분류 병원체보유자 신고대상 여부
        /// </summary>
        public bool Scope03 { get; }

        /// <summary>
        /// 전자신고 신고 대상 감염병 관리 item
        /// </summary>
        /// <param name="icdCode">감염병 코드</param>
        /// <param name="scope01">환자 신고 범위</param>
        /// <param name="scope02">의사환자 신고 범위</param>
        /// <param name="scope03">병원체보유자 신고범위</param>
        /// <param name="icdName">감염병 명</param>
        public LegalU11OnlineSendScopeItem(string icdCode, string scope01, string scope02, string scope03, string icdName)
        {
            this.IcdCode = icdCode;
            this.Scope01 = scope01 == "O";
            this.Scope02 = scope02 == "O";
            this.Scope03 = scope03 == "O";
            this.IcdName = icdName;
        }
    }
}
