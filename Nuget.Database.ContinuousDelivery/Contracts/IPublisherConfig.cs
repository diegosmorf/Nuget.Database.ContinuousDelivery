using System.Collections.Generic;

namespace Nuget.Database.ContinuousDelivery.Contracts
{
    public interface IPublisherConfig
    {
        string CaminhoDacPac { get; set; }
        string NomeBancoDados { get; set; }
        IDictionary<string, string> ParametrosAdicionais { get; set; }
        string ConexaoBancoDados { get; }
        string ConexaoBancoDadosExclusao { get; }
    }
}