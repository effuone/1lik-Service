using Birlik.Core.Interfaces;
using Birlik.Data.Models;
using Birlik.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Birlik.Api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationRepository locationRepository, ICityRepository cityRepository, ICountryRepository countryRepository, ILogger<LocationController> logger)
        {
            _locationRepository = locationRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _logger = logger;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocationAsync(int id)
        {
            var model = await _locationRepository.GetAsync(id);
            if(model is null)
            {
                return StatusCode(404);
            }
            _logger.LogInformation($"{DateTime.Now}: \t Retrieved location {model.LocationId}");
            return model;
        }
        [HttpGet]
        public async Task<IEnumerable<Location>> GetLocationsAsync()
        {
            var list = await _locationRepository.GetAllAsync();
            _logger.LogInformation($"{DateTime.Now}: \t Retrieved location {list.Count()}");
            return list;
        }
        [HttpPost]
        public async Task<ActionResult> PostLocationAsync(CreateLocationViewModel viewModel)
        {
            var existingCountry = await _countryRepository.GetAsync(viewModel.CountryId);
            var existingCity = await _cityRepository.GetAsync(viewModel.CityId);
            if( existingCity is not null && existingCountry is not null)
            {
                var id = await _locationRepository.CreateAsync(new Location{CountryId = existingCountry.CountryId, CityId = existingCity.CityId});
                _logger.LogInformation($"{DateTime.Now}: \t Created location {existingCountry.CountryName} {existingCity.CityName}");
                return StatusCode(200);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLocationAsync(int id, UpdateLocationViewModel viewModel)
        {
            var existingModel = await _locationRepository.GetAsync(id);
            var existingCity = await _cityRepository.GetAsync(viewModel.CityId);
            var existingCountry = await _countryRepository.GetAsync(viewModel.CountryId);
            if(existingModel is null || existingCity is null || existingCity is null)
            {
                return NotFound();
            }
            existingModel.CityId = existingCity.CityId;
            existingModel.CountryId = existingCountry.CountryId;
            await _locationRepository.UpdateAsync(id, existingModel);
            _logger.LogInformation($"{DateTime.Now}: \t Updated location {id}");
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocationAsync(int id)
        {
            var existingModel = await _locationRepository.GetAsync(id);
            if(existingModel is null)
            {
                return NotFound();   
            }
            await _locationRepository.DeleteAsync(id);
            _logger.LogInformation($"{DateTime.Now}: \t Deleted location {id}");
            return NoContent();
        }
    }   
}
