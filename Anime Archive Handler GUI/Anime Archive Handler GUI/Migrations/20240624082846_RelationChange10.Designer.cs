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
    [Migration("20240624082846_RelationChange10")]
    partial class RelationChange10
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

                    b.Property<long?>("MalUrlDtoId")
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

                    b.HasIndex("MalUrlDtoId");

                    b.ToTable("Animes");
                });

            modelBuilder.Entity("AnimeTrailerDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmbedUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("YoutubeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId")
                        .IsUnique();

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

                    b.Property<string>("LargeImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MaximumImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MediumImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SmallImageUrl")
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
                    b.Property<long>("Id")
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

                    b.HasKey("Id");

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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("AnimeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

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

                    b.Property<long?>("ExplicitGenreAnimeId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ExplicitGenreAnimeId");

                    b.ToTable("MalUrls", t =>
                        {
                            t.Property("ExplicitGenreAnimeId")
                                .HasColumnName("DemographicsDto_ExplicitGenreAnimeId");
                        });

                    b.HasDiscriminator().HasValue("DemographicsDto");
                });

            modelBuilder.Entity("ExplicitGenresDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.Property<long?>("ExplicitGenreAnimeId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ExplicitGenreAnimeId");

                    b.HasDiscriminator().HasValue("ExplicitGenresDto");
                });

            modelBuilder.Entity("GenresDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.Property<long?>("LicensorAnimeId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("LicensorAnimeId");

                    b.HasDiscriminator().HasValue("GenresDto");
                });

            modelBuilder.Entity("LicensorsDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("LicensorsDto");
                });

            modelBuilder.Entity("ProducersDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.HasIndex("AnimeId");

                    b.HasDiscriminator().HasValue("ProducersDto");
                });

            modelBuilder.Entity("StudiosDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.Property<long?>("ProducerAnimeId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ProducerAnimeId");

                    b.HasDiscriminator().HasValue("StudiosDto");
                });

            modelBuilder.Entity("ThemesDto", b =>
                {
                    b.HasBaseType("MalUrlDto");

                    b.Property<long?>("ThemeAnimeId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ThemeAnimeId");

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
                        .HasForeignKey("MalUrlDtoId");

                    b.Navigation("Broadcast");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("AnimeTrailerDto", b =>
                {
                    b.HasOne("AnimeDto", "Anime")
                        .WithOne("Trailer")
                        .HasForeignKey("AnimeTrailerDto", "AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImageDto", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

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

            modelBuilder.Entity("DemographicsDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("Demographics")
                        .HasForeignKey("ExplicitGenreAnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExplicitGenresDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("ExplicitGenres")
                        .HasForeignKey("ExplicitGenreAnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GenresDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("Genres")
                        .HasForeignKey("LicensorAnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LicensorsDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("Licensors")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProducersDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("Producers")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudiosDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("Studios")
                        .HasForeignKey("ProducerAnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ThemesDto", b =>
                {
                    b.HasOne("AnimeDto", null)
                        .WithMany("Themes")
                        .HasForeignKey("ThemeAnimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnimeDto", b =>
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

            modelBuilder.Entity("MalUrlDto", b =>
                {
                    b.Navigation("Anime");
                });
#pragma warning restore 612, 618
        }
    }
}
