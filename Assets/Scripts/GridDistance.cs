using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll){
        if (coll.transform.tag == "Player"){
            Debug.Log("collision");
        }
    }
}
