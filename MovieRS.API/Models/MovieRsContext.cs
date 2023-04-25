using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieRS.API.Models;

public partial class MovieRsContext : DbContext
{
    public MovieRsContext()
    {
    }

    public MovieRsContext(DbContextOptions<MovieRsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Param> Params { get; set; }

    public virtual DbSet<RawTrainingModel> RawTrainingModels { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COUNTRY__3214EC07DB3E63CF");

            entity.ToTable("COUNTRY");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MovieId }).HasName("PkFavorite");

            entity.ToTable("FAVOURITE");

            entity.Property(e => e.NotUse).HasColumnName("NOT_USE");

            entity.HasOne(d => d.Movie).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkFavoriteMovie");

            entity.HasOne(d => d.User).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkFavoriteUser");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GENRE__3214EC078226D474");

            entity.ToTable("GENRE");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MovieId }).HasName("PkHistory");

            entity.ToTable("HISTORY");

            entity.Property(e => e.TimeStamp).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Histories)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkHistory");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkHistoryUser");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MOVIE__3214EC07CC466EE7");

            entity.ToTable("MOVIE");

            entity.HasMany(d => d.Genres).WithMany(p => p.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "DetailGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKDetailGenreGenre"),
                    l => l.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FKDetailGenreMovie"),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId").HasName("PkDetailGenre");
                        j.ToTable("DETAIL_GENRE");
                    });
        });

        modelBuilder.Entity<Param>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PARAMS__3214EC0707E93F9A");

            entity.ToTable("PARAMS");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RawTrainingModel>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MovieId }).HasName("PkRawTrainingModel");

            entity.ToTable("RAW_TRAINING_MODEL");

            entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

            entity.HasOne(d => d.Movie).WithMany(p => p.RawTrainingModels)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkRawTrainingModelMovie");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REVIEW__3214EC078461CE60");

            entity.ToTable("REVIEW");

            entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkReviewMovie");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkReviewUser");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER___3214EC07EA9A7E47");

            entity.ToTable("USER_");

            entity.HasIndex(e => e.Email, "UQ__USER___A9D105342DB23183").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Username).HasMaxLength(200);

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FkUSERCountry");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
