using Anime_Archive_Handler_GUI;
using Microsoft.EntityFrameworkCore;

public class AnimeContext : DbContext
{
    public DbSet<AnimeDto> Animes { get; set; }

    private readonly string _dbPath;

    public AnimeContext(string dbPath)
    {
        _dbPath = dbPath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimeDto>().HasKey(a => a.MalId); // Configure primary key
        
        modelBuilder.Entity<AnimeBroadcastDto>().HasKey(b => b.Id); // Primary key for AnimeBroadcast
        modelBuilder.Entity<AnimeTrailerDto>().HasKey(b => b.Id); // Primary key for AnimeTrailer
        modelBuilder.Entity<ImageDto>().HasKey(b => b.Id); // Primary key for Image
        modelBuilder.Entity<MalUrlDto>().HasKey(b => b.Id); // Primary key for Image
        modelBuilder.Entity<TitleEntryDto>().HasKey(b => b.Id); // Primary key for Image
        modelBuilder.Entity<ImagesSetDto>().HasKey(b => b.Id); // Primary key for Image
        modelBuilder.Entity<TimePeriodDto>().HasKey(b => b.Id); // Primary key for Image

        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Broadcast)
            .WithOne(b => b.Anime)
            .HasForeignKey<AnimeBroadcastDto>(b => b.AnimeDtoId);

        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Trailer)
            .WithOne(t => t.Anime)
            .HasForeignKey<AnimeTrailerDto>(t => t.AnimeDtoId);

        modelBuilder.Entity<AnimeTrailerDto>()
            .HasOne(t => t.Image)
            .WithOne(i => i.AnimeTrailer)
            .HasForeignKey<ImageDto>(i => i.ParentId);

        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Images)
            .WithOne(i => i.Anime)
            .HasForeignKey<ImagesSetDto>(i => i.AnimeDtoId);

        modelBuilder.Entity<AnimeDto>()
            .HasOne(a => a.Aired)
            .WithOne(tp => tp.Anime)
            .HasForeignKey<TimePeriodDto>(tp => tp.AnimeDtoId);

        // Configure AnimeProducer relationship
        modelBuilder.Entity<AnimeProducer>()
            .HasKey(ap => new { ap.AnimeDtoId, ap.MalUrlId });
        modelBuilder.Entity<AnimeProducer>()
            .HasOne(ap => ap.Anime)
            .WithMany(a => a.Producers)
            .HasForeignKey(ap => ap.AnimeDtoId);
        modelBuilder.Entity<AnimeProducer>()
            .HasOne(ap => ap.Producer)
            .WithMany()
            .HasForeignKey(ap => ap.MalUrlId);

        // Configure AnimeLicensor relationship
        modelBuilder.Entity<AnimeLicensor>()
            .HasKey(al => new { al.AnimeDtoId, al.MalUrlId });
        modelBuilder.Entity<AnimeLicensor>()
            .HasOne(al => al.Anime)
            .WithMany(a => a.Licensors)
            .HasForeignKey(al => al.AnimeDtoId);
        modelBuilder.Entity<AnimeLicensor>()
            .HasOne(al => al.Licensor)
            .WithMany()
            .HasForeignKey(al => al.MalUrlId);

        // Configure AnimeStudio relationship
        modelBuilder.Entity<AnimeStudio>()
            .HasKey(ans => new { ans.AnimeDtoId, ans.MalUrlId });
        modelBuilder.Entity<AnimeStudio>()
            .HasOne(ans => ans.Anime)
            .WithMany(a => a.Studios)
            .HasForeignKey(ans => ans.AnimeDtoId);
        modelBuilder.Entity<AnimeStudio>()
            .HasOne(ans => ans.Studio)
            .WithMany()
            .HasForeignKey(ans => ans.MalUrlId);

        // Configure AnimeGenre relationship
        modelBuilder.Entity<AnimeGenre>()
            .HasKey(ag => new { ag.AnimeDtoId, ag.MalUrlId });
        modelBuilder.Entity<AnimeGenre>()
            .HasOne(ag => ag.Anime)
            .WithMany(a => a.Genres)
            .HasForeignKey(ag => ag.AnimeDtoId);
        modelBuilder.Entity<AnimeGenre>()
            .HasOne(ag => ag.Genre)
            .WithMany()
            .HasForeignKey(ag => ag.MalUrlId);

        // Configure AnimeExplicitGenre relationship
        modelBuilder.Entity<AnimeExplicitGenre>()
            .HasKey(aeg => new { aeg.AnimeDtoId, aeg.MalUrlId });
        modelBuilder.Entity<AnimeExplicitGenre>()
            .HasOne(aeg => aeg.Anime)
            .WithMany(a => a.ExplicitGenres)
            .HasForeignKey(aeg => aeg.AnimeDtoId);
        modelBuilder.Entity<AnimeExplicitGenre>()
            .HasOne(aeg => aeg.ExplicitGenre)
            .WithMany()
            .HasForeignKey(aeg => aeg.MalUrlId);

        // Configure AnimeTheme relationship
        modelBuilder.Entity<AnimeTheme>()
            .HasKey(at => new { at.AnimeDtoId, at.MalUrlId });
        modelBuilder.Entity<AnimeTheme>()
            .HasOne(at => at.Anime)
            .WithMany(a => a.Themes)
            .HasForeignKey(at => at.AnimeDtoId);
        modelBuilder.Entity<AnimeTheme>()
            .HasOne(at => at.Theme)
            .WithMany()
            .HasForeignKey(at => at.MalUrlId);

        // Configure AnimeDemographic relationship
        modelBuilder.Entity<AnimeDemographic>()
            .HasKey(ad => new { ad.AnimeDtoId, ad.MalUrlId });
        modelBuilder.Entity<AnimeDemographic>()
            .HasOne(ad => ad.Anime)
            .WithMany(a => a.Demographics)
            .HasForeignKey(ad => ad.AnimeDtoId);
        modelBuilder.Entity<AnimeDemographic>()
            .HasOne(ad => ad.Demographic)
            .WithMany()
            .HasForeignKey(ad => ad.MalUrlId);

        modelBuilder.Entity<AnimeDto>()
            .HasMany(a => a.Titles)
            .WithOne(t => t.Anime)
            .HasForeignKey(t => t.AnimeDtoId);

        modelBuilder.Entity<AnimeBroadcastDto>()
            .Property(b => b.AnimeDtoId)
            .IsRequired();

        modelBuilder.Entity<AnimeTrailerDto>()
            .Property(t => t.AnimeDtoId)
            .IsRequired();

        modelBuilder.Entity<ImageDto>()
            .Property(i => i.ParentId)
            .IsRequired();

        modelBuilder.Entity<ImageDto>()
            .Property(i => i.ImagesSetJpgId)
            .IsRequired(false);

        modelBuilder.Entity<ImageDto>()
            .Property(i => i.ImagesSetWebPId)
            .IsRequired(false);

        modelBuilder.Entity<ImagesSetDto>()
            .Property(ims => ims.AnimeDtoId)
            .IsRequired();

        modelBuilder.Entity<ImagesSetDto>()
            .HasOne(ims => ims.Jpg)
            .WithOne(i => i.ImagesSetJpg)
            .HasForeignKey<ImagesSetDto>(ims => ims.JpgId);

        modelBuilder.Entity<ImagesSetDto>()
            .HasOne(ims => ims.WebP)
            .WithOne(i => i.ImagesSetWebP)
            .HasForeignKey<ImagesSetDto>(ims => ims.WebPId);

        modelBuilder.Entity<TimePeriodDto>()
            .Property(tp => tp.AnimeDtoId)
            .IsRequired();
    }
}
