using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TrainController : MonoBehaviour3D 
{
	[SerializeField] float _currentSpeed = 		1;
	[SerializeField] float _maxSpeed = 			5f;
	[SerializeField] float _accelRate = 		3f;
	[SerializeField] float _offense = 			1f;
	[SerializeField] float _defense = 			1f;
	[SerializeField] TrainBox _hurtBox;
	[SerializeField] TrainBox _hitbox;

	// Properties
	public float currentSpeed
	{
		get 							{ return _currentSpeed; }
		set 							{ _currentSpeed = Mathf.Clamp(value, 0, _maxSpeed); }
	}

	public float offense
	{
		get 							{ return _offense; }
		set 							{ _offense = value;}
	}

	public float defense 
	{
		get 							{ return _defense; }
		set 							{ _defense = value; }
	}
	public TrainBox hurtBox 			{ get { return _hurtBox; } }
	public TrainBox hitBox 				{ get { return _hitbox; } }

	bool dying;
	
	void Start()
	{
		SetupCallbacks();
	}

	// Update is called once per frame
	void Update () 
	{
		HandleAutomaticMovement();
		HandleAcceleration();
	}

	void OnHitboxCollision(Collider other)
	{
		TrainBox otherBox = other.GetComponent<TrainBox>();

		if (otherBox != null)
		{
			TrainController otherTrain = 	otherBox.parentTrain;

			bool killOtherTrain = 			otherBox.type == TrainBox.Type.hurt;

			if (killOtherTrain)
				otherTrain.Die();
		}
	}

	void HandleAutomaticMovement()
	{
		rigidbody.velocity = 				transform.forward * currentSpeed;
	}

	void HandleAcceleration()
	{
		// Let the player accelerate or decelerate the train
		float steerAxis = 					Input.GetAxis("Steer");

		currentSpeed += 					steerAxis * Time.deltaTime;
	}

	public void Die()
	{
		hurtBox.collider.enabled =		 	false;
		hitBox.collider.enabled = 			false;
		Destroy(this.gameObject);
	}

	void SetupCallbacks()
	{
		hitBox.contactEvents.TriggerEnter.AddListener(OnHitboxCollision);
	}

}
