using CineAnimeWeb.Models;

namespace CineAnimeWeb.ViewModels;

public class ObraDiretorConsultaViewModel
{
    public string Titulo { get; set; } = string.Empty;
    public TipoObra Tipo { get; set; }
    public int AnoLancamento { get; set; }
    public string NomeDiretorEstudio { get; set; } = string.Empty;
    public string PaisDiretorEstudio { get; set; } = string.Empty;
}

public class GrupoTipoConsultaViewModel
{
    public TipoObra Tipo { get; set; }
    public int QuantidadeObras { get; set; }
    public decimal MediaNotas { get; set; }
}

public class GrupoDiretorConsultaViewModel
{
    public string NomeDiretorEstudio { get; set; } = string.Empty;
    public int QuantidadeObras { get; set; }
    public decimal MediaAvaliacoes { get; set; }
}

public class ConsultasIndexViewModel
{
    public int AnoFiltro { get; set; } = 2010;
    public List<ObraDiretorConsultaViewModel> ObrasComDiretores { get; set; } = new();
    public List<GrupoTipoConsultaViewModel> ObrasPorTipo { get; set; } = new();
    public List<GrupoDiretorConsultaViewModel> DiretoresComMaisDeUmaObra { get; set; } = new();
}
