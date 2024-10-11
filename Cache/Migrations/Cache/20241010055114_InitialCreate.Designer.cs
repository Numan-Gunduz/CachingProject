﻿// <auto-generated />
using Cache.Servisler.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cache.Migrations.Cache
{
    [DbContext(typeof(CacheContext))]
    [Migration("20241010055114_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cache.Models.Kategori", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kategoris");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Isim = "Kategori-1"
                        },
                        new
                        {
                            Id = 2,
                            Isim = "Kategori-2"
                        },
                        new
                        {
                            Id = 3,
                            Isim = "Kategori-3"
                        },
                        new
                        {
                            Id = 4,
                            Isim = "Kategori-4"
                        },
                        new
                        {
                            Id = 5,
                            Isim = "Kategori-5"
                        },
                        new
                        {
                            Id = 6,
                            Isim = "Kategori-6"
                        },
                        new
                        {
                            Id = 7,
                            Isim = "Kategori-7"
                        },
                        new
                        {
                            Id = 8,
                            Isim = "Kategori-8"
                        },
                        new
                        {
                            Id = 9,
                            Isim = "Kategori-9"
                        },
                        new
                        {
                            Id = 10,
                            Isim = "Kategori-10"
                        });
                });

            modelBuilder.Entity("Cache.Models.Urun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KategoriId");

                    b.ToTable("Uruns", null, t =>
                        {
                            t.HasTrigger("UrunsTableChangeTrigger");
                        });

                    b.ToView("ActiveUruns", (string)null);

                    b.HasAnnotation("SqlServer:UseSqlOutputClause", false);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Isim = "Urun-1",
                            KategoriId = 1
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Isim = "Urun-2",
                            KategoriId = 2
                        },
                        new
                        {
                            Id = 3,
                            IsActive = true,
                            Isim = "Urun-3",
                            KategoriId = 3
                        },
                        new
                        {
                            Id = 4,
                            IsActive = true,
                            Isim = "Urun-4",
                            KategoriId = 4
                        },
                        new
                        {
                            Id = 5,
                            IsActive = true,
                            Isim = "Urun-5",
                            KategoriId = 5
                        },
                        new
                        {
                            Id = 6,
                            IsActive = true,
                            Isim = "Urun-6",
                            KategoriId = 6
                        },
                        new
                        {
                            Id = 7,
                            IsActive = true,
                            Isim = "Urun-7",
                            KategoriId = 7
                        },
                        new
                        {
                            Id = 8,
                            IsActive = true,
                            Isim = "Urun-8",
                            KategoriId = 8
                        },
                        new
                        {
                            Id = 9,
                            IsActive = true,
                            Isim = "Urun-9",
                            KategoriId = 9
                        },
                        new
                        {
                            Id = 10,
                            IsActive = true,
                            Isim = "Urun-10",
                            KategoriId = 10
                        });
                });

            modelBuilder.Entity("Cache.Models.Urun", b =>
                {
                    b.HasOne("Cache.Models.Kategori", "Kategori")
                        .WithMany("Uruns")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("Cache.Models.Kategori", b =>
                {
                    b.Navigation("Uruns");
                });
#pragma warning restore 612, 618
        }
    }
}
