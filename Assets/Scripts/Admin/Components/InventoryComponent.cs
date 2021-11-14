using System;
using System.Collections.Generic;

[Serializable]
public class InventoryComponent
{
    public ID m_ID;
    public List<Item> m_Items;

    [Serializable]
    public struct Item
    {
        public ID m_ItemID;
        public int m_Ammount;
    }
}
