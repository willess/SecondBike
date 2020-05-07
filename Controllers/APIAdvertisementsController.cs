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
    [Route("api/advertisements")]
    [ApiController]
    [Produces("application/json")]
    public class APIAdvertisementsController : Controller
    {
        private readonly ISecondBikeRepository _repository;
        private readonly ILogger<APIAdvertisementsController> _logger;
        private readonly IMapper _mapper;

        public APIAdvertisementsController(ISecondBikeRepository repository, ILogger<APIAdvertisementsController> logger, IMapper mapper)
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
        public IActionResult Create([FromBody] AdvertisementCreationModel model)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    // get category
                    var category = _repository.GetCategory(model.CategoryId);

                    var advertisement = new Advertisement()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Category = category
                    };

                    _repository.AddEntity(advertisement);

                    if (_repository.SaveAll())
                    {
                        // Saved and return the body
                        return Created($"/api/advertisements/{advertisement.AdvertisementId}", _mapper.Map<Advertisement, AdvertisementViewModel>(advertisement));
                    }
                    else
                    {
                        return BadRequest("Could not save for some reason");
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
            return BadRequest("Creating new advertisement failed!");
        }
    }
}
