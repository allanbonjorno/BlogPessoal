﻿using blogpessoal.Model;
using BlogPessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace blogpessoal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postagem>().ToTable("tb_postagens");
            modelBuilder.Entity<Tema>().ToTable("tb_temas");

            // Relacionamento Postagem -> Tema
            _ = modelBuilder.Entity<Postagem>()
                .HasOne(_ => _.Tema)
                .WithMany(t => t.Postagem)
                .HasForeignKey("TemaId")
                .OnDelete(DeleteBehavior.Cascade);

        }

        // Registro das Entidades
        public DbSet<Postagem> Postagens { get; set; } = null!;
        public DbSet<Tema> Temas { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                                   .Where(x => x.State == EntityState.Added)
                                   .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                //Se uma propriedade da Classe Auditable estiver sendo criada. 
                if (insertedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                //Se uma propriedade da Classe Auditable estiver sendo atualizada.  
                if (modifiedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}