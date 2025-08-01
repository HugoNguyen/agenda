﻿// <auto-generated />
using System;
using BookingGuru.Modules.Mocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingGuru.Modules.Mocks.Infrastructure.Database.Migrations
{
    [DbContext(typeof(MocksDbContext))]
    [Migration("20250721071723_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("mocks")
                .HasAnnotation("ProductVersion", "8.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingGuru.Common.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("occurredOnUtc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("processedOnUtc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pK_InboxMessage");

                    b.ToTable("InboxMessage", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Common.Infrastructure.Inbox.InboxMessageConsumer", b =>
                {
                    b.Property<Guid>("InboxMessageId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("inboxMessageId");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("name");

                    b.HasKey("InboxMessageId", "Name")
                        .HasName("pK_InboxMessageConsumer");

                    b.ToTable("InboxMessageConsumer", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Common.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("occurredOnUtc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnName("processedOnUtc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pK_OutboxMessage");

                    b.ToTable("OutboxMessage", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Common.Infrastructure.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("OutboxMessageId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("outboxMessageId");

                    b.Property<string>("Name")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("name");

                    b.HasKey("OutboxMessageId", "Name")
                        .HasName("pK_OutboxMessageConsumer");

                    b.ToTable("OutboxMessageConsumer", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Modules.Mocks.Domain.FirstFeats.FirstEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Field1")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("field1");

                    b.Property<string>("Field1Nullable")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("field1Nullable");

                    b.Property<DateTimeOffset?>("Field2Utc")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("field2Utc");

                    b.HasKey("Id")
                        .HasName("pK_FirstEntity");

                    b.ToTable("FirstEntity", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Modules.Mocks.Domain.PublishClones.PublishClone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.Property<DateTimeOffset>("PublishDateUtc")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("publishDateUtc");

                    b.HasKey("Id")
                        .HasName("pK_PublishClone");

                    b.ToTable("PublishClone", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Modules.Mocks.Domain.SecondFeats.SecondEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("creationTime");

                    b.Property<Guid?>("CreatorUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("creatorUserId");

                    b.Property<Guid?>("DeleterUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("deleterUserId");

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("deletionTime");

                    b.Property<string>("Field1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("field1");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.Property<DateTimeOffset?>("LastModificationTime")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("lastModificationTime");

                    b.Property<Guid?>("LastModifierUserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("lastModifierUserId");

                    b.HasKey("Id")
                        .HasName("pK_SecondEntity");

                    b.HasIndex("CreatorUserId")
                        .HasDatabaseName("iX_SecondEntity_creatorUserId");

                    b.HasIndex("DeleterUserId")
                        .HasDatabaseName("iX_SecondEntity_deleterUserId");

                    b.HasIndex("LastModifierUserId")
                        .HasDatabaseName("iX_SecondEntity_lastModifierUserId");

                    b.ToTable("SecondEntity", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Modules.Mocks.Domain.SecondFeats.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("userName");

                    b.HasKey("Id")
                        .HasName("pK_User");

                    b.ToTable("User", "mocks");
                });

            modelBuilder.Entity("BookingGuru.Modules.Mocks.Domain.SecondFeats.SecondEntity", b =>
                {
                    b.HasOne("BookingGuru.Modules.Mocks.Domain.SecondFeats.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .HasConstraintName("fK_SecondEntity_User_creatorUserId");

                    b.HasOne("BookingGuru.Modules.Mocks.Domain.SecondFeats.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId")
                        .HasConstraintName("fK_SecondEntity_User_deleterUserId");

                    b.HasOne("BookingGuru.Modules.Mocks.Domain.SecondFeats.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId")
                        .HasConstraintName("fK_SecondEntity_User_lastModifierUserId");

                    b.Navigation("CreatorUser");

                    b.Navigation("DeleterUser");

                    b.Navigation("LastModifierUser");
                });
#pragma warning restore 612, 618
        }
    }
}
