using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationalWaypoint : MonoBehaviour 
{

    public RotationalWaypoint nextPoint, leftPoint, rightPoint, backPoint;
    public GameObject midPoint;


	// Use this for initialization
	void Awake () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		TrainController train = other.GetComponent<TrainController>();

		if (train != null)
		{
			float horizontalAxis = 						Input.GetAxis("Horizontal");
			RotationalWaypoint nextWaypoint = 			null;

			if (horizontalAxis < 0 && leftPoint != null)
				nextWaypoint = 							leftPoint;
			else if (horizontalAxis > 0 && rightPoint != null)
				nextWaypoint = 							rightPoint;
			else 
				nextWaypoint = 							nextPoint;

			train.waypointTarget = 						nextWaypoint;
		}
		/*
		Transform otherTrans = 			other.transform;

		Vector3 rotToApply = transform.rotation.eulerAngles;
		rotToApply.y -= 				90;

		otherTrans.rotation = 			Quaternion.Euler(rotToApply);
		*/
		//PlayerMovement pm = other.GetComponent<PlayerMovement>();
		//pm.HandleAutomaticMovement();
	}
}
