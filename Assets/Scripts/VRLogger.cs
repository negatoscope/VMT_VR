using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRLogger : MonoBehaviour
{
    public Text logtext;
    public VRCamLook controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        logtext.text = controller.distance.ToString();
    }
}
