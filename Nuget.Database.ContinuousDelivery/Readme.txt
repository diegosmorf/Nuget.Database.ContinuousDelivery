Para gerar o pacote:

nuget pack C:\git\Nuget.Database.ContinuousDelivery\Nuget\Nuget.Database.ContinuousDelivery.csproj

Para Publicar

0 - Adicionar Source
nuget.exe push yourpackage.nupkg -Source https://#YourAccount.pkgs.visualstudio.com/_packaging/nuget/v3/index.json -ApiKey VSTS

1 - Digitar Credenciais do source Visual Studio.com
CredentialProvider.VSS.exe -U https://#YourAccount.pkgs.visualstudio.com/_packaging/nuget/v3/index.json

2 - Publicar!
nuget.exe push Nuget.Database.ContinuousDelivery.1.0.2.nupkg -Source https://#YourAccount.pkgs.visualstudio.com/_packaging/nuget/v3/index.json -ApiKey VSTS