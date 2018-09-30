using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TrainController : MonoBehaviour3D 
{
	[SerializeField] float currentSpeed = 	0;
	[SerializeField] float maxSpeed = 		5f;
	[SerializeField] float accelRate = 		3f;
	[SerializeField] float offense = 		1f;
	[SerializeField] float defense = 		1f;
	[SerializeField] Collider _hurtBox;
	[SerializeField] Collider _hitbox;

	// Properties
	public Collider hurtBox { get { return _hurtBox; } }
	public Collider hitBox { get { return _hitbox; } }

	new public Rigidbody rigidbody;
	bool dying;

	// Use this for initialization
	protected virtual void Awake () 
	{
		rigidbody = 						GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		// When the hurtbox gets hit by a hitbox, the one with the hurtbox dies. The one dying 
		// has its colliders disabled.

		TrainController otherTrain = 			other.GetComponent<TrainController>();
		if (otherTrain != null)
			HandleTrainCollision(otherTrain, other);
	}

	protected virtual void HandleTrainCollision(TrainController otherTrain, Collider collidedWith)
	{
		
	}

}
