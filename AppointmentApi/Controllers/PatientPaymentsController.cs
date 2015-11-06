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
    public class PatientPaymentsController : ApiController
    {
        private AppointmentEntities db = new AppointmentEntities();

        // GET: api/PatientPayments
        public IQueryable<PatientPayment> GetPatientPayment()
        {
            return db.PatientPayment;
        }

        // GET: api/PatientPayments/5
        [ResponseType(typeof(PatientPayment))]
        public IHttpActionResult GetPatientPayment(int id)
        {
            PatientPayment patientPayment = db.PatientPayment.Find(id);
            if (patientPayment == null)
            {
                return NotFound();
            }

            return Ok(patientPayment);
        }

        // PUT: api/PatientPayments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatientPayment(int id, PatientPayment patientPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patientPayment.Id)
            {
                return BadRequest();
            }

            db.Entry(patientPayment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientPaymentExists(id))
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

        // POST: api/PatientPayments
        [ResponseType(typeof(PatientPayment))]
        public IHttpActionResult PostPatientPayment(PatientPayment patientPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PatientPayment.Add(patientPayment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PatientPaymentExists(patientPayment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = patientPayment.Id }, patientPayment);
        }

        // DELETE: api/PatientPayments/5
        [ResponseType(typeof(PatientPayment))]
        public IHttpActionResult DeletePatientPayment(int id)
        {
            PatientPayment patientPayment = db.PatientPayment.Find(id);
            if (patientPayment == null)
            {
                return NotFound();
            }

            db.PatientPayment.Remove(patientPayment);
            db.SaveChanges();

            return Ok(patientPayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientPaymentExists(int id)
        {
            return db.PatientPayment.Count(e => e.Id == id) > 0;
        }
    }
}