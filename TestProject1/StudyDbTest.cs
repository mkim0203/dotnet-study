using TestProject1.Db;
using TestProject1.DbModel;

namespace TestProject1
{
    /// <summary>
    /// entity f/w �׽�Ʈ
    /// </summary>
    public class StudyDbTest
    {
        StudyDbContext db;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("db context ��ü ����");
            db = new StudyDbContext();
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
        public void ������ȸ()
        {
            // sql server�� ��ҹ��� ���о��ϰ� �ִµ�?

            var data = db.User.Single(x => x.Id == "mkim");
            Console.WriteLine(data);
        }

        [Test]
        public void ��������ȸ()
        {
            var datas = db.User.Where(x => x.Id.StartsWith("mkim"));
            foreach (var item in datas)
            {
                Console.WriteLine(item);
            }
        }

        [Test]
        public void ������Ʈ()
        {
            var data = db.User.Single(x => x.Id == "mkim");
            data.Age = 99;
            db.SaveChanges();
        }

        [Test]
        public void ����()
        {
            db.Add(new User { Id = "test", Name = "aa" });
            db.SaveChanges();

            db.Remove(db.User.Single(x => x.Id == "test"));
            db.SaveChanges();
        }
    }
}