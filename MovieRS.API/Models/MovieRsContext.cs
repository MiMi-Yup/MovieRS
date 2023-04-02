using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MovieRS.API.Models;

public partial class MovieRsContext : DbContext
{
    private readonly IConfiguration _configuration;
    public MovieRsContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MovieRsContext(DbContextOptions<MovieRsContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DetailGenre> DetailGenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__COUNTRY__3214EC077A9657E6");

            entity.ToTable("COUNTRY");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DetailGenre>(entity =>
        {
            entity.HasKey(e => new { e.IdMovie, e.IdGenre }).HasName("PkDetailGenre");

            entity.ToTable("DETAIL_GENRE");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GENRE__3214EC0726441A9B");

            entity.ToTable("GENRE");

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
                .HasConstraintName("FkHistoryUSER");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MOVIE__3214EC0737DEC4EC");

            entity.ToTable("MOVIE");

            entity.Property(e => e.YearRelease)
                .HasColumnType("date")
                .HasColumnName("YEAR_RELEASE");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MovieId }).HasName("PkReview");

            entity.ToTable("REVIEW");

            entity.Property(e => e.Rating).HasColumnType("decimal(1, 0)");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkReviewMovie");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkReviewUSER");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER___3214EC07217EB26D");

            entity.ToTable("USER_");

            entity.HasIndex(e => e.Email, "UQ__USER___A9D105347EF16793").IsUnique();

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

            entity.HasMany(d => d.Movies).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<Movie>().WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FkFavoriteMovie"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FkFavoriteUSER"),
                    j =>
                    {
                        j.HasKey("UserId", "MovieId").HasName("PkFavorite");
                        j.ToTable("FAVORITE");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
