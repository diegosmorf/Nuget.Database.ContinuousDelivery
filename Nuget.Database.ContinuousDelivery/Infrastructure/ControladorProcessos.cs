using System;
using System.Diagnostics;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class ControladorProcessos : IProcessController
    {
        public void ExecutaComandoEsperandoRetorno(string executavel, string argumentos, string retornoEsperado)
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                ErrorDialog = false,
                LoadUserProfile = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = executavel,
                Arguments = argumentos,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = new Process {StartInfo = startInfo};

            process.Start();

            var resultadoComando = process.StandardOutput.ReadToEnd();
            var resultadoComandoErro = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!resultadoComando.Contains(retornoEsperado))
                throw new Exception(resultadoComando, new Exception(resultadoComandoErro));
        }
    }
}