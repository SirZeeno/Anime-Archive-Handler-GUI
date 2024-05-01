namespace Anime_Archive_Handler_GUI.Interfaces;

public interface IImportClassCreator
{
    void Import();
}

public class AnimeImport : IImportClassCreator
{
    public void Import()
    {
        
    }
}

public class MangaImport : IImportClassCreator
{
    public void Import()
    {
        
    }
}

public class HentaiImport : IImportClassCreator
{
    public void Import()
    {
        
    }
}

public abstract class ImportFactory
{
    public abstract IImportClassCreator ImportClassCreator();
}

public class AnimeImportFactory : ImportFactory
{
    public override IImportClassCreator ImportClassCreator()
    {
        return new AnimeImport();
    }
}

public class MangaImportFactory : ImportFactory
{
    public override IImportClassCreator ImportClassCreator()
    {
        return new MangaImport();
    }
}

public class HentaiImportFactory : ImportFactory
{
    public override IImportClassCreator ImportClassCreator()
    {
        return new HentaiImport();
    }
}