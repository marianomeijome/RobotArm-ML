using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class JumperAgent : Agent
{
    Rigidbody rBody;
    GameObject[] goal;

    public Transform platforms;

    public Material GoalMaterial;
    public Material SafeMaterial;
    GameObject Target;
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        goal = GameObject.FindGameObjectsWithTag("Goal");
        Target = goal[0];

    }

    void ResetPlatforms()
    {
        foreach (Transform child in platforms)
        {
            child.tag = "Safe";
            child.GetComponent<Renderer>().material = SafeMaterial;
        }

        //Debug.Log(platforms.childCount);
        Transform newGoal = platforms.GetChild(Random.Range(0, platforms.childCount));
        newGoal.tag = "Goal";
        newGoal.GetComponent<Renderer>().material = GoalMaterial;
        goal = GameObject.FindGameObjectsWithTag("Goal");
        Target = goal[0];
    }

    public override void AgentReset()
    {
        if(this.transform.position.y < 0.65f || this.transform.position.y > 20)
        {
            //If the Agent Falls off, zero its momentum
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.position = new Vector3(0, 1, 0);
        }

        //Assign new goal to random tile by changing its tag and material
        ResetPlatforms();
        //Target.position = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations()
    {
        // Target and Agent positions
        AddVectorObs(Target.transform.position);
        AddVectorObs(this.transform.position);
        

        // Agent velocity
        AddVectorObs(rBody.velocity.x);
        //AddVectorObs(rBody.velocity.y);
        AddVectorObs(rBody.velocity.z);
    }

    public float speed = 10;
    int score = 0;


    //Detect collisions between the GameObjects with Colliders attached
    
    public override void AgentAction(float[] vectorAction)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.AddForce(controlSignal * speed);

        //controlSignal.y = vectorAction[2];
        //rBody.AddForce(0,controlSignal.y,0, ForceMode.Impulse);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position,
                                                  Target.transform.position);
        
        

        if (distanceToTarget <1)
        {
            SetReward(1.2f);
            Debug.Log("WOW YOU SCORED BRO HOLY HECK!!!");
            score = 1;
            scored = true;
            Done();
        }

        if (scored)
        {
            SetReward(1.0f);
            Debug.Log("WOW NICE BROOOO!!!");
            score += 1;
            scored = false;
            Done();
        }

        // Fell off platform
        if (this.transform.position.y < 0.35f)
        {
            SetReward(-1.0f);
            score = 0;
            Done();
        }

        // Reset if Stuck
        //var speedy = rBody.velocity.magnitude;
        //Debug.Log(speedy);
        

    }
    bool scored = false;
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("WOW GOOD JOB!!!");
            score += 1;
            SetReward(1.0f * score + GetReward());
            Debug.Log(GetReward());
            Done();
            scored = true;
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        //action[2] = Input.GetAxis("Jump");
        return action;
    }

}
