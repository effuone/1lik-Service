namespace Birlik.Data.ViewModels
{
    public class CountryViewModel 
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
    public class CreateCountryViewModel 
    {
        public string CountryName { get; set; }
    }
    public class UpdateCountryViewModel 
    {
        public string CountryName { get; set; }
    }
}