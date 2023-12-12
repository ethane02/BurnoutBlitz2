using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointContainer : MonoBehaviour
{
    //List of waypoionts 
    public List<Transform> waypoints;
    // Start is called before the first frame update
    void Awake()
    {
        //Put each waypoint in waypoint container in list
        foreach(Transform tr in gameObject.GetComponentsInChildren<Transform>()){
            waypoints.Add(tr);
        }
        //Remove first way point because parent is child of itself
        waypoints.Remove(waypoints[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
