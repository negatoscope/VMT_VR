using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//igidbody rigidbody;
public class WallCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter(Collision collision) 
 {
        // if(collision.gameObject.tag == "Maze")  
        if(collision.gameObject.CompareTag("Maze"))
         {
                     print("hitting a wall");
                     GetComponent<Rigidbody>().velocity = Vector3.zero;
         }
 }
}
