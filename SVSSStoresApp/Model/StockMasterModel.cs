using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SVSSStoresApp.Model
{
    public class StockMasterModel
    {
        public long StockMasterId { get; set; }
        public DateTime StockMaasterDate { get; set; }
        public string StockMasterOrderNo { get; set; }
        public DateTime StockMasterOrderDate { get; set; }
        public string StockMasterBillNo { get; set; }
        public DateTime StockMasterBillDate { get; set; }
        public string StockMasterSupplier { get; set; }
        public DateTime StockMasterSuppliedOn { get; set; }
        public int StockMasterAdditionType { get; set; }
        public int StockMasterTransferType { get; set; }
        public string StockMasterParticulars { get; set; }
        public string StockMasterRemarks { get; set; }

        public ObservableCollection<StockDetailModel> StockDetailList { get; set; }
    }
}
