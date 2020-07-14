using CrazyFramework.Dtos.JobTitles;
using CrazyFramework.App.Handlers.JobTitles.Commands.CreateJobTitle;
using CrazyFramework.App.Handlers.JobTitles.Commands.DeleteJobTitle;
using CrazyFramework.App.Handlers.JobTitles.Commands.UpdateJobTitle;
using CrazyFramework.App.Handlers.JobTitles.Queries.GetJobTitles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CrazyFramework.App.Entities.Accounts;

namespace CrazyFramework.API.Controllers
{
	[Authorize(Roles = RoleType.Administrator)]
	public class JobTitlesController : ApiController
	{
		[HttpGet]
		public async Task<ActionResult<JobTitleDto[]>> Get()
		{
			return await Mediator.Send(new GetJobTitlesQuery());
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Create(CreateJobTitleCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(Guid id, UpdateJobTitleCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}
			await Mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(Guid id)
		{
			await Mediator.Send(new DeleteJobTitleCommand { Id = id });
			return NoContent();
		}
	}
}