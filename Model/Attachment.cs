using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HeksaAgen.Model
{
    public class Attachment
    {
        [Key]
        public long ID { get; set; }
        public long AgenID { get; set; }
        public string AttachmentType { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = "Guest";
        [JsonIgnore]
        public Agen Agen { get; set; }
    }
}
