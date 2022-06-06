using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Birlik.Data.Context
{
    public class BirlikUser : IdentityUser
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
    }
}