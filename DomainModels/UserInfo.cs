susing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    // UserInfo.cs
    public class UserInfo
    {

        public int? Phone { get; set; }

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = default!;
    }
}
