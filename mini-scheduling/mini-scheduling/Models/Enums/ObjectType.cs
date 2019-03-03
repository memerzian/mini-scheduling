using System.ComponentModel;

namespace min_scheduling.Models.Enums
{
    public enum ObjectType
    {
        [Description("Inv")]
        Inventory = 1,
        [Description("PO")]
        PurchaseOrder = 2,
        [Description("WO")]
        WorkOrder = 3,
        [Description("PL")]
        PlannedOrder = 4,
        [Description("MS")]
        MasterSchedule = 5
    }
}