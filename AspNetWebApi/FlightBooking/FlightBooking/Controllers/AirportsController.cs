using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FlightBooking.Data;
using FlightBooking.Models;

namespace FlightBooking.Controllers
{
    public class AirportsController : ApiController
    {
        private FlightBookingContext db = new FlightBookingContext();

        // GET: api/Airports
        public IQueryable<Airport> GetAirports()
        {
            IQueryable < Airport > q = db.Airports.Include(airport => airport.city);
            //var list = q.ToList();
            return q;
        }

        // GET: api/Airports
        public List<Airport> GetAirports(string q)
        {
            var matches = db.Airports.Where(m => m.name.ToLower().StartsWith(q.ToLower()) || m.code.ToLower().StartsWith(q.ToLower()));
            return matches.ToList();
        }

        // GET: api/Airports/5
        [ResponseType(typeof(Airport))]
        public async Task<IHttpActionResult> GetAirport(int id)
        {
            Airport airport = await db.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }

            return Ok(airport);
        }

        // PUT: api/Airports/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAirport(int id, Airport airport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airport.Id)
            {
                return BadRequest();
            }

            db.Entry(airport).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Airports
        [ResponseType(typeof(Airport))]
        public async Task<IHttpActionResult> PostAirport(Airport airport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Cities.Attach(airport.city);
            db.Airports.Add(airport);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = airport.Id }, airport);
        }

        // DELETE: api/Airports/5
        [ResponseType(typeof(Airport))]
        public async Task<IHttpActionResult> DeleteAirport(int id)
        {
            Airport airport = await db.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }

            db.Airports.Remove(airport);
            await db.SaveChangesAsync();

            return Ok(airport);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AirportExists(int id)
        {
            return db.Airports.Count(e => e.Id == id) > 0;
        }
    }
}