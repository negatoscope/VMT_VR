using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Vignetter : MonoBehaviour
{
    public CharacterController2 player; 
    public Volume volume;
    private Vignette vignette;
    private bool isMoving = false;
    public float intensity = 0.75f;
    public float duration = 0.5f;

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
    void Update()
    {
        if (player.translation != 0 && !isMoving)
        {
            FadeIn();
            isMoving = true;
        } else if (player.translation == 0 && isMoving)
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
