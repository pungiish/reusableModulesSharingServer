﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WidgetServer.Data;

namespace WidgetServer.Migrations
{
    [DbContext(typeof(WidgetsDataContext))]
    partial class UserModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("reusable_modules_sharing_server.Models.Widget", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Widget");
                });

            modelBuilder.Entity("WidgetServer.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Familyname");

                    b.Property<string>("GoogleID");

                    b.Property<string>("Name");

                    b.HasKey("Email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("reusable_modules_sharing_server.Models.Widget", b =>
                {
                    b.HasOne("WidgetServer.Models.User", "User")
                        .WithMany("Widgets")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
