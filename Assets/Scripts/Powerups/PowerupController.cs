﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupController : MonoBehaviour3D 
{
	[SerializeField] List<TrainPowerupEffect> effects;

	protected override void OnTriggerEnter(Collider other)
	{
		// Apply the effects to the train that touched this, then disappear.
		base.OnTriggerEnter(other);

		TrainController train = 					other.GetComponent<TrainController>();

		if (train != null)
		{
			foreach (TrainPowerupEffect effect in effects)
				effect.ApplyTo(train);
		}

		Destroy(this.gameObject);
	}

}
