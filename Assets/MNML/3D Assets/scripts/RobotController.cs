using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{

    public GameObject[] Joints;

    // Start is called before the first frame update
    void Start()
    {
        rotateJoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void rotateJoint()
    {
        foreach(var joint in Joints) {
            Debug.Log(joint);
        }
        
        //which axis?
        //min-max?

        //float vertRot = Input.GetAxis("Vertical");
        //float horizRot = Input.GetAxis("Horizontal");

        //vertRot *= Time.deltaTime * speed;
        //horizRot *= Time.deltaTime * speed;

        //Quaternion rotation = Quaternion.Euler(vertRot, -horizRot, 0);

        //transform.rotation *= rotation;

    }
}
