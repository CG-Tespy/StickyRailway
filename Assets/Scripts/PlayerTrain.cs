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
        HandleSteering();
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

    void HandleSteering()
    {
        // Choose what waypoint to go to next after reaching the target, all based on use input.

        float steerFB =                     Input.GetAxis("SteerFB");
        float steerLR =                     Input.GetAxis("SteerLR");

        bool goForward =                    steerFB > 0;
        bool goBackward =                   steerFB < 0;
        bool goLeft =                       steerLR < 0;
        bool goRight =                      steerLR > 0;

        if (goForward)
            nextTarget = waypointTarget.forwardPoint;
        
        if (goBackward)
            nextTarget = waypointTarget.backPoint;

        else if (goLeft)
            nextTarget = waypointTarget.leftPoint;
        
        if (goRight)
            nextTarget = waypointTarget.rightPoint;

        
    }
}
