using System.ComponentModel.DataAnnotations;

namespace CineAnimeWeb.Models;

public class Genero
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do genero.")]
    [StringLength(80)]
    public string Nome { get; set; } = string.Empty;

    public ICollection<ObraGenero> ObraGeneros { get; set; } = new List<ObraGenero>();
}
