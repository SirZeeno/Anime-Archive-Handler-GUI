using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Anime_Archive_Handler_GUI.Tests;

public class DatabasePorter
{
    private readonly string _liteDbPath;
    private readonly string _sqliteDbPath;

    public DatabasePorter(string liteDbPath, string sqliteDbPath)
    {
        _liteDbPath = liteDbPath;
        _sqliteDbPath = sqliteDbPath;
    }

    public void PortLiteDbToSqlite()
    {
        using var sqliteContext = new AnimeContext();
        sqliteContext.Database.EnsureCreated();

        using var liteDb = new LiteDatabase($"Filename={_liteDbPath};Connection=shared");
        var liteCollection = liteDb.GetCollection<AnimeDto>("Anime");

        var animeDtos = liteCollection.FindAll().ToList();

        foreach (var anime in animeDtos)
        {
            TrackAndSaveEntities(sqliteContext, anime);
        }

        sqliteContext.SaveChanges();

        foreach (var anime in animeDtos)
        {
            SetForeignKeys(anime);
            var existingAnime = sqliteContext.Animes.SingleOrDefault(a => a.MalId == anime.MalId);
            if (existingAnime == null)
            {
                sqliteContext.Animes.Add(anime);
            }
        }

        sqliteContext.SaveChanges();
        Console.WriteLine($"Successfully ported {animeDtos.Count} anime entries from LiteDB to SQLite.");
    }

    private void TrackAndSaveEntities(AnimeContext context, AnimeDto anime)
    {
        if (anime.Producers != null)
        {
            foreach (var producer in anime.Producers)
            {
                TrackAndSaveMalUrlEntity(context, producer.Producer);
            }
        }

        if (anime.Licensors != null)
        {
            foreach (var licensor in anime.Licensors)
            {
                TrackAndSaveMalUrlEntity(context, licensor.Licensor);
            }
        }

        if (anime.Studios != null)
        {
            foreach (var studio in anime.Studios)
            {
                TrackAndSaveMalUrlEntity(context, studio.Studio);
            }
        }

        if (anime.Genres != null)
        {
            foreach (var genre in anime.Genres)
            {
                TrackAndSaveMalUrlEntity(context, genre.Genre);
            }
        }

        if (anime.ExplicitGenres != null)
        {
            foreach (var explicitGenre in anime.ExplicitGenres)
            {
                TrackAndSaveMalUrlEntity(context, explicitGenre.ExplicitGenre);
            }
        }

        if (anime.Themes != null)
        {
            foreach (var theme in anime.Themes)
            {
                TrackAndSaveMalUrlEntity(context, theme.Theme);
            }
        }

        if (anime.Demographics != null)
        {
            foreach (var demographic in anime.Demographics)
            {
                TrackAndSaveMalUrlEntity(context, demographic.Demographic);
            }
        }

        TrackAndSaveImageEntities(context, anime.Images?.JPG);
        TrackAndSaveImageEntities(context, anime.Images?.WebP);
        TrackAndSaveImageSetEntities(context, anime.Images);
        TrackAndSaveImageEntities(context, anime.Trailer?.Image);
        TrackAndSaveTrailerEntities(context, anime.Trailer);
        TrackAndSaveTimePeriodEntities(context, anime.Aired);
        TrackAndSaveBroadcastEntities(context, anime.Broadcast);
        TrackAndSaveTitleEntities(context, anime.Titles);

        // Log the state of the context before saving
        LogContextState(context);

        try
        {
            context.SaveChanges(); // Save related entities immediately
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error saving changes: {ex.InnerException?.Message}");
            throw;
        }
    }

    private void TrackAndSaveMalUrlEntity(AnimeContext context, MalUrlDto entity)
    {
        if (entity != null)
        {
            var existingEntity = context.MalUrls.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.MalUrls.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.MalUrls.Update(entity);
            }
        }
    }

    private void TrackAndSaveImageEntities(AnimeContext context, ImageDto entity)
    {
        if (entity != null)
        {
            var existingEntity = context.Images.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.Images.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.Images.Update(entity);
            }
        }
    }

    private void TrackAndSaveImageSetEntities(AnimeContext context, ImagesSetDto entity)
    {
        if (entity != null)
        {
            var existingEntity = context.ImageSets.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.ImageSets.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.ImageSets.Update(entity);
            }
        }
    }

    private void TrackAndSaveTrailerEntities(AnimeContext context, AnimeTrailerDto entity)
    {
        if (entity != null)
        {
            var existingEntity = context.Trailers.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.Trailers.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.Trailers.Update(entity);
            }
        }
    }

    private void TrackAndSaveTimePeriodEntities(AnimeContext context, TimePeriodDto entity)
    {
        if (entity != null)
        {
            var existingEntity = context.TimePeriods.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.TimePeriods.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.TimePeriods.Update(entity);
            }
        }
    }

    private void TrackAndSaveBroadcastEntities(AnimeContext context, AnimeBroadcastDto entity)
    {
        if (entity != null)
        {
            var existingEntity = context.Broadcasts.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.Broadcasts.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.Broadcasts.Update(entity);
            }
        }
    }

    private void TrackAndSaveTitleEntities(AnimeContext context, ICollection<TitleEntryDto> entities)
    {
        if (entities == null) return;

        foreach (var entity in entities)
        {
            var existingEntity = context.Titles.SingleOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                context.Titles.Add(entity);
            }
            else
            {
                context.Entry(existingEntity).State = EntityState.Detached;
                context.Titles.Update(entity);
            }
        }
    }

    private void SetForeignKeys(AnimeDto anime)
    {
        if (anime.Producers != null)
        {
            foreach (var producer in anime.Producers)
            {
                producer.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.Licensors != null)
        {
            foreach (var licensor in anime.Licensors)
            {
                licensor.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.Studios != null)
        {
            foreach (var studio in anime.Studios)
            {
                studio.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.Genres != null)
        {
            foreach (var genre in anime.Genres)
            {
                genre.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.ExplicitGenres != null)
        {
            foreach (var explicitGenre in anime.ExplicitGenres)
            {
                explicitGenre.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.Themes != null)
        {
            foreach (var theme in anime.Themes)
            {
                theme.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.Demographics != null)
        {
            foreach (var demographic in anime.Demographics)
            {
                demographic.AnimeDtoId = anime.MalId;
            }
        }

        if (anime.Images != null)
        {
            anime.Images.AnimeDtoId = anime.MalId;
            if (anime.Images.JPG != null)
            {
                anime.Images.JPG.ImagesSetJpgId = anime.Images.Id;
            }
            if (anime.Images.WebP != null)
            {
                anime.Images.WebP.ImagesSetWebPId = anime.Images.Id;
            }
        }

        if (anime.Trailer != null)
        {
            anime.Trailer.AnimeDtoId = anime.MalId;
            if (anime.Trailer.Image != null)
            {
                anime.Trailer.Image.ParentId = anime.Trailer.Id;
            }
        }

        if (anime.Aired != null)
        {
            anime.Aired.AnimeDtoId = anime.MalId;
        }

        if (anime.Broadcast != null)
        {
            anime.Broadcast.AnimeDtoId = anime.MalId;
        }

        if (anime.Titles != null)
        {
            foreach (var title in anime.Titles)
            {
                title.AnimeDtoId = anime.MalId;
            }
        }
    }

    private void LogContextState(AnimeContext context)
    {
        var entities = context.ChangeTracker.Entries().ToList();
        foreach (var entity in entities)
        {
            Console.WriteLine($"Entity: {entity.Entity.GetType().Name}, State: {entity.State}, ID: {entity.Property("Id").CurrentValue}");
        }
    }

}