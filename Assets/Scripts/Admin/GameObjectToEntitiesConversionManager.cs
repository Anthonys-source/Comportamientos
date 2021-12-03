using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates Components for all the gameobject entities in the world
public class GameObjectToEntitiesConversionManager : MonoBehaviour
{
    public static GameObjectToEntitiesConversionManager s_Instance;

    [SerializeField] private List<CharacterEntity> m_CharacterEntities = new List<CharacterEntity>();
    [SerializeField] private List<ItemEntity> m_ItemEntities = new List<ItemEntity>();


    public void Initialize(ComponentRegistry reg)
    {
        s_Instance = this;

        // Init Character Entities
        for (int i = 0; i < m_CharacterEntities.Count; i++)
        {
            var c = m_CharacterEntities[i];
            var moodCont = reg.GetComponentsContainer<MoodComponent>();
            var charCont = reg.GetComponentsContainer<CharacterComponent>();
            var invCont = reg.GetComponentsContainer<InventoryComponent>();
            var bakers = reg.GetSingletonComponent<BakersComponent>();

            ID instanceID = c.GetComponent<EntityID>().GetInstID();

            MoodComponent moodComp = new MoodComponent();
            moodComp.m_ID = instanceID;
            CharacterComponent charComp = new CharacterComponent();
            charComp.m_ID = instanceID;
            InventoryComponent invComp = new InventoryComponent();
            invComp.m_ID = instanceID;

            moodCont.Add(instanceID, moodComp);
            charCont.Add(instanceID, charComp);
            invCont.Add(instanceID, invComp);
            bakers.m_AllBakersList.Add(instanceID);
        }

        //// Init Item Entities
        //for (int i = 0; i < m_ItemEntities.Count; i++)
        //{
        //    var c = m_ItemEntities[i];
        //    var itmCont = reg.GetComponentsContainer<ItemTypeComponent>();

        //    ID instanceID = c.GetComponent<EntityID>().GetInstID();

        //    ItemTypeComponent itmComp = new ItemTypeComponent();
        //    itmComp.m_ID = instanceID;

        //    itmCont.Add(instanceID, itmComp);
        //}
    }

    public static GameObjectToEntitiesConversionManager GetInst()
    {
        return s_Instance;
    }
}
