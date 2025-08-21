using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using DomainModels;
using DomainModels.Mapping;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public HotelsController(AppDBContext context)
        {
            _context = context;
        }
		/// <summary>
		/// Henter alle hoteller
		/// </summary>
		/// <returns>Hotellets info</returns>
		/// <response code="500">internal server error</response>
		/// <response code="404">Hotellet blev ikke fundet</response>
		/// <response code="403">ingen adgang</response>
		/// <response code="200">Hotellet blev fundet og retuneret</response>
		// GET: api/Hotels
		[HttpGet]
        public async Task<ActionResult<IEnumerable<HotelGetDto>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return HotelMapping.ToHotelGetDtos(hotels);


        }
		/// <summary>
		/// Henter hotellets baseret på id.
		/// </summary>
		/// <param name="id"> Hotellets id</param>
		/// <returns>Hotellets info</returns>
		/// <response code="500">internal server error</response>
		/// <response code="404">Hotellet blev ikke fundet</response>
		/// <response code="403">ingen adgang</response>
		/// <response code="200">Hotellet blev fundet og retuneret</response>
		// GET: api/Hotels/5
		[HttpGet("{id}")]
        public async Task<ActionResult<HotelGetDto>> GetHotel(string id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return HotelMapping.ToHotelGetDto(hotel);
        }
		/// <summary>
		/// Updatere hotellets baseret på id.
		/// </summary>
		/// <param name="hotel"> Hotellets id</param>
		/// <returns>updatere hotellets info</returns>
		/// <response code="500">internal server error</response>
		/// <response code="404">Hotellet blev ikke opdateret</response>
		/// <response code="403">ingen adgang</response>
		/// <response code="200">Hotellet blev opdateret</response>
		// PUT: api/Hotels/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(string id, HotelPutDto hotel)
        {

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
		/// <summary>
		/// Opretter et nyt hotel
		/// </summary>
		/// <param name="hotelDto"> Hotellets id</param>
		/// <returns>opretter et nyt hotel</returns>
		/// <response code="500">internal server error</response>
		/// <response code="404">Hotellet blev ikke oprettet</response>
		/// <response code="403">ingen adgang</response>
		/// <response code="200">Hotellet blev oprettet</response>
		// POST: api/Hotels
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(HotelPostDto hotelDto)
        {
            Hotel hotel = HotelMapping.ToHotelFromDto(hotelDto);
            _context.Hotels.Add(hotel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HotelExists(hotel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }
		/// <summary>
		/// Sletter et hotel
		/// </summary>
		/// <param name="id"> Hotellets id</param>
		/// <returns>Sletter et hotel</returns>
		/// <response code="500">internal server error</response>
		/// <response code="404">Hotellet blev ikke Slettet</response>
		/// <response code="403">ingen adgang</response>
		/// <response code="200">Hotellet blev slettet</response>
		// DELETE: api/Hotels/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(string id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }
    }
}
