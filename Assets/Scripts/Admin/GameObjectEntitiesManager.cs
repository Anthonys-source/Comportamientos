using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectEntitiesManager : MonoBehaviour
{
    public static GameObjectEntitiesManager s_Instance;

    [SerializeField] private List<CharacterEntity> m_CharacterEntities = new List<CharacterEntity>();
    [SerializeField] private List<ItemEntity> m_ItemEntities = new List<ItemEntity>();


    public void Initialize(ComponentsRegistry reg)
    {
        s_Instance = this;

        // Init Character Entities
        for (int i = 0; i < m_CharacterEntities.Count; i++)
        {
            var c = m_CharacterEntities[i];
            var moodCont = reg.GetComponentsContainer<MoodComponent>();
            var charCont = reg.GetComponentsContainer<CharacterComponent>();
            var invCont = reg.GetComponentsContainer<InventoryComponent>();

            ID entityID = new ID(c.CharacterNameID);

            MoodComponent moodComp = new MoodComponent();
            moodComp.m_ID = entityID;
            CharacterComponent charComp = new CharacterComponent();
            charComp.m_ID = entityID;
            InventoryComponent invComp = new InventoryComponent();
            invComp.m_ID = entityID;

            moodCont.Add(entityID, moodComp);
            charCont.Add(entityID, charComp);
            invCont.Add(entityID, invComp);
        }

        // Init Item Entities
        for (int i = 0; i < m_ItemEntities.Count; i++)
        {
            var c = m_ItemEntities[i];
            var itmCont = reg.GetComponentsContainer<ItemComponent>();

            ID entityID = new ID(c.m_ItemNameID);

            ItemComponent itmComp = new ItemComponent();
            itmComp.m_ID = entityID;

            itmCont.Add(entityID, itmComp);
        }
    }

    public static GameObjectEntitiesManager GetInst()
    {
        return s_Instance;
    }
}
