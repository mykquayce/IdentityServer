var builder = WebApplication.CreateBuilder(args);

{
	var certPemFilePath = builder.Configuration["Kestrel:Certificates:Default:Path"];
	var keyPemFilePath = builder.Configuration["Kestrel:Certificates:Default:KeyPath"];

	var cert = System.Security.Cryptography.X509Certificates.X509Certificate2
		.CreateFromPemFile(certPemFilePath!, keyPemFilePath);

	builder.Services
		.AddIdentityServer()
		.AddInMemoryApiScopes(builder.Configuration.GetSection("ApiScopes"))
		.AddInMemoryClients(builder.Configuration.GetSection("Clients"))
		.AddSigningCredential(cert);
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseIdentityServer();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }
