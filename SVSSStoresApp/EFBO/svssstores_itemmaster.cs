//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SVSSStoresApp.EFBO
{
    using System;
    using System.Collections.Generic;
    
    public partial class svssstores_itemmaster
    {
        public svssstores_itemmaster()
        {
            this.svssstores_stockdetail = new HashSet<svssstores_stockdetail>();
            this.svssstores_stockissuedetail = new HashSet<svssstores_stockissuedetail>();
        }
    
        public long ItemMaster_ID { get; set; }
        public string ItemMaster_ItemName { get; set; }
        public string ItemMaster_UOM { get; set; }
        public Nullable<bool> ItemMaster_IsActive { get; set; }
        public Nullable<decimal> ItemMaster_UnitPrice { get; set; }
        public string ItemMaster_ItemCode { get; set; }
        public Nullable<decimal> ItemMaster_QtyOnHand { get; set; }
        public Nullable<long> ItemMaster_GroupId { get; set; }
    
        public virtual svssstores_itemgroup svssstores_itemgroup { get; set; }
        public virtual ICollection<svssstores_stockdetail> svssstores_stockdetail { get; set; }
        public virtual ICollection<svssstores_stockissuedetail> svssstores_stockissuedetail { get; set; }
    }
}
