using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SVSSStoresApp.Model
{
    public class StockDetailModel : INotifyPropertyChanged
    {
        private decimal stockDetailQty = 0;
        private decimal stockDetailUnitPrice = 0;
        public long StockDetailId { get; set; }
        public long StockDetailMasterId { get; set; }
        public long StockDetailItemId { get; set; }
        public int StockDetailTransferType { get; set; }
        public string StockDetailRemarks { get; set; }
        public int StockDetailPurchaserType { get; set; }
        public decimal StockDetailQty
        {
            get
            {
                return stockDetailQty;
            }
            set
            {
                stockDetailQty = value;
                OnPropertyChanged("StockDetailQty");
                OnPropertyChanged("StrockDetailTotalPrice");

            }
        }
        public decimal StockDetailUnitPrice
        {
            get
            {
                return stockDetailUnitPrice;
            }
            set
            {
                stockDetailUnitPrice = value;
                OnPropertyChanged("StockDetailUnitPrice");
                OnPropertyChanged("StrockDetailTotalPrice");
            }
        }

        public StockMasterModel StockMasterModel { get; set; }
        public ItemMasterModel StockDetailItem { get; set; }

        public virtual decimal StrockDetailTotalPrice
        {
            get
            {
                return this.StockDetailUnitPrice * this.StockDetailQty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
