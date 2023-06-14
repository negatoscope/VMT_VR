/* 
 * author : jiankaiwang
 * description : The script provides you with basic operations of first personal control.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour {

    public float speed = 0f;
    public float maxSpeed = 1f;
    public float acceleration = 0.5f;
    public float deceleration = 1f;

    public float translation;

    public float rotation; 
    public float distance;
    public float walk;
    public float stepCounter;
    private Vector3 previousLoc;
    public float currentSpeed;

    Rigidbody playerBody;
    Vector3 m_EulerAngleVelocity;
    public Camera playerCamera;
     

    // Use this for initialization
    void Start () {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
        // read speed from PlayerPrefs
        maxSpeed = PlayerPrefs.GetFloat("speed");
        previousLoc = transform.position;
        playerBody = GetComponent<Rigidbody>();
        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
        m_EulerAngleVelocity = new Vector3(0, 100, 0);
        //speed = 0;	
        

	}
	
	void FixedUpdate()
    {
        //Vector3 input = new Vector3(0, 0, Input.GetAxis("Vertical"));
        //float cameraRot = playerCamera.transform.rotation.eulerAngles.y; // Camera.main.transform.rotation.eulerAngles.y;
        //print(cameraRot);
        //playerBody.position += Quaternion.Euler(0, cameraRot, 0) * input * maxSpeed * Time.deltaTime;
        
        //translation = Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime * 100;     
        //Vector3 m_Input = new Vector3(0, 0, translation);
        //print(translation);
        //playerBody.MovePosition(transform.position + m_Input * Time.deltaTime * maxSpeed);
        
        //playerBody.AddForce(transform.forward + m_Input  * maxSpeed);

        //var v=Input.GetButton("Vertical");
        //if (v){
            //playerBody.AddForce(transform.forward + m_Input  * maxSpeed);

        //}
        //rotation = Input.GetAxis("Horizontal") * maxSpeed * 36 * Time.deltaTime;
        //transform.Rotate(0, rotation, 0);
        
        // //Store user input as a movement vector
        // Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // //Apply the movement vector to the current position, which is
        // //multiplied by deltaTime and speed for a smooth MovePosition
        // playerBody.MovePosition(transform.position + m_Input * Time.deltaTime * maxSpeed);

        Quaternion deltaRotation = Quaternion.Euler(Input.GetAxis("Horizontal") * m_EulerAngleVelocity * Time.fixedDeltaTime);
        playerBody.MoveRotation(playerBody.rotation * deltaRotation);
        // previousLoc = transform.position; 
        // translation = Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;
        // transform.Translate(0, 0, translation);
        // rotation = Input.GetAxis("Horizontal") * maxSpeed * 36 * Time.deltaTime;
        // transform.Rotate(0, rotation, 0);
        // previousLoc = transform.position;
        // stepCounter = 1;
        // walk = stepCounter * speed * Time.deltaTime;
        // //distance += walk;
        // distance += Vector3.Distance(transform.position, previousLoc);
        // currentSpeed = (transform.position - previousLoc).magnitude/Time.deltaTime;
        // previousLoc = transform.position; 
    } 
    
    // Update is called once per frame
	void Update () {
    //     if ((Input.GetKey("w")) && speed < maxSpeed){
    //         speed = speed + acceleration * Time.deltaTime;
    //     } else {
    //         if (speed > deceleration * Time.deltaTime){
    //             speed = speed - deceleration * Time.deltaTime;
    //         } else if (speed < -deceleration * Time.deltaTime){
    //             speed = speed + deceleration * Time.deltaTime;
    //         } else {
    //             speed = 0;
    //         }
        
    // }
        //previousLoc = transform.position;
        Vector3 input = new Vector3(0, 0, Input.GetAxis("Vertical"));
        float cameraRot = playerCamera.transform.rotation.eulerAngles.y; // Camera.main.transform.rotation.eulerAngles.y;
        playerBody.position += Quaternion.Euler(0, cameraRot, 0) * input * maxSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Input.GetAxis("Horizontal") * m_EulerAngleVelocity * Time.fixedDeltaTime);
        playerBody.MoveRotation(playerBody.rotation * deltaRotation);

        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        ///previousLoc = transform.position;
        
        //translation = Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;
        //transform.Translate(0, 0, translation);
        //rotation = Input.GetAxis("Horizontal") * maxSpeed * 36 * Time.deltaTime;
        //transform.Rotate(0, rotation, 0);
        stepCounter = 1;
        walk = stepCounter * speed * Time.deltaTime;
        //distance += walk;
        
        //distance += Vector3.Distance(transform.position, previousLoc);
        currentSpeed = (transform.position - previousLoc).magnitude/Time.deltaTime;
        if (Input.GetAxis("Vertical") != 0)
        {
            //print("up");
            distance += Vector3.Distance(transform.position, previousLoc);

        }
        previousLoc = transform.position; 
        
        // if (Input.GetKey("w")) {
        //     translation = Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime;
        //     transform.Translate(0, 0, translation);
        //     stepCounter = 1;
        //     walk = stepCounter * speed * Time.deltaTime;
        //     //distance += walk;
        //     distance += Vector3.Distance(transform.position, previousLoc);
        //     previousLoc = transform.position;
        //     }
        
        //currentSpeed = playerBody.velocity.magnitude;

        if (Input.GetKeyDown("space")) {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
}