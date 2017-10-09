namespace Nuget.Database.ContinuousDelivery.Contracts
{
    public interface IPublisherDacPac
    {
        void ExecutaSqlPackage(string caminhoDacPac, string conexaoBancoDados);
    }
}