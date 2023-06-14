using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggerTime : MonoBehaviour
{
    public Text logtime;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        logtime.text = GameObject.Find("exit").GetComponent<EndTrial>().Timer.ToString();
    }
}
