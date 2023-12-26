using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeksaAgen.DTO
{
    public class InsertEducationDTO
    {
        public long AgenID { get; set; }
        public string Strata { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public string GPA { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "Guest";
    }
}
