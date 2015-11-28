using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SVSSStoresApp.Model
{
    public partial class ItemMasterModel 
    {
        public long ItemMasterId { get; set; }
        public String ItemCode { get; set; }
        public String ItemMasterName { get; set; }
        public String UOM { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal QtyOnHand { get; set; }
        public bool IsActive { get; set; }
        public long itemGroupId { get; set; }
        public ItemGroupModel itemGroupModel { get; set; }
       
    }
}
