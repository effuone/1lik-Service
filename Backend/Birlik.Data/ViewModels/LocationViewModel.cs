namespace Birlik.Data.ViewModels
{
    public class LocationViewModel 
    {
        public int LocationId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
    public class CreateLocationViewModel 
    {
        public int CountryId { get; set; }
        public int CityId { get; set; }
    }
    public class UpdateLocationViewModel 
    {
        public int CountryId { get; set; }
        public int CityId {get; set;}
    }
}