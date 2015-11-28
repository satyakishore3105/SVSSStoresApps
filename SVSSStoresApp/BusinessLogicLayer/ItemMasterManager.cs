using SVSSStoresApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SVSSStoresApp.BusinessLogicLayer
{
    public class ItemMasterManager
    {
        readonly ItemMasterRepository itemRepositry;

        public ItemMasterManager()
        {
            itemRepositry = new ItemMasterRepository();
        }

        public bool SaveItem(ItemMasterModel itemModel)
        {
            if (itemModel.ItemMasterId > 0)
            {
                itemRepositry.UpdateItem(itemModel);
            }
            else
            {
                itemRepositry.AddItem(itemModel);
            }
            return true;
        }

        public ObservableCollection<ItemMasterModel> GetItemList()
        {
            return itemRepositry.GetItemList();
        }

        public bool SaveItemGroup(ItemGroupModel itemGroupModel)
        {
            if (itemGroupModel.ItemGroupId > 0)
            {
                itemRepositry.UpdateItemGroup(itemGroupModel);
            }
            else
            {
                itemRepositry.AddItemGroup(itemGroupModel);
            }
            return true;
        }

        public ObservableCollection<ItemGroupModel> GetIemGroupList()
        {
            return itemRepositry.GetIemGroupList();
        }

        public ObservableCollection<ItemMasterModel> GetItemListbyGroup(long groupId)
        {
            return itemRepositry.GetItemListbyGroupId(groupId);
        }
        public ObservableCollection<ItemMasterModel> GetItemListbySearch(string searchKey)
        {
            return itemRepositry.GetItemListbySearch(searchKey);
        }
    }
}
