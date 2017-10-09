using System.IO;
using System.Reflection;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class PublisherDacPac : IPublisherDacPac
    {
        protected readonly IToolsController controladorFerramentas;
        protected readonly IProcessController controladorProcessos;

        public PublisherDacPac(IToolsController controladorFerramentas,
            IProcessController controladorProcessos)
        {
            this.controladorFerramentas = controladorFerramentas;
            this.controladorProcessos = controladorProcessos;
        }

        public void ExecutaSqlPackage(string caminhoDacPac, string conexaoBancoDados)
        {
            var caminhoAplicacao = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (caminhoAplicacao != null)
            {
                var pastaFerramentas = Path.Combine(caminhoAplicacao, "SqlTools");

                var executavelSqlPackage = Path.Combine(pastaFerramentas, "sqlpackage.exe");

                var argumentos =
                    $"/action:publish /SourceFile:\"{caminhoDacPac}\" /TargetConnectionString:\"{conexaoBancoDados}\"";

                controladorFerramentas.ExtrairFerramentas(pastaFerramentas);

                controladorProcessos.ExecutaComandoEsperandoRetorno(executavelSqlPackage, argumentos,
                    "Successfully published database");
            }
        }
    }
}