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
    public class IndicationInfoesController : ApiController
    {
        private AppointmentEntities db = new AppointmentEntities();

        // GET: api/IndicationInfoes
        public IQueryable<IndicationInfo> GetIndicationInfo()
        {
            return db.IndicationInfo;
        }

        // GET: api/IndicationInfoes/5
        [ResponseType(typeof(IndicationInfo))]
        public IHttpActionResult GetIndicationInfo(int id)
        {
            IndicationInfo indicationInfo = db.IndicationInfo.Find(id);
            if (indicationInfo == null)
            {
                return NotFound();
            }

            return Ok(indicationInfo);
        }

        // PUT: api/IndicationInfoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIndicationInfo(int id, IndicationInfo indicationInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != indicationInfo.Id)
            {
                return BadRequest();
            }

            db.Entry(indicationInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndicationInfoExists(id))
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

        // POST: api/IndicationInfoes
        [ResponseType(typeof(IndicationInfo))]
        public IHttpActionResult PostIndicationInfo(IndicationInfo indicationInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IndicationInfo.Add(indicationInfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IndicationInfoExists(indicationInfo.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = indicationInfo.Id }, indicationInfo);
        }

        // DELETE: api/IndicationInfoes/5
        [ResponseType(typeof(IndicationInfo))]
        public IHttpActionResult DeleteIndicationInfo(int id)
        {
            IndicationInfo indicationInfo = db.IndicationInfo.Find(id);
            if (indicationInfo == null)
            {
                return NotFound();
            }

            db.IndicationInfo.Remove(indicationInfo);
            db.SaveChanges();

            return Ok(indicationInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IndicationInfoExists(int id)
        {
            return db.IndicationInfo.Count(e => e.Id == id) > 0;
        }
    }
}