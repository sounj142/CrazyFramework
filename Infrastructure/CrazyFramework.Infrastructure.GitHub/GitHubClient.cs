using CrazyFramework.App.Entities.GitHub;
using CrazyFramework.App.Infrastructure.Github;
using System.Threading.Tasks;

namespace CrazyFramework.Infrastructure.GitHub
{
	internal class GitHubClient : IGitHubClient
	{
		public Task<GitHubUser> GetUser(string username)
		{
			return Task.FromResult((GitHubUser)null);
		}
	}
}