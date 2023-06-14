using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallResizer : MonoBehaviour
{
    public CharacterController2 controller;
    float defaultWallY = 0;
    float timer = 0;
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultWallY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Mathf.Abs(controller.translation) > 0.01f || player.position.x != 0f)  //in case we involve controller movement
        if(player.position.x != 0f)
        {
            //Player is moving
            timer += Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, defaultWallY + Mathf.Sin(timer), transform.localScale.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(transform.localScale.y, defaultWallY, Time.deltaTime), transform.localScale.z);
        }
    }
}
