using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieRS.API.Models;

public partial class RsContext : DbContext
{
    public RsContext()
    {
    }

    public RsContext(DbContextOptions<RsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PreMovie> PreMovies { get; set; }

    public virtual DbSet<PreTraining> PreTrainings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PreMovie>(entity =>
        {
            entity.HasKey(e => e.MovieId);

            entity.ToTable("PRE_MOVIE");

            entity.Property(e => e.MovieId).ValueGeneratedNever();
        });

        modelBuilder.Entity<PreTraining>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MovieId });

            entity.ToTable("PRE_TRAINING");

            entity.HasOne(d => d.Movie).WithMany(p => p.PreTrainings)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
