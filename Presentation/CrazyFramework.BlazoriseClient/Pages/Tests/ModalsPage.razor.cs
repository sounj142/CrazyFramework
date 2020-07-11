using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;

namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class ModalsPage
	{
		private Modal modalRef;
		private bool centered = false;
		private ModalSize modalSize = ModalSize.Default;
		private int? maxHeight = null;

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