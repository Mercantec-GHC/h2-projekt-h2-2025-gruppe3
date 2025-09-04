using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    // Hotel.cs
    public class Hotel : Common
    {
        public string Name { get; set; } = string.Empty;
        public string Road { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double PercentagePrice { get; set; } = 1;
        public Facility? Facility { get; set; }
        public int FacilityId { get; set; }

        public List<Room> Rooms { get; set; } = new(); // 1:n

        public List<Booking> Bookings { get; set; } = new();
    }

    // DTO for hotel retrieval / GET
    public class HotelGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Road { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double PercentagePrice { get; set; }
        public int FacilityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // DTO for hotel creation / POST
    public class HotelPostDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel road is required")]
        public string Road { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel zip code is required")]
        public string Zip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel city is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel phone number is required")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Hotel email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel description is required")]
        [StringLength(200, ErrorMessage = "Hotel description must be a maximum of 200 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel percentage price is required")]
        [Range(0, 2, ErrorMessage = "Hotel percentage price must be between 0 and 2")]
        public double PercentagePrice { get; set; }

        [Required(ErrorMessage = "Hotel facility ID is required")]
        public int FacilityId { get; set; }
    }

    // DTO for hotel med rum retrieval / GET
    //public class HotelWithRoomsGetDto
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public string Road { get; set; } = string.Empty;
    //    public string Zip { get; set; } = string.Empty;
    //    public string City { get; set; } = string.Empty;
    //    public string Country { get; set; } = string.Empty;
    //    public int Phone { get; set; }
    //    public string Email { get; set; } = string.Empty;
    //    public string Description { get; set; } = string.Empty;
    //    public double PercentagePrice { get; set; } = 1;
    //    public Facility? Facility { get; set; }
    //    public int FacilityId { get; set; }

    //    public List<Room> Rooms { get; set; } = new(); // 1:n

    //    public List<Booking> Bookings { get; set; } = new();
    //    public DateTime CreatedAt { get; set; }
    //    public DateTime UpdatedAt { get; set; }
    //}


    // DTO for hotel update / PUT
    public class HotelPutDto
    {
        [Required(ErrorMessage = "Hotel ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel road is required")]
        public string Road { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel zip code is required")]
        public string Zip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel city is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel phone number is required")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Hotel email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel description is required")]
        [StringLength(200, ErrorMessage = "Hotel description must be a maximum of 200 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hotel percentage price is required")]
        [Range(0, 2, ErrorMessage = "Hotel percentage price must be between 0 and 2")]
        public double PercentagePrice { get; set; }

        [Required(ErrorMessage = "Hotel facility ID is required")]
        public int FacilityId { get; set; }
    }
}

