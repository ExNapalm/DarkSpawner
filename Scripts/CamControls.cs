using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    //controls rotation speed
    public float rotspeedX;
    public float rotspeedY;

    public float Camspeed;

    public Transform Cam;
    public Transform Pos;
    [SerializeField]
    float PosLimitZ, PosLimitY, ScrollLimit;

    float rotationX;
    float rotationY;
    float inputZ;
    float inputY;


    // Update is called once per frame
    void Update()
    {
        //New rotation for the Z(horizontal) Axis
        float newRotationX = rotationX + Input.GetAxis("CameraRotateX") * rotspeedX * Time.deltaTime;
        //New rotation for the Y(vertical) Axis
        float newRotationY = rotationY + Input.GetAxis("CameraRotateY") * rotspeedY * Time.deltaTime;

        RotationControl(newRotationX, newRotationY);

        MoveControl(inputZ, inputY);

        ZoomControl();
    }
    void RotationControl(float rot, float rota)//Controls the Rotation of the Camera(Q,E,R,T)
    {
        inputZ = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        rotationY = Mathf.Clamp(rota, -25f, 25f);

        rotationX = Mathf.Clamp(rot, -45f, 45f);
        Cam.rotation = Quaternion.Euler(Cam.rotation.x, rot, rota);

    }

    void MoveControl(float mvZ, float mvY)//Controls the Left, Right, Up and Down movement of the camera(Local position)
    {

        if (mvZ < 0 && Pos.transform.localPosition.z <= PosLimitZ)
        {
            Pos.transform.Translate(new Vector3(0f, 0f, Camspeed * Time.deltaTime));
           // Debug.Log("Moving Left");
           // Debug.Log(Pos.transform.localPosition.z);
        }
        if (mvZ > 0 && Pos.transform.localPosition.z >= -PosLimitZ)
        {
            {
                Pos.transform.Translate(new Vector3(0f, 0f, -Camspeed * Time.deltaTime));
               // Debug.Log("Moving Right");
               // Debug.Log(Pos.transform.localPosition.z);
            }
        }

        if (mvY > 0 && Pos.transform.localPosition.y <= PosLimitY)
        {
            {
                Pos.transform.Translate(new Vector3(0f, Camspeed * Time.deltaTime, 0f));
               // Debug.Log("Moving Up");
               // Debug.Log(Pos.transform.localPosition.y);
            }
        }

        if (mvY < 0 && Pos.transform.localPosition.y >= -PosLimitY)
        {
            Pos.transform.Translate(new Vector3(0f, -Camspeed * Time.deltaTime, 0f));
            //Debug.Log("Moving Down");
            //Debug.Log(Pos.transform.localPosition.y);
        }

    }

    void ZoomControl()//Controls Camera Zoom
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponentInChildren<Camera>().fieldOfView > ScrollLimit)
        {
            GetComponentInChildren<Camera>().fieldOfView -= (Camspeed * 10) * Time.deltaTime;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponentInChildren<Camera>().fieldOfView < ScrollLimit * 2)
        {
            GetComponentInChildren<Camera>().fieldOfView += (Camspeed * 10) * Time.deltaTime;
        }
    }
}
