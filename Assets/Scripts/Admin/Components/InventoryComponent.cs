using System;
using System.Collections.Generic;

[Serializable]
public class InventoryComponent
{
    public ID m_ID;
    public List<InventoryItem> m_ItemsList = new List<InventoryItem>();
}

[Serializable]
public class InventoryItem
{
    public ID m_ItemID;
    public int m_Amount;
}