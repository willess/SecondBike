using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondBike.Data;
using SecondBike.Data.Entities;
using SecondBike.ViewModels;

namespace SecondBike.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AdvertisementsController : Controller
    {
        private readonly ISecondBikeRepository _repository;
        private readonly ILogger<AdvertisementsController> _logger;
        private readonly IMapper _mapper;

        public AdvertisementsController(ISecondBikeRepository repository, ILogger<AdvertisementsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Advertisement>> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Advertisement>, IEnumerable<AdvertisementViewModel>>(_repository.GetAllAdvertisements()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falied to get advertisements: {ex}");
                return BadRequest("Falied to get advertisements");
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetSingleAdvertisement(int id)
        {
            try
            {
                var advertisement = _repository.GetAdvertisementById(id);

                if(advertisement != null)
                {
                    return Ok(_mapper.Map<Advertisement, AdvertisementViewModel>(advertisement));
                } 
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to load Advertisement: {ex}");
                return BadRequest($"Failed to load Advertisement");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] AdvertisementViewModel model)
        {
            //add to database
            try
            {
                if (ModelState.IsValid)
                {

                    var newAdvertisement = _mapper.Map<AdvertisementViewModel, Advertisement>(model);

                    _repository.AddEntity(newAdvertisement);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/advertisements/{newAdvertisement.AdvertisementId}", _mapper.Map<Advertisement, AdvertisementViewModel>(newAdvertisement));
                    }
                    else
                    {
                        return BadRequest("could not save!!");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new advertisement: {ex}");
            }

            return BadRequest("Failed to save new advertisement");

        }
    }
}
