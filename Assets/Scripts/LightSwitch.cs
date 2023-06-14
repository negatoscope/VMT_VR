using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(){
        gameObject.GetComponent<Light>().enabled = true;
       
    }

    public void OnTriggerExit(){
        gameObject.GetComponent<Light>().enabled = false;
        
    }
}
