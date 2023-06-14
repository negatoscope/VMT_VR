using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamLook : MonoBehaviour
{
    public Transform vrCamera;
    //public float moveSpeed; 
    //public OVRHand hand;
    public float speed;
    public float maxSpeed;
    private Vector3 forward;
    //public float acceleration = 0.5f;
    //public float deceleration = 1f; 

    //public float stepCounter; 

     public float distance;
     private Vector3 previousLoc;
     //private CharacterController CharCont;

     Rigidbody m_Rigidbody;
     public Vector3 movement;
     //public float walk;
     public float translation;
     public Quaternion rotation;
     public Quaternion previousAng;

     public bool turnStatus;
     public float angle;

     // Use this for initialization
     void Start () {
 
         //CharCont = GetComponent<CharacterController>();
         m_Rigidbody = GetComponent<Rigidbody>();
         maxSpeed = PlayerPrefs.GetFloat("speed");
         previousLoc = transform.position;
         turnStatus = false;
         
         
     }
     
     // Update is called once per frame
     void FixedUpdate () {
 
    //Quaternion previousRotation = vrCamera.rotation;
    //angle = Quaternion.Angle(previousRotation, vrCamera.rotation);
     //transform.rotation = Quaternion.Euler(0, vrCamera.rotation.eulerAngles.y, 0);
     //transform.localRotation = Quaternion.LookRotation(vrCamera.transform.forward, vrCamera.transform.up);
     //SetRotate();
    //   if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger) && speed < maxSpeed){
    //     speed = speed + acceleration * Time.deltaTime;
        
    //   } else {
    //         if (speed > deceleration * Time.deltaTime){
    //             speed = speed - deceleration * Time.deltaTime;
    //         } else if (speed < -deceleration * Time.deltaTime){
    //             speed = speed + deceleration * Time.deltaTime;
    //         } else {
    //             speed = 0;
    //         }
        
    // }
        
        
        //StartCoroutine(AngleTime());
        //AngleRunner();
        //Debug.Log(angle);
        translation = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * maxSpeed * Time.deltaTime;
        //rotation = Mathf.DeltaAngle(vrCamera.rotation.y, previousAng);
        //previousAng = vrCamera.rotation.y;

        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) //|| hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            //print(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
            forward = vrCamera.TransformDirection(Vector3.forward) * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * maxSpeed * Time.deltaTime;
            
            //this.transform.Translate(forward.x, 0, forward.z); //this ignores physics
            movement = new Vector3(forward.x, 0, forward.z); //comment to fly
            //m_Rigidbody.MovePosition(transform.position + forward); //uncomment to fly
            m_Rigidbody.MovePosition(transform.localPosition + movement);
             
            //stepCounter = 1;
            //walk = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * Time.deltaTime;
            //walk = stepCounter * speed * Time.deltaTime; //this compensates for oversized world
            //distance += walk;
            distance += Vector3.Distance(transform.position, previousLoc);
            previousLoc = transform.position;
             //print(walk);
 
             //CharCont.SimpleMove(forward * moveSpeed * Time.deltaTime);
 
        }
        
     }

    public void AngleRunner()
    {
        StartCoroutine(AngleTime());
    }
     public IEnumerator AngleTime()
     {
         previousAng = vrCamera.rotation;
         yield return StartCoroutine(WaitFor.Frames(30));
         //yield return new WaitForSecondsRealtime(5f);
         angle = Quaternion.Angle(vrCamera.rotation, previousAng);
         //yield return null;
        //  //rotation = Mathf.Abs(Mathf.DeltaAngle(vrCamera.rotation.y, previousAng.y));
        //  if (rotation > 0.005f){
        //      turnStatus = true;
        //      }
        //      else
        //      {
        //          turnStatus = false;
        //      }
         //print(turnStatus);
     }

     void SetRotate()
     {
        //You can call this function for any game object and any camera, just change the parameters when you call this function
        transform.rotation = Quaternion.Lerp(this.transform.rotation, vrCamera.transform.rotation, 0.1f * Time.deltaTime);
     }

}
