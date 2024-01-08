using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

    [ApiController]
    [Route("[Controller]")]

    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //Fetch data from database -domain walks

            var walksDomain = await walkRepository.GetAllAsync();

            //Convert domain to DTO Walks

            var walksDTO = mapper.Map<List<Model.DTO.Walk>>(walksDomain);

            //return response

            return Ok(walksDTO);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]

        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walkDomain = await walkRepository.GetAsync(id);

            //convert Domain Object to DTO
            var walkDTO = mapper.Map<Model.DTO.Walk>(walkDomain);

            return Ok(walkDTO);

        }

        [HttpPost]

        public async Task<IActionResult> AddWalkAsync([FromBody] Model.DTO.AddWalkRequest addWalkRequest)
        {
            //convert DTO TO DOMAIN OBJECT

            var walkDomain = new Model.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //pass domain to repository to persists this

            walkDomain = await walkRepository.AddAsync(walkDomain);

            //convert domain to DTO
            var walkDTO = new Model.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };

            //Send DTO TO client

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequest updateWalkRequest)
        {
            //Convert DTO TO Domain Object

            var walk = new Model.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            //pass Details to Repository- Get Domain object in Response

            var walkDomain = await walkRepository.UpdateAsync(id, walk);

            //Handle null
            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = new Model.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };


            //Rreturn response

            return Ok(walkDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walkDomain= await walkRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
              var walkDTO=mapper.Map<Model.DTO.Walk>(walkDomain);

            return Ok(walkDTO);
        }
    }
}
