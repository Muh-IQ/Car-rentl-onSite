using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(50)]
        public string CategoryName { get; set; }
    }
}
