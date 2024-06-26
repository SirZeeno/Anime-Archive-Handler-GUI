﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Anime_Archive_Handler_GUI.Migrations
{
    [DbContext(typeof(AnimeContext))]
    [Migration("20240624044817_RelationChange6")]
    partial class RelationChange6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("AnimeBroadcastDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("String")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnimeBroadcasts");
                });

            modelBuilder.Entity("AnimeDto", b =>
                {
                    b.Property<long?>("MalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Airing")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Approved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("BroadcastId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Episodes")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Favorites")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ImagesId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("MalUrlDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Members")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Popularity")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double?>("Score")
                        .HasColumnType("REAL");

                    b.Property<int?>("ScoredBy")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Season")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Synopsis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleEnglish")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleJapanese")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TrailerYoutubeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("MalId");

                    b.HasIndex("BroadcastId");

                    b.HasIndex("ImagesId");

                    b.HasIndex("MalUrlDtoMalId");

                    b.HasIndex("TrailerYoutubeId");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("AnimeDtoDemographicsDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("DemographicsMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "DemographicsMalId");

                    b.HasIndex("DemographicsMalId");

                    b.ToTable("AnimeDtoDemographicsDto");
                });

            modelBuilder.Entity("AnimeDtoExplicitGenresDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ExplicitGenresMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "ExplicitGenresMalId");

                    b.HasIndex("ExplicitGenresMalId");

                    b.ToTable("AnimeDtoExplicitGenresDto");
                });

            modelBuilder.Entity("AnimeDtoGenresDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("GenresMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "GenresMalId");

                    b.HasIndex("GenresMalId");

                    b.ToTable("AnimeDtoGenresDto");
                });

            modelBuilder.Entity("AnimeDtoLicensorsDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("LicensorsMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "LicensorsMalId");

                    b.HasIndex("LicensorsMalId");

                    b.ToTable("AnimeDtoLicensorsDto");
                });

            modelBuilder.Entity("AnimeDtoProducersDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ProducersMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "ProducersMalId");

                    b.HasIndex("ProducersMalId");

                    b.ToTable("AnimeDtoProducersDto");
                });

            modelBuilder.Entity("AnimeDtoStudiosDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("StudiosMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "StudiosMalId");

                    b.HasIndex("StudiosMalId");

                    b.ToTable("AnimeDtoStudiosDto");
                });

            modelBuilder.Entity("AnimeDtoThemesDto", b =>
                {
                    b.Property<long>("AnimeDtoMalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ThemesMalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AnimeDtoMalId", "ThemesMalId");

                    b.HasIndex("ThemesMalId");

                    b.ToTable("AnimeDtoThemesDto");
                });

            modelBuilder.Entity("AnimeTrailerDto", b =>
                {
                    b.Property<string>("YoutubeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmbedUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("YoutubeId");

                    b.HasIndex("ImageId");

                    b.ToTable("AnimeTrailers");
                });

            modelBuilder.Entity("ImageDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ImageDto");
                });

            modelBuilder.Entity("ImagesSetDto", b =>
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

            modelBuilder.Entity("MalUrlDto", b =>
                {
                    b.Property<long>("MalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MalId");

                    b.ToTable("MalUrls");

                    b.HasDiscriminator<string>("Discriminator").HasValue("MalUrlDto");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TimePeriod", b =>
                {
                    b.Property<DateTime?>("From")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("To")
                        .HasColumnType("TEXT");

                    b.ToTable("TimePeriod");
                });

            modelBuilder.Entity("TimePeriodDto", b =>
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

            modelBuilder.Entity("TitleEntryDto", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Type");

                    b.HasIndex("AnimeId");

                    b.ToTable("TitleEntries");
                });

            modelBuilder.Entity("TitleSynonymDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Synonym")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.ToTable("TitleSynonyms");
                });

            modelBuilder.Entity("DemographicsDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("DemographicsDto");
                });

            modelBuilder.Entity("ExplicitGenresDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("ExplicitGenresDto");
                });

            modelBuilder.Entity("GenresDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("GenresDto");
                });

            modelBuilder.Entity("LicensorsDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("LicensorsDto");
                });

            modelBuilder.Entity("ProducersDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("ProducersDto");
                });

            modelBuilder.Entity("StudiosDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("StudiosDto");
                });

            modelBuilder.Entity("ThemesDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasDiscriminator().HasValue("ThemesDto");
                });

            modelBuilder.Entity("AnimeDto", b =>
                {
                    b.HasOne("AnimeBroadcastDto", "Broadcast")
                        .WithMany()
                        .HasForeignKey("BroadcastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImagesSetDto", "Images")
                        .WithMany()
                        .HasForeignKey("ImagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MalUrlDto", null)
                        .WithMany("Anime")
                        .HasForeignKey("MalUrlDtoMalId");

                    b.HasOne("AnimeTrailerDto", "Trailer")
                        .WithMany()
                        .HasForeignKey("TrailerYoutubeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Broadcast");

                    b.Navigation("Images");

                    b.Navigation("Trailer");
                });

            modelBuilder.Entity("AnimeDtoDemographicsDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DemographicsDto", null)
                        .WithMany()
                        .HasForeignKey("DemographicsMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDtoExplicitGenresDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExplicitGenresDto", null)
                        .WithMany()
                        .HasForeignKey("ExplicitGenresMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDtoGenresDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GenresDto", null)
                        .WithMany()
                        .HasForeignKey("GenresMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDtoLicensorsDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LicensorsDto", null)
                        .WithMany()
                        .HasForeignKey("LicensorsMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDtoProducersDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProducersDto", null)
                        .WithMany()
                        .HasForeignKey("ProducersMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDtoStudiosDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudiosDto", null)
                        .WithMany()
                        .HasForeignKey("StudiosMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeDtoThemesDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany()
                        .HasForeignKey("AnimeDtoMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThemesDto", null)
                        .WithMany()
                        .HasForeignKey("ThemesMalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeTrailerDto", b =>
                {
                    b.HasOne("ImageDto", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("ImagesSetDto", b =>
                {
                    b.HasOne("ImageDto", "JPG")
                        .WithMany()
                        .HasForeignKey("JPGId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImageDto", "WebP")
                        .WithMany()
                        .HasForeignKey("WebPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JPG");

                    b.Navigation("WebP");
                });

            modelBuilder.Entity("TimePeriodDto", b =>
                {
                    b.HasOne("AnimeDto", "Anime")
                        .WithOne("Aired")
                        .HasForeignKey("TimePeriodDto", "AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("TitleEntryDto", b =>
                {
                    b.HasOne("AnimeDto", "Anime")
                        .WithMany("Titles")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("TitleSynonymDto", b =>
                {
                    b.HasOne("AnimeDto", "Anime")
                        .WithMany("TitleSynonyms")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");
                });

            modelBuilder.Entity("AnimeDto", b =>
                {
                    b.Navigation("Aired")
                        .IsRequired();

                    b.Navigation("TitleSynonyms");

                    b.Navigation("Titles");
                });

            modelBuilder.Entity("MalUrlDto", b =>
                {
                    b.Navigation("Anime");
                });
#pragma warning restore 612, 618
        }
    }
}
