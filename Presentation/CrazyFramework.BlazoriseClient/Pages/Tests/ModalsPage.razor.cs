using Blazorise;

namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class ModalsPage
	{
		protected Modal modalRef;
		protected bool centered = false;
		protected ModalSize modalSize = ModalSize.Default;
		protected int? maxHeight = null;

		private void ShowModal(ModalSize modalSize, int? maxHeight = null, bool centered = false)
		{
			this.centered = centered;
			this.modalSize = modalSize;
			this.maxHeight = maxHeight;

			modalRef.Show();
		}

		private void HideModal()
		{
			modalRef.Hide();
		}
	}
}