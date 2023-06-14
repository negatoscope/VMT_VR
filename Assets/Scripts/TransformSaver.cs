using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransformSaver : MonoBehaviour
{
    StreamWriter sw; 
    // Start is called before the first frame update
    void Start()
    {
        var folder = Directory.CreateDirectory(Application.dataPath + "/Transforms");
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "training" || currentScene.name == "vr_training")
        {
            sw = new StreamWriter(Application.dataPath + "/Transforms/table_" + PlayerPrefs.GetString("id") + "_" + "training" + ".csv");
           
        } else
        {
            sw = new StreamWriter(Application.dataPath + "/Transforms/table_" + PlayerPrefs.GetString("id") + "_" + PlayerPrefs.GetFloat("session") + "_" + PlayerPrefs.GetInt("trials") + ".csv");
        }
    }


    // Update is called once per frame
    async void Update()
    {
        

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "vr_maze" || currentScene.name == "vr_maze_vr")
        {
            sw.WriteLine(transform.position.x + ";" + transform.position.y + ";" + transform.position.z + ";" + transform.rotation.w + ";" + transform.rotation.x + ";" + transform.rotation.y + ";" + transform.rotation.z);
            //Debug.Log(transform.position);
        }

        //Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "training" || currentScene.name == "training_vr")
        {
            sw.WriteLine(transform.position.x + ";" + transform.position.y + ";" + transform.position.z + ";" + transform.rotation.w + ";" + transform.rotation.x + ";" + transform.rotation.y + ";" + transform.rotation.z);
            //Debug.Log(transform.position);
        }

        //sw.Close();

    }

    // public void WriteToFile()
    // {
    //     StreamWriter sw = new StreamWriter(Application.dataPath + "/Transforms/" + "table.txt" );

    //     Scene currentScene = SceneManager.GetActiveScene();
    //     if (currentScene.name == "vr_maze" || currentScene.name == "vr_maze_vr")
    //     {
    //         sw.Write(transform.position);
    //         //Debug.Log(transform.position);
    //     }

    //     sw.Close();
    // }
}
