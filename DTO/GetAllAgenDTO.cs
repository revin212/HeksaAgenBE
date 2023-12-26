using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeksaAgen.DTO
{
    public class GetAllAgenDTO
    {
        public long ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string IdCard { get; set; } = string.Empty;
    }
}
