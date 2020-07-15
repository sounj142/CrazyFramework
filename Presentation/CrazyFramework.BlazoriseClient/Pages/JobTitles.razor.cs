using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.BlazoriseClient.Components.JobTitles;
using CrazyFramework.BlazoriseClient.Models.Users;
using CrazyFramework.BlazoriseClient.Services;
using CrazyFramework.BlazoriseClient.Shared;
using CrazyFramework.Dtos.JobTitles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Pages
{
	[Authorize(Roles = RoleType.Administrator)]
	public partial class JobTitles
	{
		[Inject]
		public NotificationService notificationService { get; set; }

		[Inject]
		public IJobTitleService jobTitleService { get; set; }

		private IList<JobTitleDto> jobTitles = new List<JobTitleDto>();

		private AddOrUpdateJobTitle addOrUpdateModal;
		private DeleteJobTitle deleteModal;

		protected override Task OnInitializedAsync()
		{
			return notificationService.CatchAndDisplayErrors(LoadData);
		}

		private async Task LoadData()
		{
			jobTitles = await jobTitleService.GetJobTitles();
		}

		public void ShowAddOrUpdateModal(Guid? jobTitleId = null)
		{
			if (jobTitleId == null)
			{
				addOrUpdateModal.ShowJobTitleModal(null);
			}
			else
			{
				var jobTitleDto = jobTitles.FirstOrDefault(x => x.Id == jobTitleId);
				if (jobTitleDto != null)
				{
					addOrUpdateModal.ShowJobTitleModal(jobTitleDto);
				}
			}
		}

		private void ShowDeleteModal(Guid jobTitleId)
		{
			var jobTitleDto = jobTitles.FirstOrDefault(x => x.Id == jobTitleId);
			if (jobTitleDto != null)
			{
				deleteModal.ShowDeleteModal(jobTitleDto);
			}
		}
	}
}