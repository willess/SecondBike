using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondBike.Data;
using SecondBike.Data.Entities;

namespace SecondBike.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AdvertisementsController : Controller
    {
        private readonly ISecondBikeRepository _repository;
        private readonly ILogger<AdvertisementsController> _logger;

        public AdvertisementsController(ISecondBikeRepository repository, ILogger<AdvertisementsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Advertisement>> Get()
        {
            try
            {
                return Ok(_repository.GetAllAdvertisements());
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
                    return Ok(advertisement);
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
    }
}
