using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "PowerupEffects/TrainStatChange", fileName = "TrainStatChange")]
public class TrainStatChange : TrainPowerupEffect
{
	[SerializeField] float _offenseChange;
	[SerializeField] float _defenseChange;
	[SerializeField] float _speedChange;

	public override void ApplyTo(TrainController train)
	{
		train.offense += 						_offenseChange;
		train.defense += 						_defenseChange;
		train.currentSpeed +=	 				_speedChange;
	}

}
