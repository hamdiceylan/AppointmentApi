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
    public class ThreatmentGroupsController : ApiController
    {
        private AppointmentEntities db = new AppointmentEntities();

        // GET: api/ThreatmentGroups
        public IQueryable<ThreatmentGroup> GetThreatmentGroup()
        {
            return db.ThreatmentGroup;
        }

        // GET: api/ThreatmentGroups/5
        [ResponseType(typeof(ThreatmentGroup))]
        public IHttpActionResult GetThreatmentGroup(int id)
        {
            ThreatmentGroup threatmentGroup = db.ThreatmentGroup.Find(id);
            if (threatmentGroup == null)
            {
                return NotFound();
            }

            return Ok(threatmentGroup);
        }

        // PUT: api/ThreatmentGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutThreatmentGroup(int id, ThreatmentGroup threatmentGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != threatmentGroup.Id)
            {
                return BadRequest();
            }

            db.Entry(threatmentGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreatmentGroupExists(id))
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

        // POST: api/ThreatmentGroups
        [ResponseType(typeof(ThreatmentGroup))]
        public IHttpActionResult PostThreatmentGroup(ThreatmentGroup threatmentGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ThreatmentGroup.Add(threatmentGroup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ThreatmentGroupExists(threatmentGroup.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = threatmentGroup.Id }, threatmentGroup);
        }

        // DELETE: api/ThreatmentGroups/5
        [ResponseType(typeof(ThreatmentGroup))]
        public IHttpActionResult DeleteThreatmentGroup(int id)
        {
            ThreatmentGroup threatmentGroup = db.ThreatmentGroup.Find(id);
            if (threatmentGroup == null)
            {
                return NotFound();
            }

            db.ThreatmentGroup.Remove(threatmentGroup);
            db.SaveChanges();

            return Ok(threatmentGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThreatmentGroupExists(int id)
        {
            return db.ThreatmentGroup.Count(e => e.Id == id) > 0;
        }
    }
}