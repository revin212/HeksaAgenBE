using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeksaAgen.Model
{
    public class Agen
    {
        [Key]
        public long ID { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;
        public string RegStatus { get; set; } = "Active";
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string BirthPlace { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string IdCard { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "Guest";
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Education> Educations { get; set; }
    }
}
