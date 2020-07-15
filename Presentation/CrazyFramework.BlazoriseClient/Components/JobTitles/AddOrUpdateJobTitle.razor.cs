using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models.JobTitles;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.JobTitles;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.JobTitles
{
	public partial class AddOrUpdateJobTitle
	{
		private JobTitle jobTitle = new JobTitle();
		private bool isEditing = false;
		private Modal updateModal;
		private ValidationChangingSupport validationChangingSupport;

		[Inject]
		public NotificationService notificationService { get; set; }

		[Inject]
		public IJobTitleService jobTitleService { get; set; }

		[Parameter]
		public EventCallback OnSuccessedCallback { get; set; }

		public void ShowJobTitleModal(JobTitleDto jobTitleDto = null)
		{
			if (jobTitleDto == null)
			{
				isEditing = false;
				jobTitle = new JobTitle();
				updateModal.Show();
			}
			else
			{
				isEditing = true;
				jobTitle = new JobTitle
				{
					Id = jobTitleDto.Id,
					Name = jobTitleDto.Name,
					Description = jobTitleDto.Description
				};
				updateModal.Show();
			}
		}

		private Task Update()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await jobTitleService.UpdateJobTitle(new UpdateJobTitleDto
				{
					Id = jobTitle.Id,
					Name = jobTitle.Name,
					Description = jobTitle.Description
				});

				await OnSuccessedCallback.InvokeAsync(this);

				notificationService.ShowSuccessSnackbar($"Updated job title '{jobTitle.Name}'");

				jobTitle = new JobTitle();
				HideJobTitleModal();
			});
		}

		private Task Create()
		{
			return notificationService.CatchAndDisplayErrors(async () =>
			{
				await jobTitleService.CreateJobTitle(new CreateJobTitleDto
				{
					Name = jobTitle.Name,
					Description = jobTitle.Description
				});

				await OnSuccessedCallback.InvokeAsync(this);

				notificationService.ShowSuccessSnackbar($"Created job title '{jobTitle.Name}'");

				jobTitle = new JobTitle();
				HideJobTitleModal();
			});
		}

		private Task CreateOrUpdate()
		{
			return isEditing ? Update() : Create();
		}

		private void HideJobTitleModal()
		{
			updateModal.Hide();
		}

		private void OnFormElementChanged()
		{
			validationChangingSupport?.OnModelChanged();
		}
	}
}