using System.ComponentModel.DataAnnotations;

namespace CineAnimeWeb.Models;

public class DiretorEstudio
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome.")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o pais.")]
    [StringLength(80)]
    public string Pais { get; set; } = string.Empty;

    public ICollection<Obra> Obras { get; set; } = new List<Obra>();
}
