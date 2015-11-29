using SVSSStoresApp.EFBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SVSSStoresApp.ResourceAccessLayer
{
    public class DBHelper
    {
        private static SVSSStoresEntities dbContext = new SVSSStoresEntities();

        
        public DBHelper()
        {
            if (dbContext != null)
            {
                dbContext.Database.CreateIfNotExists();
            }
        }

        public static bool SaveItemMaster(svssstores_itemmaster itemMaster)
        {
            if (itemMaster != null)
            {
                if (!(itemMaster.ItemMaster_ID > 0))
                {
                    dbContext.svssstores_itemmaster.Add(itemMaster);
                }
                dbContext.SaveChanges();

            }
            return true;
        }

        public static bool SaveItemGroup(svssstores_itemgroup itemGroup)
        {
            if (itemGroup != null)
            {
                if (!(itemGroup.itemgroup_id > 0))
                {
                    dbContext.svssstores_itemgroup.Add(itemGroup);
                }
                dbContext.SaveChanges();
            }
            return true;
        }

        public static svssstores_itemmaster GetItemMaterbyId(long itemMasterId)
        {
            svssstores_itemmaster itemMaster = dbContext.svssstores_itemmaster.FirstOrDefault(s => s.ItemMaster_ID == itemMasterId);
            return itemMaster;
        }

        public static List<svssstores_itemmaster> GetItemMasterList()
        {

            if (dbContext.svssstores_itemmaster != null)
            {

                return dbContext.svssstores_itemmaster.ToList();
            }
            return null;
        }

        public static List<svssstores_itemmaster> GetItemMasterListbyGroupId(long groupId)
        {

            if (dbContext.svssstores_itemmaster != null)
            {

                var  itemList=dbContext.svssstores_itemmaster.Where(a => a.ItemMaster_GroupId == groupId);
                if (itemList != null)
                {
                   return itemList.ToList();
                }
            }
            return null;
        }

        public static List<svssstores_itemgroup> GetItemGroupList()
        {
            if (dbContext.svssstores_itemgroup != null)
            {
                return dbContext.svssstores_itemgroup.ToList();
            }
            return null;
        }

        public static List<svssstores_itemmaster> GetItemListbyName(String nameLike)
        {
            List<svssstores_itemmaster> itemReturnList = new List<svssstores_itemmaster>();
            var itemList = dbContext.svssstores_itemmaster.Where(a => (a.ItemMaster_ItemName.Contains(nameLike) || a.ItemMaster_ItemCode.Contains(nameLike)));
            if (itemList != null)
            {
                return itemList.ToList();
            }
            return itemReturnList;
        }


       
    }
}
