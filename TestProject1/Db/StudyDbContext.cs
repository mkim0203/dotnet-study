using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestProject1.DbModel;

namespace TestProject1.Db
{
    /// <summary>
    /// entity f/w db context
    /// </summary>
    public class StudyDbContext : DbContext
    {
        public DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=Study;Trusted_Connection=True");
            // console 로그 출력.
            optionsBuilder.LogTo(Console.WriteLine);
        }
    }
}

