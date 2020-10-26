using Microsoft.EntityFrameworkCore;

namespace agendamento.Models
{
    public class AgendamentoContexto : DbContext
    {
        public AgendamentoContexto(DbContextOptions<AgendamentoContexto> options) : base(options)
        {

        }

        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Fornecedor)
                .WithMany(a => a.Agendamentos)
                .HasForeignKey(a => a.IdFornecedor);
        }
    }
}