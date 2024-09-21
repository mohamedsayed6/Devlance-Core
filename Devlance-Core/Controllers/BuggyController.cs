using Devlance.Infrastructure.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	//Draft controller to see the different types of errors
	public class BuggyController : ControllerBase
	{
		private readonly DevlanceContext _dbContext;

		public BuggyController(DevlanceContext dbContext)
		{
			_dbContext = dbContext;
		}


		[HttpGet("notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var freelancer = _dbContext.FreelancerProfiles.Find((long)300);
			if (freelancer is null)
				return NotFound(new ApiResponse(404));

			return Ok(freelancer);
		}


		[HttpGet("servererror")]
		public ActionResult GetServerError()
		{
			var freelancer = _dbContext.FreelancerProfiles.Find((long)300);
			var freelancerToReturn = freelancer.ToString();

			return Ok(freelancerToReturn);
		}


		[HttpGet("badrequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

		[HttpGet("Unauthorized")]
		public ActionResult GetUnauthorizedError()
		{
			return Unauthorized(new ApiResponse(401));
		}
	}
}
