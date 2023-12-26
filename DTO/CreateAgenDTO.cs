using HeksaAgen.Model;
using System;
using System.Collections.Generic;

namespace HeksaAgen.DTO
{
    public class CreateAgenDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string BirthPlace { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string IdCard { get; set; } = string.Empty;
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<Attachment> Attachments { get; set; }
        public List<Education> Educations { get; set; }
    }
}
