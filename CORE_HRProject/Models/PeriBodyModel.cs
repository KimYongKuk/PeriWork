using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_HRProject.Models
{
    public class PeriBodyModel
    {
        public string reqNo { get; set; }
        public string departmentName { get; set; }
        public string userName { get; set; }
        //public int workName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string reason { get; set; }
        public string appYN_DESC { get; set; }
        public string hrYN_DESC { get; set; }
        public string filePath { get; set; }
        public string afterFilePath { get; set; }
        public string personalCode { get; set; }
        public string gViewSelName { get; set; }
        public string cancelDESC { get; set; }
    }
}
