using Riok.Mapperly.Abstractions;

namespace Anime_Archive_Handler_GUI;

[Mapper(UseDeepCloning = true, ThrowOnPropertyMappingNullMismatch = false, ThrowOnMappingNullMismatch = false, AllowNullPropertyAssignment = true)]
public partial class AnimeMapper
{ 
    public partial AnimeDto AnimeDtodbToAnimeDto(AnimeDtodb source);
}