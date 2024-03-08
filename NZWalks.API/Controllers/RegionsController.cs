using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;
   
namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            ////return dto regions
            //var regionsDTO = new List<Model.DTO.Region>();

            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Model.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };


            //});


            var regionsDTO = mapper.Map<List<Model.DTO.Region>>(regions);

            return Ok(regionsDTO);

        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult>GetRegionAsync(Guid id)
        {
           var region= await regionRepository.GetAsync(id);

            if(region == null)
            {
                return NotFound();
            }
            var regionDTO=mapper.Map<Model.DTO.Region>(region);
            return Ok(regionDTO);


        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult>AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //Validate the request

            //if (!ValidateAddRegionAsync(addRegionRequest))
            //{
            //    return BadRequest(ModelState);
            //}
             
            //request DTO to domain model

            var region = new Model.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Area = addRegionRequest.Area,
                Population = addRegionRequest.Population,
            };

            //pass details to repository

            region = await regionRepository.AddAsync(region);

            //convert to dto from domain model

            var regionDTO = new Model.DTO.Region()
            {
                Id=region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult>DeleteRegionAsync(Guid id)
        {
            //get regions from database
            var region = await regionRepository.DeleteAsync(id);

            //if null notfound

            if (region == null)
            {
                return NotFound();
            }

            //convert back to DTO

            var regionDTO = new Model.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population,
            };

            //RETURN OK RESPONSE

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateRegionsAsync([FromRoute]Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {

            //Validate  the request

            //if (!ValidateUpdateRegionsAsync(updateRegionRequest))
            //{
            //    return BadRequest(ModelState);
            //}
            //dto to domain model

            var region = new Model.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Area = updateRegionRequest.Area,
                Population = updateRegionRequest.Population,
            };

            //update in repository


            region = await regionRepository.UpdateAsync(id, region);

            //if null not found
            if(region == null)
            {
                return NotFound();
            }

            //convert back to dto

            var regionDTO = new Model.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
                Area = region.Area,
                Population = region.Population,
            };

            //return ok response

            return Ok(regionDTO);

        }


        #region Private  methods

        private bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if (addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                    $"Add Region Data is Required.");
                return false;
            }


            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), $"{nameof(addRegionRequest.Code)} cannot be Empty and White Space");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), $"{nameof(addRegionRequest.Name)} cannot be Empty and White Space");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), $"{nameof(addRegionRequest.Area)} cannot be Empty or Zero");
            }

            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), $"{nameof(addRegionRequest.Population)} cannot be less than Zero");
            }

            if(ModelState.ErrorCount >0)
            {
                return false;
            }

            return true;

        }

        private bool ValidateUpdateRegionsAsync(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"Add Region Data is Required.");
                return false;
            }


            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code), $"{nameof(updateRegionRequest.Code)} cannot be Empty and White Space");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name), $"{nameof(updateRegionRequest.Name)} cannot be Empty and White Space");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area), $"{nameof(updateRegionRequest.Area)} cannot be Empty or Zero");
            }

            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population), $"{nameof(updateRegionRequest.Population)} cannot be less than Zero");
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
