using System.ComponentModel.DataAnnotations;

namespace CineAnimeWeb.Models;

public class Avaliacao
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do usuario.")]
    [Display(Name = "Usuario")]
    [StringLength(100)]
    public string NomeUsuario { get; set; } = string.Empty;

    [Range(0, 10, ErrorMessage = "A nota deve estar entre 0 e 10.")]
    public decimal Nota { get; set; }

    [Required(ErrorMessage = "Informe um comentario.")]
    [StringLength(500)]
    public string Comentario { get; set; } = string.Empty;

    [Display(Name = "Data da avaliacao")]
    [DataType(DataType.Date)]
    public DateTime DataAvaliacao { get; set; } = DateTime.Today;

    [Display(Name = "Obra")]
    [Required(ErrorMessage = "Selecione uma obra.")]
    public int ObraId { get; set; }

    public Obra? Obra { get; set; }
}
