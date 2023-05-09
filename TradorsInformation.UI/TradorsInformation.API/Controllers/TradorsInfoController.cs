using Microsoft.AspNetCore.Mvc;
using TradorsInformation.API.Repositories;
using TradorsInformation.DB.Entities;

namespace TradorsInformation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradorInfoController : ControllerBase
    {
        private readonly IAsyncRepository<TradorInfo> _tradorInfoRepository;

        public TradorInfoController(IAsyncRepository<TradorInfo> traderInfoRepository)
        {
            _tradorInfoRepository = traderInfoRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] TradorInfo traderInfo)
        {
            await _tradorInfoRepository.AddAsync(traderInfo);
            return Ok(traderInfo.Id);
        }

        [HttpPost("CreateList")]
        public async Task<IActionResult> CreateList([FromBody] List<TradorInfo> traderInfos)
        {
            foreach (var traderInfo in traderInfos)
            {
                await _tradorInfoRepository.AddAsync(traderInfo);
            }
            return Ok(true);
        }

        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TradorInfo traderInfo)
        {
            if (id != traderInfo.Id)
            {
                return BadRequest();
            }

            var existingTraderInfo = await _tradorInfoRepository.GetAsync(ti => ti.Id == id);
            if (existingTraderInfo == null)
            {
                return NotFound();
            }

            existingTraderInfo.Name = traderInfo.Name;
            existingTraderInfo.Surname = traderInfo.Surname;
            existingTraderInfo.Email = traderInfo.Email;
            existingTraderInfo.Phone = traderInfo.Phone;

            await _tradorInfoRepository.UpdateAsync(existingTraderInfo);

            return Ok(true);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var traderInfos = await _tradorInfoRepository.GetAllAsync();
            return Ok(traderInfos);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var traderInfo = await _tradorInfoRepository.GetAsync(ti => ti.Id == id);
            if (traderInfo == null)
            {
                return NotFound();
            }

            return Ok(traderInfo);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var traderInfo = await _tradorInfoRepository.GetAsync(ti => ti.Id == id);
            if (traderInfo == null)
            {
                return NotFound();
            }

            await _tradorInfoRepository.DeleteAsync(traderInfo);

            return Ok(true);
        }

        [HttpGet("Search/{search}")]
        public async Task<IActionResult> Delete(string search)
        {
            var All = await _tradorInfoRepository.GetAllAsync();
            var result = All.Where(obj =>
            obj.Name.Contains(search) ||
            obj.Surname.Contains(search) ||
            obj.Id.ToString().Contains(search) ||
            obj.Email.Contains(search)).ToList();
            return Ok(result);
        }

    }

}
