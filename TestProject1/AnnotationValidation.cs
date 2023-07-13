using RecursiveDataAnnotationsValidation;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using TestProject1.DbModel;
using TestProject1.Models;
using TestProject1.Utils;

namespace TestProject1;

public class AnnotationValidation
{
    [Test]
    public Task 정합성체크()
    {
        // 단순 구조 model 이면 RecursiveDataAnnotationValidator 안써도됨. class in class 있는경우 기본 validator로는 안되어 RecursiveDataAnnotationValidator 써서 하게됨
        User user = new();
        (bool, string?) result = ModelValidator.Worker(user);
        if(result.Item1 == false)
        {
            Console.WriteLine(result.Item2);
        }

        return Task.CompletedTask;
    }

    [Test]
    public Task 접합성체크_1_N()
    {
        // 1:N 구조 객체 정합서 체크
        Dep dep = new Dep();
        dep.DepId = "";
        dep.Users.Add(new User() { Id = "user1" });
        dep.Users.Add(new User());

        (bool, string?) result = ModelValidator.Worker(dep);

        if (result.Item1 == false)
        {
            Console.WriteLine(result.Item2);
        }

        return Task.CompletedTask;
    }
}

