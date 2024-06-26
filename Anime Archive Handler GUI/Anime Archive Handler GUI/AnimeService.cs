namespace Anime_Archive_Handler_GUI;

public class AnimeService
{
    private readonly AnimeMapper _animeMapper;

    public AnimeService(AnimeMapper animeMapper)
    {
        _animeMapper = animeMapper;
    }

    public AnimeDto GetAnimeDto(AnimeDtodb animeDtodb)
    {
        return _animeMapper.AnimeDtodbToAnimeDto(animeDtodb);
    }
}