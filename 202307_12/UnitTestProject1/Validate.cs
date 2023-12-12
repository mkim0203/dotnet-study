using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class Validate
    {
        [TestMethod]
        public void TestMethod1()
        {
            ItemModel item = new ItemModel();


            var errorMessage = CheckVaildate(item);
            Console.WriteLine($"item => {errorMessage}");

            ItemModel item2 = new ItemModel() { Id = "       ", Number = 2000, Code = "Code5", AlpaText = "ABCD1EFFFF" };
            Console.WriteLine($"item2 => {CheckVaildate(item2)}");

            ItemModel item3 = new ItemModel() { Id = "B", Number = 20, Code = "Code1", Content = "ABC" };
            Console.WriteLine($"item3 => {CheckVaildate(item3) ?? "데이터 정합성 완료"}");

            
        }

        private string CheckVaildate<T>(T data)
        {
            ValidationContext ctx = new ValidationContext(data);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(data, ctx, results, true);

            var errorMessages = results.Select(msg => msg.ErrorMessage).ToList();

            if (data is IAdditionValid)
            {
                string checkAdditionVaild = (data as IAdditionValid).RunVaild();
                if(string.IsNullOrEmpty(checkAdditionVaild) == false) errorMessages.Add(checkAdditionVaild);
            }

            if (errorMessages.Count > 0)
                return string.Join(Environment.NewLine, errorMessages);
            else return null;

            //if (isValid == false)
            //{
            //    return string.Join(Environment.NewLine, results.Select(msg => msg.ErrorMessage));
            //}
            //else 
            //    return null;
        }

        [TestMethod]
        public void SendRequestTest()
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
            string checkResult = CheckVaildate(item);
            Console.WriteLine(checkResult);

            var dt = Util.ToDataTable(new List<SendRequestItem>() { item });
        }
    }

    public class ItemModel
    {
        [Required]
        public string Id { get; set; }

        [Range(1,1000)]
        public int Number { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "길이초과")]
        public string Content { get; set; }

        [EnumDataType(typeof(ItemCode))]
        [Required]
        public string Code { get; set; }

        [RegularExpression("^[A-Za-z]+$", ErrorMessage =("영문만 가능합니다."))]
        public string AlpaText { get; set; }
    }

    public enum ItemCode
    {
        Code1,
        Code2
    }
}
