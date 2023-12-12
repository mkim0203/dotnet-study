using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace UnitTestProject1
{
    [TestClass]
    public class WebRequestTest
    {
        private string _hostUrl = "https://tids.goability.co.kr:5444/";
        /// <summary>
        /// 발생신고
        /// </summary>
        private string _autoInftnsdsRegist = "/api/isa/autoInftnsds/autoInftnsdsRegist";

        [TestMethod]
        public void TestMethod1()
        {
            string sendURL = "https://www.daum.net/24asldkfjaskjvkasdf";
            WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
            string result = client.DownloadString(new Uri(new Uri(_hostUrl), _autoInftnsdsRegist));
            Console.WriteLine(result);
            SendResult sendResult = JsonConvert.DeserializeObject<SendResult>(result);
            if (sendResult?.Result?.Count > 0)
            {

                Console.WriteLine(string.Join(Environment.NewLine, sendResult.Result.Select(x => x.Message)));
            }

            // {"success":false,"result":[{"stat":"Bad Request","code":"400","codeDt":"5001","message":"송신 파라미터에 인증키 토큰 값이 존재하지 않습니다.","expryYn":null,"expryPrnmntYmd":null}],"parameter":{"regDt":null,"ntcMttrTtlNm":null,"rsltMsg":null,"ntcMttrTypeCd":null,"rsltCd":null,"rspnsMsgTypeCd":"1","regNm":null,"certKeyVl":null,"ntcMttrCn":null,"ntcMttrSn":null}}
        }

        [TestMethod]
        public void HttpWebRequestTest()
        {
            string sendParam = "?aa=bb&cc=dd&ee=ff";
            Uri sendUrl = new Uri(new Uri(_hostUrl), _autoInftnsdsRegist);
            Console.WriteLine(sendUrl.AbsoluteUri);
            //Uri sendUrlAndParam = new Uri(sendUrl, sendParam);
            //Console.WriteLine(sendUrlAndParam.AbsoluteUri);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sendUrl);
            req.Method = "GET";

            try
            {
                using (HttpWebResponse respon = (HttpWebResponse)req.GetResponse())
                {

                    var status = respon.StatusCode;
                    Console.WriteLine($"응답 코드 => {status}");

                    Stream respStream = respon.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        string json = sr.ReadToEnd();
                        SendResult sendResult = JsonConvert.DeserializeObject<SendResult>(json);
                        Console.WriteLine(sendResult);
                    }
                }
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message);
                using(WebResponse respon = webEx.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)respon;
                    Console.WriteLine($"응답 코드 => {httpResponse.StatusCode}");
                    using (Stream data = respon.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string json = reader.ReadToEnd();
                        SendResult sendResult = JsonConvert.DeserializeObject<SendResult>(json);
                        if(sendResult?.Result?.Count> 0)
                        {

                            Console.WriteLine(string.Join(Environment.NewLine, sendResult.Result.Select(x => x.Message)));
                        }
                    }
                }
            }
            // {"success":false,"result":[{"stat":"Bad Request","code":"400","codeDt":"5001","message":"인증키 또는 인증비밀번호가 올바르지 않습니다.","expryYn":null,"expryPrnmntYmd":null,"expryAprhYn":null}]
        }

        [TestMethod]
        public void ConverUriEncodingTest()
        {
            List<KeyValuePair<string, string>> sendParams = new List<KeyValuePair<string, string>>();
            sendParams.Add(new KeyValuePair<string, string>("aa", "1"));
            sendParams.Add(new KeyValuePair<string, string>("bb", "가나다"));
            sendParams.Add(new KeyValuePair<string, string>("bb", "!@*&^*#&^$)*)(!@{}:"));

            Console.WriteLine(GetQueryStringConvertUrlEncoding(sendParams));
        }

        /// <summary>
        /// url get 파라메터 url encoding 처리
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private string GetQueryStringConvertUrlEncoding(List<KeyValuePair<string, string>> datas)
        {
            if (datas == null || datas.Count == 0) return string.Empty;
            IList<string> convertDatas = new List<string>();
            foreach (var param in datas)
            {
                //string value = HttpUtility.UrlEncode(param.Value);
                string value = Uri.EscapeUriString(param.Value);
                convertDatas.Add($"{param.Key}={value}");
            }

            string queryString = $"?{string.Join("&", convertDatas)}";
            return queryString;
        }


        [TestMethod]
        public void SendRequest_URL_인코딩_Test()
        {
            SendRequestItem item = new SendRequestItem();
            item.certKeyVl = "DKFJDSKJFKVdjfjdjdjd";
            item.certPswdVl = "DJDJDJJDKSKSKKSLALALAL";
            item.crltnYn = 0;
            item.rspnsMsgTypeCd = 1;
            item.hsptlDevBizPicNm = "자체전산";
            item.hsptlDevKndNm = "amis";
            item.ptntNm = "테스터";
            item.idntUnknwnYn = "Y";
            item.dgnsYmd = "20232201";

            List<KeyValuePair<string, string>> modelPropertiesData = new List<KeyValuePair<string, string>>();
            foreach(var propertyInfo in item.GetType().GetProperties())
            {
                modelPropertiesData.Add(new KeyValuePair<string, string>(propertyInfo.Name, Convert.ToString(propertyInfo.GetValue(item, null))));
            }

            var querySTring = GetQueryStringConvertUrlEncoding(modelPropertiesData);

            Console.WriteLine(querySTring);
        }
    }

    public class SendResult
    {
        /// <summary>
        /// 전송결과
        /// </summary>
        public bool Success { get; set; }
        public IList<SendResultItem> Result { get; set; }
    }

    public class SendResultItem
    {
        /// <summary>
        /// 결과코드
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 결과세부코드
        /// </summary>
        public string CodeDt { get; set; }
        /// <summary>
        /// 결과메시지
        /// </summary>
        public string Stat { get; set; }
        /// <summary>
        /// 결과세부 메시지
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 토큰 만료 여부
        /// </summary>
        public string ExpryYn { get; set; }
        /// <summary>
        /// 토큰 만료 일자 yyyMMdd
        /// </summary>
        public string ExpryPrnmntYmd { get; set; }
        /// <summary>
        /// 토큰 만료 임박 여부
        /// </summary>
        public string ExpryAprhYn { get; set; }
    }

  
}
