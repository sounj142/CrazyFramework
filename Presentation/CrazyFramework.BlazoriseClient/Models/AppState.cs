using System;

namespace CrazyFramework.BlazoriseClient.Models
{
	public partial class AppState
	{
		#region State Data

		public string AppName { get; } = "Crazy App";

		private bool _siderBarVisible = true;

		public event Action SiderBarChanged;

		#endregion State Data

		#region State Methods

		public bool GetSiderBarVisible()
		{
			return _siderBarVisible;
		}

		public void ToggleSiderBarVisible()
		{
			_siderBarVisible = !_siderBarVisible;
			SiderBarChanged?.Invoke();
		}

		#endregion State Methods
	}
}