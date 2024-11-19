using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);
var apiService = builder.AddProject<Projects.MallMedia_API>("apiservice"); //deploy on http://10.20.54.244:5056

builder.AddProject<Projects.MallMedia_Presentation>("webfrontend") //deploy on http://0.20.54.244:5179
.WithExternalHttpEndpoints()
.WithReference(apiService);



