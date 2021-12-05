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
            m_WaypointsMap.Add(new ID(waypoint.m_IDName), waypoint.m_Transform);
        }
    }

    public Vector3 GetWaypointPosition(ID waypointID)
    {
        return m_WaypointsMap[waypointID].position;
    }


    [System.Serializable]
    public struct InspectorWaypoint
    {
        public string m_IDName;
        public Transform m_Transform;

        public InspectorWaypoint(string IDName, Transform transform)
        {
            m_IDName = IDName;
            m_Transform = transform;
        }
    }
}

public static class WaypointID
{
    public static readonly ID BAKERY = new ID("bakery");
    public static readonly ID BAKERY1 = new ID("bakery_1");
    public static readonly ID BAKERY2 = new ID("bakery_2");
    public static readonly ID BAKERY3 = new ID("bakery_3");
    public static readonly ID FACTORY = new ID("factory");
    public static readonly ID BAR = new ID("bar");
    public static readonly ID HOUSE = new ID("house");
    public static readonly ID SCHOOL = new ID("school");
    public static readonly ID CAFE = new ID("cafe");
}