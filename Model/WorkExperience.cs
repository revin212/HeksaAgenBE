using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace HeksaAgen.Model
{
    public class WorkExperience
    {
        [Key]
        public long ID { get; set; }
        public long AgenID { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string JobDesc { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "Guest";
        [JsonIgnore]
        public Agen Agen { get; set; }
    }
}
