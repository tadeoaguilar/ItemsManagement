var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.eItems_ApiService>("apiservice");

builder.AddProject<Projects.eItems_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
