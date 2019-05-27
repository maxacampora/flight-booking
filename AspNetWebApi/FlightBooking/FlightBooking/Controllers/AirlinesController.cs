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
    public class AirlinesController : ApiController
    {
        private FlightBookingContext db = new FlightBookingContext();

        // GET: api/Airlines
        public IQueryable<Airline> GetAirlines()
        {
            return db.Airlines;
        }

        // GET: api/Airlines
        public IQueryable<Airline> GetAirlines(string q)
        {
            var matches = from m in db.Airlines
                          where m.name.ToLower().StartsWith(q.ToLower())
                          select m;
            return matches;
        }

        // GET: api/Airlines/5
        [ResponseType(typeof(Airline))]
        public async Task<IHttpActionResult> GetAirline(int id)
        {
            Airline airline = await db.Airlines.FindAsync(id);
            if (airline == null)
            {
                return NotFound();
            }

            return Ok(airline);
        }

        // PUT: api/Airlines/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAirline(int id, Airline airline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airline.Id)
            {
                return BadRequest();
            }

            db.Entry(airline).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirlineExists(id))
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

        // POST: api/Airlines
        [ResponseType(typeof(Airline))]
        public async Task<IHttpActionResult> PostAirline(Airline airline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Airlines.Add(airline);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = airline.Id }, airline);
        }

        // DELETE: api/Airlines/5
        [ResponseType(typeof(Airline))]
        public async Task<IHttpActionResult> DeleteAirline(int id)
        {
            Airline airline = await db.Airlines.FindAsync(id);
            if (airline == null)
            {
                return NotFound();
            }

            db.Airlines.Remove(airline);
            await db.SaveChangesAsync();

            return Ok(airline);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AirlineExists(int id)
        {
            return db.Airlines.Count(e => e.Id == id) > 0;
        }
    }
}