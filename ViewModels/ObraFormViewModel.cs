using CineAnimeWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CineAnimeWeb.ViewModels;

public class ObraFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o titulo.")]
    [StringLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public TipoObra Tipo { get; set; }

    [Display(Name = "Ano de Lancamento")]
    [Range(1900, 2026, ErrorMessage = "Informe um ano valido.")]
    public int AnoLancamento { get; set; }

    [Display(Name = "Duracao em minutos")]
    [Range(1, 228, ErrorMessage = "Informe uma duracao valida.")]
    public int DuracaoMinutos { get; set; }

    [Required(ErrorMessage = "Informe a sinopse.")]
    [StringLength(228)]
    public string Sinopse { get; set; } = string.Empty;

    [Display(Name = "Diretor/Estudio")]
    [Required(ErrorMessage = "Selecione um diretor ou estudio.")]
    public int DiretorEstudioId { get; set; }

    [Display(Name = "Generos")]
    public List<int> GenerosSelecionados { get; set; } = new();

    public SelectList? DiretoresEstudios { get; set; }
    public MultiSelectList? Generos { get; set; }
}
