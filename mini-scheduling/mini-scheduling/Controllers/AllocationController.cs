using mini_scheduling.DAL;
using mini_scheduling.Models;
using System.Linq;
using System.Web.Http;

namespace mini_scheduling.Controllers
{
    public class AllocationController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        [Route("api/PartAllocationStatus/{partID}/{runID}")]
        public PartAllocationStatus GetStatus(int partID, int runID)
        {
            var allocations = db.Allocations
               .Where(a => a.ChildScheduledObject.PartID == partID)
               .Where(a => a.RunID == runID)
               .Select(a => new PartAllocationDetails
               {
                   ParentDueDate = a.ParentScheduledObject.DueDate,
                   ParentStartDate = a.ParentScheduledObject.StartDate,
                   ParentPartNumber = a.ParentScheduledObject.Part.Name,
                   ParentObjectName = a.ParentScheduledObject.Type.Name + " " + a.ParentScheduledObject.SupplyID + a.ParentScheduledObject.Sequence + a.ParentScheduledObject.MasterScheduleID,

                   AllocationQuantity = a.Quantity,

                   DueDate = a.ChildScheduledObject.DueDate,
                   StartDate = a.ChildScheduledObject.StartDate,
                   ObjectName = a.ChildScheduledObject.Type.Name + " " + a.ChildScheduledObject.SupplyID + a.ChildScheduledObject.Sequence + a.ChildScheduledObject.MasterScheduleID,
                   SupplyQuantity = a.ChildScheduledObject.Quantity
               })
               .ToArray();

            var part = db.Parts.Find(partID);

            for (int i =1; i < allocations.Length; i++)
            {
                var allocation = allocations[i];
                var previousAllocation = allocations[i - 1];

                if (allocation.ObjectName == previousAllocation.ObjectName)
                {
                    allocation.SupplyOrderRepeat = true;
                }

                if (allocation.ParentObjectName == previousAllocation.ParentObjectName && allocation.ParentPartNumber == previousAllocation.ParentPartNumber)
                {
                    allocation.DemandOrderRepeat = true;
                }
            }

            var status = new PartAllocationStatus()
            {
                PartNumber = part.Name,
                Leadtime = part.Leadtime,
                Details = allocations
            };

            return status;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}