/* 
 * author : jiankaiwang
 * description : The script provides you with basic operations of first personal control.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSize : MonoBehaviour {

    public float speed;
    public float magnitude;
    private float translation;
    Vector3 centre;
    float dist;
    Vector3 nscale;


    // Use this for initialization
    void Start () {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;		
	}
	
	// Update is called once per frame
	void Update () {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, translation);

        centre = new Vector3(0.0f, 0.0f, 0.0f);
        float dist = Vector3.Distance(transform.position, centre);
        //nscale = new Vector3(((0.5f)-dist/25), ((0.5f)-dist/25), ((0.5f)-dist/25));
        transform.localScale = new Vector3(transform.localScale.x, ((magnitude)-(dist*(magnitude/20))), transform.localScale.z);
        
        if (Input.GetKeyDown("escape")) {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }
}