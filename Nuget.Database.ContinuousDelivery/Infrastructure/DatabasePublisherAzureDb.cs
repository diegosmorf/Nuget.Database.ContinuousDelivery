using System;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class DatabasePublisherAzureDb : IDatabasePublisher
    {
        public void Apagar()
        {
            throw new NotImplementedException();
        }

        public string ConexaoBancoDados => "";

        public void Publicar()
        {
            throw new NotImplementedException();
        }
    }
}