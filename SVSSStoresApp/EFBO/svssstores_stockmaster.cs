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
    
    public partial class svssstores_stockmaster
    {
        public svssstores_stockmaster()
        {
            this.svssstores_stockdetail = new HashSet<svssstores_stockdetail>();
        }
    
        public long stockmaster_id { get; set; }
        public Nullable<System.DateTime> stockmaster_date { get; set; }
        public string stockmaster_orderno { get; set; }
        public Nullable<System.DateTime> stockmaster_orderdate { get; set; }
        public string stockmaster_billno { get; set; }
        public Nullable<System.DateTime> stockmaster_billdate { get; set; }
        public string stockmaster_supplier { get; set; }
        public Nullable<System.DateTime> stockmaster_suppliedOn { get; set; }
        public Nullable<int> stockmaster_additiontype { get; set; }
        public Nullable<int> stockmaster_transfertype { get; set; }
        public string stockmaster_particulars { get; set; }
        public string stockmaster_remarks { get; set; }
        public string stockmaster_number { get; set; }
    
        public virtual ICollection<svssstores_stockdetail> svssstores_stockdetail { get; set; }
    }
}