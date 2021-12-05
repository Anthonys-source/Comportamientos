using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EstaEnFabricaPerception : Perception {

    //Evaluates wether it should fire this perception or not
    public bool Check(GameObject deliveryman)
    {
        return deliveryman.GetComponent<Repartidor>().getInFactory();
    }

    //Called when the transition launches to restore any variables (if you need to)
    public override void Reset()
    {
        return;
    }
}