﻿// <auto-generated />
using System;
using Anime_Archive_Handler_GUI.Database_Handeling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    [DbContext(typeof(AnimeContext))]
    [Migration("20240705143512_RelationChange34")]
    partial class RelationChange34
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("Anime_Archive_Handler_GUI.AnimeBroadcastDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Day")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("String")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Timezone")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnimeBroadcasts");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.AnimeDto", b =>
                {
                    b.Property<long?>("MalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Airing")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Approved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Background")
                        .HasColumnType("TEXT");

                    b.Property<int>("BroadcastId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Episodes")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Favorites")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ImagesId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("MalUrlDtoId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Members")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Popularity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rating")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<double?>("Score")
                        .HasColumnType("REAL");

                    b.Property<int?>("ScoredBy")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Season")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Synopsis")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleEnglish")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleJapanese")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("MalId");

                    b.HasIndex("BroadcastId");

                    b.HasIndex("ImagesId");

                    b.HasIndex("MalUrlDtoId");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.AnimeTrailerDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmbedUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("YoutubeId")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.ToTable("AnimeTrailers");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ImageDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("LargeImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("MaximumImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("MediumImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("SmallImageUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ImageDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ImagesSetDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("JPGId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WebPId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JPGId");

                    b.HasIndex("WebPId");

                    b.ToTable("ImagesSets");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.MalUrlDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MalUrls");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MalUrlDto");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TimePeriod", b =>
                {
                    b.Property<DateTime?>("From")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("To")
                        .HasColumnType("TEXT");

                    b.ToTable("TimePeriod");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TimePeriodDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("From")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId")
                        .IsUnique();

                    b.ToTable("TimePeriodDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TitleEntryDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("TitleEntries");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TitleFtsDto", b =>
                {
                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.ToTable("Titles_fts", (string)null);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TitleSynonymDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Synonym")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("TitleSynonyms");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.DemographicsDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("DemographicsDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ExplicitGenresDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("ExplicitGenresDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.GenresDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("GenresDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.LicensorsDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("LicensorsDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ProducersDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("ProducersDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.StudiosDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("StudiosDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ThemesDto", b =>
                {
                    b.HasBaseType("Anime_Archive_Handler_GUI.MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("ThemesDto");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.AnimeDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeBroadcastDto", "Broadcast")
                        .WithMany()
                        .HasForeignKey("BroadcastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Anime_Archive_Handler_GUI.ImagesSetDto", "Images")
                        .WithMany()
                        .HasForeignKey("ImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Anime_Archive_Handler_GUI.MalUrlDto", null)
                        .WithMany("Anime")
                        .HasForeignKey("MalUrlDtoId");

                    b.Navigation("Broadcast");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.AnimeTrailerDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", "Anime")
                        .WithOne("Trailer")
                        .HasForeignKey("Anime_Archive_Handler_GUI.AnimeTrailerDto", "AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Anime_Archive_Handler_GUI.ImageDto", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ImagesSetDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.ImageDto", "JPG")
                        .WithMany()
                        .HasForeignKey("JPGId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Anime_Archive_Handler_GUI.ImageDto", "WebP")
                        .WithMany()
                        .HasForeignKey("WebPId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("JPG");

                    b.Navigation("WebP");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TimePeriodDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", "Anime")
                        .WithOne("Aired")
                        .HasForeignKey("Anime_Archive_Handler_GUI.TimePeriodDto", "AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TitleEntryDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", "Anime")
                        .WithMany("Titles")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.TitleSynonymDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", "Anime")
                        .WithMany("TitleSynonyms")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.DemographicsDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("Demographics")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_MalUrls_Animes_AnimeId1");
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ExplicitGenresDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("ExplicitGenres")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.GenresDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("Genres")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.LicensorsDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("Licensors")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ProducersDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("Producers")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.StudiosDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("Studios")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.ThemesDto", b =>
                {
                    b.HasOne("Anime_Archive_Handler_GUI.AnimeDto", null)
                        .WithMany("Themes")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.AnimeDto", b =>
                {
                    b.Navigation("Aired")
                        .IsRequired();

                    b.Navigation("Demographics");

                    b.Navigation("ExplicitGenres");

                    b.Navigation("Genres");

                    b.Navigation("Licensors");

                    b.Navigation("Producers");

                    b.Navigation("Studios");

                    b.Navigation("Themes");

                    b.Navigation("TitleSynonyms");

                    b.Navigation("Titles");

                    b.Navigation("Trailer")
                        .IsRequired();
                });

            modelBuilder.Entity("Anime_Archive_Handler_GUI.MalUrlDto", b =>
                {
                    b.Navigation("Anime");
                });
#pragma warning restore 612, 618
        }
    }
}
