namespace mini_scheduling.Models
{
    public class BomRelationship
    {
        public int ParentPartID { get; set; }
        public int ChildPartID { get; set; }

        public BomRelationship(int parentPartID, int childPartID)
        {
            ParentPartID = parentPartID;
            ChildPartID = childPartID;
        }
    }
}