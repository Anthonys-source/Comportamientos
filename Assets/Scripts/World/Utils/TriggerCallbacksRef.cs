using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCallbacksRef : MonoBehaviour
{
    public event Action<Collider> OnTriggerEnterCallback;
    public event Action<Collider> OnTriggerExitCallback;


    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterCallback?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitCallback?.Invoke(other);
    }
}
