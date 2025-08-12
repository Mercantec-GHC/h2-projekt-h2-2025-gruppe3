using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
	// Role.cs
	public class Role : Common
	{
		public required string Name { get; set; }

		public ICollection<User> Users { get; set; } = new List<User>();
	}
}
