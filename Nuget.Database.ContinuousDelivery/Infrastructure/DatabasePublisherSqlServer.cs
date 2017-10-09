using System;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class DatabasePublisherSqlServer : IDatabasePublisher
    {
        public string ConexaoBancoDados => "";

        public void Apagar()
        {
            throw new NotImplementedException();
        }

        public void Publicar()
        {
            throw new NotImplementedException();
        }
    }
}