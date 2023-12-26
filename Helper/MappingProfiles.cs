using AutoMapper;
using HeksaAgen.DTO;
using HeksaAgen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeksaAgen.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Agen, GetAllAgenDTO>();
            CreateMap<CreateAgenDTO, Agen>();

        }
    }
}
