using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystemDebugger : MonoBehaviour
{
    [SerializeField] private string m_ItemIDName;
    [SerializeField] private string m_InventoryIDName;

    private EventChannel<InventoryItemEvtArgs> m_AddItemEventChannel;

    private void Awake()
    {
        EventSystem.GetInst().GetGlobal().GetEventChannel(new ID("add_item_to_inventory"), out m_AddItemEventChannel);
    }

    public void AddItem()
    {
        m_AddItemEventChannel.Invoke(new InventoryItemEvtArgs { m_ItemID = new ID(m_ItemIDName), m_Amount = 1, m_InventoryID = new ID(m_InventoryIDName) });
    }
}
