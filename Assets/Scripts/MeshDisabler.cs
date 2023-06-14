using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDisabler : MonoBehaviour
{
    public MeshRenderer[] allChildren;
    public float timer;
    public float seconds;
    
    // Start is called before the first frame update
    void Start()
    {
        allChildren = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        seconds =  PlayerPrefs.GetFloat("missing_rate");
    }

    // Update is called once per frame
    void Update()
    {
         //allChildren.enabled = true;
         for (var i=0;i<allChildren.Length ;i++) 
            {
                timer += Time.deltaTime/300;
                
                if (timer > seconds)
                    {
                        var rdm_off = Random.Range(0, allChildren.Length);
                        allChildren[rdm_off].enabled = false;
                        var rdm_on = Random.Range(0, allChildren.Length);
                        allChildren[rdm_on].enabled = true;
                        timer = 0;
                        }
            }
    }
}
