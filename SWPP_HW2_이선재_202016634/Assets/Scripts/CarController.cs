using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheel_col;
    public Transform[] wheels;
   
    float torque = 500f;
    float angle = 45;

    public Vector3 centerOfMassOffset = new Vector3(0, -0.5f, 1);  // Adjust Y value to lower the center of mass
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = rb.centerOfMass + centerOfMassOffset;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<wheel_col.Length;i++)
        {
            wheel_col[i].motorTorque=Input.GetAxis("Vertical")*torque;
            if(i==0||i==1)
            {
                wheel_col[i].steerAngle=Input.GetAxis("Horizontal")*angle;
            }
            var pos = transform.position;
            var rot = transform.rotation;
            wheel_col[i].GetWorldPose(out pos, out rot);
            wheels[i].position = pos;
            wheels[i].rotation = rot;
        }
        

        if(Input.anyKeyDown) 
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                foreach(var i in wheel_col)
                {
                    i.brakeTorque=7000;
                }
            }
            else{   //reset the brake torque when another key is pressed
                foreach(var i in wheel_col)
                {
                    i.brakeTorque=0;
                }
                
            }
        }

        // Visualize the center of mass in the Scene view
        Debug.Log(rb.worldCenterOfMass);
        Debug.DrawLine(rb.position, rb.worldCenterOfMass, Color.red);
    }
}
