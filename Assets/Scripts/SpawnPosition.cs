using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public GameObject[] spawnPos = new GameObject[3];
    private Vector3[] spawnVec = new Vector3[3];
    private int spawnPoint;
    private Scene scene;
        
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if(scene.name == "vr_maze" || scene.name == "vr_maze_vr"){
            // spawnVec[0] = new Vector3(spawnPos[0].transform.position.x, spawnPos[0].transform.position.y, spawnPos[0].transform.position.z);
            // spawnVec[1] = new Vector3(spawnPos[1].transform.position.x, spawnPos[1].transform.position.y, spawnPos[1].transform.position.z);
            // spawnVec[2] = new Vector3(spawnPos[2].transform.position.x, spawnPos[2].transform.position.y, spawnPos[2].transform.position.z);
            spawnVec[0] = new Vector3(spawnPos[0].transform.position.x, PlayerPrefs.GetFloat("size"), spawnPos[0].transform.position.z);
            spawnVec[1] = new Vector3(spawnPos[1].transform.position.x, PlayerPrefs.GetFloat("size"), spawnPos[1].transform.position.z);
            spawnVec[2] = new Vector3(spawnPos[2].transform.position.x, PlayerPrefs.GetFloat("size"), spawnPos[2].transform.position.z);


            SpawnPlayer();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnPlayer() {
        //spawnPoint = 0;
        //spawnPoint = Random.Range(0, 3);
        print(PlayerPrefs.GetInt("trials"));
        print(PlayerPrefs.GetInt("trial1"));
        if (PlayerPrefs.GetInt("trials") == 1)
        {
            this.transform.position = spawnVec[PlayerPrefs.GetInt("trial1")];
        }else if(PlayerPrefs.GetInt("trials") == 2)
        {
            this.transform.position = spawnVec[PlayerPrefs.GetInt("trial2")];
        }else if(PlayerPrefs.GetInt("trials") == 3)
        {
            this.transform.position = spawnVec[PlayerPrefs.GetInt("trial3")];
        } else 
        {
            spawnPoint = Random.Range(0, 3);
            this.transform.position = spawnVec[spawnPoint];
        }
        
        
    }
}
