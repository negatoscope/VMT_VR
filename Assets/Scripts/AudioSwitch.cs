using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour
{
    
    public AudioSource ambient;
    public Transform sound;
           
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(this.gameObject.transform.position, sound.position);
        Debug.DrawRay(this.gameObject.transform.position, sound.position, Color.red);
        if (Physics.Raycast(landingRay, out hit, 10)){
            if (hit.collider.tag == "Sound"){
                ambient.volume = 1;
                //Debug.DrawRay(gameObject.transform.position, sound.position, Color.red);
                //print(gameObject.transform.position);
                //print(sound.position);
                //ambient.pitch = 1;
                //StartCoroutine(FadeAudioSource.StartFade(ambient, 0.25f, 1f)); 
            } else {
                ambient.volume = 0.75f;
                //print(gameObject.transform.position);
                //print(sound.position);
                //ambient.pitch = 0.75f;
                //StartCoroutine(FadeAudioSource.StartFade(ambient, 0.25f, 0.75f));
            }
        }
    }

    // public void OnTriggerEnter(){
    //     //ambient.volume = 0.5f;
    //     //StartCoroutine(FadeAudioSource.StartFade(ambient, 0.25f, 0.75f));   
    // }

    // public void OnTriggerExit(){
    //     //ambient.volume = 1f; 
    //     //StartCoroutine(FadeAudioSource.StartFade(ambient, 0.25f, 1f));          
    // }
}
