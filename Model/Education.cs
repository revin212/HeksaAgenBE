using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HeksaAgen.Model
{
    public class Education
    {
        [Key]
        public long ID { get; set; }
        public long AgenID { get; set; }
        public string Strata { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public string GPA { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "Guest";
        [JsonIgnore]
        public Agen Agen { get; set; }
    }
}
