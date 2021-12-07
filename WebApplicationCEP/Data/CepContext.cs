using Microsoft.EntityFrameworkCore;
using WebApplicationCEP.Models;

namespace WebApplicationCEP.Data
{
    public class CepContext : DbContext
    {
        public CepContext(DbContextOptions<CepContext> options) : base(options){ }

        public DbSet<Cep> Ceps { get; set; }
    } 
}
