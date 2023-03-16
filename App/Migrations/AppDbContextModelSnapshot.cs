﻿// <auto-generated />
using App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("App.Model.Users", b =>
                {
                    b.Property<string>("USER_CD")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("REMARK")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USER_CONTACT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USER_DEPT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USER_EMAIL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USER_NM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("USER_CD");

                    b.ToTable("USERS");
                });
#pragma warning restore 612, 618
        }
    }
}
