using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingsSystem : MonoBehaviour
{
    private ComponentsContainer<BuildingZoneComponent> _buildingZoneComponents;


    public void Initialize()
    {
        _buildingZoneComponents = ComponentsRegistry.GetInst().GetComponentsContainer<BuildingZoneComponent>();
    }
}
