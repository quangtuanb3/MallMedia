var builder = DistributedApplication.CreateBuilder(args);
var apiService = builder.AddProject<Projects.MallMedia_API>("apiservice");

builder.AddProject<Projects.MallMedia_Presentation>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);
builder.Build().Run();
