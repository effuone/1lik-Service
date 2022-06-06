using Birlik.Core.Interfaces;
using Birlik.Data.Models;
using Birlik.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Birlik.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CityController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ILogger<CityController> _logger;

        public CityController(ICountryRepository countryRepository, ICityRepository cityRepository, ILogger<CityController> logger)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            var list = await _cityRepository.GetAllAsync();
            _logger.LogInformation($"{DateTime.Now}: Retrieved {list.Count()} cities");
            return list;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityAsync(int id)
        {
            var existingModel = await _cityRepository.GetAsync(id);
            if(existingModel is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _logger.LogInformation($"{DateTime.Now}: \t Retrieved city {existingModel.CityName}");
            return existingModel;
        }
        [HttpPost]
        public async Task<ActionResult<CityViewModel>> CreateCityAsync(CreateCityViewModel viewModel)
        {
            var existingModel = await _cityRepository.GetAsync(viewModel.CityName);
            if(existingModel is not null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            await _cityRepository.CreateAsync(new City{CityName = viewModel.CityName});
            _logger.LogInformation($"{DateTime.Now}: \t Created city {existingModel.CityName}");
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCityAsync(int id, UpdateCityViewModel viewModel)
        {
            var existingModel = await _cityRepository.GetAsync(id);
            if(existingModel is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            await _cityRepository.UpdateAsync(id, new City{CityId = id, CityName = viewModel.CityName});
            _logger.LogInformation($"{DateTime.Now}: \t Updated city {id}");
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCityAsync(int id)
        {
            var existingModel = await _cityRepository.GetAsync(id);
            if(existingModel is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            await _cityRepository.DeleteAsync(id);
            _logger.LogInformation($"{DateTime.Now}: \t Deleted city {id}");
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}