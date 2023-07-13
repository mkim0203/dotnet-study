using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.DbModel;

namespace TestProject1.Models;

public sealed record Dep
{
    [Required]
    public string DepId { get; set; } = string.Empty;
    public List<User> Users { get; set; } = new List<User>();
}
