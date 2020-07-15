using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models.JobTitles;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.JobTitles;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.JobTitles
{
	public partial class DeleteJobTitle
	{
		private Modal deleteModal;
		private JobTitle jobTitle = new JobTitle();

		[Parameter]
		public EventCallback OnSuccessedCallback { get; set; }

		[Inject]
		public IJobTitleService jobTitleService { get; set; }

		[Inject]
		public NotificationService notificationService { get; set; }

		public void ShowDeleteModal(JobTitleDto jobTitleDto)
		{
			if (jobTitleDto != null)
			{
				jobTitle = new JobTitle
				{
					Id = jobTitleDto.Id,
					Name = jobTitleDto.Name,
					Description = jobTitleDto.Description
				};
				deleteModal.Show();
			}
		}

		private Task Delete()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await jobTitleService.RemoveJobTitle(jobTitle.Id);

				await OnSuccessedCallback.InvokeAsync(this);

				notificationService.ShowSuccessSnackbar($"Deleted job title '{jobTitle.Name}'");

				jobTitle = new JobTitle();

				HideDeleteModal();
			});
		}

		private void HideDeleteModal()
		{
			deleteModal.Hide();
		}
	}
}