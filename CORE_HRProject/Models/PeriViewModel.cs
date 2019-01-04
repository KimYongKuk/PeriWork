using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace CORE_HRProject.Models
{
    public class PeriViewModel
    {
        public string Peri { get; set; }

        public IEnumerable <PeriWork> PeriWorkModel { get; set; }
        public IEnumerable<PeriHeaderModel> PeriHeaderModel { get; set; }
        public IEnumerable<PeriHeaderModel2> PeriHeaderModel2 { get; set; }
        
        public IEnumerable<PeriBodyModel> PeriBodyModel { get; set; }

        public IEnumerable<PeriBodyModel2> PeriBodyModel2 { get; set; }

        public IEnumerable<PeriHeaderModel3> PeriHeaderModel3 { get; set; }
        public IEnumerable<PeriHeaderModel4> PeriHeaderModel4 { get; set; }
        public IEnumerable<PeriHeaderModel5> PeriHeaderModel5 { get; set; }
        public IEnumerable<PeriHeaderModel6> PeriHeaderModel6 { get; set; }
        public IEnumerable<PeriHeaderModel7> PeriHeaderModel7 { get; set; }
        public IEnumerable<PeriHeaderModel8> PeriHeaderModel8 { get; set; }

        public List<SelectListItem> Codes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "MX", Text = "Mexico" },
            new SelectListItem { Value = "CA", Text = "Canada" },
            new SelectListItem { Value = "US", Text = "USA"  },
        };

    }
}
