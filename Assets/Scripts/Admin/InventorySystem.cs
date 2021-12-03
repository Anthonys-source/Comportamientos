public class InventorySystem : BaseSystem
{
    private ComponentsContainer<InventoryComponent> m_InventoryComp;

    // Get components and events here
    public override void Initialize(ComponentRegistry c, EventSystem e)
    {
        m_InventoryComp = c.GetComponentsContainer<InventoryComponent>();

        var evt = e.GetGlobal();
        evt.AddEventChannel<InventoryItemEvtArgs>(EventID.ADD_ITEM_TO_INVENTORY).OnInvoked +=
            (args) => AddItemToInventory(args.m_ItemID, args.m_Amount, args.m_InventoryID);
        evt.AddEventChannel<InventoryItemEvtArgs>(EventID.REMOVE_ITEM_FROM_INVENTORY).OnInvoked +=
            (args) => RemoveItemFromInventory(args.m_ItemID, args.m_Amount, args.m_InventoryID);
    }

    // Optional Update Loop
    public override void Update(float timeStep)
    {
    }

    public void AddItemToInventory(ID itemID, int amount, ID inventoryID)
    {
        // GC
        InventoryComponent invComp = m_InventoryComp.GetFromEntity(inventoryID);
        InventoryItem item = invComp.m_ItemsList.Find(i => i.m_ItemID == itemID);
        if (item == null)
            invComp.m_ItemsList.Add(new InventoryItem { m_ItemID = itemID, m_Amount = amount });
        else
            item.m_Amount += amount;
    }

    public void RemoveItemFromInventory(ID itemID, int amount, ID inventoryID)
    {
        // GC
        InventoryComponent invComp = m_InventoryComp.GetFromEntity(inventoryID);
        InventoryItem item = invComp.m_ItemsList.Find(i => i.m_ItemID == itemID);
        if (item != null)
        {
            item.m_Amount -= amount;
            if (item.m_Amount <= 0)
                invComp.m_ItemsList.Remove(item);
        }
    }
}
