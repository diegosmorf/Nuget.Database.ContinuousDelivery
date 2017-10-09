using System.Collections.Generic;
using System.Text;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class PublisherConfigLocalDb : IPublisherConfig
    {
        public string CaminhoDacPac { get; set; }
        public string NomeBancoDados { get; set; }
        public IDictionary<string, string> ParametrosAdicionais { get; set; }
        public string ConexaoBancoDados => CriarStringDeConexao(NomeBancoDados, ParametrosAdicionais);
        public string ConexaoBancoDadosExclusao => CriarStringDeConexao("", ParametrosAdicionais);

        private string CriarStringDeConexao(string nomeBancoDados, IDictionary<string, string> parametrosAdicionais)
        {
            var stringConexao = new StringBuilder();
            stringConexao.Append("Data Source=(localdb)\\MSSQLLocalDB; ");

            //O nome do banco de dados vai vir em branco quando vamos remover o bancod e dados
            if (!string.IsNullOrWhiteSpace(nomeBancoDados))
                stringConexao.Append($"Initial Catalog = {nomeBancoDados};");

            if (parametrosAdicionais != null)
                foreach (var parametro in parametrosAdicionais)
                    stringConexao.Append($"{parametro.Key}={parametro.Value}");

            return stringConexao.ToString();
        }
    }
}