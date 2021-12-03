using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private float m_SpawnDelay = 5.0f;
    [SerializeField] private GameObject m_ItemPrefab;

    private GameObject m_CurrentItem;

    public void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (gameObject.activeInHierarchy)
        {
            var wait = new WaitForSeconds(m_SpawnDelay);
            yield return wait;
            if (m_CurrentItem == null)
                m_CurrentItem = Instantiate(m_ItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
