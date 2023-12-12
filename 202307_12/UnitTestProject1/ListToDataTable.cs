using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace UnitTestProject1
{
    [TestClass]
    public class ListToDataTable
    {
        [TestMethod]
        public void TestMethod1()
        {
            //dicCombo.Add("D", "Date");
            //dicCombo.Add("T", "Text");
            //dicCombo.Add("C", "Combo");
            //dicCombo.Add("N", "Number");
            //dicCombo.Add("TM", "Time");

            string mgmtDocNo = "000001";
            List<GridRecordModel> initData = new List<GridRecordModel>();
            initData.Add(new GridRecordModel(mgmtDocNo, "LEVEL1_CD", "level1 코드", "100", "T", "1", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "LEVEL2_CD", "level2 코드", "100", "T", "2", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "TASK_CD", "업무코드", "150", "T", "3", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "TASK_NM", "업무명", "200", "T", "4", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "ST_PREARNG_DT", "시작예정일자", "150", "D", "5", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "FINSH_PREARNG_DT", "완료예정일자", "150", "D", "6", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "ST_DT", "시작일자", "150", "D", "7", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "FINSH_DT", "완료일자", "150", "D", "8", ""));
            //initData.Add(new GridRecordModel(mgmtDocNo, "PROGRS_STAT", "진행상태", "150", "C", "9", ""));
            initData.Add(new GridRecordModel(mgmtDocNo, "PROGRS_RT", "진행률", "100", "N", "9", ""));

            DataTable dt = Util.ToDataTable(initData);

            
        }
    }

    public class GridRecordModel
    {
        public GridRecordModel(string mgmtDocNo, string colNm, string titleNm, string colWidth, string colType, string sortseq, string tooltip)
        {
            prjMgmtDocNo = mgmtDocNo;
            prjMgmtDocSn = "0";
            docComnNm = colNm;
            docComnTitleNm = titleNm;
            comnLthVal = colWidth;
            comnDataTypCd = colType;
            this.sortSeq = sortseq;
            comnTtpCnte = tooltip;
            comnFixYn = "N";
            wrtnPosblYn = "Y";
            addHdrYn = "N";
            niud = "I";
        }
        /**
         * 프로젝트관리문서번호
         */
        public string prjMgmtDocNo { get; set; }

        /**
         * 프로젝트관리문서일련번호
         */
        public string prjMgmtDocSn { get; set; }

        /**
         * 문서컬럼생성일련번호
         */
        public string docComnCreatSn { get; set; }

        /**
         * 문서컬럼명
         */
        public string docComnNm { get; set; }

        /**
         * 문서컬럼타이틀명
         */
        public string docComnTitleNm { get; set; }

        /**
         * 컬럼길이값
         */
        public string comnLthVal { get; set; }

        /**
         * 컬럼데이터유형코드
         */
        public string comnDataTypCd { get; set; }

        /**
         * 상위문서컬럼명
         */
        public string uprDocComnNm { get; set; }

        /**
         * 컬럼툴팁내용
         */
        public string comnTtpCnte { get; set; }

        /**
         * 컬럼고정여부
         */
        public string comnFixYn { get; set; }

        /**
         * 마스크내용
         */
        public string maskCnte { get; set; }

        /**
         * 기본값
         */
        public string basicVal { get; set; }

        /**
         * 쓰기가능여부
         */
        public string wrtnPosblYn { get; set; }

        /**
         * 추가헤더여부
         */
        public string addHdrYn { get; set; }

        /**
         * 팝업검색화면정보내용
         */
        public string poupSrchScrnInfoCnte { get; set; }

        /**
         * 팝업검색리턴정보내용
         */
        public string poupSrchRtrnInfoCnte { get; set; }

        /**
         * 표시순서
         */
        public string sortSeq { get; set; }

        /**
         * 사용여부
         */
        public string useYn { get; set; }

        /**
         * 특기사항
         */
        public string rmrk { get; set; }

        /**
         * 리스트아이템값
         */
        public string listItemVal { get; set; }

        /**
         * 리스트아이템명
         */
        public string listItemNm { get; set; }

        /**
         * 이전 리스트아이템값
         */
        public string oldListItemVal { get; set; }

        /**
         * 문서구성종류코드
         */
        public string docCstiKindCd { get; set; }

        /**
         * 테이블 이름
         */
        public string tableNm { get; set; }

        /**
         * 상위 아이템 리스트 값
         */
        public string uprListItemVal { get; set; }

        /**
         * 디자인템플릿 문서생성 일련번호
         */
        public string docComnSn { get; set; }

        /**
         * NIUD
         */
        public string niud { get; set; }
    }
}
