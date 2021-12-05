using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singleton_Bakery_Queue: MonoBehaviour
{
    #region Singleton
    private static Singleton_Bakery_Queue _instance;

    public static Singleton_Bakery_Queue Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("More than one Singleton Bakery Queue instance!");
            Destroy(gameObject);
        }
        else
            _instance = this;
    }

    #endregion

    [SerializeField] private List<GameObject> charactersInQueue = new List<GameObject>();
    [SerializeField] private Transform initialPos;
    [SerializeField] private Vector3 queueOffset;

    public int maxSize = 2;

    public bool EnterQueue(GameObject newObj)
    {
        if (!CanQueue()) return false;

        CharacterActions actions = newObj.GetComponent<CharacterActions>();

        if (actions == null) return false;

        if (charactersInQueue.Count == 0)
            actions.MoveTo(initialPos.position);
        else
            actions.MoveTo(charactersInQueue[charactersInQueue.Count - 1].transform.position + queueOffset);

        charactersInQueue.Add(newObj);

        return true;
    }

    private bool CanQueue()
    {
        return charactersInQueue.Count < maxSize;
    }

    public void AdvanceQueue()
    {
        foreach(GameObject obj in charactersInQueue)
        {
            obj.GetComponent<CharacterActions>().MoveTo(obj.transform.position + queueOffset);
        }
    }
}
