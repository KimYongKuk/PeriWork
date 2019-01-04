using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_HRProject.Models
{
    public class PeriWork
    {
        [Key]
        public string userId { get; set; }
        public int personalCode { get; set; }
        public int departmentCode { get; set; }
        public string departmentName { get; set; }
        public string userName { get; set; }
        public int getRestOfDay { get; set; }
        public int totalRestDay { get; set; }
        public int usedRestDay { get; set; }
        public string etc { get; set; }
    }
}
