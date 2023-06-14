using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPrefs : MonoBehaviour
{
    public Slider sizeSlider;
    public Slider speedSlider;
    public Slider colorSlider;
    public Slider displacementSlider;
    public Slider flipSlider;
    public Slider missingrateSlider;
    public Toggle fogToggle;
    public Toggle pathToggle;
    public Toggle sizerToggle;
    public Toggle missingToggle;
    public Toggle vrToggle;
    public Toggle blurToggle;
    public Toggle vignetteToggle;

    public Text participantID;
    public Dropdown lang;
    public Dropdown session; 

    public Dropdown trials;

    private Vector3[] trialOrder = new Vector3[6];
    private int trialSet;
    private Vector3 trialVector;
    public Slider waitingSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        trialOrder[0] = new Vector3(0, 1, 2);
        trialOrder[1] = new Vector3(0, 2, 1);
        trialOrder[2] = new Vector3(1, 2, 0);
        trialOrder[3] = new Vector3(1, 0, 2);
        trialOrder[4] = new Vector3(2, 1, 0);
        trialOrder[5] = new Vector3(2, 0, 1);
        trialSet = Random.Range(0, 6);
        trialVector = trialOrder[trialSet];

        PlayerPrefs.SetInt("trials", 0);

        PlayerPrefs.SetInt("trial1", (int)trialVector.x);
        PlayerPrefs.SetInt("trial2", (int)trialVector.y);
        PlayerPrefs.SetInt("trial3", (int)trialVector.z);
        
        Debug.Log(PlayerPrefs.GetInt("trials"));
        Debug.Log(PlayerPrefs.GetInt("trial1"));
        Debug.Log(PlayerPrefs.GetInt("trial2"));
        Debug.Log(PlayerPrefs.GetInt("trial3"));
        
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        // General settings
        //Debug.Log(lang.value);
        
        
        PlayerPrefs.SetFloat("speed", speedSlider.value);
        int currentToggleState = fogToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("fog", currentToggleState);
        int currentPathToggleState = pathToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("path", currentPathToggleState);
        int currentVrToggleState = vrToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("vr", currentVrToggleState);
            
        // Dream scene settings
        PlayerPrefs.SetFloat("color", colorSlider.value);
        PlayerPrefs.SetFloat("displacement", displacementSlider.value);
        PlayerPrefs.SetFloat("flip", flipSlider.value);
        int currentSizerToggleState = sizerToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("sizer", currentSizerToggleState);
        int currentMissingToggleState = missingToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("missing", currentMissingToggleState);
        PlayerPrefs.SetFloat("missing_rate", missingrateSlider.value);

        // For some reason new playerprefs have to be placed after
        PlayerPrefs.SetFloat("size", sizeSlider.value);
        PlayerPrefs.SetString("id", participantID.text);
        PlayerPrefs.SetFloat("session", session.value);
        
        PlayerPrefs.SetFloat("max_trials", trials.value);

        int currentBlurToggleState = blurToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("blur", currentBlurToggleState);
        int currentVignetteToggleState = vignetteToggle.isOn == true ? 1 : 0;
        PlayerPrefs.SetInt("vignette", currentVignetteToggleState);
        
        PlayerPrefs.SetFloat("debug_time", waitingSlider.value);

        PlayerPrefs.SetFloat("lang", lang.value);

        PlayerPrefs.Save();

        //Debug.Log(PlayerPrefs.GetFloat("lang"));
    }

}
