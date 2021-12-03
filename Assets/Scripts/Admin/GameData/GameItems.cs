using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameItems
{
    public static List<ItemTypeComponent> GetItemTypes()
    {
        List<ItemTypeComponent> i = new List<ItemTypeComponent>();

        // Bakery Items
        AddItemType(ItemID.BREAD, "Bread");
        AddItemType(ItemID.FLOUR, "Flour");
        AddItemType(ItemID.YEAST, "Yeast");
        AddItemType(ItemID.WATER, "Water");

        return i;

        void AddItemType(ID itemID, string itemName)
        {
            ItemTypeComponent c = new ItemTypeComponent();
            c.m_ID = itemID;
            c.m_Name = itemName;
            i.Add(c);
        }
    }
}
