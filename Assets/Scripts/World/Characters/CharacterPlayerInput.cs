using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterActionsController))]
public class CharacterPlayerInput : MonoBehaviour
{
    private Camera _cam;
    private CharacterActionsController _charActions;


    private void Awake()
    {
        _charActions = GetComponent<CharacterActionsController>();
        _cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.name == "Ground")
                {
                    _charActions.MoveTo(hits[i].point, 1);
                    _charActions.InteractWithClosest();
                    break;
                }
            }
        }
    }
}
