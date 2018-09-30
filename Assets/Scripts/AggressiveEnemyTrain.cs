using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class AggressiveEnemyTrain : TrainController
{
	PlayerTrain player;


	// Use this for initialization
	protected override void Awake () 
	{
		base.Awake();
		player = 				GameObject.FindObjectOfType<PlayerTrain>();
	}
	
	// Update is called once per frame
	public override void HandleAutomaticMovement () 
	{
		//.Update();
		navMeshAgent.SetDestination(player.waypointTarget.transform.position);
	}

	protected override void Update(){
		base.Update();
		waypointTarget = player.waypointTarget;

	}
}
