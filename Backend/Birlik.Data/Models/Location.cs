
using System.Text.Json.Serialization;
using Birlik.Data.Models;
using Dapper.Contrib.Extensions;

namespace Birlik.Data.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        [JsonIgnore]
        public Country City { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
        public Location()
        {
            
        }
        public Location(int locationId, int countryId, int cityId)
        {
            LocationId = locationId;
            CountryId = countryId;
            CityId = cityId;
        }
    }
}
