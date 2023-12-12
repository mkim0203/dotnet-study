using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class SendRequestItem : IAdditionValid
    {

        /// <summary>
        /// 토큰 KEY(사용자 인증 키)
        /// </summary>
        [Required(ErrorMessage = "토큰 KEY(사용자 인증 키)는 필수값 입니다.")]
        public string certKeyVl { get; set; }
        /// <summary>
        /// 토큰 암호 KEY(사용자 인증 암호 키)
        /// </summary>
        [Required(ErrorMessage = "토큰 암호 KEY(사용자 인증 암호 키)는 필수값 입니다.")]
        public string certPswdVl { get; set; }
        /// <summary>
        /// 배포 여부
        /// 0: 테스트모드(신고 처리 안 됨) ,1: 운영모드
        /// </summary>
        [Range(0, 1)]
        [Required]
        public int crltnYn { get; set; } = 0;
        /// <summary>
        /// 응답메시지 유형
        /// 0: XML 응답메시지, 1: JSON 응답메시지
        /// </summary>
        [Range(0, 1)]
        [Required]
        public int rspnsMsgTypeCd { get; set; } = 1;
        /// <summary>
        /// 병원 소프트웨어개발사(사업자)
        /// 개발업체가 있는 경우 업체명, 없을 경우 "자체전산"
        /// </summary>
        [Required]
        public string hsptlDevBizPicNm { get; set; } = "자체전산";
        /// <summary>
        /// 병원 소프트웨어 종류(버전)
        /// 개발업체가 있는 경우 프로그램명, 없을 경우 개발언어
        /// </summary>
        [Required]
        public string hsptlDevKndNm { get; set; } = "AMIS";
        /// <summary>
        /// 성명
        /// # 감염병 환자의 성명을 입력  # 영문 성명으로 작성하는 경우에는 여권 또는 외국인등록증에 기재된 성명을 기준으로 대문자로 작성
        /// # 신원미상인 경우 "신원미상"
        /// </summary>
        [Required]
        public string ptntNm { get; set; }
        /// <summary>
        /// 신원 미상 여부
        /// Y : 신원미상, N(Default) : 아님
        /// </summary>
        [RegularExpression("^[YN]$", ErrorMessage = "신원 미상 여부 'Y' 또는 'N'만 가능합니다.")]
        [Required]
        public string idntUnknwnYn { get; set; }
        /// <summary>
        /// 연락처
        /// 환자의 전화번호 '-' 포함하여 전송
        /// </summary>
        [Required]
        public string ptntTelno { get; set; }
        /// <summary>
        /// 보호자 성명
        /// 환자에 대한 보호자 명을 입력
        /// </summary>
        public string ptrcrNm { get; set; }
        /// <summary>
        /// 보호자 연락처
        /// 환자의 보호자 전화번호 '-' 포함하여 전송
        /// </summary>
        public string prtcrTelno { get; set; }
        /// <summary>
        /// 환자 외국인 여부
        /// Y:외국인, N:내국인
        /// </summary>
        [RegularExpression("^[YN]$", ErrorMessage = "환자 외국인 여부 'Y' 또는 'N'만 가능합니다.")]
        [Required]
        public string ptntFrgnrYn { get; set; }
        /// <summary>
        /// 국가명 코드
        /// # 외국인일 경우에만 입력 # 환자 국가 코드 입력 (국가코드표 참고)
        /// </summary>
        [StringLength(2, MinimumLength = 2, ErrorMessage = "국가명 코드 길이는 2자리 입니다.")]
        public string ptntNtnCd { get; set; }
        /// <summary>
        /// 환자 주민(외국인) 등록번호 여부
        /// "# 환자 주민(외국인)등록번호 존재 시 'Y' 입력
        /// 환자 주민(외국인)등록번호 없으며 여권번호 존재 시 'N' 입력"
        /// </summary>
        [RegularExpression("^[YN]$", ErrorMessage = "환자 주민(외국인) 등록번호 여부 'Y' 또는 'N'만 가능합니다.")]
        [Required]
        public string ptntRrnoYn { get; set; }
        /// <summary>
        /// 주민(외국인) 등록번호
        /// # 환자의 주민(외국인)등록번호 입력 # 신원미상 체크 하였을 경우에는 000000-0000000 입력
        /// </summary>
        [Required]
        public string ptntRrno { get; set; }
        /// <summary>
        /// 여권번호
        /// 환자 주민등록번호 여부 'N'입력시 필수   # 환자의 여권번호 입력
        /// </summary>
        public string ptntPno { get; set; }
        /// <summary>
        /// 성별 코드
        /// # 신원미상 여부가 'Y(신원미상)' 일 경우 전송 # 01 : 남성, 02 : 여성
        /// </summary>
        [RegularExpression("^(01|02)$", ErrorMessage = "성별 코드는 '01'(남성) 또는 '02'(여성) 만 가능합니다.")]
        public string ptntGndrCd { get; set; }
        /// <summary>
        /// 환자 연령
        /// # 신원미상 여부가 'Y(신원미상)' 일 경우 전송 # 입력 타입 : 숫자
        /// </summary>
        public string ptntAgeVl { get; set; }
        /// <summary>
        /// 직업 코드
        /// </summary>
        public string ptntCrCd { get; set; }
        /// <summary>
        /// 주민등록주소 우편번호
        /// 환자 주소의 우편번호
        /// </summary>
        public string ptntRnZip { get; set; }
        /// <summary>
        /// 주민등록주소
        /// # 거주지불명이 아닌 경우 필수 # 환자의 주민등록주소(도로명)
        /// </summary>
        public string ptntRnAddr { get; set; }
        /// <summary>
        /// 주민등록주소 상세
        /// 환자의 주민등록주소 상세(도로명)
        /// </summary>
        public string ptntRnDaddr { get; set; }
        /// <summary>
        /// 주민등록주소 건물관리번호
        /// # 거주지불명이 아닌 경우 필수 , # 도로명주소 DB의 25자리 건물관리번호 , # 도로명주소 개발자센터 참조
        /// </summary>
        public string ptntBldgMngNo { get; set; }
        /// <summary>
        /// 거주지 불명 여부
        /// Y : 거주지불명, N(Default) : 아님
        /// </summary>
        [RegularExpression("^[YN]$", ErrorMessage = "거주지 불명 여부 'Y' 또는 'N'만 가능합니다.")]
        public string rsdncIndstnctYn { get; set; }
        /// <summary>
        /// 감염병환자등의 상태 코드
        /// 10 : 생존, 20 : 사망
        /// </summary>
        [RegularExpression("^(10|20)$", ErrorMessage = "감염병환자등의 상태 코드는 '10'(생존) 또는 '20'(사망) 만 가능합니다.")]
        [Required]
        public string ptntDthCd { get; set; }
        /// <summary>
        /// 감염병 코드
        /// </summary>
        [Required]
        public string icdCd { get; set; }
        /// <summary>
        /// 증상 및 증후
        /// 신종감염병증후군 신고 시 증상 및 징후 값 필수
        /// </summary>
        public string icdSmptmSignCn { get; set; }
        /// <summary>
        /// 감염병환자등 분류 코드
        /// # 환자 분류 코드 값 , # 01 : 환자, 02 : 의사환자, 03 : 병원체보유자
        /// </summary>
        [RegularExpression("^(01|02|03)$", ErrorMessage = "감염병환자등 분류 코드는  '01'(환자), '02'(의사환자), '03'(병원체보유자) 만 가능합니다.")]
        [Required]
        public string ptntClsfCd { get; set; }
        /// <summary>
        /// 진단일
        /// 입력 타입 : YYYYMMDD
        /// </summary>
        [RegularExpression("^\\d{8}$", ErrorMessage = "진단일 데이터가 잘못되었습니다.")]
        [Required]
        public string dgnsYmd { get; set; }
        /// <summary>
        /// 의심증상 코드
        /// 01 : 있음 02 : 없음
        /// </summary>
        [RegularExpression("^(01|02)$", ErrorMessage = "의심증상 코드는  '01'(있음), '02'(없음) 만 가능합니다.")]
        [Required]
        public string dbtSmptmCd { get; set; }
        /// <summary>
        /// 발병일
        /// 의심증상 여부 '있음' 선택 시에만 입력 가능 , 입력 타입 : YYYYMMDD
        /// </summary>
        [RegularExpression("^\\d{8}$", ErrorMessage = "발병일 데이터가 잘못되었습니다.")]
        public string onstYmd { get; set; }
        /// <summary>
        /// 진단검사 코드
        /// # 진단검사에 대해 실시 여부 판단 값 , # 01 : 미실시, 02 : 실시
        /// </summary>
        [RegularExpression("^(01|02)$", ErrorMessage = "성별 코드는 '01'(미실시) 또는 '02'(실시) 만 가능합니다.")]
        [Required]
        public string dgnsInspExctnCd { get; set; }
        /// <summary>
        /// 비고(특이사항)
        /// 1,000자(1000Byte) 이내로 작성 , ** 한글은 개당 3Byte로 계산
        /// </summary>
        public string rmrkCn { get; set; }
        /// <summary>
        /// 검사 거부자 여부
        /// N : 아니요, Y : 예
        /// </summary>
        [RegularExpression("^[YN]$", ErrorMessage = "검사 거부자 여부 'Y' 또는 'N'만 가능합니다.")]
        [Required]
        public string invrYn { get; set; }
        /// <summary>
        /// 신고기관번호
        /// 요양기관번호 8자리
        /// </summary>
        [StringLength(8, MinimumLength = 8, ErrorMessage = "신고기관번호 길이는 8자리 입니다.")]
        [Required]
        public string dclrInstId { get; set; }
        /// <summary>
        /// 진단 의사 성명
        /// </summary>
        [Required]
        public string dclrInstDgnsDrNm { get; set; }

        #region 사망정보
        /// <summary>
        /// (가) 직접사인 내용
        /// </summary>
        public string csdth1Cn { get; set; }
        /// <summary>
        /// (나) (가)의 원인 내용
        /// </summary>
        public string csdth2Cn { get; set; }
        /// <summary>
        /// (다) (나)의 원인 내용
        /// </summary>
        public string csdth3Cn { get; set; }
        /// <summary>
        /// (라) (다)의 원인 내용
        /// </summary>
        public string csdth4Cn { get; set; }
        /// <summary>
        /// (가) 발병부터 사망까지의 기간 내용
        /// </summary>
        public string srvlPerd1Cn { get; set; }
        /// <summary>
        /// (나) 발병부터 사망까지의 기간 내용
        /// </summary>
        public string srvlPerd2Cn { get; set; }
        /// <summary>
        /// (다) 발병부터 사망까지의 기간 내용
        /// </summary>
        public string srvlPerd3Cn { get; set; }
        /// <summary>
        /// (라) 발병부터 사망까지의 기간 내용
        /// </summary>
        public string srvlPerd4Cn { get; set; }
        /// <summary>
        /// (가)부터 (라)까지의 사망 원인 
        /// 외의 그 밖의 신체 상황
        /// </summary>
        public string bodyStnCn { get; set; }
        /// <summary>
        /// 수술의 주요 소견
        /// </summary>
        public string srgrOpnCn { get; set; }
        /// <summary>
        /// 해부(또는 검안)의 주요 소견
        /// </summary>
        public string atstnOpnCn { get; set; }
        [RegularExpression("^\\d{8}$", ErrorMessage = "사망일 데이터가 잘못되었습니다.")]
        public string dthYmd { get; set; }
        #endregion

        public string RunVaild()
        {
            List<string> failValidMessage = new List<string>();

            if (string.IsNullOrEmpty(dgnsYmd) == false && CheckDateYMD(dgnsYmd) == false) { failValidMessage.Add($"진단일 데이터가 날짜형식(yyyyMMdd)가 아닙니다. => {dgnsYmd}"); }


            // 신원 미상인경우
            if (idntUnknwnYn == "Y")
            {
                // 이름을 신원미상으로 전달 해야함
                // 신원 미상인경우 주민번호 0으로 체움
                ptntNm = "신원미상";
                ptntRrno = "0000000000000";
                // 주민등록 등록여부 N, 여권번호 empty
                ptntRrnoYn = "N";
                ptntPno = string.Empty;
                // 신원 미상인경우 거주지 불명 처리
                rsdncIndstnctYn = "Y";
                ptntRnZip = string.Empty;
                ptntRnAddr = string.Empty;
                ptntRnDaddr = string.Empty;
                if (ptntAgeVl == null) { failValidMessage.Add("연령 정보를 입력해야합니다."); }

                ptntNtnCd = string.Empty;
            }
            else
            {
                // 신원 미상이 아닌경우
                if (rsdncIndstnctYn == "N")
                {
                    // 거주지 있으면
                    if (string.IsNullOrEmpty(ptntRnZip)) { failValidMessage.Add("주민등록주소 우편번호를 입력해야합니다."); }
                    if (string.IsNullOrEmpty(ptntRnAddr)) { failValidMessage.Add("주민등록주소를 입력해야합니다."); }
                }
                else
                {
                    // 거주지 불명이면
                    ptntRnZip = string.Empty;
                    ptntRnAddr = string.Empty;
                    ptntRnDaddr = string.Empty;
                }

                // 외국인 인경우
                if (ptntFrgnrYn == "Y")
                {
                    if (string.IsNullOrEmpty(ptntNtnCd)) { failValidMessage.Add("외국인경우 국가명 코드 정보가 필요합니다."); }
                }

                // 주민(외국인) 번호가 없는경우 여권번호 체크
                if (ptntRrnoYn == "N")
                {
                    // 2023.12.04 외국인 번호가 없는경우 외국인 번호 string.empty 로 전송. Api 룰 변경됨
                    ptntRrno = string.Empty;
                    if (ptntAgeVl == null) { failValidMessage.Add("연령 정보를 입력해야합니다."); }
                    if (string.IsNullOrEmpty(ptntPno)) { failValidMessage.Add("주민번호가 없는경우 여권 번호를 입력해야합니다."); }
                }
                else
                {
                    // 외국인 번호가 있는경우 여권번호 empty 처리
                    ptntPno = string.Empty;
                }
            }

            // 의심증상이 있었을경우 발병일 입력 필수. 병원체 보유자는 제외
            if (dbtSmptmCd == "01" && ptntClsfCd != "03")
            {
                if (string.IsNullOrEmpty(onstYmd)) { failValidMessage.Add("의심증상이 있는 경우 발병일을 입력해야합니다."); }
                if (string.IsNullOrEmpty(onstYmd) == false && CheckDateYMD(onstYmd) == false) { failValidMessage.Add($"발병일 데이터가 날짜형식(yyyyMMdd)가 아닙니다. => {onstYmd}"); }
            }

            // 감염병환자등의 상태 코드 사망인경우 사망신고 정보 입력 체크
            if (ptntDthCd == "20")
            {
                if (string.IsNullOrEmpty(csdth1Cn)) { failValidMessage.Add("사망인경우 (가) 직접사인을 입력해야합니다."); }
                if (string.IsNullOrEmpty(dthYmd)) { failValidMessage.Add("사망인경우 사망일을 입력해야합니다."); }


                if (string.IsNullOrEmpty(dthYmd) == false && CheckDateYMD(dthYmd) == false) { failValidMessage.Add($"사망일 데이터가 날짜형식(yyyyMMdd)가 아닙니다. => {dthYmd}"); }
            }

            if (string.IsNullOrWhiteSpace(rmrkCn) == false)
            {
                byte[] rmrkCnByte = System.Text.Encoding.UTF8.GetBytes(rmrkCn);
                if (rmrkCnByte.Length > 1000) { failValidMessage.Add($"비고(특이사항) 정보는 1,000byte를 넘을수 없습니다."); }
            }

            // 신종감염병증후군 이면 증상 및 증후가 필요함
            if (icdCd == "NA0012")
            {
                if (string.IsNullOrWhiteSpace(icdSmptmSignCn)) { failValidMessage.Add($"신종감염병증후군 신고 시 증상 및 징후 값 필수입니다."); }
            }
            else
            {
                icdSmptmSignCn = string.Empty;
            }

            return string.Join(Environment.NewLine, failValidMessage);
        }

        private bool CheckDateYMD(string dtData)
        {
            try
            {
                DateTime dtTmep = DateTime.ParseExact(dtData, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
