using System;
using Moq;
using Nuget.Database.ContinuousDelivery.Infrastructure;
using NUnit.Framework;

namespace Nuget.Database.ContinuousDelivery.TesteUnitario
{ 
    [TestFixture]
    public class DatabasePublisherMockDbTests
{
        [Test]
        public void QuandoUsoPublicadorBaseEsperoQueAConexaoEstejaCorreta()
        {
            var mock = new Mock<DatabasePublisherLocalDb>(MockBehavior.Loose,
                new PublisherConfigLocalDb
                {
                    CaminhoDacPac = "Teste.dacpac",
                    NomeBancoDados = $"Test{Guid.NewGuid()}"
                },
                new PublisherDacPac(new ControladorFerramentas(), new ControladorProcessos()));

            mock.Object.Publicar();
            mock.Object.Apagar();
            mock.Verify(m => m.Publicar());
            mock.Verify(m => m.Apagar());
        }
    }
}