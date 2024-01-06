using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult>AddRegionAsync(AddRegionRequest addRegionRequest)
        {
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
        public async Task<IActionResult> UpdateRegionsAsync([FromRoute]Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
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

    }
}
