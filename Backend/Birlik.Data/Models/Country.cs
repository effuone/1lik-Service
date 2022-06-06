using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Birlik.Data.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string CountryName { get; set; }
    }
}
