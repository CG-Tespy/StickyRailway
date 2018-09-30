using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
    public TrainController playerTrain;
    public RotationalWaypoint baseWaypoint, targetWaypoint, newWaypoint;
    // Use this for initialization
    void Update () {
        baseWaypoint = playerTrain.baseWaypoint;
        targetWaypoint = playerTrain.waypointTarget;
	}

    // Update is called once per frame
    void OnTriggerStay (Collider entrance) {
        if (entrance.gameObject != baseWaypoint.gameObject &&
            entrance.gameObject != targetWaypoint.gameObject &&
            entrance.gameObject.tag == "Waypoint") {
            baseWaypoint = entrance.gameObject.GetComponent<RotationalWaypoint>();
            targetWaypoint = baseWaypoint.nextPoint;
            Debug.Log("HitWayPT!");
            playerTrain.baseWaypoint = newWaypoint;
        }
    }
}
