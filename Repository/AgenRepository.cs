using HeksaAgen.Data;
using HeksaAgen.DTO;
using HeksaAgen.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeksaAgen.Repository
{
    public class AgenRepository
    {
        private readonly DataContext _context;

        public AgenRepository(DataContext context)
        {
            _context = context;
        }

        public List<Agen> GetAllAgen()
        {
            return _context.Agens
                .ToList();
        }

        public Agen GetAgenById(long Id)
        {
            return _context.Agens
            .Include(a => a.Attachments)
            .Include(a => a.Educations)
            .Include(a => a.WorkExperiences)
            .Where(ag => ag.ID == Id)
            .FirstOrDefault();
        }

        #nullable enable
        public Agen? CheckEmail(string Email)
        {
            return _context.Agens
            .FirstOrDefault(ag => ag.Email == Email);
        }
        #nullable disable

        public bool CreateAgen(Agen insertAgen)
        {
            try
            {
                _context.Add(insertAgen);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAgen(Agen oldAgen, Agen updateAgen)
        {
            try
            {
                oldAgen.Name = updateAgen.Name;
                oldAgen.Gender = updateAgen.Gender;
                oldAgen.BirthPlace = updateAgen.BirthPlace;
                oldAgen.BirthDate = updateAgen.BirthDate;
                oldAgen.Address = updateAgen.Address;
                oldAgen.Email = updateAgen.Email;
                oldAgen.Phone = updateAgen.Phone;
                oldAgen.IdCard = updateAgen.IdCard;
                oldAgen.WorkExperiences = updateAgen.WorkExperiences;
                oldAgen.Attachments = updateAgen.Attachments;
                oldAgen.Educations = updateAgen.Educations;

                _context.Update(oldAgen);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAgen(long Id)
        {
            try
            {
                Agen deleteAgen = new Agen { ID = Id };
                _context.Agens.Attach(deleteAgen);
                _context.Agens.Remove(deleteAgen);
                return Save();
            }
            catch
            {
                return false;
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
