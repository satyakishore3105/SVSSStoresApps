using SVSSStoresApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SVSSStoresApp.BusinessLogicLayer;
using System.Windows;

namespace SVSSStoresApp.ViewModel
{
    public class ItemMasterViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly ItemMasterModel itemMaster;
        private readonly ItemMasterManager itemMasterManger;
        private ObservableCollection<ItemMasterModel> itemMasterList;
        private ObservableCollection<ItemGroupModel> itemGroupList;
        private readonly ICommand saveItemCmd;
        private readonly ICommand clearItemCmd;
        private readonly ICommand addItemGroupCmd;
        private readonly ICommand clearItemGroupCmd;
        private readonly ICommand openGroupCmd;
        private Xceed.Wpf.Toolkit.WindowState isItemgroupOpen = Xceed.Wpf.Toolkit.WindowState.Closed;
        private readonly ItemGroupModel itemGroup;

        public ItemMasterViewModel()
        {

            itemMaster = new ItemMasterModel();
            itemMasterManger = new ItemMasterManager();
            itemGroup = new ItemGroupModel();
            itemMasterList = new ObservableCollection<ItemMasterModel>();
            saveItemCmd = new RelayCommand(Save, CanSave);
            clearItemCmd = new RelayCommand(ClearItemDetails, CanClearItemDetails);
            openGroupCmd = new RelayCommand(OpenPOPupView, CanOpenPOPupView);
            addItemGroupCmd = new RelayCommand(SaveGroup, CanSaveGroup);
            clearItemGroupCmd = new RelayCommand(SaveGroup, CanSaveGroup);
            itemMasterList = itemMasterManger.GetItemList();
            itemGroupList = itemMasterManger.GetIemGroupList();

            this.UnitPrice = 0;
        }

        public long ItemId
        {
            get { return itemMaster.ItemMasterId; }
            set
            {
                itemMaster.ItemMasterId = value;
                OnPropertyChanged("ItemId");
            }
        }

