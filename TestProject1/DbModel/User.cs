using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.DbModel
{
    [Table(name:"User")]
    public class User
    {
        [Required]
        [MaxLength(20)]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        
        public int? Age { get; set; }

        public override string ToString() => $"{Id}, {Name}, {Age}";
    }
}
