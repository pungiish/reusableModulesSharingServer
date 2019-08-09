﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WidgetServer.Data;

namespace WidgetServer.Migrations
{
    [DbContext(typeof(WidgetsDataContext))]
    [Migration("20190806115011_changedForeignKey")]
    partial class changedForeignKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Username");

                    b.Property<string>("userEmail");

                    b.HasKey("Id");

                    b.HasIndex("userEmail");

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
                    b.HasOne("WidgetServer.Models.User", "user")
                        .WithMany("Widgets")
                        .HasForeignKey("userEmail");
                });
#pragma warning restore 612, 618
        }
    }
}
