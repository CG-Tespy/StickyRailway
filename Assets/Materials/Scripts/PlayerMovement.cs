using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour 
{
	new public Transform transform 					{ get; protected set; }
	new public Rigidbody rigidbody 					{ get; protected set; }
	[SerializeField] float moveSpeed = 				5;
	[SerializeField] float accelRate = 				1;
	[SerializeField] float speedLimit = 			10;
	[SerializeField] float steerSpeed = 			10; // in degrees per second

	float currentSpeed 								{ get{ return rigidbody.velocity.magnitude; } }

	// Use this for initialization
	void Awake () 
	{
		transform = 								GetComponent<Transform>();
		rigidbody = 								GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleAutomaticMovement();
		//HandleAcceleration();
		HandleTurning();
	}

	public void HandleAutomaticMovement()
	{
		// The train just keeps moving forward all on its own.

		//rigidbody.AddForce(moveForce);
		rigidbody.velocity = 						transform.forward * moveSpeed;
	}

	void HandleTurning()
	{
		// The player can steer the train.
		float steerAxis = 							Input.GetAxis("Steer");

		if (steerAxis != 0)
		{
			

			/*
			Vector3 newRotation = 					transform.rotation.eulerAngles;
			newRotation.y += 						steerAxis * steerSpeed * Time.deltaTime;

			transform.rotation = 					Quaternion.Euler(newRotation);
			*/
		}
	}

	void HandleAcceleration()
	{
		float accelAxis = Input.GetAxis("Accelerate");

		if (accelAxis != 0)
		{
			moveSpeed += accelAxis * accelRate * Time.deltaTime;
		}
	}
}
