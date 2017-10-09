using System;
using System.Data.SqlClient;
using Nuget.Database.ContinuousDelivery.Infrastructure;
using NUnit.Framework;

namespace Nuget.Database.ContinuousDelivery.TesteIntegracao
{
    [TestFixture]
    public class DatabasePublisherLocalDbTests
    {
        [Test]
        public void QuandoPublicoUmBancoDeDadosEsperoQueEleEstejaAcessivel()
        {
            
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var publicador = new DatabasePublisherLocalDb(
                new PublisherConfigLocalDb
                {
                    CaminhoDacPac = "Teste.dacpac",
                    NomeBancoDados = $"Test{Guid.NewGuid()}"
                },
                new PublisherDacPac(new ControladorFerramentas(), new ControladorProcessos()));
            publicador.Publicar();
            
            var conexao = new SqlConnection(publicador.ConexaoBancoDados);
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText =
                "SELECT COUNT(*) FROM ESTADO WHERE IDPAIS IN (SELECT IDPAIS FROM PAIS WHERE NOME LIKE 'BRASIL')";
            var numeroDeEstados = comando.ExecuteScalar();
            conexao.Close();
            publicador.Apagar();
            Assert.AreEqual(27, numeroDeEstados);
        }
    }
}