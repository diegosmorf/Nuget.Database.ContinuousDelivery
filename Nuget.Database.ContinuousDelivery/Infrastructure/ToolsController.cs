using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Nuget.Database.ContinuousDelivery.Contracts;

namespace Nuget.Database.ContinuousDelivery.Infrastructure
{
    public class ControladorFerramentas : IToolsController
    {
        public void ExtrairFerramentas(string pastaDestino)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //Caminho deve incluir o namespace completo pois é um resource embedded.
            const string resourceName = "Nuget.Database.ContinuousDelivery.SqlTools.zip";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new ArgumentException($"No {resourceName} found!");
                if (!Directory.Exists(pastaDestino))
                {
                    var tempFile = Path.GetTempFileName();
                    var fileStream = File.Create(tempFile);
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                    fileStream.Close();
                    ZipFile.ExtractToDirectory(tempFile, pastaDestino);
                }
            }
        }
    }
}