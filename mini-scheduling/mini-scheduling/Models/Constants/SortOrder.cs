using min_scheduling.Models.Enums;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public static class SortOrder
    {
        public static Dictionary<int, int> sortDictionary = new Dictionary<int, int>
        {
            { (int)ObjectType.Inventory, 1 },
            { (int)ObjectType.PurchaseOrder, 2 },
            { (int)ObjectType.WorkOrder, 2 },
            { (int)ObjectType.PlannedOrder, 3 }
        };
    }
}