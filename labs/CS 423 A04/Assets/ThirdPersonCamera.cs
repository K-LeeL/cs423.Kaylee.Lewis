using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
     // Start is called before the first frame update
     public float turnSpeed = 4.0f;
     public GameObject target;
     private float targetDistance;
     public float minTurnAngle = -90.0f;
     public float maxTurnAngle = 0.0f;
     private float rotX;
     void Start ( )
     {
          targetDistance = Vector3.Distance ( transform.position , target.transform.position );
     }
     // Update is called once per frame     
     void Update ( )
     {
          // get the mouse inputs
          float y = Input.GetAxis ( "Mouse X" ) * turnSpeed;
          rotX += Input.GetAxis ( "Mouse Y" ) * turnSpeed;
          // clamp the vertical rotation
          rotX = Mathf.Clamp ( rotX , minTurnAngle , maxTurnAngle );
          // rotate the camera
          transform.eulerAngles = new Vector3 ( -rotX , transform.eulerAngles.y + y , 0 );
          // move the camera position
          transform.position = target.transform.position - (transform.forward * targetDistance);
     }

}
