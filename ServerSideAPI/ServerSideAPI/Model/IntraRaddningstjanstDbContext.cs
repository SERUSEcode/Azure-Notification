using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ServerSideAPI.Model;

public partial class IntraRaddningstjanstDbContext : DbContext
{
    public IntraRaddningstjanstDbContext()
    {
    }

    public IntraRaddningstjanstDbContext(DbContextOptions<IntraRaddningstjanstDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message.Message> Message { get; set; }

    public virtual DbSet<SituationTb.SituationTb> SituationTb { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=IntraRaddningstjanstDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message.Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreationTime).HasColumnType("datetime");
            entity.Property(e => e.MessageText).HasColumnType("text");
            entity.Property(e => e.MessageType).HasColumnType("text");
            entity.Property(e => e.SituationId).HasColumnType("text");
            entity.Property(e => e.UserId).HasColumnType("text");
        });

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
