using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeksaAgen.DTO
{
    public class DeleteAttachmentsDTO
    {
        public List<string> attachmentFileNames { get; set; }
        public List<bool> deleteIndex { get; set; }
    }
}
