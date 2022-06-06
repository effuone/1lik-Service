using Dapper.Contrib.Extensions;

namespace Birlik.Data.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string CityName { get; set; }
    }
}
