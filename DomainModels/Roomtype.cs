using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Roomtype : Common
    {
        public required string Name { get; set; }
        public string Description { get; set; } = "";
        public List<Room> Rooms { get; set; } = new(); // 1:n

    }    
}
