namespace CrazyFramework.BlazoriseClient.Models
{
	public partial class AppState
	{
		public class AppStateProduct
		{
			#region State Data

			//public string AppName { get; } = "Crazy App";

			//private int _currentCount = 0;

			#endregion State Data

			#region State Methods

			//public int GetCurrentCount()
			//{
			//	return _currentCount;
			//}

			//public void UpdateCurrentCount(int count)
			//{
			//	_currentCount = count;
			//}

			#endregion State Methods
		}

		public AppStateProduct Product { get; } = new AppStateProduct();
	}
}