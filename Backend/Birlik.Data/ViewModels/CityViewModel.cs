namespace Birlik.Data.ViewModels
{
    public class CityViewModel 
    {
        public int CountryId { get; set; }
        public string CityName { get; set; }
    }
    public class CreateCityViewModel 
    {
        public string CityName { get; set; }
    }
    public class UpdateCityViewModel 
    {
        public string CityName { get; set; }
    }
}