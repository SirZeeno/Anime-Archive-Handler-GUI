using JikanDotNet;
using Riok.Mapperly.Abstractions;

namespace Anime_Archive_Handler_GUI;


public static class ManualDbEditing
{
    // for porting any database to another without dataloss
    private static MapperlyMaps _mapperlyMapper = new();
}

[Mapper(UseDeepCloning = true, IgnoreObsoleteMembersStrategy = IgnoreObsoleteMembersStrategy.Both)]
public partial class MapperlyMaps
{
    public partial AnimeDto AnimeDto(Anime anime);
}