using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BuildingZone : MonoBehaviour
{
    private BoxCollider _bc;


    private void Awake()
    {
        _bc = GetComponent<BoxCollider>();
    }


}
