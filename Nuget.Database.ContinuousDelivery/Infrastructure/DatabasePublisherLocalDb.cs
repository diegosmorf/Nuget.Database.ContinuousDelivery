using System.Data.SqlClient;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class DatabasePublisherLocalDb : IDatabasePublisher
    {
        protected readonly IPublisherConfig configuracao;
        protected readonly IPublisherDacPac publicadorPacoteDac;

        public DatabasePublisherLocalDb(IPublisherConfig configuracao, IPublisherDacPac publicadorPacoteDac)
        {
            this.configuracao = configuracao;
            this.publicadorPacoteDac = publicadorPacoteDac;
        }

        public string ConexaoBancoDados => configuracao.ConexaoBancoDados;

        public virtual void Apagar()
        {
            var conexao = new SqlConnection(configuracao.ConexaoBancoDadosExclusao);
            var comando = conexao.CreateCommand();
            conexao.Open();
            comando.CommandText =
                $"ALTER DATABASE [{configuracao.NomeBancoDados}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{configuracao.NomeBancoDados}]";
            comando.ExecuteNonQuery();
        }

        public virtual void Publicar()
        {
            publicadorPacoteDac.ExecutaSqlPackage(configuracao.CaminhoDacPac, configuracao.ConexaoBancoDados);
        }
    }
}