namespace CrazyFramework.BlazoriseClient.Models
{
	public partial class AppState
	{
		public class AppStateCounter
		{
			#region State Data

			public int CurrentCount { get; private set; } = 0;

			#endregion State Data

			#region State Methods

			public void UpdateCurrentCount(int count)
			{
				CurrentCount = count;
			}

			#endregion State Methods
		}

		public AppStateCounter Counter { get; } = new AppStateCounter();
	}
}