using Microsoft.AspNetCore.Mvc;

namespace AZ204_EntraAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class KeyVaultController : ControllerBase
	{
		// TODO: Implement the GetSecretAsync method
		[HttpGet("secret")]
		public async Task<ActionResult<string>> GetSecretAsync([FromQuery] string name)
		{
			return NoContent();
		}
	}
}
