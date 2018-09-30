using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainBox : MonoBehaviour3D
{
	[SerializeField] TrainController _parentTrain;
	public TrainController parentTrain 				{ get { return _parentTrain; } }
	public enum Type 
	{
		hit, hurt
	}

	public Type type = 								Type.hit;

}
