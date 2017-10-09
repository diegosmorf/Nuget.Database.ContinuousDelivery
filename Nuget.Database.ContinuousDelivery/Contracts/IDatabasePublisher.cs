namespace Nuget.Database.ContinuousDelivery.Contracts
{
    public interface IDatabasePublisher
    {
        string ConexaoBancoDados { get; }
        void Publicar();
        void Apagar();
    }
}