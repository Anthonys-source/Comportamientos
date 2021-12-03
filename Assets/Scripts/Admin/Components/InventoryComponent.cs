using System;
using System.Collections.Generic;

[Serializable]
public class InventoryComponent
{
    public ID m_ID;
    public List<InventoryItem> m_ItemsList = new List<InventoryItem>();

    public bool HasItem(ID itemID, out InventoryItem item)
    {
        item = m_ItemsList.Find((a) => a.m_ItemID == itemID);

        if (item == null)
            return false;
        else
            return true;
    }
}

[Serializable]
public class InventoryItem
{
    public ID m_ItemID;
    public int m_Amount;
}