using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ServerSideAPI.Model;

public partial class IntraRaddningstjanstDbContext : DbContext
{
    public IntraRaddningstjanstDbContext()
    {
		this.Database.EnsureCreated();
	}

    public IntraRaddningstjanstDbContext(DbContextOptions<IntraRaddningstjanstDbContext> options)
        : base(options)
    {
    }

    public DbSet<SituationTb.SituationTb> SituationTb { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=IntraRaddningstjanstDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SituationTb.SituationTb>(entity =>
        {
            entity.ToTable("SituationTb");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreationTime).HasColumnType("datetime");
            entity.Property(e => e.IconId).HasColumnType("text");
            entity.Property(e => e.Message).HasColumnType("text");
            entity.Property(e => e.MessageCode).HasColumnType("text");
            entity.Property(e => e.MessageType).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
