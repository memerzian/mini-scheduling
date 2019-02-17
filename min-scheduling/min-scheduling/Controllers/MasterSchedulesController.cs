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
using min_scheduling.DAL;
using min_scheduling.Models;

namespace min_scheduling.Controllers
{
    public class MasterSchedulesController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        // GET: api/MasterSchedules
        public IQueryable<MasterSchedule> GetMasterSchedules()
        {
            return db.MasterSchedules;
        }

        // GET: api/MasterSchedules/5
        [ResponseType(typeof(MasterSchedule))]
        public IHttpActionResult GetMasterSchedule(int id)
        {
            MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            if (masterSchedule == null)
            {
                return NotFound();
            }

            return Ok(masterSchedule);
        }

        // PUT: api/MasterSchedules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMasterSchedule(int id, MasterSchedule masterSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != masterSchedule.ID)
            {
                return BadRequest();
            }

            db.Entry(masterSchedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MasterScheduleExists(id))
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

        // POST: api/MasterSchedules
        [ResponseType(typeof(MasterSchedule))]
        public IHttpActionResult PostMasterSchedule(MasterSchedule masterSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MasterSchedules.Add(masterSchedule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = masterSchedule.ID }, masterSchedule);
        }

        // DELETE: api/MasterSchedules/5
        [ResponseType(typeof(MasterSchedule))]
        public IHttpActionResult DeleteMasterSchedule(int id)
        {
            MasterSchedule masterSchedule = db.MasterSchedules.Find(id);
            if (masterSchedule == null)
            {
                return NotFound();
            }

            db.MasterSchedules.Remove(masterSchedule);
            db.SaveChanges();

            return Ok(masterSchedule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MasterScheduleExists(int id)
        {
            return db.MasterSchedules.Count(e => e.ID == id) > 0;
        }
    }
}