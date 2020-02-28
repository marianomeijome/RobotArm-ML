using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RollerAgent : Agent
{
    Rigidbody rBody;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public override void AgentReset()
    {
        if(this.transform.position.y < 0)
        {
            //If the Agent Falls off, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 0.5f, 0);
        }

        //Move the target to new spot
        Target.position = new Vector3(Random.Range(-8, 8), 1.5f, Random.Range(-8,8));
    }

    public override void CollectObservations()
    {
        // Target and Agent positions
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public float speed = 10;
    public override void AgentAction(float[] vectorAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position,
                                                  Target.position);

        if(distanceToTarget < 0.5f)
        {
            score = 1;
            SetReward(score);
            Done();
        }

        // Fell off platform
        if (this.transform.position.y < 0)
        {
            score = 0;
            SetReward(score);
            Done();
        }

    }

    

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Target")
        {
            score += 1;
            Debug.Log("WOW GOOD JOB!!!");
            SetReward(score);
            Debug.Log(GetReward());
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

}