        public String ItemCode
        {
            get { return itemMaster.ItemCode; }
            set
            {
                itemMaster.ItemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }

        public String ItemName
        {
            get { return itemMaster.ItemMasterName; }
            set
            {
                itemMaster.ItemMasterName = value;
                OnPropertyChanged("ItemName");
            }
        }

        public String UOM
        {
            get
            {
                return itemMaster.UOM;
            }
            set
            {
                itemMaster.UOM = value;
                OnPropertyChanged("UOM");
            }
        }

        public Xceed.Wpf.Toolkit.WindowState IsItemgroupOpen
        {
            get
            {
                return isItemgroupOpen;
            }
            set
            {
                isItemgroupOpen = value;
                OnPropertyChanged("IsItemgroupOpen");
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return itemMaster.UnitPrice;
            }
            set
            {
                itemMaster.UnitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        public long SelectedItemGroupValue
        {
            get
            {
                return itemMaster.itemGroupId;
            }
            set
            {
                itemMaster.itemGroupId = value;
                OnPropertyChanged("SelectedItemGroupValue");
            }
        }

        public long ItemGroupId
        {
            get { return itemGroup.ItemGroupId; }
            set
            {
                itemGroup.ItemGroupId = value;
                OnPropertyChanged("ItemGroupId");
            }
        }

        public String ItemGroupName
        {
            get { return itemGroup.ItemGroupName; }
            set
            {
                itemGroup.ItemGroupName = value;
                OnPropertyChanged("ItemGroupName");
            }
        }

        public ItemMasterModel SelectedItem
        {
            set
            {
                if (value != null)
                {
                    this.ItemId = value.ItemMasterId;
                    this.ItemName = value.ItemMasterName;
                    this.UOM = value.UOM;
                    this.ItemCode = value.ItemCode;
                    this.UnitPrice = value.UnitPrice;
                    this.SelectedItemGroupValue = value.itemGroupId;
                }
            }
        }

        public ItemGroupModel SelectedItemGroup
        {
            set
            {
                if (value != null)
                {
                    this.ItemGroupId = value.ItemGroupId;
                    this.ItemGroupName = value.ItemGroupName;
                }
            }
        }

        public ObservableCollection<ItemMasterModel> ItemMasterList
        {
            get
            {
                return this.itemMasterList;
            }
            set
            {
                this.itemMasterList = value;
                OnPropertyChanged("ItemMasterList");
            }
        }

        public ObservableCollection<ItemGroupModel> ItemGroupList
        {
            get
            {
                return this.itemGroupList;
            }
            set
            {
                this.itemGroupList = value;
                OnPropertyChanged("ItemGroupList");
            }
        }

        public ICommand SaveItemCmd
        {
            get { return saveItemCmd; }
        }

        public ICommand ClearItemCmd
        {
            get { return clearItemCmd; }
        }

        public ICommand AddItemGroupCmd
        {
            get { return addItemGroupCmd; }
        }

        public ICommand OpenGroupCmd
        {
            get { return openGroupCmd; }
        }

        public ICommand ClearGroupCmd
        {
            get { return clearItemCmd; }
        }

        public bool CanSave(object obj)
        {

            return string.IsNullOrEmpty(this.Error);
        }

        public void Save(object obj)
        {
            var itemMaster = new ItemMasterModel { ItemMasterId = this.ItemId, ItemMasterName = this.ItemName, UOM = this.UOM, ItemCode = this.ItemCode, UnitPrice = this.UnitPrice, itemGroupId=this.SelectedItemGroupValue };
            if (itemMasterManger.SaveItem(itemMaster))
            {
                this.ItemMasterList = (ObservableCollection<ItemMasterModel>)itemMasterManger.GetItemList();
                ClearValue();
                MessageBox.Show("Item Details Saved Successfully");
            }
            else
            {
                MessageBox.Show("Unable to Save Item  Details");
            }
        }

        public void SaveGroup(object obj)
        {
            var itemGroupDet = new ItemGroupModel { ItemGroupId = this.ItemGroupId, ItemGroupName = this.ItemGroupName };
            if (itemMasterManger.SaveItemGroup(itemGroupDet))
            {
                this.ItemGroupList = itemMasterManger.GetIemGroupList();
                MessageBox.Show("Group Saved Successfully");
                this.IsItemgroupOpen = Xceed.Wpf.Toolkit.WindowState.Closed;
                ClearGroupDetails();
            }
            else
            {
                MessageBox.Show("Unable to Save Group");
            }

        }

        public bool CanSaveGroup(object obj)
        {
            return !string.IsNullOrEmpty(this.ItemGroupName);
        }

        public void ClearItemDetails(object obj)
        {
            ClearValue();
        }

        private void ClearValue()
        {
            this.ItemId = 0;
            this.ItemName = string.Empty;
            this.UOM = string.Empty;
            this.ItemCode = string.Empty;
            this.UnitPrice = 0;
            this.SelectedItemGroupValue = 0;
        }

        private void ClearGroupDetails()
        {
            this.ItemGroupId = 0;
            this.ItemGroupName = string.Empty;
        }

        private bool CanClearItemDetails(object obj)
        {
            return true;
        }

        public void ClearGroup(object obj)
        {
            ClearGroupDetails();
        }

        public bool CanClearGroup(object obj)
        {
            return true;
        }

        public void OpenPOPupView(object obj)
        {
            this.IsItemgroupOpen = Xceed.Wpf.Toolkit.WindowState.Open;
        }

        public bool CanOpenPOPupView(object obj)
        {

            return IsItemgroupOpen == Xceed.Wpf.Toolkit.WindowState.Closed ? true : false;
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
            get
            {
                string error = string.Empty;
                error = this["ItemName"];
                if (string.IsNullOrEmpty(error))
                {
                    error = this["UOM"];
                }
                if (string.IsNullOrEmpty(error))
                {
                    error = this["ItemCode"];
                }
                if (string.IsNullOrEmpty(error))
                {
                    error = this["SelectedItemGroupValue"];
                }
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string validationMessage = null;
                switch (columnName)
                {
                    case "ItemName":
                        if (string.IsNullOrEmpty(this.ItemName))
                        {
                            validationMessage = "Item Name Should not Empty";
                        }
                        break;
                    case "UOM":
                        if (string.IsNullOrEmpty(this.UOM))
                        {
                            validationMessage = "UOM Should not empty";
                        }
                        break;
                    case "ItemCode":
                        if (string.IsNullOrEmpty(this.ItemCode))
                        {
                            validationMessage = "Item Code Should not Empty";
                        }
                        break;
                    case "UnitPrice":
                        if (string.IsNullOrEmpty(this.UnitPrice.ToString()))
                        {
                            validationMessage = "Unit Should not Empty";
                        }
                        break;
                    case "SelectedItemGroupValue":
                        if (! (this.SelectedItemGroupValue>0))
                        {
                            validationMessage = "Please Select Group";
                        }
                        break;
                }

                return validationMessage;
            }
        }
    }

}
