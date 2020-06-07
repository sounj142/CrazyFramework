using CrazyFramework.App.Entities.GitHub;
using System.Threading.Tasks;

namespace CrazyFramework.App.Infrastructure.Github
{
	public interface IGitHubClient
	{
		Task<GitHubUser> GetUser(string username);
	}
}