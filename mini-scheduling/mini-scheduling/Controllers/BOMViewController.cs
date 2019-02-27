using mini_scheduling.DAL;
using mini_scheduling.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace mini_scheduling.Controllers
{
    public class BOMViewController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        [Route ("api/GetBom/{partid}")]
        public IHttpActionResult GetBOM(int partID)
        {
            var part = db.Parts.Where(p => p.PartID == partID).First();

            // Start tree out with the node passed in
            var bom = new BOMObject()
            {
                Partnumber = part.Name,
                LeadTime = part.Leadtime,
                Quantity = 1,
                Children = new List<BOMObject>()
            };

            AddChildren(part.PartID, bom);

            return Ok(bom);
        }

        public void AddChildren(int partID, BOMObject bom)
        {
            var part = db.Parts.Where(p => p.PartID == partID).First();

            if (part.BillOfMaterialsID != null)
            {
                int bomID = (int)part.BillOfMaterialsID;

                var children = db.BillOfMaterialsRequirements.Where(b => b.BillOfMaterialsID == bomID).Select(x => new BOMObject
                {
                    Partnumber = x.Part.Name,
                    PartID = x.RequiredPartID,
                    Quantity = x.Quantity,
                    LeadTime = x.Part.Leadtime
                }).ToList();

                bom.Children.AddRange(children);

                foreach (var child in children)
                {
                    AddChildren(child.PartID, child);
                }
            }
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