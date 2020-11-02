﻿// <auto-generated />
using System;
using HelperDesk.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HelperDesk.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201024084055_AddedFileTable")]
    partial class AddedFileTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("HelperDesk.API.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("description");

                    b.Property<int>("status");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<int>("CreatedByUserId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("PositionId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UpdatedByUserId");

                    b.Property<string>("file");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<int>("CreatedByUserId");

                    b.Property<string>("ForwardEmail");

                    b.Property<int>("ManagamentId");

                    b.Property<string>("Message");

                    b.Property<int>("Status");

                    b.Property<string>("Subject");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ManagamentId");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("HelperDesk.API.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<int?>("ManagementId");

                    b.Property<int?>("PositionId");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<bool>("isMain");

                    b.HasKey("Id");

                    b.HasIndex("ManagementId");

                    b.HasIndex("PositionId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("description");

                    b.Property<int>("status");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Management", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignedUserId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<int>("CreatedByUserId");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Description");

                    b.Property<string>("File");

                    b.Property<string>("Response");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UpdatedByUserId");

                    b.Property<int>("UserCreatedId");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Management");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<int>("CreatedByUserId");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Dimension");

                    b.Property<string>("Education");

                    b.Property<DateTime>("EndDatePerfomance");

                    b.Property<DateTime>("EndDateSkills");

                    b.Property<string>("Experience");

                    b.Property<string>("Name");

                    b.Property<string>("OrganizationalPosition");

                    b.Property<string>("OtherSkills");

                    b.Property<string>("Scope");

                    b.Property<string>("Skills");

                    b.Property<DateTime>("StartDatePerfomance");

                    b.Property<DateTime>("StartDateSkills");

                    b.Property<int>("Status");

                    b.Property<string>("Target");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UpdatedByUserId");

                    b.Property<int>("UserReportedId");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("RoleDescription");

                    b.Property<bool>("add");

                    b.Property<bool>("delete");

                    b.Property<bool>("edit");

                    b.Property<bool>("status");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Sessions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Active");

                    b.Property<int>("Attempts");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Information");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AssignationDate");

                    b.Property<int?>("AssignedUserId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("RequestingUserId");

                    b.Property<int>("TicketCategoryId");

                    b.Property<int>("TicketStatusId");

                    b.Property<int>("TicketTypeId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int?>("UserAssingId");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("RequestingUserId");

                    b.HasIndex("TicketCategoryId");

                    b.HasIndex("TicketStatusId");

                    b.HasIndex("TicketTypeId");

                    b.HasIndex("UserAssingId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("HelperDesk.API.Models.TicketCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("TicketCategories");
                });

            modelBuilder.Entity("HelperDesk.API.Models.TicketDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Archive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("TicketId");

                    b.Property<int>("TracingStatusId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("TracingStatusId");

                    b.ToTable("TicketDetails");
                });

            modelBuilder.Entity("HelperDesk.API.Models.TicketStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("TicketStatus");
                });

            modelBuilder.Entity("HelperDesk.API.Models.TicketType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("HelperDesk.API.Models.TracingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Description");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("TracingStatus");
                });

            modelBuilder.Entity("HelperDesk.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Authy_id");

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Email");

                    b.Property<int>("GenderId");

                    b.Property<string>("LastName");

                    b.Property<string>("Names");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Phone");

                    b.Property<int>("PositionId");

                    b.Property<int>("RoleId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Username");

                    b.Property<int>("status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("GenderId");

                    b.HasIndex("PositionId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Document", b =>
                {
                    b.HasOne("HelperDesk.API.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelperDesk.API.Models.Email", b =>
                {
                    b.HasOne("HelperDesk.API.Models.Management", "Managament")
                        .WithMany()
                        .HasForeignKey("ManagamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelperDesk.API.Models.File", b =>
                {
                    b.HasOne("HelperDesk.API.Models.Management", "Management")
                        .WithMany()
                        .HasForeignKey("ManagementId");

                    b.HasOne("HelperDesk.API.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId");
                });

            modelBuilder.Entity("HelperDesk.API.Models.Management", b =>
                {
                    b.HasOne("HelperDesk.API.Models.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.User", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelperDesk.API.Models.Position", b =>
                {
                    b.HasOne("HelperDesk.API.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelperDesk.API.Models.Sessions", b =>
                {
                    b.HasOne("HelperDesk.API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelperDesk.API.Models.Ticket", b =>
                {
                    b.HasOne("HelperDesk.API.Models.User", "AssignedUser")
                        .WithMany()
                        .HasForeignKey("AssignedUserId");

                    b.HasOne("HelperDesk.API.Models.User", "RequestingUser")
                        .WithMany()
                        .HasForeignKey("RequestingUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.TicketCategory", "TicketCategory")
                        .WithMany()
                        .HasForeignKey("TicketCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.TicketStatus", "TicketStatus")
                        .WithMany()
                        .HasForeignKey("TicketStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.TicketType", "TicketType")
                        .WithMany()
                        .HasForeignKey("TicketTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.User", "UserAssing")
                        .WithMany()
                        .HasForeignKey("UserAssingId");
                });

            modelBuilder.Entity("HelperDesk.API.Models.TicketDetail", b =>
                {
                    b.HasOne("HelperDesk.API.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.TracingStatus", "TracingStatus")
                        .WithMany()
                        .HasForeignKey("TracingStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelperDesk.API.Models.User", b =>
                {
                    b.HasOne("HelperDesk.API.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_Users_Companies_CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.Gender", "Gender")
                        .WithMany("Users")
                        .HasForeignKey("GenderId")
                        .HasConstraintName("FK_Users_Genders_GenderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.Position", "Position")
                        .WithOne("User")
                        .HasForeignKey("HelperDesk.API.Models.User", "PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelperDesk.API.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Users_Roles_RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
