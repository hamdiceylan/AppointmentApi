using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AppointmentApi.Models;

namespace AppointmentApi.Controllers
{
    public class ThreatmentsController : ApiController
    {
        private AppointmentEntities db = new AppointmentEntities();

        // GET: api/Threatments
        public IQueryable<Threatment> GetThreatment()
        {
            return db.Threatment;
        }

        // GET: api/Threatments/5
        [ResponseType(typeof(Threatment))]
        public IHttpActionResult GetThreatment(int id)
        {
            Threatment threatment = db.Threatment.Find(id);
            if (threatment == null)
            {
                return NotFound();
            }

            return Ok(threatment);
        }

        // PUT: api/Threatments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThreatment(int id, Threatment threatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != threatment.Id)
            {
                return BadRequest();
            }

            db.Entry(threatment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreatmentExists(id))
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

        // POST: api/Threatments
        [ResponseType(typeof(Threatment))]
        public IHttpActionResult PostThreatment(Threatment threatment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Threatment.Add(threatment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ThreatmentExists(threatment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = threatment.Id }, threatment);
        }

        // DELETE: api/Threatments/5
        [ResponseType(typeof(Threatment))]
        public IHttpActionResult DeleteThreatment(int id)
        {
            Threatment threatment = db.Threatment.Find(id);
            if (threatment == null)
            {
                return NotFound();
            }

            db.Threatment.Remove(threatment);
            db.SaveChanges();

            return Ok(threatment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThreatmentExists(int id)
        {
            return db.Threatment.Count(e => e.Id == id) > 0;
        }
    }
}