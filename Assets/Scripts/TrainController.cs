using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class TrainEvent : UnityEvent<TrainController> {}

public abstract class TrainController : MonoBehaviour3D 
{
	public TrainEvent Died 					{ get; protected set; }
	[SerializeField] float _currentSpeed = 		1;
	[SerializeField] float _maxSpeed = 			5f;
	[SerializeField] float _accelRate = 		3f;
	[SerializeField] float _offense = 			1f;
	[SerializeField] float _defense = 			1f;
	[SerializeField] TrainBox _hurtBox;
	[SerializeField] TrainBox _hitbox;
	[SerializeField] RotationalWaypoint firstWaypoint;
    public RotationalWaypoint baseWaypoint;
    public Sensor sensor;

	RotationalWaypoint lastWaypointReached;
	[SerializeField] RotationalWaypoint _waypointTarget;

	protected RotationalWaypoint nextTarget;
	public RotationalWaypoint waypointTarget
	{
		get { return _waypointTarget; }
		set { _waypointTarget = value; }
	}

	protected Vector3 hitBaseSize, hurtBaseSize;
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
		Died = 							new TrainEvent();
		navMeshAgent = 					GetComponent<NavMeshAgent>();
		navMeshAgent.speed = 			currentSpeed;
		baseWaypoint  = 				firstWaypoint;
		waypointTarget = 				baseWaypoint;
	}
	
	protected virtual void Start()
	{
		SetupCallbacks();
		hitBaseSize = 					hitBox.collider.bounds.size;
		hurtBaseSize = 					hurtBox.collider.bounds.size;
	}

	// Update is called once per frame
	protected virtual void Update () 
	{
		HandleAutomaticMovement();
		//UpdateTrainBoxes();
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

	public virtual void HandleAutomaticMovement()
	{
        
		// The train just keeps moving forward all on its own.
		if (navMeshAgent.destination != waypointTarget.transform.position)
			navMeshAgent.SetDestination(waypointTarget.transform.position);
	}

	void UpdateTrainBoxes()
	{
		Vector3 hitboxSize = hitBaseSize;

		float boostModifier = 							0.05f;
		float boost = 									(offense - 1) * boostModifier;

		hitboxSize += 									Vector3.one * boost;

		//hitBox.collider.bounds.size = 					hitboxSize;
		
		// And the hurt box.

		Vector3 hurtboxSize = 							hurtBaseSize;
		float shrinkModifier = 							0.05f;
		float shrinkage = 								(defense - 1) * shrinkModifier;

		hurtboxSize -= 								Vector3.one * shrinkage;
		hurtBox.transform.localScale = 					hurtboxSize;
	}

	public void Die()
	{
		hurtBox.collider.enabled =		 			false;
		hitBox.collider.enabled = 					false;
		Died.Invoke(this);
		Destroy(this.gameObject);
	}

	void SetupCallbacks()
	{
		hitBox.contactEvents.TriggerEnter.AddListener(OnHitboxCollision);
	}

	public virtual void GoToNextTarget()
	{
		waypointTarget = 		nextTarget;
	}

}
