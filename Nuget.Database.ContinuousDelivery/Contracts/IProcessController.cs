namespace Nuget.Database.ContinuousDelivery.Contracts
{
    public interface IProcessController
    {
        void ExecutaComandoEsperandoRetorno(string executavel, string argumentos, string retornoEsperado);
    }
}