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
using mini_scheduling.DAL;
using mini_scheduling.Models;

namespace mini_scheduling.Controllers
{
    public class MasterScheduleController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        // GET: api/MasterSchedules
        public IQueryable<MasterScheduleEntity> GetMasterSchedules()
        {
            return db.MasterSchedules;
        }

        // GET: api/MasterSchedules/5
        [ResponseType(typeof(MasterScheduleEntity))]
        public IHttpActionResult GetMasterSchedule(int id)
        {
            MasterScheduleEntity masterSchedule = db.MasterSchedules.Find(id);
            if (masterSchedule == null)
            {
                return NotFound();
            }

            return Ok(masterSchedule);
        }

        // PUT: api/MasterSchedules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMasterSchedule(int id, MasterScheduleEntity masterSchedule)
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
        [ResponseType(typeof(MasterScheduleEntity))]
        public IHttpActionResult PostMasterSchedule(MasterScheduleEntity masterSchedule)
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
        [ResponseType(typeof(MasterScheduleEntity))]
        public IHttpActionResult DeleteMasterSchedule(int id)
        {
            MasterScheduleEntity masterSchedule = db.MasterSchedules.Find(id);
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