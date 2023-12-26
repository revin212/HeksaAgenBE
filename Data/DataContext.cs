using Microsoft.EntityFrameworkCore;
using HeksaAgen.Model;


namespace HeksaAgen.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Agen> Agens { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Education> Educations { get; set; }
    }
}
