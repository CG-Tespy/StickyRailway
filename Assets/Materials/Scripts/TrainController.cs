using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TrainController : MonoBehaviour3D 
{
	[SerializeField] float _currentSpeed = 					0;
	[SerializeField] float _maxSpeed = 						5f;
	[SerializeField] float _accelRate = 					3f;
	[SerializeField] float _offense = 						1f;
	[SerializeField] float _defense = 						1f;
	[SerializeField] Collider _hurtBoxCollider;
	[SerializeField] Collider _hitboxCollider;
	TrainBox hurtBox, hitbox;

	// Properties
	public Collider hurtBoxCollider 						{ get { return _hurtBoxCollider; } }
	public Collider hitBoxCollider 							{ get { return _hitboxCollider; } }
	public float currentSpeed 
	{ 
		get { return _currentSpeed; }
		set { _currentSpeed = value; }
	}

	public float offense 
	{
		get { return _offense; }
		set { _offense = value; }
	}

	public float defense 
	{
		get { return _defense; }
		set { _defense = value; }
	}
	bool dying;

	// Use this for initialization
	protected override void Awake()
	{
		base.Awake();
		SetupBoxes();
	}

	void Start()
	{
		SetupCallbacks();
	}

	protected virtual void OnHitboxCollision(Collider other)
	{
		TrainBox otherBox = 					other.GetComponent<TrainBox>();

		if (otherBox != null)
		{
			bool killOtherTrain = 				otherBox.type == TrainBox.Type.hurt;

			if (killOtherTrain)
			{
				TrainController otherTrain = 	otherBox.parentTrain;
				otherTrain.Die();
			}
		}
	}

	public virtual void Die()
	{
		hitBoxCollider.enabled = 			false;
		hurtBoxCollider.enabled = 			false;
	}

	// Helpers
	void SetupBoxes()
	{
		hurtBox = 							hurtBoxCollider.GetComponent<TrainBox>();
		hitbox = 							hitBoxCollider.GetComponent<TrainBox>();
	}

	void SetupCallbacks()
	{
		hitbox.contactEvents.TriggerEnter.AddListener(OnHitboxCollision);
	}

}
