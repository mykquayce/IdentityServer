using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Xunit;

namespace IdentityServer.WebApplications.Tests;

public class ClientConfigurationTests
{
	[Theory]
	[InlineData(/*lang=json,strict*/ @"{""Clients"": [
    {
      ""ClientId"": ""client"",
      ""AllowedGrantTypes"": [ ""client_credentials"" ],
      ""ClientSecrets"": [ { ""Value"": ""K7gNU3sdo\u002BOL0wNhqoVWhr3g6s1xYv72ol/pe/Unols="" } ],
      ""AllowedScopes"": [ ""api1"" ]
    }
  ]
}", "client", "client_credentials", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=", "api1")]
	public void BindJsonTests(string json, string expectedClientId, string expectedAllowedGrantType, string expectedClientSecret, string expectedAllowedScope)
	{
		var clients = new List<Client>();
		{
			IConfiguration configuration;
			{
				var bytes = System.Text.Encoding.UTF8.GetBytes(json);
				using var stream = new MemoryStream(bytes);

				configuration = new ConfigurationBuilder()
					.AddJsonStream(stream)
					.Build();
			}

			var section = configuration.GetSection("Clients");

			section.Bind(clients);
		}

		Assert.NotEmpty(clients);
		Assert.DoesNotContain(default, clients);
		Assert.Single(clients);
		Assert.Equal(expectedClientId, clients[0].ClientId);
		Assert.Single(clients[0].AllowedGrantTypes, expected: expectedAllowedGrantType);
		Assert.Single(clients[0].ClientSecrets);
		Assert.Equal(expectedClientSecret, clients[0].ClientSecrets.Single().Value);
		Assert.Single(clients[0].AllowedScopes, expected: expectedAllowedScope);
	}

	[Theory]
	[InlineData("client", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=", "api1")]
	public void SerializeTests(string clientId, string secret, string scope)
	{
		var client = new Client
		{
			ClientId = clientId,
			AllowedGrantTypes = GrantTypes.ClientCredentials,
			ClientSecrets =
			{
				new Secret(value: secret),
			},
			AllowedScopes = { scope, },
		};

		var json = JsonSerializer.Serialize(client);

		Assert.NotNull(json);
		Assert.StartsWith("{", json);
	}
}
