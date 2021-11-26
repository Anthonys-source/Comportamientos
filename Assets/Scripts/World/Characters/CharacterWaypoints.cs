using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterWaypoints : MonoBehaviour
{
    [SerializeField] private List<InspectorWaypoint> m_Waypoints = new List<InspectorWaypoint>();
    private Dictionary<ID, Transform> m_WaypointsMap = new Dictionary<ID, Transform>();

    private void Awake()
    {
        for (int i = 0; i < m_Waypoints.Count; i++)
        {
            var waypoint = m_Waypoints[i];
            m_WaypointsMap.Add(waypoint.m_ID, waypoint.m_Transform);
        }
    }

    public Vector3 GetWaypointPosition(ID waypointID)
    {
        return m_WaypointsMap[waypointID].position;
    }

    public struct InspectorWaypoint
    {
        public ID m_ID;
        public Transform m_Transform;

        public InspectorWaypoint(ID ID, Transform transform)
        {
            m_ID = ID;
            m_Transform = transform;
        }
    }
}
