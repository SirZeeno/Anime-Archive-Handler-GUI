using Riok.Mapperly.Abstractions;

namespace Anime_Archive_Handler_GUI;

[Mapper(UseDeepCloning = true, ThrowOnPropertyMappingNullMismatch = false, ThrowOnMappingNullMismatch = false, AllowNullPropertyAssignment = true)]
public partial class AnimeMapper
{ 
    public partial AnimeDto AnimeDtodbToAnimeDto(AnimeDtodb source); // TODO: create the properties in the AnimeDto class to match the properties in the AnimeDtodb class using riok.mapperly and also set all the ignore attributes in the AnimeMapper class
}