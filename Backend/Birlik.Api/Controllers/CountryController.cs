using Birlik.Core.Interfaces;
using Birlik.Data.Models;
using Birlik.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Birlik.Api.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var list = await _countryRepository.GetAllAsync();
            _logger.LogInformation($"{DateTime.Now}: \t Retrieved {list.Count()} countries");
            return list;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryAsync(int id)
        {
            var existingModel = await _countryRepository.GetAsync(id);
            if(existingModel is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            _logger.LogInformation($"{DateTime.Now}: \t Retrieved country {existingModel.CountryName}");
            return existingModel;
        }
        [HttpPost]
        public async Task<ActionResult<CountryViewModel>> CreateCountryAsync(CreateCountryViewModel viewModel)
        {
            var existingModel = await _countryRepository.GetAsync(viewModel.CountryName);
            if(existingModel is not null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            await _countryRepository.CreateAsync(new Country{ CountryName = viewModel.CountryName});
            _logger.LogInformation($"{DateTime.Now}: \t Created country {existingModel.CountryName}");
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCountryAsync(int id, UpdateCountryViewModel viewModel)
        {
            var existingModel = await _countryRepository.GetAsync(id);
            if(existingModel is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            await _countryRepository.UpdateAsync(id, new Country{CountryId = id, CountryName = viewModel.CountryName});
            _logger.LogInformation($"{DateTime.Now}: \t Updated country {id}");
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountryAsync(int id)
        {
            var existingModel = await _countryRepository.GetAsync(id);
            if(existingModel is null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            await _countryRepository.DeleteAsync(id);
            _logger.LogInformation($"{DateTime.Now}: \t Deleted country {id}");
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}