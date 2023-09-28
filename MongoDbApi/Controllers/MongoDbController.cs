using Microsoft.AspNetCore.Mvc;

namespace MongoDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MongoDbController : ControllerBase
    {
        private MongoDbManager MongoDbManager;
        public MongoDbController()
        {
            MongoDbManager = new MongoDbManager();
        }

        //MongoDb/test
        [HttpGet("get-all")]
        public async Task<IEnumerable<string>> GetAll()
        {
            return await MongoDbManager.GetALl();
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(string firstName, string lastName)
        {
            try
            {
                await MongoDbManager.Delete(firstName, lastName);
            }
            catch (Exception)
            {
                return BadRequest("Wrong params");
            }

            return Ok();
        }

        [HttpGet("add")]
        public async Task<IActionResult> Add(string firstName, string lastName)
        {
            await MongoDbManager.Add(firstName, lastName);
            return Ok();
        }
    }
}
