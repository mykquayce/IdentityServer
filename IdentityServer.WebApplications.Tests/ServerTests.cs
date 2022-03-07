using IdentityModel.Client;
using Xunit;

namespace IdentityServer.WebApplications.Tests;

public class ServerTests
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped", Justification = "requires identity server")]
	[Theory(Skip = "requires identity server")]
	[InlineData("https://identityserver")]
	public async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentTests(string authority)
	{
		DiscoveryDocumentResponse disco;
		{
			using var handler = new HttpClientHandler { AllowAutoRedirect = false, };
			using var client = new HttpClient(handler) { BaseAddress = new Uri(authority, UriKind.Absolute), };
			disco = await client.GetDiscoveryDocumentAsync();
		}

		Assert.False(disco.IsError, userMessage: $"{disco.ErrorType}: {disco.Error}");
		Assert.NotNull(disco.TokenEndpoint);
		Assert.NotEmpty(disco.TokenEndpoint);

		return disco;
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped", Justification = "requires identity server")]
	[Theory(Skip = "requires identity server")]
	[InlineData("https://identityserver", "elgatoapi", "8556e52c6ab90d042bb83b3f0c8894498beeb65cf908f519a2152aceb131d3ee", "networkdiscovery")]
	public async Task GetAccessTokenTests(string authority, string clientId, string clientSecret, string scope)
	{
		var disco = await GetDiscoveryDocumentTests(authority);

		TokenResponse tokenResponse;
		{
			var tokenRequest = new ClientCredentialsTokenRequest
			{
				Address = disco.TokenEndpoint,
				ClientId = clientId,
				ClientSecret = clientSecret,
				Scope = scope,
			};

			using var handler = new HttpClientHandler { AllowAutoRedirect = false, };
			using var client = new HttpClient(handler) { BaseAddress = new Uri(authority, UriKind.Absolute), };

			tokenResponse = await client.RequestClientCredentialsTokenAsync(tokenRequest);
		}

		Assert.NotNull(tokenResponse);
		Assert.False(tokenResponse.IsError, userMessage: $"{tokenResponse.ErrorType} {tokenResponse.Error} ({tokenResponse.ErrorDescription})");

		Assert.NotNull(tokenResponse.Raw);
		Assert.NotEmpty(tokenResponse.Raw);
		Assert.StartsWith("{", tokenResponse.Raw);
	}
}
