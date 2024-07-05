using System;
using Anime_Archive_Handler_GUI;
using Microsoft.EntityFrameworkCore;

public class AnimeContext : DbContext
{
    public DbSet<AnimeDto> Animes { get; set; }
    public DbSet<MalUrlDto> MalUrls { get; set; }
    public DbSet<AnimeTrailerDto> AnimeTrailers { get; set; }
    public DbSet<ImagesSetDto> ImagesSets { get; set; }
    public DbSet<AnimeBroadcastDto> AnimeBroadcasts { get; set; }
    public DbSet<TitleEntryDto> TitleEntries { get; set; }
    public DbSet<TitleSynonymDto> TitleSynonyms { get; set; }
    public DbSet<ProducersDto> Producers { get; set; }
    public DbSet<LicensorsDto> Licensors { get; set; }
    public DbSet<StudiosDto> Studios { get; set; }
    public DbSet<GenresDto> Genres { get; set; }
    public DbSet<ExplicitGenresDto> ExplicitGenres { get; set; }
    public DbSet<ThemesDto> Themes { get; set; }
    public DbSet<DemographicsDto> Demographics { get; set; }
    
    public DbSet<TitleFtsDto> TitlesFts { get; set; }
    
    private readonly string _dbPath;

    public AnimeContext()
    {
        //_dbPath = FileHandler.GetFileInProgramFolder("SQLiteTest.db");
        _dbPath = "F:\\Rider Projects\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Anime Archive Handler GUI\\Databases\\SQLiteTest.db"; // Absolute path to the database file
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        optionsBuilder.EnableSensitiveDataLogging(); // Enable sensitive data logging
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.AddInterceptors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Producers)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Licensors)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Studios)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Genres)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.ExplicitGenres)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Themes)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Demographics)
            .WithOne()
            .HasForeignKey(a => a.AnimeId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Aired)
            .WithOne(a => a.Anime)
            .HasForeignKey<TimePeriodDto>(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Trailer)
            .WithOne(a => a.Anime)
            .HasForeignKey<AnimeTrailerDto>(a => a.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.TitleSynonyms)
            .WithOne(ts => ts.Anime)
            .HasForeignKey(ts => ts.AnimeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ImagesSetDto>()
            .HasOne(isd => isd.JPG)
            .WithMany()
            .HasForeignKey(isd => isd.JPGId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ImagesSetDto>()
            .HasOne(isd => isd.WebP)
            .WithMany()
            .HasForeignKey(isd => isd.WebPId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Images)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TimePeriod>().HasNoKey();
        modelBuilder.Entity<TitleFtsDto>().HasNoKey().ToTable("Titles_fts");
    }
}
