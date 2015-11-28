using SVSSStoresApp.EFBO;
using SVSSStoresApp.Model;
using SVSSStoresApp.ResourceAccessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace SVSSStoresApp
{
    public class ItemMasterRepository
    {

        private static ObservableCollection<ItemMasterModel> itemMasterList = new ObservableCollection<ItemMasterModel>();
        private static ObservableCollection<ItemGroupModel> itemGroupList = new ObservableCollection<ItemGroupModel>();

        public ItemMasterRepository()
        {

        }

        internal void AddItem(ItemMasterModel itemModel)
        {
            itemModel.IsActive = true;
            itemModel.QtyOnHand = 0;
            svssstores_itemmaster itemMasterEntity = ConvertModeltoEntity(itemModel);
            bool retunResult = DBHelper.SaveItemMaster(itemMasterEntity);
            if (retunResult)
            {
                //this.GetItemList();
            }
            //itemMasterList.Add(itemModel);
        }

        internal void AddItemGroup(ItemGroupModel itemGroupModel)
        {
            svssstores_itemgroup itemGroupEntity = ConvertGroupModeltoGroupEntity(itemGroupModel);
            bool returnResult = DBHelper.SaveItemGroup(itemGroupEntity);

        }

        internal void UpdateItem(ItemMasterModel itemModel)
        {
            if (itemModel.ItemMasterId > 0)
            {
                svssstores_itemmaster itemMasterEntity = DBHelper.GetItemMaterbyId(itemModel.ItemMasterId);
                itemMasterEntity.ItemMaster_ItemCode = itemModel.ItemCode;
                itemMasterEntity.ItemMaster_ItemName = itemModel.ItemMasterName;
                itemMasterEntity.ItemMaster_UOM = itemModel.UOM;
                itemMasterEntity.ItemMaster_UnitPrice = itemModel.UnitPrice;
                if (itemModel.itemGroupId > 0)
                {
                    itemMasterEntity.ItemMaster_GroupId = itemModel.itemGroupId;
                }
                bool retunResult = DBHelper.SaveItemMaster(itemMasterEntity);
                if (retunResult)
                {
                    //this.GetItemList();
                }
            }
        }

        internal void UpdateItemGroup(ItemGroupModel itemGroupModel)
        {
            List<svssstores_itemgroup> itemGroupEntityLilst = DBHelper.GetItemGroupList();
            svssstores_itemgroup findItem = itemGroupEntityLilst.FirstOrDefault(a => a.itemgroup_id == itemGroupModel.ItemGroupId);
            if (findItem != null)
            {
                findItem.itemgroup_groupname = itemGroupModel.ItemGroupName;
                DBHelper.SaveItemGroup(findItem);
            }

        }

        internal ObservableCollection<ItemMasterModel> GetItemList()
        {
            itemMasterList = new ObservableCollection<ItemMasterModel>();
            itemMasterList = GetItemMasterList();
            return itemMasterList;
        }

        internal ObservableCollection<ItemGroupModel> GetIemGroupList()
        {
            itemGroupList = new ObservableCollection<ItemGroupModel>();
            itemGroupList = GetItemGroupList();
            return itemGroupList;
        }

        private int GetIndex(long id)
        {
            int index = -1;
            if (itemMasterList.Count > 0)
            {
                for (int i = 0; i < itemMasterList.Count; i++)
                {
                    if (itemMasterList[i].ItemMasterId == id)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

        private svssstores_itemmaster ConvertModeltoEntity(ItemMasterModel itemModel)
        {
            svssstores_itemmaster itemMasterEntity = new svssstores_itemmaster();
            itemMasterEntity.ItemMaster_ID = itemModel.ItemMasterId;
            itemMasterEntity.ItemMaster_ItemName = itemModel.ItemMasterName;
            itemMasterEntity.ItemMaster_UOM = itemModel.UOM;
            itemMasterEntity.ItemMaster_IsActive = itemModel.IsActive;
            itemMasterEntity.ItemMaster_UnitPrice = itemModel.UnitPrice;
            itemMasterEntity.ItemMaster_QtyOnHand = itemModel.QtyOnHand;
            itemMasterEntity.ItemMaster_ItemCode = itemModel.ItemCode;
            itemMasterEntity.ItemMaster_GroupId = itemModel.itemGroupId;
            return itemMasterEntity;
        }

        private ItemMasterModel ConvertEntitytoModel(svssstores_itemmaster itemMasterEntity)
        {
            ItemMasterModel itemModel = new ItemMasterModel();
            itemModel.ItemMasterId = itemMasterEntity.ItemMaster_ID;
            itemModel.ItemMasterName = itemMasterEntity.ItemMaster_ItemName;
            itemModel.UOM = itemMasterEntity.ItemMaster_UOM;
            itemModel.IsActive = itemMasterEntity.ItemMaster_IsActive.HasValue ? itemMasterEntity.ItemMaster_IsActive.Value : true;
            itemModel.ItemCode = itemMasterEntity.ItemMaster_ItemCode;
            itemModel.UnitPrice = itemMasterEntity.ItemMaster_UnitPrice.HasValue ? itemMasterEntity.ItemMaster_UnitPrice.Value : 0;
            itemModel.QtyOnHand = itemMasterEntity.ItemMaster_QtyOnHand.HasValue ? itemMasterEntity.ItemMaster_QtyOnHand.Value : 0;
            itemModel.itemGroupId = itemMasterEntity.ItemMaster_GroupId.HasValue ? itemMasterEntity.ItemMaster_GroupId.Value : 0;
            if (itemMasterEntity.svssstores_itemgroup != null)
            {
                itemModel.itemGroupModel = ConvertGroupEntitytoGroupGroup(itemMasterEntity.svssstores_itemgroup);
            }
            return itemModel;
        }

        private ObservableCollection<ItemMasterModel> GetItemMasterList()
        {
            ObservableCollection<ItemMasterModel> itemModelList = new ObservableCollection<ItemMasterModel>();
            try
            {
                List<svssstores_itemmaster> itemEnitiyList = DBHelper.GetItemMasterList();
                if (itemEnitiyList != null)
                {
                    itemEnitiyList.ForEach(delegate(svssstores_itemmaster itemEntity)
                    {
                        itemModelList.Add(ConvertEntitytoModel(itemEntity));
                    });
                }
            }
            catch (Exception ex)
            {

            }

            return itemModelList;
        }

        private ObservableCollection<ItemGroupModel> GetItemGroupList()
        {
            ObservableCollection<ItemGroupModel> itemGroupList = new ObservableCollection<ItemGroupModel>();
            List<svssstores_itemgroup> itemGroupEntityLilst = DBHelper.GetItemGroupList();
            if (itemGroupEntityLilst != null)
            {
                itemGroupEntityLilst.ForEach(delegate(svssstores_itemgroup itemGroup)
                {
                    itemGroupList.Add(ConvertGroupEntitytoGroupGroup(itemGroup));
                });
            }
            return itemGroupList;
        }

        private svssstores_itemgroup ConvertGroupModeltoGroupEntity(ItemGroupModel itemGroupModel)
        {
            svssstores_itemgroup itemGroupEntity = new svssstores_itemgroup();
            itemGroupEntity.itemgroup_id = itemGroupModel.ItemGroupId;
            itemGroupEntity.itemgroup_groupname = itemGroupModel.ItemGroupName;
            return itemGroupEntity;
        }

        private ItemGroupModel ConvertGroupEntitytoGroupGroup(svssstores_itemgroup itemGroupEntity)
        {
            ItemGroupModel itemGroupModel = new ItemGroupModel();
            itemGroupModel.ItemGroupId = itemGroupEntity.itemgroup_id;
            itemGroupModel.ItemGroupName = itemGroupEntity.itemgroup_groupname;
            return itemGroupModel;
        }

        internal ObservableCollection<ItemMasterModel> GetItemListbyGroupId(long groupId)
        {
            List<svssstores_itemmaster> itemEnitiyList = DBHelper.GetItemMasterListbyGroupId(groupId);
            var itemMasterListbyGroup = new ObservableCollection<ItemMasterModel>();
            if (itemEnitiyList != null)
            {
                itemEnitiyList.ForEach(delegate(svssstores_itemmaster itemEntity)
                {
                    itemMasterListbyGroup.Add(ConvertEntitytoModel(itemEntity));
                });
            }
            return itemMasterListbyGroup;
        }

        internal ObservableCollection<ItemMasterModel> GetItemListbySearch(string searchKey)
        {
            var itemMasterList = new ObservableCollection<ItemMasterModel>();
            var itemSearchList = DBHelper.GetItemListbyName(searchKey);
            if (itemSearchList != null)
            {
                itemSearchList.ForEach(delegate(svssstores_itemmaster itemEntity)
                {
                    itemMasterList.Add(ConvertEntitytoModel(itemEntity));
                });
            }
            return itemMasterList;
        }
    }

}
