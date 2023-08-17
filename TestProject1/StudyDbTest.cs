using Microsoft.EntityFrameworkCore;
using TestProject1.Db;
using TestProject1.DbModel;

namespace TestProject1
{
    /// <summary>
    /// entity f/w 테스트
    /// </summary>
    public class StudyDbTest
    {
        StudyDbContext db;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("db context 객체 생성");
            db = new StudyDbContext();
        }

        [Test]
        public async Task 데이터베이스생성()
        {
            await db.Database.EnsureCreatedAsync().ConfigureAwait(false);
        }

        [Test]
        public void UserAdd()
        {
            db.Add(new User() { Id = "mKim99", Name = "Test" });
            db.SaveChanges();

        }

        [Test]
        public void UserRead()
        {
            var data = db.User;
            
            foreach(var item in data)
            {
                //Console.WriteLine($"{item.Id}, {item.Name}, {item.Age}");
                Console.WriteLine(item);
            }
        }

        [Test]
        public void 단일조회()
        {
            // sql server라서 대소문자 구분안하고 있는듯?

            var data = db.User.Single(x => x.Id == "mkim");
            Console.WriteLine(data);
        }

        [Test]
        public void 조건절조회()
        {
            var datas = db.User.Where(x => x.Id.StartsWith("mkim"));
            foreach (var item in datas)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void 업데이트()
        {
            var data = db.User.Single(x => x.Id == "mkim");
            data.Age = 99;
            db.SaveChanges();
        }

        [Test]
        public void 삭제()
        {
            db.Add(new User { Id = "test", Name = "aa" });
            db.SaveChanges();

            db.Remove(db.User.Single(x => x.Id == "test"));
            db.SaveChanges();
        }

        [Test]
        public void 쿼리Ouput()
        {
            IQueryable datas = db.User.Where(x => x.Id.StartsWith("mkim"));

            string query = datas.ToQueryString();

            Console.WriteLine(query);
        }
    }
}