﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationalWaypoint : MonoBehaviour 
{

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
		Transform otherTrans = 			other.transform;

		Vector3 rotToApply = 			transform.rotation.eulerAngles;
		rotToApply.y -= 				90;

		otherTrans.rotation = 			Quaternion.Euler(rotToApply);

		//PlayerMovement pm = other.GetComponent<PlayerMovement>();
		//pm.HandleAutomaticMovement();
	}
}
