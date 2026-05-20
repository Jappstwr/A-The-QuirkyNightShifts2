using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [Tooltip("Camera index this waypoint belongs to")]
    public int roomIndex;

    [Tooltip("Layer name for this room")]
    public string roomLayer;

    public bool isDoorWaypoint;
    public bool isBackHallwayPoint;
    public bool isHallwayPoint; 
}
