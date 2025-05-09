var builder = DistributedApplication.CreateBuilder(args);




var postgres = builder.AddPostgres("postgres").WithPgAdmin()
        .WithDataVolume(isReadOnly: false);
var postgresdb = postgres.AddDatabase("eItems");



var apiService = builder.AddProject<Projects.eItems_ApiService>("apiservice")
                    .WithReference(postgresdb)
                    .WaitFor(postgresdb);


// After adding all resources, run the app...
builder.AddProject<Projects.eItems_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
