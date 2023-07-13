using RecursiveDataAnnotationsValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Utils;

public static class ModelValidator
{
    /// <summary>
    /// 데이터 정합성 체크
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestData"></param>
    /// <returns></returns>
    public static (bool, string?) Worker<T>(T requestData)
    {
        RecursiveDataAnnotationValidator validator = new();
        List<ValidationResult> validationErrors = new();

        // list 구조이면 항목별로 정합성 체크 요청
        if (requestData is IList list)
        {
            foreach (object? item in list)
            {
                (bool, string?) itemResult = Worker(item);
                if (!itemResult.Item1)
                {
                    return itemResult;
                }
            }
        }

        if (!validator.TryValidateObjectRecursive(requestData, validationErrors))
        {
            string errorMessage = string.Join(Environment.NewLine, validationErrors);

            return (false, errorMessage);
        }

        return (true, null);
    }
}
