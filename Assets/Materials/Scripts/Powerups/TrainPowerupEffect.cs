using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TrainPowerupEffect : ScriptableObject
{
	public abstract void ApplyTo(TrainController toApplyTo);
}
