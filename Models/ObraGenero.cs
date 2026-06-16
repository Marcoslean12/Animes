namespace CineAnimeWeb.Models;

public class ObraGenero
{
    public int ObraId { get; set; }
    public Obra? Obra { get; set; }

    public int GeneroId { get; set; }
    public Genero? Genero { get; set; }
}
