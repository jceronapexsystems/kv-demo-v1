using AZ204_EntraAPI.Model;
using AZ204_EntrAuth;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace AZ204_EntraAPI.Services
{
	public class MsGraphService(ConfidentialClientSettings clientSettings) : IMsGraphService
	{
		private readonly ConfidentialClientSettings _clientSettings = clientSettings;

		/// <summary>
		/// Graph invitations only works for Azure AD, not Azure B2C
		/// </summary>
		public async Task<Invitation?> InviteUserAsync(UserModel userModel)
		{
			string[]? scopes = _clientSettings.Scopes;

			// TODO: Get these from Key Vault
			var tenantId = string.Empty;
			var clientId = string.Empty;
			var clientSecret = string.Empty;
			string redirectUrl = string.Empty;

			var options = new TokenCredentialOptions
			{
				AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
			};

			// https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
			var clientSecretCredential = new ClientSecretCredential(
				  tenantId, clientId, clientSecret, options);

			var graphServiceClient = new GraphServiceClient(
				  clientSecretCredential, scopes);

			var invitation = new Invitation
			{
				InvitedUserEmailAddress = userModel.Email,
				SendInvitationMessage = true,
				InviteRedirectUrl = redirectUrl,
				InvitedUserType = "guest" // default is guest,member
			};

			var invite = await graphServiceClient.Invitations.PostAsync(invitation);

			return invite;
		}
	}
}