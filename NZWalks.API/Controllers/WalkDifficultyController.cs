using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficulty()
        {
            var walkDifficulty = await walkDifficultyRepository.GetAllAsync();

            var walkDiffficultyDTO = mapper.Map<List<Model.DTO.WalkDifficulty>>(walkDifficulty);
            return Ok(walkDiffficultyDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        [Authorize(Roles = "reader")]

        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult>AddWalkDifficultyAsync([FromBody]UpdateWalkDifficultyRequest addWalkDifficultyRequest)
        {
            //Validate the INcoming Request


            //if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };

            walkDifficultyDomain=await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            var walkDifficultyDTO = new Model.DTO.WalkDifficulty
            {
                Id=walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code,
            };

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute]Guid id , [FromBody]UpdateWalkDifficultyRequest addWalkDifficultyRequest )
        {

            //Validate the Incoming Request

            //if (!ValidateUpdateWalkDifficultyAsync(addWalkDifficultyRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };

            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id,walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDTO = new Model.DTO.WalkDifficulty
            {
                Id=walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code,
            };

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]

        public async Task<IActionResult>DeleteAsync(Guid id)
        {
           var walkDifficultyDomain=await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = new Model.DTO.WalkDifficulty
            {
                Id=walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code,
            };
            return Ok(walkDifficultyDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficultyAsync( Model.DTO.UpdateWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"{nameof(addWalkDifficultyRequest)} is Required");

            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)}  is Required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(Model.DTO.UpdateWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"{nameof(addWalkDifficultyRequest)} is Required");

            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code),
                    $"{nameof(addWalkDifficultyRequest.Code)}  is Required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
