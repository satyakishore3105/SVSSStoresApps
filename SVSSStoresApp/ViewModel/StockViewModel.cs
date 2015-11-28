using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SVSSStoresApp.Model;
using System.Windows.Input;
using SVSSStoresApp.View;
using System.Collections.ObjectModel;
using SVSSStoresApp.BusinessLogicLayer;
using System.Windows;

namespace SVSSStoresApp.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        private readonly ItemMasterManager itemMasterManger;
        private StockMasterModel stockMasterModel;
        private readonly ICommand searchItemCmd;
        private readonly ICommand addItemsCmd;
        private readonly ICommand addItemsStockDetailsCmd;
        private String strItemSearchKey;
        private ObservableCollection<ItemMasterModel> searchItemList;
        private ObservableCollection<ItemGroupModel> itemGroupList;
        private ObservableCollection<ItemMasterModel> selectedItemList;
        private Xceed.Wpf.Toolkit.WindowState isAddItemsOpen = Xceed.Wpf.Toolkit.WindowState.Closed;
        private readonly ICommand openAddItemsCmd;
        private int searchSelectedIndex = 0;
        private Visibility groupVisibility = Visibility.Visible;
        private Visibility itemVisibility = Visibility.Collapsed;
        private long selectedGroupId;

        public StockViewModel()
        {
            var tempDate = DateTime.Now;
            stockMasterModel = new StockMasterModel();
            itemMasterManger = new ItemMasterManager();
            searchItemList = new ObservableCollection<ItemMasterModel>();
            itemGroupList = new ObservableCollection<ItemGroupModel>();
            selectedItemList = new ObservableCollection<ItemMasterModel>();
            this.StockDate = tempDate;
            this.StockOrderNo = "-";
            this.StockOrderDate = tempDate;
            this.StockBillNo = "-";
            this.StockBillDate = tempDate;
            this.StockSupplier = "-";
            this.StockSuppliedOn = tempDate;
            searchItemCmd = new RelayCommand(SearchItem, CanSearchItem);
            addItemsCmd = new RelayCommand(AddSelectedItems, CanAddSelectedItems);
            openAddItemsCmd = new RelayCommand(OpenAddItems, CanOpenAddItems);

            this.ItemGroupList = itemMasterManger.GetIemGroupList();
        }

        #region Properties

        public long StockId
        {
            get { return stockMasterModel.StockMasterId; }
            set
            {
                stockMasterModel.StockMasterId = value;
                OnPropertyChanged("StockId");
            }
        }

        public DateTime StockDate
        {
            get { return stockMasterModel.StockMaasterDate; }
            set
            {
                stockMasterModel.StockMaasterDate = value;
                OnPropertyChanged("StockDate");
            }
        }

        public string StockOrderNo
        {
            get { return stockMasterModel.StockMasterOrderNo; }
            set
            {
                stockMasterModel.StockMasterOrderNo = value;
                OnPropertyChanged("StockOrderNo");
            }
        }

        public DateTime StockOrderDate
        {
            get { return stockMasterModel.StockMasterOrderDate; }
            set
            {
                stockMasterModel.StockMasterOrderDate = value;
                OnPropertyChanged("StockOrderDate");
            }
        }

        public string StockBillNo
        {
            get { return stockMasterModel.StockMasterBillNo; }
            set
            {
                stockMasterModel.StockMasterBillNo = value;
                OnPropertyChanged("StockBillNo");
            }
        }

        public DateTime StockBillDate
        {
            get { return stockMasterModel.StockMasterBillDate; }
            set
            {
                stockMasterModel.StockMasterBillDate = value;
                OnPropertyChanged("StockBillDate");
            }
        }

        public string StockSupplier
        {
            get { return stockMasterModel.StockMasterSupplier; }
            set
            {
                stockMasterModel.StockMasterSupplier = value;
                OnPropertyChanged("StockSupplier");
            }
        }

        public DateTime StockSuppliedOn
        {
            get { return stockMasterModel.StockMasterSuppliedOn; }
            set
            {
                stockMasterModel.StockMasterSuppliedOn = value;
                OnPropertyChanged("StockSuppliedOn");
            }
        }

        public string StockParticulars
        {
            get { return stockMasterModel.StockMasterParticulars; }
            set
            {
                stockMasterModel.StockMasterParticulars = value;
                OnPropertyChanged("StockParticulars");
            }
        }

        public string StockRemarks
        {
            get { return stockMasterModel.StockMasterRemarks; }
            set
            {
                stockMasterModel.StockMasterRemarks = value;
                OnPropertyChanged("StockRemarks");
            }
        }

        public ObservableCollection<StockDetailModel> StockDetailList
        {
            get { return stockMasterModel.StockDetailList; }
            set
            {
                stockMasterModel.StockDetailList = value;
                OnPropertyChanged("StockDetailList");
            }
        }

        public String SearchKey
        {
            get { return strItemSearchKey; }
            set
            {
                strItemSearchKey = value;
                OnPropertyChanged("SearchKey");
            }
        }

        public ObservableCollection<ItemGroupModel> ItemGroupList
        {
            get { return itemGroupList; }
            set
            {
                itemGroupList = value;
                OnPropertyChanged("ItemGroupList");
            }
        }

        public Xceed.Wpf.Toolkit.WindowState IsAddtemsOpen
        {
            get
            {
                return isAddItemsOpen;
            }
            set
            {
                isAddItemsOpen = value;
                OnPropertyChanged("IsAddtemsOpen");
            }
        }

        public ICommand OpenAddItemsCommand
        {
            get { return this.openAddItemsCmd; }
        }

        public ICommand AddItemsCmd
        {
            get { return this.addItemsCmd; }
        }

        public int SearchSelectedIndex
        {
            get { return searchSelectedIndex; }
            set
            {
                searchSelectedIndex = value;
                OnPropertyChanged("SearchSelectedIndex");
                ChangeGrouporItemVisibility();
            }

        }

        public Visibility GroupVisibility
        {
            get { return groupVisibility; }
            set
            {
                groupVisibility = value;
                OnPropertyChanged("GroupVisibility");
            }
        }

        public Visibility ItemVisibility
        {
            get { return itemVisibility; }
            set
            {
                itemVisibility = value;
                OnPropertyChanged("ItemVisibility");
            }
        }

        public long SelectedItemGroupValue
        {
            get
            {
                return selectedGroupId;
            }
            set
            {
                selectedGroupId = value;
                OnPropertyChanged("SelectedItemGroupValue");
            }
        }

        public ICommand SearchItemCmd
        {
            get { return searchItemCmd; }
        }

        public ObservableCollection<ItemMasterModel> SearchItemList
        {
            get { return searchItemList; }
            set
            {
                searchItemList = value;
                OnPropertyChanged("SearchItemList");
            }
        }

        public ObservableCollection<ItemMasterModel> SelectedItemList
        {
            get
            {
                return this.selectedItemList;
            }
            set
            {
                this.selectedItemList = value;
                OnPropertyChanged("SelectedItemList");
            }
        }


        #endregion

        public void OpenAddItems(object obj)
        {
            this.IsAddtemsOpen = Xceed.Wpf.Toolkit.WindowState.Open;
        }

        public bool CanOpenAddItems(object obj)
        {
            return true;
        }

        public void SearchItem(object obj)
        {
            GetItemSearchResult();
        }

        public bool CanSearchItem(object obj)
        {
            if (this.SearchSelectedIndex == 0)
            {
                if (this.SelectedItemGroupValue > 0)
                {
                    return true;
                }

            }
            else
            {
                if (!String.IsNullOrEmpty(this.SearchKey))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddSelectedItems(object obj)
        {
            ConvertSelectedItemstoStock(obj);
        }

        public bool CanAddSelectedItems(object obj)
        {
            if (obj != null)
            {
                var selectedItems = (System.Collections.IList)obj;
                if (selectedItems != null)
                {
                    return selectedItems.Count > 0;
                }
            }
            return true;
        }

        private void ChangeGrouporItemVisibility()
        {
            if (this.SearchSelectedIndex == 0)
            {
                this.GroupVisibility = Visibility.Visible;
                this.ItemVisibility = Visibility.Collapsed;
            }
            else
            {
                this.GroupVisibility = Visibility.Collapsed;
                this.ItemVisibility = Visibility.Visible;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            //Fire the PropertyChanged event in case somebody subscribed to it
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string validationMessage = string.Empty;
                switch (columnName)
                {
                    case "StockDate":
                        if (string.IsNullOrEmpty(this.StockDate.ToString()))
                        {
                            validationMessage = "Please Select Stock Date";
                        }
                        break;
                    case "StockOrderNo":
                        if (string.IsNullOrEmpty(this.StockOrderNo))
                        {
                            validationMessage = "Please Enter Order Number";
                        }
                        break;
                    case "StockOrderDate":
                        if (string.IsNullOrEmpty(this.StockOrderDate.ToString()))
                        {
                            validationMessage = "Please Select Order Date";
                        }
                        break;
                    case "StockBillNo":
                        if (string.IsNullOrEmpty(this.StockBillNo))
                        {
                            validationMessage = "Please Enter Bill Number";
                        }
                        break;
                    case "StockBillDate":
                        if (string.IsNullOrEmpty(this.StockBillDate.ToString()))
                        {
                            validationMessage = "Please Select Bill Date";
                        }
                        break;
                    case "StockSupplier":
                        if (string.IsNullOrEmpty(this.StockSupplier))
                        {
                            validationMessage = "Please Enter Supplier";
                        }
                        break;
                    case "StockSuppliedOn":
                        if (string.IsNullOrEmpty(this.StockSuppliedOn.ToString()))
                        {
                            validationMessage = "Please Select Supplied Date";
                        }
                        break;
                    default:
                        break;
                }
                return validationMessage;
            }
        }

        private void GetItemSearchResult()
        {
            ObservableCollection<ItemMasterModel> itemList = new ObservableCollection<ItemMasterModel>();
            if (this.SearchSelectedIndex == 0)
            {
                itemList = itemMasterManger.GetItemListbyGroup(this.SelectedItemGroupValue);
            }
            else
            {
                if (!string.IsNullOrEmpty(this.SearchKey))
                {
                    itemList = itemMasterManger.GetItemListbySearch(this.SearchKey);
                }
            }
            if (itemList != null)
            {
                this.SearchItemList = itemList;
                this.SelectedItemList = new ObservableCollection<ItemMasterModel>();
            }
        }

        private void ConvertSelectedItemstoStock(object obj)
        {
            if (obj != null)
            {
                try
                {
                    var selectedItems = (System.Collections.IList)obj;
                    if (selectedItemList != null)
                    {
                        foreach (var selecteItem in selectedItems)
                        {
                            ItemMasterModel itemMaster = (ItemMasterModel)selecteItem;
                            if (itemMaster != null)
                            {
                                if (!CheckItemexitsorNot(itemMaster.ItemMasterId))
                                {
                                    if (this.StockDetailList == null)
                                    {
                                        this.StockDetailList = new ObservableCollection<StockDetailModel>();
                                    }
                                    StockDetailModel stockModel = ConvertItemtoStok(itemMaster);
                                    if (stockModel != null)
                                    {
                                        this.StockDetailList.Add(stockModel);
                                        OnPropertyChanged("StockDetailList");
                                    }
                                }
                            }
                        }
                        this.SearchItemList.Clear();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public StockDetailModel ConvertItemtoStok(ItemMasterModel itemMaster)
        {
            if (itemMaster != null)
            {
                StockDetailModel stockDetailModel = new StockDetailModel { StockDetailItemId = itemMaster.ItemMasterId, StockDetailItem = itemMaster, StockDetailQty = 1, StockDetailUnitPrice = 0, StockDetailPurchaserType = 1, StockDetailTransferType = 1 };
                return stockDetailModel;
            }
            return null;
        }

        public bool CheckItemexitsorNot(long itemId)
        {
            if (this.StockDetailList != null)
            {
                var stockDetailbyItem = this.StockDetailList.Where(a => a.StockDetailItemId == itemId);
                if (stockDetailbyItem != null)
                {
                    return stockDetailbyItem.ToList().Count > 0;
                }
            }
            return false;
        }
    }
}
