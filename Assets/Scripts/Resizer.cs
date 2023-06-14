using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    private float newSize;
    
    // Start is called before the first frame update
    void Start()
    {
        newSize = PlayerPrefs.GetFloat("size");
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, newSize, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
