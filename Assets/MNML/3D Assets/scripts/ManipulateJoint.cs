using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ManipulateJoint : MonoBehaviour
{
    public string keyBinding = "";
    public float speed = 20f;
   
    public Vector3 minRotation = Vector3.zero;
    public Vector3 maxRotation = Vector3.zero;

    public Vector3 minTranslation = Vector3.zero;
    public Vector3 maxTranslation = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        //RotateJoint RJOnChildren = GetComponentInChildren<RotateJoint>();
        //var bindings = RJOnChildren.keyBinding;

        //RJOnChildren.keyBinding = false;s
    }

    // Update is called once per frame
    void Update()
    {
        ConstrainAxes();
        ConstrainTranslations();
    }
    void ConstrainAxes()
    {
        var currentX = transform.localEulerAngles.x;
        var currentY = transform.localEulerAngles.y;
        var currentZ = transform.localEulerAngles.z;

        if (minRotation.x != maxRotation.x)
        {
            float axes = Input.GetAxis(keyBinding);
            axes *= Time.deltaTime * speed;

            var newRot = currentX + axes;

            if (newRot < 0)
            {
                newRot -= 360;

            }


            if (newRot > minRotation.x && newRot < maxRotation.x)
            {
                Vector3 rotationX = new Vector3(newRot, currentY, currentZ);
                transform.localEulerAngles = rotationX;
            }
        }

        if (minRotation.y != maxRotation.y)
        {
            float axes = Input.GetAxis(keyBinding);
            axes *= Time.deltaTime * speed;

            var newRot = currentY + axes;

            if (newRot < 0)
            {
                newRot -= 360;

            }


            if (newRot > minRotation.y && newRot < maxRotation.y)
            {
                Vector3 rotationY = new Vector3(currentX, newRot, currentZ);
                transform.localEulerAngles = rotationY;
            }
        }

        if (minRotation.z != maxRotation.z)
        {
            float axes = Input.GetAxis(keyBinding);
            axes *= Time.deltaTime * speed;

            var newRot = currentZ + axes;

            if (newRot < 0)
            {
                newRot -= 360;
                
            }


            if (newRot > minRotation.z && newRot < maxRotation.z)
            {
                Vector3 rotationZ = new Vector3(currentX, currentY, newRot);
                transform.localEulerAngles = rotationZ;
            }
        }

    }

    void ConstrainTranslations()
    {
        
        var currentX = transform.position.x;
        var currentY = transform.position.y;
        var currentZ = transform.position.z;

        if (minTranslation.x != maxTranslation.x)
        {
            float axes = Input.GetAxis(keyBinding);

            if (axes > 0.2f || axes < -0.2f)
            {
                axes *= Time.deltaTime * speed;

                var newTranslation = currentX + axes;
                //Debug.Log(newTranslation);
                if (newTranslation > minTranslation.x && newTranslation < maxTranslation.x)
                {
                    Vector3 translationX = new Vector3(newTranslation, currentY, currentZ);
                    transform.position = translationX;
                }


            }

        }

        if (minTranslation.y != maxTranslation.y)
        {
            float axes = Input.GetAxis(keyBinding);

            if (axes > 0.2f || axes < -0.2f)
            {
                axes *= Time.deltaTime * speed;

                var newTranslation = currentY + axes;
                //Debug.Log(newTranslation);
                if (newTranslation > minTranslation.y && newTranslation < maxTranslation.y)
                {
                    Vector3 translationY = new Vector3(currentX, newTranslation, currentZ);
                    transform.position = translationY;
                }


            }

        }

        if (minTranslation.z != maxTranslation.z)
        {
            float axes = Input.GetAxis(keyBinding);

            if(axes > 0.2f || axes < -0.2f)
            {
                axes *= Time.deltaTime * speed;

                var newTranslation = currentZ + axes;
                //Debug.Log(newTranslation);
                if (newTranslation > minTranslation.z && newTranslation < maxTranslation.z)
                {
                    Vector3 translationZ = new Vector3(currentX, currentY, newTranslation);
                    transform.position = translationZ;
                }

                
            }
            
        }
    }
}
