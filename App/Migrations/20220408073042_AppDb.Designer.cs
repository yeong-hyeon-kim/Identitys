// <auto-generated />
using App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace App.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220408073042_AppDb")]
    partial class AppDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("App.MODEL.Users", b =>
                {
                    b.Property<string>("USER_CD")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("REMARK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USER_CONTACT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("USER_DEPT")
                        .IsRequired()
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
