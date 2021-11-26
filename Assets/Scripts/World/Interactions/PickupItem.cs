using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableBehaviour))]
public class PickupItem : MonoBehaviour
{
    [SerializeField] private string m_ItemIDName;

    private InteractableBehaviour m_Interactable;
    private EventChannel<InventoryItemEvtArgs> m_AddItemEventChannel;

    private void Awake()
    {
        m_Interactable = GetComponent<InteractableBehaviour>();
        m_Interactable.OnInteraction += Pickup;
        GameEventSystem.GetInst().GetGlobalEventSystem().GetEventChannel(new ID("add_item_to_inventory"), out m_AddItemEventChannel);
    }

    private void Pickup(ID InteracterID)
    {
        m_AddItemEventChannel.Invoke(new InventoryItemEvtArgs { m_Amount = 1, m_InventoryID = InteracterID, m_ItemID = new ID(m_ItemIDName) });
    }
}
