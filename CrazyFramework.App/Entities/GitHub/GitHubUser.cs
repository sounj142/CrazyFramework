namespace CrazyFramework.App.Entities.GitHub
{
	public class GitHubUser
	{
		public string Id { get; private set; }
		public string UserName { get; private set; }

		public GitHubUser(string id, string userName)
		{
			Id = id;
			UserName = userName;
		}
	}
}