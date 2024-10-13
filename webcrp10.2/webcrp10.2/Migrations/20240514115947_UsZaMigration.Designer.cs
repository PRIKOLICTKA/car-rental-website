﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webcrp10._2.Services;

#nullable disable

namespace webcrp10._2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240514115947_UsZaMigration")]
    partial class UsZaMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webcrp10._2.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("webcrp10._2.Model.Zayavki", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_avto")
                        .HasColumnType("int");

                    b.Property<int>("ID_user")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Время_Возврата")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("Время_Вывоза")
                        .HasColumnType("time");

                    b.Property<DateOnly>("Дата_Конца_Аренды")
                        .HasColumnType("date");

                    b.Property<DateOnly>("Дата_Начала_Аренды")
                        .HasColumnType("date");

                    b.Property<string>("Имя")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Отчество")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Получение_авто")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Сдача_авто")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Телефон")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Фамилия")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ZAYAVKI", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
