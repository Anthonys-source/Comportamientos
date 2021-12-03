using System;

// Defines all the game persistent data and loads it at startup
public class GameDataLoader
{
    public void LoadData()
    {
        var itemTypes = GameItems.GetItemTypes();
        var itemTypesComp = ComponentRegistry.GetInst().GetComponentsContainer<ItemTypeComponent>();

        // Load Item Types
        for (int i = 0; i < itemTypes.Count; i++)
            itemTypesComp.Add(itemTypes[i].m_ID, itemTypes[i]);
    }
}