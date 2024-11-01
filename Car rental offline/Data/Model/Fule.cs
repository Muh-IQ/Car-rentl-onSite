using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Fule
    {
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(20)]
        public string FuleType { get; set; }
    }
}
