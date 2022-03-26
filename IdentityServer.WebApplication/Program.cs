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

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseIdentityServer();

app.UseHttpsRedirection();

static bool dontProxy(HttpRequest r)
	=> r.Host.Host.Equals("identityserver", StringComparison.OrdinalIgnoreCase);

app.MapWhen(
	context => !dontProxy(context.Request),
	b => b.UseRouting().UseEndpoints(e => e.MapReverseProxy()));

app.Run();

public partial class Program { }
