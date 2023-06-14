using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderListener : MonoBehaviour
 {
     void Awake()
     {
         // Check if Colider is in another GameObject
         Collider collider = GetComponentInChildren<Collider>();
         if (collider.gameObject != gameObject)
         {
             ColliderBridge cb = collider.gameObject.AddComponent<ColliderBridge>();
             cb.Initialize(this);
         }
     }
     public void OnCollisionEnter(Collision collision)
     {
         if (collision.transform.tag == "Player"){
            // Do your stuff here
            print("collision");
         }

     }
     public void OnTriggerEnter(Collider other)
     {
         if (other.transform.tag == "Player"){
            // Do your stuff here
            print("collision");
         }
     }
 }