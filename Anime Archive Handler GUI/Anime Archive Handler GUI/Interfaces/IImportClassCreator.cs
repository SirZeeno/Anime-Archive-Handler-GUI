using System;

namespace Anime_Archive_Handler_GUI.Interfaces;

public interface IImportClassCreator
{
    void Scan();
    void Import();
}

public class AnimeImport : IImportClassCreator
{
    public void Scan()
    {
        
    }
    public void Import()
    {
        
    }
}

public class MangaImport : IImportClassCreator
{
    public void Scan()
    {
        
    }
    public void Import()
    {
        
    }
}

public class HentaiImport : IImportClassCreator
{
    public void Scan()
    {
        
    }
    public void Import()
    {
        
    }
}

public class ImportFactory
{
    public IImportClassCreator ImportClassCreator(ImportSettings settings)
    {
        return settings.ImportType switch
        {
            ImportType.Anime => new AnimeImport(),
            ImportType.Manga => new MangaImport(),
            ImportType.Hentai => new HentaiImport(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}