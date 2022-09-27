﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yun.Share.Voice.DataBase;

namespace Yun.Share.Voice.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    [Migration("20220927143803_addCarTypename")]
    partial class addCarTypename
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.CarType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Icon")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Subname")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.ErrorPracticeLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PracticeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PracticeId");

                    b.ToTable("ErrorPracticeLoges");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.Option", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PracticeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PracticeId");

                    b.ToTable("Optiones");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.Practice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CarTypeId")
                        .HasColumnType("char(36)");

                    b.Property<int>("ChoiceTyope")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Introduce")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("KeyWordls")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Skill")
                        .HasColumnType("longtext");

                    b.Property<string>("SkillLast")
                        .HasColumnType("longtext");

                    b.Property<int>("StatusTypeEnum")
                        .HasColumnType("int");

                    b.Property<Guid>("SubjectTypeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("SubjectTypeId");

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.PracticeImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PracticeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PracticeId");

                    b.ToTable("PracticeImages");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.SubjectType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SubjectTypes");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("CreatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("DeletedId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime");

                    b.Property<Guid?>("LastModifierId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StrTime")
                        .HasColumnType("datetime");

                    b.Property<int>("UserStatusTypeEnum")
                        .HasColumnType("int");

                    b.Property<int>("UserTypeEnum")
                        .HasColumnType("int");

                    b.Property<string>("WeChatId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.ErrorPracticeLog", b =>
                {
                    b.HasOne("Yun.Share.Voice.Models.Entities.Practice", "Practice")
                        .WithMany()
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practice");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.Option", b =>
                {
                    b.HasOne("Yun.Share.Voice.Models.Entities.Practice", "Practice")
                        .WithMany("Options")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practice");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.Practice", b =>
                {
                    b.HasOne("Yun.Share.Voice.Models.Entities.CarType", "CarType")
                        .WithMany()
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yun.Share.Voice.Models.Entities.SubjectType", "SubjectType")
                        .WithMany()
                        .HasForeignKey("SubjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarType");

                    b.Navigation("SubjectType");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.PracticeImage", b =>
                {
                    b.HasOne("Yun.Share.Voice.Models.Entities.Practice", "Practice")
                        .WithMany("PracticeImages")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practice");
                });

            modelBuilder.Entity("Yun.Share.Voice.Models.Entities.Practice", b =>
                {
                    b.Navigation("Options");

                    b.Navigation("PracticeImages");
                });
#pragma warning restore 612, 618
        }
    }
}
