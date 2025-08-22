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

        /// <summary>
        /// Navigation property til brugere med denne rolle
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();


    }
}
