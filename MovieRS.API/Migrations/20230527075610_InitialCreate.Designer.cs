﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieRS.API.Models;

#nullable disable

namespace MovieRS.API.Migrations
{
    [DbContext(typeof(MovieRsContext))]
    [Migration("20230527075610_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieRS.API.Models.Country", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("Id"));

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NameVi")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name_Vi");

                    b.HasKey("Id")
                        .HasName("PK__COUNTRY__3214EC07DB3E63CF");

                    b.ToTable("COUNTRY", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.Favourite", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime");

                    b.HasKey("UserId", "MovieId")
                        .HasName("PkFavorite");

                    b.HasIndex("MovieId");

                    b.ToTable("FAVOURITE", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.History", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime");

                    b.HasKey("UserId", "MovieId")
                        .HasName("PkHistory");

                    b.HasIndex("MovieId");

                    b.ToTable("HISTORY", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdImdb")
                        .HasColumnType("int");

                    b.Property<int?>("IdTmdb")
                        .HasColumnType("int");

                    b.Property<short?>("YearRelease")
                        .HasColumnType("smallint");

                    b.HasKey("Id")
                        .HasName("PK__MOVIE__3214EC07CC466EE7");

                    b.ToTable("MOVIE", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.Param", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK__PARAMS__3214EC0707E93F9A");

                    b.ToTable("PARAMS", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(3, 1)");

                    b.Property<DateTime?>("TimeStamp")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__REVIEW__3214EC078461CE60");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("REVIEW", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short?>("CountryId")
                        .HasColumnType("smallint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<byte[]>("Password")
                        .HasMaxLength(32)
                        .HasColumnType("binary(32)")
                        .IsFixedLength();

                    b.Property<string>("Username")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id")
                        .HasName("PK__USER___3214EC07EA9A7E47");

                    b.HasIndex("CountryId");

                    b.HasIndex(new[] { "Email" }, "UQ__USER___A9D105342DB23183")
                        .IsUnique();

                    b.ToTable("USER_", (string)null);
                });

            modelBuilder.Entity("MovieRS.API.Models.Favourite", b =>
                {
                    b.HasOne("MovieRS.API.Models.Movie", "Movie")
                        .WithMany("Favourites")
                        .HasForeignKey("MovieId")
                        .IsRequired()
                        .HasConstraintName("FkFavoriteMovie");

                    b.HasOne("MovieRS.API.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FkFavoriteUser");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieRS.API.Models.History", b =>
                {
                    b.HasOne("MovieRS.API.Models.Movie", "Movie")
                        .WithMany("Histories")
                        .HasForeignKey("MovieId")
                        .IsRequired()
                        .HasConstraintName("FkHistory");

                    b.HasOne("MovieRS.API.Models.User", "User")
                        .WithMany("Histories")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FkHistoryUser");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieRS.API.Models.Review", b =>
                {
                    b.HasOne("MovieRS.API.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .IsRequired()
                        .HasConstraintName("FkReviewMovie");

                    b.HasOne("MovieRS.API.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FkReviewUser");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieRS.API.Models.User", b =>
                {
                    b.HasOne("MovieRS.API.Models.Country", "Country")
                        .WithMany("Users")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FkUSERCountry");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MovieRS.API.Models.Country", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MovieRS.API.Models.Movie", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Histories");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MovieRS.API.Models.User", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Histories");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}