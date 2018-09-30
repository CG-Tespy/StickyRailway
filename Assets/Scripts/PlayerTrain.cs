using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrain : TrainController
{
    public KeyCode accelerateButton, backwardsButton;
    public override float currentSpeed
    {
        get  { return base.currentSpeed;  }
        set 
        {
            base.currentSpeed =         value;
            navMeshAgent.speed =        base.currentSpeed;
        }
    }
    protected override void Update()
    {
        base.Update();
        HandleAcceleration();
    }

    void HandleAcceleration()
	{
		// Let the player accelerate or decelerate the train
        float speedChange = accelRate * Time.deltaTime;
		if (Input.GetKey(accelerateButton))
		    currentSpeed += 							speedChange;
        else if (Input.GetKey(backwardsButton))
            currentSpeed -=                             speedChange;
	}
}
