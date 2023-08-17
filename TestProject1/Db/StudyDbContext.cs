using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestProject1.DbModel;

namespace TestProject1.Db;

/// <summary>
/// entity f/w db context
/// </summary>
public class StudyDbContext : DbContext
{
    public DbSet<User> User { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // MSSQLLocalDB 테스트 환경. mssql local db 설치후 진행
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=Study;Trusted_Connection=True");
        // console 로그 출력.
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 초기 데이터 설정하는 곳
        modelBuilder.Entity<User>().HasData(SeedData.SeedUsers);

        base.OnModelCreating(modelBuilder);
    }
}

public static class SeedData
{
    internal static IList<User> SeedUsers = new List<User>()
    {
        new User() { Id = "user1", Name = "사용자1",  Age = 20 },
        new User() { Id = "user2", Name = "사용자2",  Age = 22 }
    };
}