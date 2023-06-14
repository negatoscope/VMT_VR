using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VREndTrial : MonoBehaviour
{
    public VRCamLook controller;
    public ColliderDetector detector;
    public GameObject EndTrialMenu_eng;
    public GameObject InstructionMenu_eng;
    public GameObject EndExperimentMenu_eng;
    public GameObject EndTrialMenu_spa;
    public GameObject InstructionMenu_spa;
    public GameObject EndExperimentMenu_spa;
    private GameObject EndTrialMenu;
    private GameObject InstructionMenu;
    private GameObject EndExperimentMenu; 
    public GameObject TimeOut_eng;
    public GameObject TimeOut_spa;
    private GameObject TimeOut;
    private Texture2D paths;
    private GameObject player;
    private DrawPath pathScript;

    public float Timer = 0;
    public bool IncreaseTime = true;
    private Scene scene;
    public int trialCount;

    public Slider progressBar;
    public Slider progressBar_spa;
    public GameObject bar;
    public GameObject bar_spa; 
    public GameObject continueButton;
    public GameObject continueButton_spa;
    public CanvasGroup UIgroup;
    private bool fadeIn = false;
    private float fadeTime;

    public Camera VRcamera;


    static List<int> LevelsToPlay = new List<int> { 3, 4 };

    // Start is called before the first frame update
    void Start()
    {
        var folder = Directory.CreateDirectory(Application.dataPath + "/Paths");
  
        //EndTrialMenu.SetActive(false);
        
        IncreaseTime = true;
        player = GameObject.Find("Capsule");
        pathScript = player.GetComponent<DrawPath>();
        scene = SceneManager.GetActiveScene();
        if (scene.name == "vr_training" || scene.name == "vr_adaptation")
        {
            if (PlayerPrefs.GetFloat("lang") == 0)
            {
                InstructionMenu_spa.SetActive(true);
                continueButton_spa.SetActive(false);
            } else
            {
                InstructionMenu_eng.SetActive(true);
                continueButton.SetActive(false);
            }
            controller.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            //continueButton.SetActive(false);
        }
        if (scene.name == "vr_maze_vr")
        {
            trialCount = PlayerPrefs.GetInt("trials") + 1;
            PlayerPrefs.SetInt("trials", trialCount);
            //print(PlayerPrefs.GetFloat("trials"));
            //print(PlayerPrefs.GetFloat("max_trials"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IncreaseTime == true)
        {
            Timer += Time.deltaTime;
            if ((scene.name == "vr_training" || scene.name == "vr_adaptation"))
            {
                if (PlayerPrefs.GetFloat("lang") == 0)
                {
                    progressBar_spa.value = Timer;
                } else 
                {
                    progressBar.value = Timer;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                //choose the index of a level:
                int nextLevelIndex = Random.Range(0, LevelsToPlay.Count);
                //get the actual sceneIndex by the index of our list:
                int nextLevel = LevelsToPlay[nextLevelIndex];
                // load the level:
                if (scene.name == "vr_adaptation")
                {
                    if (PlayerPrefs.GetFloat("lang") == 0)
                    {
                        EndTrialMenu_spa.SetActive(false);
                    } else
                    {
                        EndTrialMenu_eng.SetActive(false);
                    }
                    //EndTrialMenu.SetActive(false);
                    InitiateVR.Fade("vr_training", Color.black, 0.5f, VRcamera);
                    //SceneManager.LoadScene("vr_training");
                }
                else if (scene.name == "vr_training")
                {
                    if (PlayerPrefs.GetFloat("lang") == 0)
                    {
                        EndTrialMenu_spa.SetActive(false);
                    } else
                    {
                        EndTrialMenu_eng.SetActive(false);
                    }
                    //EndTrialMenu.SetActive(false);
                    InitiateVR.Fade("vr_maze_vr", Color.black, 0.25f, VRcamera);
                    //SceneManager.LoadScene("vr_maze_vr");
                } 
                else
                {
                    if (trialCount < PlayerPrefs.GetFloat("max_trials"))
                    {
                        if (PlayerPrefs.GetFloat("lang") == 0)
                        {
                            EndTrialMenu_spa.SetActive(false);
                        } else
                        {
                            EndTrialMenu_eng.SetActive(false);
                        }
                        //EndTrialMenu.SetActive(false);
                        InitiateVR.Fade("vr_maze_vr", Color.black, 0.25f, VRcamera);
                        //SceneManager.LoadScene("vr_maze_vr");
                    }
                }
            }
        }

        if ((Timer > PlayerPrefs.GetFloat("debug_time") || Input.GetKeyDown(KeyCode.LeftControl)) && (scene.name == "vr_training" || scene.name == "vr_adaptation"))
        {
            if (PlayerPrefs.GetFloat("lang") == 0)
            {
                continueButton_spa.SetActive(true);
                bar_spa.SetActive(false);  
            } else
            {
                continueButton.SetActive(true);
                bar.SetActive(false);   
            }
            //continueButton.SetActive(true);
            //bar.SetActive(false);
            
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.Return))
            {
                //InstructionMenu.SetActive(false);
                StartCoroutine(FadeInScene());
 
                controller.enabled = true;
            }
        }

        if ((Timer > 300 + PlayerPrefs.GetFloat("debug_time") || Input.GetKeyDown(KeyCode.Space)) && scene.name == "vr_training")
        {
            print("time up");
            IncreaseTime = false;
            controller.enabled = false;
            controller.speed = 0;
            if (PlayerPrefs.GetFloat("lang") == 0)
            {
                EndTrialMenu_spa.SetActive(true);
            } else
            {
                EndTrialMenu_eng.SetActive(true);
            }
            //EndTrialMenu.SetActive(true);
            CSVManager.AppendToReport(new string[]
                {
                        PlayerPrefs.GetString("id"),
                        PlayerPrefs.GetFloat("session").ToString(),
                        scene.name.ToString(),
                        "VR",
                        Timer.ToString(),
                        controller.distance.ToString(),
                        detector.gridCounter.ToString(),
                        detector.backtrackCounter.ToString(),
                        "training"
                });
        }
        else if (scene.name == "vr_maze_vr" && (Timer > 600 || Input.GetKeyDown(KeyCode.Space)))
        {
            if (trialCount == PlayerPrefs.GetFloat("max_trials"))
            {
                IncreaseTime = false;
                controller.enabled = false;
                controller.speed = 0;
                if (PlayerPrefs.GetFloat("lang") == 0)
                {
                    EndExperimentMenu_spa.SetActive(true);
                } else
                {
                    EndExperimentMenu_eng.SetActive(true);
                }
                //EndExperimentMenu.SetActive(true);
                paths = pathScript.image;
                byte[] bytes = paths.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/Paths/path_" + PlayerPrefs.GetString("id") + "_vr_" + PlayerPrefs.GetFloat("session") + "_" + trialCount + ".png", bytes);
                CSVManager.AppendToReport(new string[]
                {
                        PlayerPrefs.GetString("id"),
                        PlayerPrefs.GetFloat("session").ToString(),
                        scene.name.ToString(),
                        "VR",
                        Timer.ToString(),
                        controller.distance.ToString(),
                        detector.gridCounter.ToString(),
                        detector.backtrackCounter.ToString(),
                        "fail"
                });
                StartCoroutine(CountdownToMainMenu());
            }
            else
            {
                IncreaseTime = false;
                controller.enabled = false;
                controller.speed = 0;
                if (PlayerPrefs.GetFloat("lang") == 0)
                {
                    TimeOut_spa.SetActive(true);
                } else
                {
                    TimeOut_eng.SetActive(true);
                }
                //TimeOut.SetActive(true);
                paths = pathScript.image;
                byte[] bytes = paths.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/Paths/path_" + PlayerPrefs.GetString("id") + "_vr_" + PlayerPrefs.GetFloat("session") + "_" + trialCount + ".png", bytes);
                CSVManager.AppendToReport(new string[]
                {
                        PlayerPrefs.GetString("id"),
                        PlayerPrefs.GetFloat("session").ToString(),
                        scene.name.ToString(),
                        "VR",
                        Timer.ToString(),
                        controller.distance.ToString(),
                        detector.gridCounter.ToString(),
                        detector.backtrackCounter.ToString(),
                        "fail"
                });
                //SceneManager.LoadScene("vr_maze_vr");
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //IncreaseTime = false;
            //controller.enabled = false;
            //EndTrialMenu.SetActive(true);
            //Cursor.lockState = CursorLockMode.None;
            //print("collision");
            if (scene.name == "vr_maze_vr")
            {
                paths = pathScript.image;
                byte[] bytes = paths.EncodeToPNG();
                File.WriteAllBytes(Application.dataPath + "/Paths/path_" + PlayerPrefs.GetString("id") + "_vr_" + PlayerPrefs.GetFloat("session") + "_" + trialCount + ".png", bytes);
            }
            CSVManager.AppendToReport(new string[]
            {
                PlayerPrefs.GetString("id"),
                PlayerPrefs.GetFloat("session").ToString(),
                scene.name.ToString(),
                "VR",
                Timer.ToString(),
                controller.distance.ToString(),
                detector.gridCounter.ToString(),
                detector.backtrackCounter.ToString(),
                "success"
            });
            if (trialCount == PlayerPrefs.GetFloat("max_trials"))
            {
                IncreaseTime = false;
                controller.enabled = false;
                controller.speed = 0;
                if (PlayerPrefs.GetFloat("lang") == 0)
                {
                    EndExperimentMenu_spa.SetActive(true);
                } else
                {
                    EndExperimentMenu_eng.SetActive(true);
                }
                //EndExperimentMenu.SetActive(true);
                StartCoroutine(CountdownToMainMenu());
            }
            else
            {
                IncreaseTime = false;
                controller.enabled = false;
                if (PlayerPrefs.GetFloat("lang") == 0)
                {
                    EndTrialMenu_spa.SetActive(true);
                    print("success");
                } else
                {
                    EndTrialMenu_eng.SetActive(true);
                }
                //EndTrialMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    IEnumerator CountdownToMainMenu()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("main_menu");
    }

    IEnumerator FadeInScene()
    {
        fadeIn = true;
                while (fadeIn)
                {
                    if (UIgroup.alpha >= 0)
                    {
                        //fadeTime -= Time.deltaTime;
                        UIgroup.alpha = Mathf.MoveTowards(UIgroup.alpha, 0, Time.deltaTime);
                        yield return null;
                        //print(UIgroup.alpha);
                        if (UIgroup.alpha == 0)
                        {
                            fadeIn = false;
                            if (PlayerPrefs.GetFloat("lang") == 0)
                            {
                            InstructionMenu_spa.SetActive(false);
                            } else
                            {
                            InstructionMenu_eng.SetActive(false);
                            }
                            //InstructionMenu.SetActive(false);
                            UIgroup.alpha = 1;
                        }
                    }
                }
    }
}
