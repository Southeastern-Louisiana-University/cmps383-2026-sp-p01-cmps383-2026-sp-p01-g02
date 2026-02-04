using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Data;
using Selu383.SP26.Api.DTOs;
using Selu383.SP26.Api.Models;

namespace Selu383.SP26.Api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly DataContext _context;

        public LocationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations()
        {
            var locations = await _context.Locations.ToListAsync();
            
            var locationDtos = locations.Select(l => new LocationDto
            {
                Id = l.Id,
                Name = l.Name,
                Address = l.Address,
                TableCount = l.TableCount
            }).ToList();

            return Ok(locationDtos);
        }

        // GET: api/locations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocationById(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            var locationDto = new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                TableCount = location.TableCount
            };

            return Ok(locationDto);
        }

        // POST: api/locations
        [HttpPost]
        public async Task<ActionResult<LocationDto>> CreateLocation(LocationDto locationDto)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(locationDto.Name))
            {
                return BadRequest("Name must be provided");
            }

            if (locationDto.Name.Length > 120)
            {
                return BadRequest("Name cannot be longer than 120 characters");
            }

            if (string.IsNullOrWhiteSpace(locationDto.Address))
            {
                return BadRequest("Address must be provided");
            }

            if (locationDto.TableCount < 1)
            {
                return BadRequest("Must have at least 1 table");
            }

            // Create new location entity
            var location = new Location
            {
                Name = locationDto.Name,
                Address = locationDto.Address,
                TableCount = locationDto.TableCount
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            // Map back to DTO
            var createdDto = new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                TableCount = location.TableCount
            };

            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, createdDto);
        }

        // PUT: api/locations/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<LocationDto>> UpdateLocation(int id, LocationDto locationDto)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(locationDto.Name))
            {
                return BadRequest("Name must be provided");
            }

            if (locationDto.Name.Length > 120)
            {
                return BadRequest("Name cannot be longer than 120 characters");
            }

            if (string.IsNullOrWhiteSpace(locationDto.Address))
            {
                return BadRequest("Address must be provided");
            }

            // Find existing location
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            // Update properties
            location.Name = locationDto.Name;
            location.Address = locationDto.Address;
            location.TableCount = locationDto.TableCount;

            await _context.SaveChangesAsync();

            // Map back to DTO
            var updatedDto = new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                TableCount = location.TableCount
            };

            return Ok(updatedDto);
        }

        // DELETE: api/locations/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocation(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}