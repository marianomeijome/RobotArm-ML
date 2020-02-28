using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCube : MonoBehaviour
{
    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertRot = Input.GetAxis("Vertical");
        float horizRot = Input.GetAxis("Horizontal");
                        
        vertRot *= Time.deltaTime * speed;
        horizRot *= Time.deltaTime * speed;
        
        Quaternion rotation = Quaternion.Euler(vertRot , -horizRot, 0);
                
        transform.rotation *= rotation;

    }

   
}
