using min_scheduling.Models.Enums;
using mini_scheduling.DAL;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace mini_scheduling.Controllers
{
    public class MasterScheduleController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        [Route("api/MasterSchedules")]
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

        [Route("api/MasterScheduleProgress/{masterScheduleID}")]
        public IHttpActionResult GetMasterScheduleProgress(int masterScheduleID)
        {
            var bomCost = new BOMCost
            {
                MasterScheduleID = masterScheduleID,
                CostItems = new Dictionary<string, int>()
            };

            int runID = db.Runs
                .Where(r => r.StatusID == (int)Status.Completed)
                .Select(r => r.RunID)
                .FirstOrDefault();

            int scheduledObjectID = db.ScheduledObjects
                .Where(s => s.MasterScheduleID == masterScheduleID)
                .Where(s => s.RunID == runID)
                .Select(s => s.ScheduledObjectID)
                .FirstOrDefault();

            var objectTypes = db.ObjectTypes
                .Where(o => o.Name != "Master Schedule")
                .Select(o => o.Name)
                .ToArray();

            bomCost.CostItems.Add("Complete", 0);

            foreach (string type in objectTypes)
            {
                bomCost.CostItems.Add(type, 0);
            }

            int parentPartCost = db.MasterSchedules
                .Where(m => m.MasterScheduleID == masterScheduleID)
                .Select(m => m.Part.ThisLevelUnitCost)
                .FirstOrDefault();

            int partID = db.MasterSchedules
              .Where(m => m.MasterScheduleID == masterScheduleID)
              .Select(m => m.Part.PartID)
              .FirstOrDefault();

            int totalPartCost = AddChildrenPartCost(partID, parentPartCost);
            AddChildrenCost(scheduledObjectID, bomCost, runID);

            // Assume $ complete is the total part cost - everything currently pegging to the item
            bomCost.CostItems["Complete"] = totalPartCost - bomCost.CostItems.Values.Sum();

            return Ok(bomCost);
        }

        public int AddChildrenPartCost(int partID, int partCost)
        {
            var part = db.Parts.Where(p => p.PartID == partID).First();

            if (part.BillOfMaterialsID != null)
            {
                int bomID = (int)part.BillOfMaterialsID;

                var children = db.BillOfMaterialsRequirements.Where(b => b.BillOfMaterialsID == bomID)
                    .Select(x => new
                    {
                        PartID = x.RequiredPartID,
                        Quantity = x.Quantity,
                        Cost = x.Part.ThisLevelUnitCost
                    })
                    .ToList();

                foreach (var child in children)
                {
                    partCost += (child.Cost * child.Quantity) + AddChildrenPartCost(child.PartID, partCost);
                }
            }

            return partCost;
        }

        private void AddChildrenCost(int scheduledObjectID, BOMCost bomCost, int runID)
        {
            // Find direct children
            var children = db.Allocations
                .Where(a => a.RunID == runID)
                .Where(a => a.ParentScheduledObjectID == scheduledObjectID)
                .Select(a => new
                {
                    ScheduledObjectID = a.ChildScheduledObject.ScheduledObjectID,
                    Quantity = a.Quantity,
                    Cost = a.ChildScheduledObject.Part.ThisLevelUnitCost,
                    ObjectType = a.ChildScheduledObject.Type.Name
                })
                .ToArray();

            foreach(var child in children)
            {
                bomCost.CostItems[child.ObjectType] += child.Cost * child.Quantity;
                AddChildrenCost(child.ScheduledObjectID, bomCost, runID);
            }

            return;
        }

        [Route("api/SaveMasterSchedules")]
        public IHttpActionResult PutMasterSchedules(MasterScheduleEntity[] masterSchedules)
        {
            foreach(MasterScheduleEntity masterSchedule in masterSchedules)
            {
                if (masterSchedule.MasterScheduleID > 0)
                {
                    var msEntity = db.MasterSchedules.Find(masterSchedule.MasterScheduleID);
                    msEntity.Date = masterSchedule.Date;
                    msEntity.Name = masterSchedule.Name;
                }
                else
                {
                    db.MasterSchedules.Add(masterSchedule);
                }
            }

            db.SaveChanges();
            return Ok();
        }

        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutMasterSchedule(int id, MasterScheduleEntity masterSchedule)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != masterSchedule.ID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(masterSchedule).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MasterScheduleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //[ResponseType(typeof(MasterScheduleEntity))]
        //public IHttpActionResult PostMasterSchedule(MasterScheduleEntity masterSchedule)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.MasterSchedules.Add(masterSchedule);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = masterSchedule.ID }, masterSchedule);
        //}

        //[ResponseType(typeof(MasterScheduleEntity))]
        //public IHttpActionResult DeleteMasterSchedule(int id)
        //{
        //    MasterScheduleEntity masterSchedule = db.MasterSchedules.Find(id);
        //    if (masterSchedule == null)
        //    {
        //        return NotFound();
        //    }

        //    db.MasterSchedules.Remove(masterSchedule);
        //    db.SaveChanges();

        //    return Ok(masterSchedule);
        //}

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
            return db.MasterSchedules.Count(e => e.MasterScheduleID == id) > 0;
        }
    }
}