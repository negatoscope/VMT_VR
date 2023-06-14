using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VRVignetter : MonoBehaviour
{
    // Start is called before the first frame update
    public VRCamLook player; 
    public Volume volume;
    private Vignette vignette;
    private bool isMoving = false;
    public float intensity = 0.75f;
    public float duration = 0.5f;

    public Quaternion rotation;
     public Quaternion previousAng;
     public bool turnStatus;
     public float angle;
     public Transform vrCamera;
     private int count;
     private float movingAverage;
     private int movingAverageLength = 5;

    private void Awake()
    {
        //locomotionProvider = GetComponent<LocomotionProvider>();

        if (volume.profile.TryGet(out Vignette vignette))
            this.vignette = vignette;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Quaternion previousRotation = vrCamera.rotation;
        //angle = Quaternion.Angle(previousRotation, vrCamera.rotation);
        //StartCoroutine(AngleTime());

        count ++;

        if (count > movingAverageLength)
        {
            movingAverage = movingAverage + (angle - movingAverage) / (movingAverageLength + 1);
            //Debug.Log(movingAverage);
        }
        else
        {
            movingAverage += angle;
            if (count == movingAverageLength)
            {
                movingAverage = movingAverage / count;
                //Debug.Log(movingAverage);
            }
        }
        // //if ((player.translation != 0 || player.angle != 0) && !isMoving)
        // if (player.angle != 0 && !isMoving)
        // {
        //     FadeIn();
        //     isMoving = true;
        // } 
        // //else if ((player.translation == 0 || player.angle == 0) && isMoving)
        // else if (player.translation == 0 && isMoving)
        // {
        //     FadeOut();
        //     isMoving = false;
        // }
    }

    public IEnumerator AngleTime()
    {
        previousAng = vrCamera.rotation;
        yield return StartCoroutine(WaitFor.Frames(30));
        angle = Quaternion.Angle(vrCamera.rotation, previousAng);
        if ((player.translation != 0 || angle != 0) && !isMoving)
        {
            FadeIn();
            isMoving = true;
        } 
        else if ((player.translation == 0 || angle == 0) && isMoving)
        {
            FadeOut();
            isMoving = false;
        }
    }
    public void FadeIn()
    {
        Tween.Value(0, intensity, ApplyValue, duration, 0);
        //StartCoroutine(Fade(0, intensity));
    }

    public void FadeOut()
    {
        Tween.Value(intensity, 0, ApplyValue, duration, 0);
        //StartCoroutine(Fade(intensity, 0));
    }

    private void ApplyValue(float value)
    {
        vignette.intensity.Override(value);
    }
}