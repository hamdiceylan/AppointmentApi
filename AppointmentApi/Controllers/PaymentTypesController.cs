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
    [Authorize]
    public class PaymentTypesController : ApiController
    {
        private AppointmentEntities db = new AppointmentEntities();

        // GET: api/PaymentTypes
        public IQueryable<PaymentType> GetPaymentType()
        {
            return db.PaymentType;
        }

        // GET: api/PaymentTypes/5
        [ResponseType(typeof(PaymentType))]
        public IHttpActionResult GetPaymentType(int id)
        {
            PaymentType paymentType = db.PaymentType.Find(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return Ok(paymentType);
        }

        // PUT: api/PaymentTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaymentType(int id, PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            db.Entry(paymentType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
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

        // POST: api/PaymentTypes
        [ResponseType(typeof(PaymentType))]
        public IHttpActionResult PostPaymentType(PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentType.Add(paymentType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(paymentType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [ResponseType(typeof(PaymentType))]
        public IHttpActionResult DeletePaymentType(int id)
        {
            PaymentType paymentType = db.PaymentType.Find(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            db.PaymentType.Remove(paymentType);
            db.SaveChanges();

            return Ok(paymentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentTypeExists(int id)
        {
            return db.PaymentType.Count(e => e.Id == id) > 0;
        }
    }
}