using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float tumbleSpeed = 2f;
    public float zoomSpeed = 100f;
    public float momentum = 4f;
    public int minY = -4;
    public int maxY = -4;

    public int minX = 20;
    public int maxX = 20;

    public float minZ = -1f;
    public float maxZ = -10f;

    public Camera cam;

    float startTime;
    bool mouseDown = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomControls();
        TumbleControls();
    }

    private void ZoomControls()
    {
        //zoom
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        zoomInput *= Time.deltaTime;

        var currentPos = cam.transform.localPosition;
        var camPos = cam.transform.localPosition;

        camPos.z += zoomInput * zoomSpeed;

        if (camPos.z <= minZ && camPos.z >= maxZ)
        {
            //momentum = mass * vel;
            cam.transform.localPosition = camPos;
        }

    }

    private void TumbleControls()
    {
        GameObject rotX = transform.GetChild(0).gameObject;
        mouseDown = Input.GetMouseButton(0);

        if (mouseDown)
        {
            float vertRot = Input.GetAxis("Mouse Y");
            float horizRot = Input.GetAxis("Mouse X");

            Quaternion rotationX = Quaternion.Euler(-vertRot, 0, 0);

            Quaternion rotationY = Quaternion.Euler(0, horizRot, 0);
            vertRot *= Time.deltaTime * tumbleSpeed;
            horizRot *= Time.deltaTime * tumbleSpeed;

            rotX.transform.rotation *= rotationX;
            transform.rotation *= rotationY;
        }
    }

}
