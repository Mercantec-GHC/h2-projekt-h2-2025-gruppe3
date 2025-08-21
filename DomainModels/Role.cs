using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    /// <summary>
    /// Role entity til database
    /// </summary>
    public class Role : Common
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(200)]
        /// <summary>
        /// Navigation property til brugere med denne rolle
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        /// <summary>
        /// Rolle navne konstanter til brug med [Authorize(Roles)]
        /// </summary>
        public static class Names
        {
            public const string User = "User";
            public const string CleaningStaff = "CleaningStaff";
            public const string Reception = "Reception";
            public const string Admin = "Admin";
        }
    }
}