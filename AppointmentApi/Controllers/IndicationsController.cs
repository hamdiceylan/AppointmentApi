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
    public class IndicationsController : ApiController
    {
        private AppointmentEntities db = new AppointmentEntities();

        // GET: api/Indications
        public IQueryable<Indication> GetIndication()
        {
            return db.Indication;
        }

        // GET: api/Indications/5
        [ResponseType(typeof(Indication))]
        public IHttpActionResult GetIndication(int id)
        {
            Indication indication = db.Indication.Find(id);
            if (indication == null)
            {
                return NotFound();
            }

            return Ok(indication);
        }

        // PUT: api/Indications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIndication(int id, Indication indication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != indication.Id)
            {
                return BadRequest();
            }

            db.Entry(indication).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicationExists(id))
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

        // POST: api/Indications
        [ResponseType(typeof(Indication))]
        public IHttpActionResult PostIndication(Indication indication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Indication.Add(indication);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IndicationExists(indication.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = indication.Id }, indication);
        }

        // DELETE: api/Indications/5
        [ResponseType(typeof(Indication))]
        public IHttpActionResult DeleteIndication(int id)
        {
            Indication indication = db.Indication.Find(id);
            if (indication == null)
            {
                return NotFound();
            }

            db.Indication.Remove(indication);
            db.SaveChanges();

            return Ok(indication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IndicationExists(int id)
        {
            return db.Indication.Count(e => e.Id == id) > 0;
        }
    }
}