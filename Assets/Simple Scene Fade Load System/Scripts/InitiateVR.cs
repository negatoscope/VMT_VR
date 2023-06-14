using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public static class InitiateVR
{
    static bool areWeFading = false;

    //Create Fader object and assing the fade scripts and assign all the variables
    public static void Fade(string scene, Color col, float multiplier, Camera VRcamera)
    {
        if (areWeFading)
        {
            Debug.Log("Already Fading");
            return;
        }

        GameObject init = new GameObject();
        init.name = "Fader";
        Canvas myCanvas = init.AddComponent<Canvas>();
        myCanvas.sortingOrder = 999;
        myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        myCanvas.worldCamera = VRcamera;
        myCanvas.planeDistance = 0.5f;
        init.AddComponent<FaderVR>();
        init.AddComponent<CanvasGroup>();
        init.AddComponent<Image>();

        FaderVR scr = init.GetComponent<FaderVR>();
        scr.fadeDamp = multiplier;
        scr.fadeScene = scene;
        scr.fadeColor = col;
        scr.start = true;
        areWeFading = true;
        scr.InitiateFader();
        
    }

    public static void DoneFading() {
        areWeFading = false;
    }
}
