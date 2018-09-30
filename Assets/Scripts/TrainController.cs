using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public abstract class TrainController : MonoBehaviour3D 
{
	[SerializeField] float _currentSpeed = 		1;
	[SerializeField] float _maxSpeed = 			5f;
	[SerializeField] float _accelRate = 		3f;
	[SerializeField] float _offense = 			1f;
	[SerializeField] float _defense = 			1f;
	[SerializeField] TrainBox _hurtBox;
	[SerializeField] TrainBox _hitbox;
	[SerializeField] RotationalWaypoint firstWaypoint;

	RotationalWaypoint lastWaypointReached;
	[SerializeField] RotationalWaypoint _waypointTarget;
	public RotationalWaypoint waypointTarget
	{
		get { return _waypointTarget; }
		set { _waypointTarget = value; }
	}

	// Properties
	public virtual float currentSpeed
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
	public float accelRate
	{
		get 							{ return _accelRate; }
		set 							{ _accelRate = value; }
	}

	public TrainBox hurtBox 			{ get { return _hurtBox; } }
	public TrainBox hitBox 				{ get { return _hitbox; } }

	bool dying;
	protected NavMeshAgent navMeshAgent;

	protected override void Awake()
	{
		base.Awake();
		navMeshAgent = 					GetComponent<NavMeshAgent>();
		navMeshAgent.speed = 			currentSpeed;
		waypointTarget = 				firstWaypoint;
	}
	
	protected virtual void Start()
	{
		SetupCallbacks();
	}

	// Update is called once per frame
	protected virtual void Update () 
	{
		HandleAutomaticMovement();
	}

	void OnHitboxCollision(Collider other)
	{
		TrainBox otherBox = 						other.GetComponent<TrainBox>();

		if (otherBox != null)
		{
			TrainController otherTrain = 			otherBox.parentTrain;

			bool killOtherTrain = 					otherBox.type == TrainBox.Type.hurt;

			if (killOtherTrain)
				otherTrain.Die();
		}
	}

	public void HandleAutomaticMovement()
	{
		// The train just keeps moving forward all on its own.
		if (navMeshAgent.destination != waypointTarget.transform.position)
			navMeshAgent.SetDestination(waypointTarget.transform.position);
	}

	

	public void Die()
	{
		hurtBox.collider.enabled =		 			false;
		hitBox.collider.enabled = 					false;
		Destroy(this.gameObject);
	}

	void SetupCallbacks()
	{
		hitBox.contactEvents.TriggerEnter.AddListener(OnHitboxCollision);
	}

}
