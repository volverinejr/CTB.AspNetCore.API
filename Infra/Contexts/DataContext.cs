using Domain.Modulos.Infracao;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Infracao;
using Domain.Modulos.Infracao.Natureza;
using Domain.Modulos.TaxaSelic;
using Microsoft.EntityFrameworkCore;

namespace Infra.Contexts
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TaxaSelicModel> TaxaSelic { get; set; }

        public DbSet<NaturezaModel> InfracaoNatureza { get; set; }
        public DbSet<GrupoModel> InfracaoGrupo { get; set; }
        public DbSet<InfracaoModel> Infracao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
/*             modelBuilder.Entity<TaxaSelicModel>().ToTable("TaxaSelic");
            modelBuilder.Entity<TaxaSelicModel>().Property(x => x.Id);
            modelBuilder.Entity<TaxaSelicModel>().Property(x => x.Ano).IsRequired().HasColumnType("int(4)");
            modelBuilder.Entity<TaxaSelicModel>().Property(x => x.Mes).IsRequired().HasColumnType("int(4)");
            modelBuilder.Entity<TaxaSelicModel>().Property(x => x.Valor).IsRequired().HasColumnType("decimal(8,5)");
 */        }


    }
}
